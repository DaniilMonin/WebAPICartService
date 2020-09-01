using System;

namespace ProtoCart.Services.Common.Infrastructure.Operations
{
    public delegate void OperationStatusChanged(object sender, EventArgs eventArgs);
    
    public interface IOperation
    {
        event OperationStatusChanged OnOperationStatusChanged;

        OperationStatus Status { get; }
    }
}