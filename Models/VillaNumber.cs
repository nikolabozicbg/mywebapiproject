using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApiProject.Models;

public class VillaNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VillaNo { get; set; }

    public string SpecialDetail { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}