using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models;
using DogsBarberShop_Api.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogsBarberShop_Api.Controllers
{
    [Authorize]
    [Route("api/{controller}")]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<AppHttpResponse> GetOrders(int currentPage, int ordersOnPage)
        {
            var orders = await _unitOfWork.Orders.Get();

            return new AppHttpResponse
            {
                StatusCode = 200,
                Payload = new AppHttpResponse.ResponsePayload
                {
                    ResponseObject = orders
                }
            };
        }
    }
}