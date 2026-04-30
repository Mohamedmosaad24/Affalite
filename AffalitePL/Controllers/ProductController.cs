using AffaliteBL.DTOs;
using AffaliteBL.Helpers;
using AffaliteBL.IServices;
using AffaliteBL.Services;
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
        private readonly IMerchantService merchantService;

        public ProductsController(IProductService service, IMapper mapper,IMerchantService merchantService)
        {
            _service = service;
            _mapper = mapper;
            this.merchantService = merchantService;
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
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            // تحويل الخصائص البسيطة فقط
            var product = _mapper.Map<Product>(dto);

            // رفع الملفات يدويًا
            if (dto.Images != null && dto.Images.Any())
            {
                foreach (var file in dto.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine("wwwroot/images/products/", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    product.Images.Add(new ProductImage
                    {
                        FileName = fileName,
                        ImageUrl = fileName
                    });
                }
            }


            _service.Create(product);

            return Ok(product);
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateProductDto dto)
        {
            //var product = _mapper.Map<Product>(dto);
            _service.Update(id, dto);
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
            //var merchantId = User.FindFirst("uid")?.Value;
           //var merchant= merchantService.GetMerchantByUserId(merchantId);
            query.MerchantId = merchantId;

            var products = _service.GetAll(query);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(result);
        }
        [HttpGet("merchant")]
        public IActionResult GetByMerchant([FromQuery] ProductQueryParams query)
        {
            // ننسخ query عشان نضيف MerchantId
            var merchantId = User.FindFirst("uid")?.Value;
            var merchant = merchantService.GetMerchantByUserId(merchantId);
            query.MerchantId = merchant.Id;

            var products = _service.GetAll(query);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(result);
        }

    }
}