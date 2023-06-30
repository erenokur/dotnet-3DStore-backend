namespace dotnet_3D_store_backend.Services;

using Microsoft.Extensions.Options;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.Entities;
using dotnet_3D_store_backend.Enumerators;
using dotnet_3D_store_backend.DTOs;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly DatabaseContext _dbContext;
    private AppSettings _appSettings;
    public ProductService(DatabaseContext dbContext, AppSettings appSettings)
    {
        _dbContext = dbContext;
        _appSettings = appSettings;
    }
    public bool AddProduct(AddProductRequest request)
    {
        var imagePath = Path.Combine(_appSettings.ImageServerLocalPath, request.Image.FileName);
        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            request.Image.CopyTo(stream);
        }
        var product = new Products
        {
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            Image = imagePath,
            Currency = request.Currency,
            Price = request.Price,
            Quantity = request.Quantity,
            Created = DateTime.UtcNow,
            SellerUserId = request.SellerUserId,
            ProductStatus = ProductStatus.Available
        };
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return true;
    }
    public bool DeleteProduct(RemoveProductRequest request)
    {
        var product = _dbContext.Products.Find(request.ProductId);
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

    // I changed this method to async need to be tested after some orders are created
    public async Task<IEnumerable<Products>> GetSuggestedProductsAsync(SuggestedProductsRequest request)
    {
        int calculateIndex = request.CurrentPage * _appSettings.PageSize;
        // Get all user orders
        var userOrders = await _dbContext.Orders.Where(o => o.CostumerUserId == request.UserId).ToListAsync();
        // Get all user order items
        var userOrderItems = await _dbContext.OrderItems.Where(oi => userOrders.Any(o => o.Id == oi.OrderId)).ToListAsync();
        // Get all user products
        var userProducts = await _dbContext.Products.Where(p => userOrderItems.Any(oi => oi.ProductId == p.Id)).ToListAsync();
        // Get all user categories
        var userCategories = userProducts.Select(p => p.Category).Distinct();
        // Calculate the most popular category
        var mostPopularCategory = userCategories.GroupBy(c => c).OrderByDescending(c => c.Count()).First().Key;
        // Get all products from the most popular category
        var suggestedProducts = await _dbContext.Products
            .Where(p => p.Category == mostPopularCategory)
            .Skip(calculateIndex)
            .Take(_appSettings.PageSize)
            .ToListAsync();
        return suggestedProducts.Any() ? suggestedProducts : await _dbContext.Products.Skip(calculateIndex).Take(_appSettings.PageSize).ToListAsync();
    }

    public IEnumerable<Products> GetProductsByCategory(ProductsByCategoryRequest request)
    {
        int calculateIndex = request.CurrentPage * _appSettings.PageSize;
        return _dbContext.Products.Where(p => p.Category == request.Category).Skip(calculateIndex).Take(_appSettings.PageSize);
    }
    public async Task<IEnumerable<Products>> GetProductsBySearchAsync(ProductsBySearchRequest request)
    {
        int calculateIndex = request.CurrentPage * _appSettings.PageSize;
        var products = await _dbContext.Products
            .Where(p => p.Name.Contains(request.Search))
            .Skip(calculateIndex)
            .Take(_appSettings.PageSize)
            .ToListAsync();

        return products;
    }
}