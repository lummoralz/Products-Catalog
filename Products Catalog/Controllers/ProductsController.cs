using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_Catalog.Context;
using Products_Catalog.Models;
using System.Security.Claims;

namespace Products_Catalog.Controllers;

[ApiController, Route("api/[controller]"), Authorize]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, ILogger<ProductsController> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _db.Products.ToListAsync();
        _logger.LogInformation("There are {ProductCount} products", products.Count);
        return products;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductRequest input)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogDebug("Invalid product {Product}", input);
            return BadRequest(ModelState);
        }

        var userId = _httpContextAccessor.HttpContext!.User.FindAll(ClaimTypes.NameIdentifier).Last();
        var product = new Product
        {
            Name = input.Name,
            Description = input.Description,
            UnitPrice = input.UnitPrice,
            Amount = input.Amount,
            UserId = userId!.Value,
        };
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        _logger.LogInformation("New product {ProductName} created!", input.Name);
        return Ok();
    }
}
