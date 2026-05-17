using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw10.Entities;

public class Component
{
    public int Id { get; set; }
    public char Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ComponentManufacturerId { get; set; }
    public int ComponentTypeId { get; set; }
    
    public ComponentManufacturer ComponentManufacturer { get; set; }
    public ComponentType ComponentType { get; set; }
    
    public ICollection<PcComponent> PcComponent { get; set; } = [];
}