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
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
            .Where(c => c.Id == id)
            .Select(p => new
            {
                p.ItemNumber,
                p.Description,
                p.PricePerKg,
                p.ProductName,
                p.ImageUrl
            })
            .SingleOrDefaultAsync();
        if (product is not null)
        {
            return Ok(new { Success = true, StatusCode = 1337, items = 1, Data = product});
        }

        return NotFound();
    }
}
    
    

