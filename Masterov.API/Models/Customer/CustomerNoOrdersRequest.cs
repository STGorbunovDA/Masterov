﻿namespace Masterov.API.Models.Customer;

public class CustomerNoOrdersRequest
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}