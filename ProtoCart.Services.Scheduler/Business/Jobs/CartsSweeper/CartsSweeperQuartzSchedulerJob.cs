using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;
using Quartz;
using RestSharp;

namespace ProtoCart.Services.Scheduler.Business.Jobs.CartsSweeper
{
    internal sealed class CartsSweeperQuartzSchedulerJob : QuartzSchedulerJob
    {
        private readonly IReadOnlyEntitiesRepository<Hook> _hooReadOnlyEntitiesRepository;
        private readonly IFactory<IOperation<CartsSweeperRequest>> _factory;

        public CartsSweeperQuartzSchedulerJob(
            ILogService logService, 
            ISettingsService settingsService, 
            IReadOnlyEntitiesRepository<Hook> hooReadOnlyEntitiesRepository,
            IFactory<IOperation<CartsSweeperRequest>> factory) 
            : base(logService, settingsService)
        {
            _hooReadOnlyEntitiesRepository = hooReadOnlyEntitiesRepository;
            _factory = factory;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            LogService.Info?.Write("Sweeper job started...");
            
            CartsSweeperRequest request = new CartsSweeperRequest()
            {
                //TODO filter should be here
                DaysToRemove = SettingsService.SweeperDays
            };

            await _factory.Create().ExecuteAsync(request, CancellationToken.None);

            if (request.Result is null || request.Result.Count == 0)
            {
                return;
            }

            IReadOnlyCollection<Hook> webHooks = new List<Hook>(await _hooReadOnlyEntitiesRepository.ReadAsync(CancellationToken.None));
            
            IList<Task> webHooksReties = new List<Task>();
            
            //TODO convert to yield from operation
            foreach (Cart cart in request.Result)
            {
                foreach (Hook webHook in webHooks)
                {
                    AsyncRetryPolicy retryPolicy = 
                        Policy.Handle<HttpRequestException>().RetryAsync(SettingsService.HooksRetryCount,
                            (exception, i) =>
                            {
                                LogService.Error?.Write($"Hook error, retry #{i} - message {exception.Message}");
                            });

                    webHooksReties.Add(retryPolicy.ExecuteAsync(async () =>
                    {
                        IRestResponse response = await new RestClient(webHook.ServiceUri)
                        {
                            ThrowOnAnyError = true,
                            
                            //Proxy =
                            //CachePolicy =
                            
                        }.ExecutePostAsync(new RestRequest
                        {
                            RequestFormat = DataFormat.Json,
                            Parameters = { new Parameter("cartId", cart.Id, ParameterType.UrlSegment)}
                        });

                        if (response.IsSuccessful)
                        {
                            return;
                        }
                        
                        throw new HttpRequestException(response.ErrorMessage);
                        
                    }));
                }
            }

            await Task.WhenAll(webHooksReties);
        }
    }
}