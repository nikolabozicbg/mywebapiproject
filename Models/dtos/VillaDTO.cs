using System.ComponentModel.DataAnnotations;

namespace MyWebApiProject.Models.dtos;

public class VillaDTO
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }
}