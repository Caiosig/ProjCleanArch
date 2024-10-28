using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
	public class CategoriesController : Controller
	{
		#region Injeção de dependencia
		private readonly ICategoryService _categoryService;
		#endregion

		#region Construtor
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		#endregion

		/// <summary>
		/// Recupera uma lista de Categorias
		/// </summary>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja recuperado com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetCategories();
			return View(categories);
		}

		/// <summary>
		/// Redireciona para a action criação de uma nova categoria
		/// </summary>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja redirecionado com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Adiciona uma nova categoria no banco
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para criação de uma categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="201">Caso inserção seja feita com sucesso</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(CategoryDTO category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.Add(category);
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		/// <summary>
		/// Redireciona para a action edição de categoria
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

			var categoryDTO = await _categoryService.GetById(id);

			if (categoryDTO == null)
				return NotFound();

			return View(categoryDTO);
		}

		/// <summary>
		/// Editando uma categoria
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para edição da categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="201">Caso inserção seja feita com sucesso</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _categoryService.Update(categoryDTO);
				}
				catch (Exception)
				{

					throw;
				}

				return RedirectToAction(nameof(Index));
			}
			return View(categoryDTO);
		}

		/// <summary>
		/// Removendo uma categoria
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para remoção da categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso seja retornado o Id com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return NotFound();

			var categoryDTO = await _categoryService.GetById(id);

			if (categoryDTO == null)
				return NotFound();

			return View(categoryDTO);
		}

		/// <summary>
		/// Confirmando Remoção
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para remoção da categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso remoção seja feita com sucesso</response>
		[HttpPost(), ActionName("Delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _categoryService.Delete(id);

			return RedirectToAction("Index");
		}


		/// <summary>
		/// Retorna os detalhes da categoria
		/// </summary>
		/// <param name="CategoryDTO">Objetos com os campos necessários para apresentar detalhes da categoria</param>
		/// <returns>IActionResult</returns>
		/// <response code="200">Caso as informações sejam retornadas com sucesso</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return NotFound();

			var categoryDTO = await _categoryService.GetById(id);

			if (categoryDTO == null)
				return NotFound();

			return View(categoryDTO);
		}
	}
}
