using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DogsbarberShop.Controllers.Filters;
using DogsbarberShop.Controllers.Filters.NewOrderValidatorFilters;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
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
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IUtilsService utilsService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _utilsService = utilsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProvideUserIdActionFilter]
        public async Task<AppResponse> GetAllOrders([FromQuery] string target)
        {
            IEnumerable<Order> orders;

            if (target == "user")
                orders = await _unitOfWork.Orders.Get(o => o.UserId == HttpContext.Items["userId"] as string);
            else
                orders = await _unitOfWork.Orders.Get();

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
        [TypeFilter(typeof(ProvideEntityActionFilter), Arguments = new[] { typeof(Pet) })]
        [ServiceFilter(typeof(NoRegisteredOrderForAPetActionFilter))]
        [ServiceFilter(typeof(ArrivalDateTimeOnWorkingTimeActionFilter))]
        [ServiceFilter(typeof(RoundArrivalTimeActionFilter))]
        [ServiceFilter(typeof(NoRegisteredOrderOnArrivalDateTime))]
        [ProvideUserIdActionFilter]
        public async Task<AppResponse> AddOrder(OrderInputDto orderInputDto)
        {
            var newOrder = _mapper.Map<Order>(orderInputDto);
            newOrder.UserId = HttpContext.Items["userId"] as string;
            await _unitOfWork.Orders.Add(newOrder);

            var newOrderInDb = await _unitOfWork.Orders.GetById(newOrder.Id);

            return new AppResponse
            {
                StatusCode = 201,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = _mapper.Map<OrderOutputDto>(newOrderInDb)
                }
            };
        }

        [HttpDelete]
        [Route("{orderId}")]
        [TypeFilter(typeof(ProvideEntityActionFilter), Arguments = new[] { typeof(Order) })]
        public async Task<AppResponse> DeleteOrder()
        {
            var orderToDelete = HttpContext.Items["order"] as Order;

            await _unitOfWork.Orders.Delete(orderToDelete);

            return new AppResponse
            {
                StatusCode = 204
            };
        }

        [HttpPatch]
        [Route("{orderId}")]
        [TypeFilter(typeof(ProvideEntityActionFilter), Arguments = new[] { typeof(Order) })]
        [ServiceFilter(typeof(ArrivalDateTimeOnWorkingTimeActionFilter))]
        [ServiceFilter(typeof(RoundArrivalTimeActionFilter))]
        [ServiceFilter(typeof(NoRegisteredOrderOnArrivalDateTime))]
        public async Task<AppResponse> UpdateOrder(OrderInputDto orderInputDto)
        {
            var order = HttpContext.Items["order"] as Order;
            await _unitOfWork.Orders.PatchUpdate(order, _utilsService.MapPropertiesToDictionary(orderInputDto));

            order.ArrivalDate = orderInputDto.ArrivalDate;

            return new AppResponse
            {
                StatusCode = 200,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = _mapper.Map<OrderOutputDto>(order)
                }
            };
        }
    }
}