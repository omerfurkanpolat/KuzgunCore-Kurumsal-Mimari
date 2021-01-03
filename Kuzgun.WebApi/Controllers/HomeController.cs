using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public HomeController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getactivecategories")]
        public IActionResult GetActiveCategories()
        {
            var result = _categoryService.GetActiveCategories();
            if (result.Success)
            {
                var categories = _mapper.Map<List<CategoryForReturnDTO>>(result.Data);
                return Ok(categories);
            }

            return BadRequest(result.Message);


        }
    }
}
