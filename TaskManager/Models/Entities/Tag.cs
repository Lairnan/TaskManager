using System.Collections.Generic;

namespace TaskManager.Models.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Task> Tasks { get; set; } = new();
}