using System.Threading.Tasks;

namespace PolyGeocoder.Support
{
    public interface ISimpleGeocoder
    {
        Task<Response> GeocodeAsync(string request);
    }
}