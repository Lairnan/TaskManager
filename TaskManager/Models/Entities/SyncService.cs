using System;

namespace TaskManager.Models.Entities;

public class SyncService
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}