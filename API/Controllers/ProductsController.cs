using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using System.Linq;
using AutoMapper;
using API.Error;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly  IGenericRepository<ProductType> _productType ;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo,
         IGenericRepository<ProductBrand> productBrand,
         IGenericRepository<ProductType> productType,
         IMapper mapper
        )
        {
          _productRepo = productRepo;
          _productBrand = productBrand ;
          _productType = productType;
          _mapper=mapper;

        }
           [HttpGet]
           public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
           {
               var spec = new ProductsWithTypesAndBrandsSpecification();
               var products = await _productRepo.ListAsync(spec);
               return Ok(_mapper
                    .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
           }
           
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id){
                var spec = new ProductsWithTypesAndBrandsSpecification(id);
                var product= await _productRepo.GetEntityWithSpec(spec);
                if(product==null) return NotFound(new ApiResponse(404));
           // return await _productRepo.GetEntityWithSpec(spec);
           return _mapper.Map<Product,ProductToReturnDto>(product);
            }
            [HttpGet("brands")]
            public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){

                return  Ok(await _productBrand.ListAllAsync());
            } 
            [HttpGet("types")]
            public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
                return Ok(await _productType.ListAllAsync());
            }
    }
}