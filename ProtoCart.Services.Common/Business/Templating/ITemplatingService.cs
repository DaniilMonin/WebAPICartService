using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Business.Templating
{
    public interface ITemplatingService
    {
        string Render<TReportData>(int templateId, TReportData data) where TReportData : class;

        Task<string> RenderAsync<TReportData>(int templateId, TReportData data, CancellationToken cancellationToken, bool captureContext = false) where TReportData : class;
    }
}