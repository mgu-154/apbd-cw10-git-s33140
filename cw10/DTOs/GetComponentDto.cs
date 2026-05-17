namespace cw10.DTOs;

public class GetComponentDto
{
    public char Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GetManufacturerDto Manufacturer { get; set; } = new GetManufacturerDto();
    public GetTypeDto Type { get; set; } = new GetTypeDto();
}