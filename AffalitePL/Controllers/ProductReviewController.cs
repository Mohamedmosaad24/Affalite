using AffaliteBLL.DTOs;
using AffaliteBLL.Services.Interfaces;
using AffaliteDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AffalitePL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _service;

        public ProductReviewController(IProductReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reviews = _service.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _service.GetById(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetByProductId(int productId)
        {
            var reviews = _service.GetByProductId(productId);
            return Ok(reviews);
        }

        [HttpGet("affiliate/{affiliateId}")]
        public IActionResult GetByAffiliateId(int affiliateId)
        {
            var reviews = _service.GetByAffiliateId(affiliateId);
            return Ok(reviews);
        }

        [HttpPost]
        public IActionResult Create(CreateProductReviewDto dto)
        {
            var createdReview = _service.Create(dto);
            return Ok(createdReview);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductReviews review)
        {
            var result = _service.Update(id, review);
            if (!result)
                return NotFound();

            return Ok(new { message = "Review updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Review deleted successfully" });
        }

        [HttpGet("average/{productId}")]
        public IActionResult GetAverageRating(int productId)
        {
            var avg = _service.GetAverageRating(productId);
            return Ok(avg);
        }
    }
}