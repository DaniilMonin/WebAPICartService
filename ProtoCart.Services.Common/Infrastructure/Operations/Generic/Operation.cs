using System;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Infrastructure.Operations.Generic
{
    public abstract class Operation<TArgument> : IOperation<TArgument>
        where TArgument : class
    {
        private readonly ILogService _logService;
        private readonly ISettingsService _settingsService;
        
        private OperationStatus _status = OperationStatus.Validation;

        protected Operation(ILogService logService, ISettingsService settingsService)
        {
            _logService = logService;
            _settingsService = settingsService;
        }

        public event OperationStatusChanged OnOperationStatusChanged;

        public OperationStatus Status
        {
            get => _status;
            private set
            {
                if (_status == value)
                {
                    return;
                }
                
                _status = value;

                OnOperationStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual async Task ExecuteAsync(TArgument argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            Status = OperationStatus.Planning;
            
            try
            {
                Status = OperationStatus.Process;
                
                await DoProcessAsync(argument, cancellationToken, captureContext).ConfigureAwait(captureContext);
            }
            catch (Exception exception)
            {
                LogService.Error?.Write(exception.Message);
            }

            Status = OperationStatus.Finished;
        }
        
        protected ILogService LogService => _logService;

        protected ISettingsService SettingsService => _settingsService;

        protected void ForceStatusToFinish()
            => Status = OperationStatus.Finished;
        
        protected abstract Task DoProcessAsync(TArgument argument, CancellationToken cancellationToken, bool captureContext = false);
    }
}