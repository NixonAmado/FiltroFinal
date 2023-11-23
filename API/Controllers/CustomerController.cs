using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class CustomerController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomerController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("GetWithOrdersQuantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetWithOrdersQuantity()
    {
        var Customers = await _unitOfWork.Customers.GetWithOrdersQuantity();
        return Ok(Customers);
    }

    [HttpGet("GetNameNotDeliveratedOnTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetNameNotDeliveratedOnTime()
    {
        var Customers = await _unitOfWork.Customers.GetNameNotDeliveratedOnTime();
        return Ok(Customers);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_CustomerDto>>> Get()
    {
        var Customers = await _unitOfWork.Customers.GetAllAsync();
        return _mapper.Map<List<P_CustomerDto>>(Customers);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Customer Customer)
    {
         if (Customer == null)
        {
            return BadRequest();
        }
        _unitOfWork.Customers.Add(Customer);
        await _unitOfWork.SaveAsync();
       
        return "Customer Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Customer Customer)
    {
        if (Customer == null|| id != Customer.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Customers.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Customer, proveedExiste);
        _unitOfWork.Customers.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Customer Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (Customer == null)
        {
            return NotFound();
        }
        _unitOfWork.Customers.Remove(Customer);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Customer {Customer.Id} se eliminó con éxito." });
    }
}