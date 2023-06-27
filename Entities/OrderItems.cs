namespace dotnet_3D_store_backend.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public string Status { get; set; } = String.Empty;

    public string OrderNote { get; set; } = String.Empty;

}