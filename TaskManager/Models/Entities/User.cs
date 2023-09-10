using System;
using System.Collections.Generic;

namespace TaskManager.Models.Entities;

public class User
{
    public User() { Id = new Guid(); }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public List<SyncService> SyncServices { get; set; } = new();
}