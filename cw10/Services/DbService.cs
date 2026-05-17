using cw10.Data;
using cw10.DTOs;
using cw10.Entities;
using cw10.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace cw10.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _dbContext;
    public DbService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPcDto>> GetAllPcs()
    {
        var pcs = await _dbContext.Pcs.Select(e => new GetPcDto()
        {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock,
        }).ToListAsync();
        
        return pcs;
    }

    public async Task<GetPcComponentsDetailsDto> GetComponents(int id)
    {
        var components = await _dbContext.Pcs
            .Where(e => e.Id == id)
            .Select(e => new GetPcComponentsDetailsDto()
            {
                Id = e.Id,
                Name = e.Name,
                Weight = e.Weight,
                Warranty = e.Warranty,
                CreatedAt = e.CreatedAt,
                Stock = e.Stock,
                Components = e.PcComponent.Select(pcc => new GetPcComponentsDto{
                    Amount = pcc.Amount,
                    Component = new GetComponentDto{
                        Code = pcc.Component.Code,
                        Name = pcc.Component.Name,
                        Description = pcc.Component.Description,
                        Manufacturer = new GetManufacturerDto
                        {
                            Id = pcc.Component.ComponentManufacturer.Id,
                            Abbreviation = pcc.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcc.Component.ComponentManufacturer.FullName,
                            FoundationDate = pcc.Component.ComponentManufacturer.FoundationDate
                        },
                        Type = new GetTypeDto
                        {
                            Id = pcc.Component.ComponentType.Id,
                            Abbreviation = pcc.Component.ComponentType.Abbreviation,
                            Name = pcc.Component.ComponentType.Name
                        }
                    }
                }).ToList()
            }).FirstOrDefaultAsync();

        if (components == null)
        {
            throw new NotFoundException();
        }
        
        return components;
    }

    public async Task<GetPcDto> CreatePc(CreatePcDto pcDto)
    {
        var newPc = new Pc
        {
            Name = pcDto.Name,
            Weight = pcDto.Weight,
            Warranty = pcDto.Warranty,
            CreatedAt = pcDto.CreatedAt,
            Stock = pcDto.Stock,
        };
        
        _dbContext.Pcs.Add(newPc);
        await _dbContext.SaveChangesAsync();

        return new GetPcDto
        {
            Id = newPc.Id,
            Name = newPc.Name,
            Weight = newPc.Weight,
            Warranty = newPc.Warranty,
            CreatedAt = newPc.CreatedAt,
            Stock = newPc.Stock,
        };
    }

    public async Task<GetPcDto> UpdatePc(int id, UpdatePcDto pcDto)
    {
        var pc = await _dbContext.Pcs.FirstOrDefaultAsync(e => e.Id == pcDto.Id);
        
        if (pc == null)
        { 
            throw new NotFoundException();
        }
        
        pc.Name = pcDto.Name;
        pc.Weight = pcDto.Weight;
        pc.Warranty = pcDto.Warranty;
        pc.CreatedAt = pcDto.CreatedAt;
        pc.Stock = pcDto.Stock;
        
        await _dbContext.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
        };
    }

    public async Task DeletePc(int id)
    {
        var pc = await _dbContext.Pcs.FindAsync(id);
        
        if (pc == null)
        {
            throw new NotFoundException();
        }
        _dbContext.Pcs.Remove(pc);
        
        await _dbContext.SaveChangesAsync();
    }
}