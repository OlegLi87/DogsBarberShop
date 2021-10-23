using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DogsbarberShop.Controllers.Filters;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos;
using DogsBarberShop.Entities.Dtos.PetDtos;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Authorize]
    [PetsOrdersExceptionFilter]
    public sealed class PetsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public PetsController(IUnitOfWork unitOfWork, IUtilsService utilsService, IMapper mapper,
                               IOptions<AppSettings> opts)
        {
            _unitOfWork = unitOfWork;
            _utilsService = utilsService;
            _mapper = mapper;
            _appSettings = opts.Value;
        }

        [HttpGet]
        [ProvideUserIdActionFilter]
        public async Task<AppResponse> GetAllUserPets()
        {
            var userId = HttpContext.Items["userId"] as string;
            var usersPets = await _unitOfWork.Pets.Get(p => p.UserId == userId);

            return new AppResponse
            {
                StatusCode = 200,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = usersPets.Select(_mapper.Map<PetOutputDto>)
                }
            };
        }

        [HttpPost]
        [ProvideUserIdActionFilter]
        public async Task<AppResponse> AddPet(PetInputDto petInputDto)
        {
            var newPet = _mapper.Map<Pet>(petInputDto);

            var userId = HttpContext.Items["userId"] as string;
            newPet.UserId = userId;

            await _unitOfWork.Pets.Add(newPet);

            return new AppResponse
            {
                StatusCode = 201,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = _mapper.Map<PetOutputDto>(newPet)
                }
            };
        }

        [HttpDelete]
        [Route("{petId}")]
        [ProvideUserIdActionFilter]
        [TypeFilter(typeof(ProvideEntityActionFilter), Arguments = new[] { typeof(Pet) })]
        public async Task<AppResponse> DeletePet()
        {
            var petToDelete = HttpContext.Items["pet"] as Pet;
            await _unitOfWork.Pets.Delete(petToDelete);

            return new AppResponse
            {
                StatusCode = 204,
                Payload = new AppResponse.ResponsePayload()
            };
        }

        [HttpPatch]
        [Route("{petId}")]
        [TypeFilter(typeof(ProvideEntityActionFilter), Arguments = new[] { typeof(Pet) })]
        public async Task<AppResponse> UpdatePet(PetUpdateDto petUpdateDto)
        {
            var petToUpdate = HttpContext.Items["pet"] as Pet;
            await _unitOfWork.Pets.PatchUpdate(petToUpdate, _utilsService.MapPropertiesToDictionary(petUpdateDto));

            return new AppResponse
            {
                StatusCode = 200,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = _mapper.Map<PetOutputDto>(petToUpdate)
                }
            };
        }

        [HttpPost]
        [Route("uploadImage")]
        [ServiceFilter(typeof(UplaodImageSizeLimitResourceFilter))]
        public async Task<AppResponse> UploadImage([FromForm] UploadImageDto imageDto)
        {
            var image = imageDto.Image;

            var fileName = image.FileName;
            var relativePath = Path.Combine(_appSettings.UploadImage.ImagesPath, "Pets");
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            await _utilsService.SaveFileAsync(Path.Combine(absolutePath, fileName), image);

            return new AppResponse
            {
                StatusCode = 201,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = Path.Combine(relativePath, fileName)
                }
            };
        }
    }
}