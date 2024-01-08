using AutoMapper;
using DtoAPI.Data;
using DtoAPI.Models;
using DtoAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DtoAPI.Controllers
{
    [ApiController]
    [Route("/api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProductController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public IActionResult GetProducts()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateProduct(ProductDTO product_dto)
        {
            var product = _mapper.Map<Product>(product_dto);
            _db.Products.Add(product);
            _db.SaveChanges();

            return Ok(product);
        }
    }
}
