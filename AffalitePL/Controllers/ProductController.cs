using AffaliteBL.DTOs;
using AffaliteBL.Helpers;
using AffaliteBL.IServices;
using AffaliteBLL.DTOs.Products;
using AffaliteDAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AffaliteAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET /api/products
        [HttpGet]
        public IActionResult GetAll([FromQuery] ProductQueryParams query)
        {
            var products = _service.GetAll(query);

            // AutoMapper mapping
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(result);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        // POST /api/products
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _service.Create(product);
            return Ok();
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _service.Update(id, product);
            return Ok();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        // GET /api/products/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId, [FromQuery] ProductQueryParams query)
        {
            // ننسخ query عشان نضيف CategoryId
            query.CategoryId = categoryId;

            var products = _service.GetAll(query);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(result);
        }

        // GET /api/products/merchant/{merchantId}
        [HttpGet("merchant/{merchantId}")]
        public IActionResult GetByMerchant(int merchantId, [FromQuery] ProductQueryParams query)
        {
            // ننسخ query عشان نضيف MerchantId
            query.MerchantId = merchantId;

            var products = _service.GetAll(query);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(result);
        }

    }
}