using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TaskManager.Models.Entities;

public class Tag : BindableBase, IDbEntity
{
    private int _id;
    private string _name;

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
    public List<Task> Tasks { get; set; } = new();
}