using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TastyPoint.API.Selling.Resources;

public class SaveProductResource
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
    
    [Required]
    public int PackId { get; set; }
}