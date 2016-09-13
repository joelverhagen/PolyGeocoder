using System.Threading.Tasks;

namespace Knapcode.PolyGeocoder
{
    public interface IStructuredGeocoder
    {
        Task<Response> GeocodeAsync(StructuredRequest request);
    }
}