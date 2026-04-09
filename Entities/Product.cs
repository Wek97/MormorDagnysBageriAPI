

namespace MormorDagnysBageri.Entities;

public record Product
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }
    public decimal PricePerKg { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    // Navigeringsegenskap låter oss hämta
    // vilken leverantör äger produkten
    public Supplier Supplier { get; set; }
}
