using System;
using System.Collections.Generic;

namespace TaskManager.Models.Entities;

public class Tag : IDbEntity
{
    public Tag() { Id = new Guid(); }
    
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Task> Tasks { get; set; } = new();
}