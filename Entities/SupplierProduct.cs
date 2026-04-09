using MormorDagnysBageri.Entities;

namespace MormorDagnysBageri;

public class SupplierProduct
{
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerKg { get; set; }
    public Supplier Supplier { get; set; }  
    public Product Product { get; set; }
}
