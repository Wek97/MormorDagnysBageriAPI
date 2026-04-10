namespace MormorDagnysBageri.Entities;

public record Supplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string Address { get; set; }
    public string ContactPerson { get; set; }   
    public string City { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    
    // Navigeringsegenskap
    public List<SupplierProduct> SupplierProducts { get; set; }
}
