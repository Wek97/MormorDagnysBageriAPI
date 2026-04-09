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

    [HttpGet("{supplierId}")]
    public async Task<ActionResult> GetSupplierWithProducts(int supplierId)
    {
        var supplier = await context.Suppliers
            .Where(s => s.SupplierId == supplierId)
            .Select(s => new
            {
                s.SupplierId,
                s.SupplierName,
                s.Address,
                s.ContactPerson,
                s.City,
                s.Phone,
                s.Email,
                Products = s.SupplierProducts.Select(sp => new
                {
                    sp.ProductId,
                    sp.Product.ProductName,
                    sp.PricePerKg
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (supplier is null) return NotFound ("Leverantör hittas ej");

        return Ok(supplier);
    }

    
}
    
        
    

