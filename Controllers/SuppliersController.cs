using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MormorDagnysBageri;
using MormorDagnysBageri.Data;
using MormorDagnysBageri.DTOs.SupplierProduct;
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

        if (supplier is null) return NotFound ("Hittar inte leverantören!");

        return Ok(supplier);
    }

    [HttpPost("{supplierId}/products")]
    public async Task<ActionResult> AddProductToSupplier(int supplierId, AddProductToSupplierDto dto)
    {
        var supplier = await context.Suppliers.FindAsync(supplierId);
        if (supplier is null) return NotFound ("Hittar inte leverantören!");

        var product = await context.Products.FindAsync(dto.ProductId);
        if (product is null) return NotFound("Hittar inte produkten!");

        var supplierProductExists = await context.SupplierProducts
            .SingleOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == dto.ProductId);

        if (supplierProductExists is not null) return
            BadRequest("Produkten finns redan hos denna leverantören!");

        var supplierProduct = new SupplierProduct
        {
            SupplierId = supplierId,
            ProductId = dto.ProductId,
            PricePerKg = dto.PricePerKg
        };

        context.SupplierProducts.Add(supplierProduct);
        await context.SaveChangesAsync();

        return Ok("Produkten är nu kopplad till vald leverantör!");
    }

    [HttpPatch("{supplierId}/products/{productId}")]
    public async Task<ActionResult> UpdateSupplierProductPrice(int supplierId, int productId, UpdateSupplierProductPriceDto dto)
    {
        var supplierProduct = await context.SupplierProducts
            .SingleOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

        if (supplierProduct is null) return NotFound("Leverantör har ej denna produkt!");

        supplierProduct.PricePerKg = dto.PricePerKg;

        await context.SaveChangesAsync();
        return Ok("Pris är nu uppdaterat");
    }

    
}
    
        
    

