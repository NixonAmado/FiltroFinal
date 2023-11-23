using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class OrderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("GetWithDataByOrderNotDelivered")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrderNotDeliveratedDto>>> GetWithDataByOrderNotDelivered()
    {
        var Orders = await _unitOfWork.Orders.GetWithDataByOrderNotDelivered();
        return _mapper.Map<List<OrderNotDeliveratedDto>>(Orders);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OrderDto>>> Get()
    {
        var Orders = await _unitOfWork.Orders.GetAllAsync();
        return _mapper.Map<List<P_OrderDto>>(Orders);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Order Order)
    {
         if (Order == null)
        {
            return BadRequest();
        }
        _unitOfWork.Orders.Add(Order);
        await _unitOfWork.SaveAsync();
       
        return "Order Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Order Order)
    {
        if (Order == null|| id != Order.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Orders.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Order, proveedExiste);
        _unitOfWork.Orders.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Order Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (Order == null)
        {
            return NotFound();
        }
        _unitOfWork.Orders.Remove(Order);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Order {Order.Id} se eliminó con éxito." });
    }
}