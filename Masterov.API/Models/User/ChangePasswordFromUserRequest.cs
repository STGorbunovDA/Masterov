﻿namespace Masterov.API.Models.User;

public class ChangePasswordFromUserRequest
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}