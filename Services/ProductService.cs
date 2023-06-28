namespace dotnet_3D_store_backend.Services;

using Microsoft.Extensions.Options;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Helpers;
using dotnet_3D_store_backend.Entities;
using dotnet_3D_store_backend.Enumerators;

public class ProductService
{
    private readonly DatabaseContext _dbContext;
    private AppSettings _appSettings;
    public ProductService(DatabaseContext dbContext, AppSettings appSettings)
    {
        _dbContext = dbContext;
        _appSettings = appSettings;
    }
    public bool AddProduct(Products product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return true;
    }
    public bool DeleteProduct(int id)
    {
        var product = _dbContext.Products.Find(id);
        if (product == null)
        {
            return false;
        }
        product.ProductStatus = ProductStatus.Deleted;
        _dbContext.SaveChanges();
        return true;
    }
    public Products? GetProductById(int id)
    {
        return _dbContext.Products.Find(id);
    }
    public IEnumerable<Products> GetSuggestedProducts(SuggestedProductsRequest model)
    {
        int calculateIndex = model.CurrentPage * _appSettings.PageSize;
        // Get all user orders
        var userOrders = _dbContext.Orders.Where(o => o.CostumerUserId == model.UserId);
        // Get all user order items
        var userOrderItems = _dbContext.OrderItems.Where(oi => userOrders.Any(o => o.Id == oi.OrderId));
        // Get all user products
        var userProducts = _dbContext.Products.Where(p => userOrderItems.Any(oi => oi.ProductId == p.Id));
        // Get all user categories
        var userCategories = userProducts.Select(p => p.Category).Distinct();
        // Calculate the most popular category
        var mostPopularCategory = userCategories.GroupBy(c => c).OrderByDescending(c => c.Count()).First().Key;
        // Get all products from the most popular category
        var suggestedProducts = _dbContext.Products
            .Where(p => p.Category == mostPopularCategory)
            .Skip(calculateIndex)
            .Take(_appSettings.PageSize);
        return suggestedProducts.Any() ? suggestedProducts : _dbContext.Products.Skip(calculateIndex).Take(_appSettings.PageSize);
    }

    public IEnumerable<Products> GetProductsByCategory(string category, int currentPage)
    {
        int calculateIndex = currentPage * _appSettings.PageSize;
        return _dbContext.Products.Where(p => p.Category == category).Skip(calculateIndex).Take(_appSettings.PageSize);
    }
}