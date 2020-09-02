using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Business.Reporting
{
    public interface IReportingService
    {
        void Generate<TDataReport>(int templateId, TDataReport dataReport) where TDataReport : class;
        
        Task GenerateAsync<TDataReport>(int templateId, TDataReport dataReport, CancellationToken cancellationToken, bool captureContext = false) where TDataReport : class;
    }
}