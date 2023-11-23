using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class PaymentMethodController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PaymentMethodController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_PaymentMethodDto>>> Get()
    {
        var PaymentMethods = await _unitOfWork.PaymentMethods.GetAllAsync();
        return _mapper.Map<List<P_PaymentMethodDto>>(PaymentMethods);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(PaymentMethod PaymentMethod)
    {
         if (PaymentMethod == null)
        {
            return BadRequest();
        }
        _unitOfWork.PaymentMethods.Add(PaymentMethod);
        await _unitOfWork.SaveAsync();
       
        return "PaymentMethod Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] PaymentMethod PaymentMethod)
    {
        if (PaymentMethod == null|| id != PaymentMethod.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.PaymentMethods.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(PaymentMethod, proveedExiste);
        _unitOfWork.PaymentMethods.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "PaymentMethod Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var PaymentMethod = await _unitOfWork.PaymentMethods.GetByIdAsync(id);
        if (PaymentMethod == null)
        {
            return NotFound();
        }
        _unitOfWork.PaymentMethods.Remove(PaymentMethod);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El PaymentMethod {PaymentMethod.Id} se eliminó con éxito." });
    }
}