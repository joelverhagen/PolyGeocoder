using System.Threading.Tasks;

namespace PolyGeocoder.Support
{
    public interface IStructuredGeocoder
    {
        Task<Response> GeocodeAsync(StructuredRequest request);
    }
}