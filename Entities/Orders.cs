namespace dotnet_3D_store_backend.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Orders
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int CostumerUserId { get; set; }

    [Required]
    public int SellerUserId { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public string Status { get; set; } = String.Empty;

    [Required]
    public string DeliveryCompany { get; set; } = String.Empty;

    [Required]
    public string DeliveryCompanyCode { get; set; } = String.Empty;

    [Required]
    public string DeliveryAddress { get; set; } = String.Empty;

    [Required]
    public DateTime EstimatedDeliveryDate { get; set; }

    public string OrderNote { get; set; } = String.Empty;

}