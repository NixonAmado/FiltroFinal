using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class OfficeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OfficeController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("GetByNotAssociatedEmployeeToGamaP/{gama}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EssencialOfficeDto>>> GetByNotAssoEmployeeToGamaP(string gama)
    {
        var Offices = await _unitOfWork.Offices.GetByNotAssociatedEmployeeToGamaP(gama);
        return _mapper.Map<List<EssencialOfficeDto>>(Offices);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OfficeDto>>> Get()
    {
        var Offices = await _unitOfWork.Offices.GetAllAsync();
        return _mapper.Map<List<P_OfficeDto>>(Offices);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Office Office)
    {
         if (Office == null)
        {
            return BadRequest();
        }
        _unitOfWork.Offices.Add(Office);
        await _unitOfWork.SaveAsync();
       
        return "Office Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Office Office)
    {
        if (Office == null|| id.ToString() != Office.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Offices.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Office, proveedExiste);
        _unitOfWork.Offices.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Office Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Office = await _unitOfWork.Offices.GetByIdAsync(id);
        if (Office == null)
        {
            return NotFound();
        }
        _unitOfWork.Offices.Remove(Office);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Office {Office.Id} se eliminó con éxito." });
    }
}