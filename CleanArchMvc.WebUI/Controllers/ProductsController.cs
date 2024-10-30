using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _environment;

		public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
		{
			_productService = productService;
			_categoryService = categoryService;
			_environment = environment;
		}

		/// <summary>
		/// Recupera uma lista de Produtos
		/// </summary>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja recuperado com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetProduts();
			return View(products);
		}

		/// <summary>
		/// Redireciona para a action criação de um novo produto
		/// </summary>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja redirecionado com sucesso</response>
		[HttpGet()]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Create()
		{
			//Função que irá retornar a lista de categorias para o operador selecionar conforme a necessidade
			//Retornado apenas Id e Name
			ViewBag.CategoryId =
						new SelectList(await _categoryService.GetCategories(), "Id", "Name");

			return View();
		}

		/// <summary>
		/// Adiciona um novo produto
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para criação de uma categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="201">Caso inserção seja feita com sucesso</response>
		[HttpPost]
		public async Task<IActionResult> Create(ProductDTO productDto)
		{
			if (ModelState.IsValid)
			{
				await _productService.Add(productDto);
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ViewBag.CategoryId =
							new SelectList(await _categoryService.GetCategories(), "Id", "Name");
			}
			return View(productDto);
		}

		/// <summary>
		/// Redireciona para a action edição de produtos
		/// </summary>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja redirecionado com sucesso</response>
		[HttpGet()]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) 
				return NotFound();

			var productDto = await _productService.GetById(id);

			if (productDto == null) 
				return NotFound();

			var categories = await _categoryService.GetCategories();
			ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

			return View(productDto);
		}

		/// <summary>
		/// Editando um produto
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para edição do produto</param>
		/// <returns>IActionResult</returns>
		/// <response code="201">Caso inserção seja feita com sucesso</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Edit(ProductDTO productDTO)
		{
			if (ModelState.IsValid)
			{
				await _productService.Update(productDTO);
				return RedirectToAction(nameof(Index));
			}
			return View(productDTO);
		}

		/// <summary>
		/// Removendo um produto
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para remoção da categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja retornado o Id com sucesso</response>
		/// Somente usuários autenticados e com role ADMIN podem excluir produtos
		[Authorize(Roles ="Admin")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var productDto = await _productService.GetById(id);

			if (id == null)
				return NotFound();

			return View(productDto);
		}

		/// <summary>
		/// Confirmando Remoção
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para remoção do produto</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso remoção seja feita com sucesso</response>
		[HttpPost(), ActionName("Delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _productService.Delete(id);

			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// Retorna os detalhes do produto
		/// </summary>
		/// <param name="ProductsDTO">Objetos com os campos necessários para apresentar detalhes do produto</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso as informações sejam retornadas com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var productsDto = await _productService.GetById(id);

			if (id == null)
				return NotFound();

			var wwwroot = _environment.WebRootPath;
			var image = Path.Combine(wwwroot, "images\\" + productsDto.Image);
			var exists = System.IO.File.Exists(image);

			ViewBag.ImageExist = exists;

			return View(productsDto);
		}
	}
}
