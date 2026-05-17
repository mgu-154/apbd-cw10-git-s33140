using System.ComponentModel.DataAnnotations;

namespace cw10.Entities;

public class PcComponent
{
    public int PcId { get; set; }
    public char ComponentCode { get; set; }
    public int Amount { get; set; }
    
    public Pc Pc { get; set; }
    public Component Component { get; set; }
}