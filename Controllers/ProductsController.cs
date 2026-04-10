using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MormorDagnysBageri.Data;
using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.Entities;

namespace MyApp.Namespace;

[Route("api/products")]
[ApiController]
public class ProductsController(MormorDagnysContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await context.Products
            .Select(p => new
            {
                p.Id,
                p.ItemNumber,
                p.ProductName
            })
            .ToListAsync();

            return Ok(new {Success = true, StatusCode = 200, Items = products.Count, Data = products});
    }



    [HttpGet("{id}")]
    public async Task<ActionResult> FindProductWithSupplier(int id)
    {
        var product = await context.Products
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.ItemNumber,
                p.ProductName,
                Suppliers = p.SupplierProducts.Select(sp => new
                {
                    sp.Supplier.SupplierId,
                    sp.Supplier.SupplierName,
                    sp.Supplier.Email,
                    sp.PricePerKg
                }).ToList()
            })
            .SingleOrDefaultAsync();

        if (product is null) return NotFound("Produkten hittades inte");
        
        return Ok(new {Success = true, StatusCode = 200, Items = 1, Data = product});

        
    }
}
    
    

