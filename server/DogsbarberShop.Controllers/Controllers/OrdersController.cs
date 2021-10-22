using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DogsbarberShop.Controllers.Filters;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Services.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<AppResponse> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders.Get();

            return new AppResponse
            {
                StatusCode = 200,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = orders.Select(_mapper.Map<OrderOutputDto>)
                }
            };
        }

        [HttpPost]
        [TypeFilter(typeof(NewOrderValidatorActionFilter), Arguments = new[] { "orderInputDto" })]
        public async Task<AppResponse> AddOrder(OrderInputDto orderInputDto)
        {
            var newOrder = _mapper.Map<Order>(orderInputDto);
            await _unitOfWork.Orders.Add(newOrder);

            return new AppResponse
            {
                StatusCode = 201,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = newOrder
                }
            };
        }
    }
}