using System.ComponentModel.DataAnnotations;

namespace MyWebApiProject.Models.dtos;

public class VillaNumberCreateDTO
{
    [Required] public int VillaNo { get; set; }

    public string SpecialDetail { get; set; }
}