using System.Threading.Tasks;

namespace Knapcode.PolyGeocoder.Support
{
    public interface ISimpleGeocoder
    {
        Task<Response> GeocodeAsync(string request);
    }
}