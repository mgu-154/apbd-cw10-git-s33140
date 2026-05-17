using System.ComponentModel.DataAnnotations;

namespace cw10.Entities;

public class ComponentType
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Component> Component { get; set; } = [];
}