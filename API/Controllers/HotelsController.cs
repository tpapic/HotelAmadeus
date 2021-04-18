using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Hotel;

namespace API.Controllers
{
    public class HotelsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetHotelSearch([FromBody] Search.Query query)
        {
            return HandleResult(await Mediator.Send(query));
        }
    }
}