using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class StateController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public StateController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_StateDto>>> Get()
    {
        var States = await _unitOfWork.States.GetAllAsync();
        return _mapper.Map<List<P_StateDto>>(States);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(State State)
    {
         if (State == null)
        {
            return BadRequest();
        }
        _unitOfWork.States.Add(State);
        await _unitOfWork.SaveAsync();
       
        return "State Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] State State)
    {
        if (State == null|| id != State.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.States.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(State, proveedExiste);
        _unitOfWork.States.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "State Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var State = await _unitOfWork.States.GetByIdAsync(id);
        if (State == null)
        {
            return NotFound();
        }
        _unitOfWork.States.Remove(State);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El State {State.Id} se eliminó con éxito." });
    }
}