using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class EmployeeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_EmployeeDto>>> Get()
    {
        var Employees = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<List<P_EmployeeDto>>(Employees);
    }

    [HttpGet("GetByNotCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetByNotCustomer()
    {
        var Employees = await _unitOfWork.Employees.GetByNotCustomer();
        return Ok(Employees);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Employee Employee)
    {
         if (Employee == null)
        {
            return BadRequest();
        }
        _unitOfWork.Employees.Add(Employee);
        await _unitOfWork.SaveAsync();
       
        return "Employee Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Employee Employee)
    {
        if (Employee == null|| id != Employee.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Employees.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Employee, proveedExiste);
        _unitOfWork.Employees.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Employee Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (Employee == null)
        {
            return NotFound();
        }
        _unitOfWork.Employees.Remove(Employee);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Employee {Employee.Id} se eliminó con éxito." });
    }
}