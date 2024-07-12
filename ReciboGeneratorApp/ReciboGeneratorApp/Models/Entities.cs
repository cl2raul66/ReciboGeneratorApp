using LiteDB;

namespace ReceiptGeneratorApp.Models;

public class Receipt
{
    public ObjectId? Id { get; set; }
    public WorkshopInfo? WorkshopDetails { get; set; }
    public ReceiptInfo? ReceiptDetails { get; set; }
    public ClientInfo? ClientDetails { get; set; }
    public ServiceDetail[]? ServiceDetails { get; set; }
}

public class WorkshopInfo
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class ReceiptInfo
{
    public DateTime? IssueDate { get; set; }
    public string? FileNumber { get; set; }
    public decimal? TotalAmount { get; set; }
}

public class ClientInfo
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public VehicleInfo? Vehicle { get; set; }
}

public class VehicleInfo
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
}

public class ServiceDetail
{
    public string? Description { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? Quantity { get; set; }
    public decimal? Subtotal { get; set; }
}
