namespace dotnet_3D_store_backend.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_3D_store_backend.Enumerators;

public class Products
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = String.Empty;

    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = String.Empty;

    [Required]
    [MaxLength(255)]
    public string Category { get; set; } = String.Empty;

    [Required]
    [MaxLength(255)]
    public string Image { get; set; } = String.Empty;

    [Required]
    public string Currency { get; set; } = "TRY";

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public int SellerUserId { get; set; }

    [Required]
    public ProductStatus ProductStatus { get; set; } = ProductStatus.Available;

}
