using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class PaymentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PaymentController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_PaymentDto>>> Get()
    {
        var Payments = await _unitOfWork.Payments.GetAllAsync();
        return _mapper.Map<List<P_PaymentDto>>(Payments);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Payment Payment)
    {
         if (Payment == null)
        {
            return BadRequest();
        }
        _unitOfWork.Payments.Add(Payment);
        await _unitOfWork.SaveAsync();
       
        return "Payment Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Payment Payment)
    {
        if (Payment == null|| id.ToString() != Payment.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Payments.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Payment, proveedExiste);
        _unitOfWork.Payments.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Payment Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Payment = await _unitOfWork.Payments.GetByIdAsync(id);
        if (Payment == null)
        {
            return NotFound();
        }
        _unitOfWork.Payments.Remove(Payment);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Payment {Payment.Id} se eliminó con éxito." });
    }
}