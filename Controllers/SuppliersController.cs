using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.Data;
using MormorDagnysBageri.Entities;

namespace MyApp.Namespace;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(MormorDagnysContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        List<Supplier> suppliers = await context.Suppliers.ToListAsync();
        return Ok(new {Success = true, StatusCode = 200, Items = suppliers.Count, Data = suppliers});
    }

    [HttpGet("search")]
    public async Task<ActionResult> SearchSupplier(string name)
    {
        Supplier supplier = await context.Suppliers
            .Include(s => s.Products)
            .SingleOrDefaultAsync(s => s.SupplierName == name);
        if (supplier is null) return NotFound();

        var supplierToReturn = new
        {
            supplier.SupplierId,
            supplier.SupplierName,
            Products = supplier.Products.Select(p => new
            {
                p.ItemNumber,
                p.ProductName,
                p.PricePerKg,
                p.Description
            })
        };
        return Ok(new {Success = true, StatusCode = 200, Items = 1, Data = supplierToReturn});
        
    }

    [HttpPost()]
    public async Task<ActionResult> AddSupplier(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(SearchSupplier), new { id = supplier.SupplierId }, supplier);
    }
}
    
        
    

