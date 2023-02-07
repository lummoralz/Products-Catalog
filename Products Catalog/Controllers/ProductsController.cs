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

    public ProductsController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _db.Products.ToListAsync();
        return products;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductRequest input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
        return Ok();
    }
}
