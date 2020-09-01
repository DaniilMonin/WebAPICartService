using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;
using Quartz;
using RestSharp;

namespace ProtoCart.Services.Scheduler.Business.Jobs.CleaningOldCarts
{
    internal sealed class CleaningOldCartsQuartzSchedulerJob : QuartzSchedulerJob
    {
        private readonly IReadOnlyEntitiesRepository<Hook> _hooReadOnlyEntitiesRepository;
        private readonly ICartLinksEntitiesRepository _cartLinksEntitiesRepository;
        private readonly IFactory<IOperation<CleanOldCartsRequest>> _factory;

        public CleaningOldCartsQuartzSchedulerJob(
            ILogService logService, 
            ISettingsService settingsService, 
            IReadOnlyEntitiesRepository<Hook> hooReadOnlyEntitiesRepository, 
            ICartLinksEntitiesRepository cartLinksEntitiesRepository,
            IFactory<IOperation<CleanOldCartsRequest>> factory) 
            : base(logService, settingsService)
        {
            _hooReadOnlyEntitiesRepository = hooReadOnlyEntitiesRepository;
            _cartLinksEntitiesRepository = cartLinksEntitiesRepository;
            _factory = factory;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            LogService.Info?.Write("Cleaning job started...");
            
            IOperation<CleanOldCartsRequest> foo = _factory.Create();

            await foo.ExecuteAsync(new CleanOldCartsRequest() {  }, CancellationToken.None);
            
            foreach (CartLink link in await _cartLinksEntitiesRepository.ReadAsync(CancellationToken.None))
            {
                
            }
            
            IEnumerable<Hook> hooks = await _hooReadOnlyEntitiesRepository.ReadAsync(CancellationToken.None);
            
            IList<Task> hooksRetries = new List<Task>();

            foreach (Hook hook in hooks)
            {
                AsyncRetryPolicy retryPolicy = 
                    Policy.Handle<HttpRequestException>().RetryAsync(SettingsService.HooksRetryCount,
                        (exception, i) =>
                        {
                            LogService.Error?.Write($"Hook error, retry #{i} - message {exception.Message}");
                        });

                hooksRetries.Add(retryPolicy.ExecuteAsync(async () =>
                {
                    await new RestClient(hook.ServiceUri).ExecuteGetAsync(new RestRequest {RequestFormat = DataFormat.Json});
                }));
            }

            await Task.WhenAll(hooksRetries);
        }
    }
}