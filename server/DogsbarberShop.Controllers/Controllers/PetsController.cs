using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DogsbarberShop.Controllers.Filters;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.Dtos;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public sealed class PetsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilsService _utilsServie;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public PetsController(IUnitOfWork unitOfWork, IUtilsService utilsService, IMapper mapper,
                               IOptions<AppSettings> opts)
        {
            _unitOfWork = unitOfWork;
            _utilsServie = utilsService;
            _mapper = mapper;
            _appSettings = opts.Value;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPet([FromRoute] Guid id)
        {
            var pet = await _unitOfWork.Pets.GetById(id);
            if (pet is not null)
                return new ObjectResult(new AppResponse
                {
                    StatusCode = 200,
                    Payload = new AppResponse.ResponsePayload
                    {
                        ResponseObject = pet
                    }
                });

            return NotFound();
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

            await _utilsServie.SaveFileAsync(Path.Combine(absolutePath, fileName), image);

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