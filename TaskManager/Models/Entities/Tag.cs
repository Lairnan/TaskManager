using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TaskManager.Models.Entities;

public class Tag : BindableBase, IDbEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private int _id;
    private string _name;
    private List<Task> _tasks = new();
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Key]
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    [Required]
    [MaxLength(255)]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    [InverseProperty("Tags")]
    public List<Task> Tasks
    {
        get => _tasks;
        set => SetProperty(ref _tasks, value);
    }
}