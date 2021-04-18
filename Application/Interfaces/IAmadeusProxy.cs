using System.Threading.Tasks;
using Application.Hotel;

namespace Application.Interfaces
{
    public interface IAmadeusProxy
    {
        Task<Response> Search(Search.Query query);
    }
}