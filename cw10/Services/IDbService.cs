using cw10.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Services;

public interface IDbService
{
    Task<IEnumerable<GetPcDto>> GetAllPcs();
    Task<GetPcComponentsDetailsDto> GetComponents(int id);
    Task<GetPcDto> CreatePc(CreatePcDto pcDto);
    Task<GetPcDto> UpdatePc(UpdatePcDto pcDto);
    Task DeletePc(int id);
}