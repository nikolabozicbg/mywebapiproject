using System.ComponentModel.DataAnnotations;

namespace MyWebApiProject.Models.dtos;

public class VillaNumberUpdateDTO
{
    [Required] public int VillaNo { get; set; }

    public string SpecialDetail { get; set; }
}