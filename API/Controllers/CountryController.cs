using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class CountryController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CountryController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_CountryDto>>> Get()
    {
        var Countries = await _unitOfWork.Countries.GetAllAsync();
        return _mapper.Map<List<P_CountryDto>>(Countries);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Country Country)
    {
         if (Country == null)
        {
            return BadRequest();
        }
        _unitOfWork.Countries.Add(Country);
        await _unitOfWork.SaveAsync();
       
        return "Country Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Country Country)
    {
        if (Country == null|| id != Country.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Countries.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Country, proveedExiste);
        _unitOfWork.Countries.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Country Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Country = await _unitOfWork.Countries.GetByIdAsync(id);
        if (Country == null)
        {
            return NotFound();
        }
        _unitOfWork.Countries.Remove(Country);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Country {Country.Id} se eliminó con éxito." });
    }
}