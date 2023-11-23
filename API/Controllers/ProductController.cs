using API.Controllers.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class ProductController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetByNotOrdered")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductWithImageDto>>> GetByNotOrdered()
    {
        var Products = await _unitOfWork.Products.GetByNotOrdered();
        return _mapper.Map<List<ProductWithImageDto>>(Products);
    }

    [HttpGet("GetByMostSold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetByMostSold()
    {
        var Products = await _unitOfWork.Products.GetByMostSold();
        return Ok(Products);
    }
    [HttpGet("GetByMostSoldLimit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetByMostSoldLimit()
    {
        var Products = await _unitOfWork.Products.GetByMostSoldLimit();
        return Ok(Products);
    }
    [HttpGet("GetGama")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetGama()
    {
        var Products = await _unitOfWork.Products.GetGama();
        return Ok(Products);
    }

    [HttpGet("GetTotalSalesByRangeIva/{price}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetTotalSalesByRangeIva(decimal range)
    {
        var Products = await _unitOfWork.Products.GetTotalSalesByRangeIva(range);
        return Ok(Products);
    }

    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_ProductDto>>> Get()
    {
        var Products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<List<P_ProductDto>>(Products);
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Product Product)
    {
         if (Product == null)
        {
            return BadRequest();
        }
        _unitOfWork.Products.Add(Product);
        await _unitOfWork.SaveAsync();
       
        return "Product Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Product Product)
    {
        if (Product == null|| id.ToString() != Product.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Products.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Product, proveedExiste);
        _unitOfWork.Products.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Product Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Product = await _unitOfWork.Products.GetByIdAsync(id);
        if (Product == null)
        {
            return NotFound();
        }
        _unitOfWork.Products.Remove(Product);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Product {Product.Id} se eliminó con éxito." });
    }
}