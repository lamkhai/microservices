using CoreAPI.Data.DBContext;
using CoreAPI.Data.Entities;
using CoreAPI.Data.Models.Requests;
using CoreAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CoreAPIDBContext _DBContext;
    private readonly IDistributedCache _distributedCache;

    public ProductsController(CoreAPIDBContext DBContext, IDistributedCache distributedCache)
    {
        _DBContext = DBContext;
        _distributedCache = distributedCache;
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

        await _distributedCache.RemoveAsync(GetProductIdCacheKey(id));

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
        return await _distributedCache.GetAsync(GetProductIdCacheKey(id),
            async token =>
            {
                var product = await _DBContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, token);

                return product;
            },
            CacheOptions.DefaultExpiration);
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

        await _distributedCache.RemoveAsync(GetProductIdCacheKey(id));

        return await _DBContext.SaveChangesAsync();
    }

    private string GetProductIdCacheKey(int id)
    {
        return $"products-{id}";
    }
}