using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
	public interface ICategoryRepository
	{
		//Retornar uma lista de categorias, assincrona
		Task<IEnumerable<Category>> GetCategories();

		//Retorna apenas uma categoria, assincrona
		Task<Category> GetById(int? id);

		//Irá criar uma nova categoria
		Task<Category> Create(Category category);

		//Irá atualizar a categoria
		Task<Category> Update(Category category);

		//Irá deletar uma categoria
		Task<Category> Remove(Category category);
	}
}
