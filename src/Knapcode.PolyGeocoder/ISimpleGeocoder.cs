using System.Threading.Tasks;

namespace Knapcode.PolyGeocoder
{
    public interface ISimpleGeocoder
    {
        Task<Response> GeocodeAsync(string request);
    }
}