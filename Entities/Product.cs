namespace MormorDagnysBageri.Entities;

public record Product
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }

    // Navigeringsegenskap låter oss hämta
    // vilken leverantör äger produkten
    public List<SupplierProduct> SupplierProducts { get; set; }
}
