using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Authorize]
    public sealed class OrdersController : ControllerBase
    {
        public IActionResult GetOrders()
        {
            return Ok();
        }
    }
}