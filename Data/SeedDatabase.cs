    using System.Text.Json;
    using Microsoft.CodeAnalysis.Options;
    using Microsoft.EntityFrameworkCore;
    using MormorDagnysBageri.Entities;

    namespace MormorDagnysBageri.Data;

    public class SeedDatabase
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public async Task InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MormorDagnysContext>();

            await context.Database.MigrateAsync();

            await SeedSuppliers(context);
            await SeedProducts(context);
            await SeedSupplierProducts(context);
        }

        private async Task SeedSuppliers(MormorDagnysContext context)
        {
            if (context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/Json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

            if(suppliers is not null && suppliers.Count > 0)
            {
                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedProducts(MormorDagnysContext context)
        {
            if (context.Products.Any()) return;

            var json = File.ReadAllText("Data/Json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options);

            if (products is not null && products.Count > 0)
            {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedSupplierProducts(MormorDagnysContext context)
        {
            if (context.SupplierProducts.Any()) return;

            var json = File.ReadAllText("Data/Json/supplierProducts.json");
            var supplierProducts = JsonSerializer.Deserialize<List<SupplierProduct>>(json, options);

            if (supplierProducts is not null && supplierProducts.Count > 0)
            {
                await context.SupplierProducts.AddRangeAsync(supplierProducts);
                await context.SaveChangesAsync();
            }
        }
    }
