using System.Threading.Tasks;
using Application.Hotel;

namespace Application.Interfaces
{
    public interface IAmadeus
    {
        Task<Response> Search(Search.Query query);
    }
}