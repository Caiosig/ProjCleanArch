using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
	public interface ICategoryService
	{
		//Retornar uma lista de categorias, assincrona
		Task<IEnumerable<CategoryDTO>> GetCategories();

		//Retorna apenas uma categoria, assincrona
		Task<CategoryDTO> GetById(int? id);

		//Irá criar uma nova categoria
		Task Add(CategoryDTO categoryDTO);

		//Irá atualizar a categoria
		Task Update(CategoryDTO categoryDTO);

		//Irá deletar uma categoria
		Task Delete(int? id);
	}
}
