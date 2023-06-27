namespace dotnet_3D_store_backend.Services;

using Microsoft.Extensions.Options;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Helpers;
using dotnet_3D_store_backend.Entities;

public class ProductService
{
    private readonly DatabaseContext _dbContext;
    public ProductService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Products> GetSuggestedProducts(int userId)
    {
        // Get all previous orders
        var previousOrders = _dbContext.Orders.Where(o => o.CostumerUserId == userId);
        if (previousOrders != null)
        {
            // Get all previous order items
            var previousOrderItems = _dbContext.OrderItems.Where(oi => previousOrders.Any(o => o.Id == oi.OrderId));
            // Get all previous products
            var previousProducts = _dbContext.Products.Where(p => previousOrderItems.Any(oi => oi.ProductId == p.Id));
            // Get all previous categories
            var previousCategories = previousProducts.Select(p => p.Category).Distinct();
            // Calculate the most popular category
            var mostPopularCategory = previousCategories.GroupBy(c => c).OrderByDescending(c => c.Count()).First().Key;
            // Get all products from the most popular category
            var suggestedProducts = _dbContext.Products.Where(p => p.Category == mostPopularCategory);
            return suggestedProducts;
        }
        else
        {
            // If there are no previous orders, return all products
            return _dbContext.Products.ToList();
        }
    }
}