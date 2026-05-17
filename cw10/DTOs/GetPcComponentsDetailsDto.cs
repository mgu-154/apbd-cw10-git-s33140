namespace cw10.DTOs;

public class GetPcComponentsDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<GetPcComponentsDto> Components { get; set; } = new();
}