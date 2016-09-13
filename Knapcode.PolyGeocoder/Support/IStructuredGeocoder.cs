using System.Threading.Tasks;

namespace Knapcode.PolyGeocoder.Support
{
    public interface IStructuredGeocoder
    {
        Task<Response> GeocodeAsync(StructuredRequest request);
    }
}