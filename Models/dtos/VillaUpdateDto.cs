using System.ComponentModel.DataAnnotations;

namespace MyWebApiProject.Models.dtos;

public class VillaUpdateDto
{
    [Required] public int Id { get; set; }

    public string Name { get; set; }
    public string Details { get; set; }
    public double Rate { get; set; }
    public int SQft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}