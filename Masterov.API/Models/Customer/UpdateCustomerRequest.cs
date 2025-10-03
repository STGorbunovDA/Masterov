namespace Masterov.API.Models.Customer;

public class UpdateCustomerRequest
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}