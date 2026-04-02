using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AffaliteBL.DTOs.CategoryDTOs;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AutoMapper;

namespace AffalitePL.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            var result = _mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound($"Category with id: {id} not found");

            var result = _mapper.Map<GetCategoryDTO>(category);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);
            _categoryService.CreateCategory(category);
            return Ok("Create category");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = _mapper.Map<Category>(updateCategoryDTO);
            category.Id = id;

            _categoryService.UpdateCategory(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound($"Category with id: {id} not found");

            _categoryService.DeleteCategory(id);
            return Ok($"Delete category with id: {id}");
        }
    }
}