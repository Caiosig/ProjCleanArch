using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productsService.GetProduts();

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProducts")]
        public async Task<ActionResult<ProductDTO>> GetById(int? id)
        {
            var products = await _productsService.GetById(id);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest("Invald Data");

            await _productsService.Add(productDTO);

            return new CreatedAtRouteResult("GetProducts", new { id = productDTO.Id }, productDTO);
        }


        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
                return BadRequest();

            if (productDTO == null)
                return BadRequest();

            await _productsService.Update(productDTO);

            return Ok(productDTO);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var products = await _productsService.GetById(id);

            if (products == null)
                return NotFound("Products not found");

            await _productsService.Delete(id);

            return Ok(products);
        }
    }
}
