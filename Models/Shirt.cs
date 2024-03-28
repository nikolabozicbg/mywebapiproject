using System.ComponentModel.DataAnnotations;

namespace MyWebApiProject.Models;

public class Shirt
{
    [Required] public int ShirtId { get; set; }

    public string? Brand { get; set; }
    public string Color { get; set; }
    public int Size { get; set; }
    public string? Gender { get; set; }
    public string? MyProperty { get; set; }
    public double? Price { get; set; }
}