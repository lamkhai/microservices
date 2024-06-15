using CoreAPI.Contracts;
using CoreAPI.Database;
using CoreAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CoreAPIDBContext _DBContext;

    public ProductsController(CoreAPIDBContext DBContext)
    {
        _DBContext = DBContext;
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(int id)
    {
        var product = await _DBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            return 0;
        }

        _DBContext.Remove(product);

        return await _DBContext.SaveChangesAsync();
    }

    [HttpGet]
    public async Task<List<Product>> Get(int page = 1, int pageSize = 10)
    {
        return await _DBContext.Products.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Product?> Get(int id)
    {
        return await _DBContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    [HttpPost]
    public async Task<Product> Insert([FromBody] CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        _DBContext.Add(product);

        await _DBContext.SaveChangesAsync();

        return product;
    }

    [HttpPut("{id}")]
    public async Task<int> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var product = await _DBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            return 0;
        }

        product.Name = request.Name;
        product.Price = request.Price;

        return await _DBContext.SaveChangesAsync();
    }
}