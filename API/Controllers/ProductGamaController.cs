using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class ProductGamaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductGamaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_ProductGamaDto>>> Get()
    {
        var ProductGamas = await _unitOfWork.ProductGamas.GetAllAsync();
        return _mapper.Map<List<P_ProductGamaDto>>(ProductGamas);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ProductGama ProductGama)
    {
         if (ProductGama == null)
        {
            return BadRequest();
        }
        _unitOfWork.ProductGamas.Add(ProductGama);
        await _unitOfWork.SaveAsync();
       
        return "ProductGama Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ProductGama ProductGama)
    {
        if (ProductGama == null|| id.ToString() != ProductGama.Gama)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.ProductGamas.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(ProductGama, proveedExiste);
        _unitOfWork.ProductGamas.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "ProductGama Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var ProductGama = await _unitOfWork.ProductGamas.GetByIdAsync(id);
        if (ProductGama == null)
        {
            return NotFound();
        }
        _unitOfWork.ProductGamas.Remove(ProductGama);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El ProductGama {ProductGama.Gama} se eliminó con éxito." });
    }
}