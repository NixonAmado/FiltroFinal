using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class CityController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CityController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_CityDto>>> Get()
    {
        var Cities = await _unitOfWork.Cities.GetAllAsync();
        return _mapper.Map<List<P_CityDto>>(Cities);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(City City)
    {
         if (City == null)
        {
            return BadRequest();
        }
        _unitOfWork.Cities.Add(City);
        await _unitOfWork.SaveAsync();
       
        return "City Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] City City)
    {
        if (City == null|| id != City.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Cities.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(City, proveedExiste);
        _unitOfWork.Cities.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "City Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var City = await _unitOfWork.Cities.GetByIdAsync(id);
        if (City == null)
        {
            return NotFound();
        }
        _unitOfWork.Cities.Remove(City);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El City {City.Id} se eliminó con éxito." });
    }
}