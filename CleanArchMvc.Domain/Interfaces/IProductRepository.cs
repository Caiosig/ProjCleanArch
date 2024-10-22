using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
	public interface IProductRepository
	{

		//Retornar uma lista de produtos, assincrona
		Task<IEnumerable<Product>> GetProductsAsync();

		//Retorna apenas uma produtos, assincrona
		Task<Product> GetByIdAsync(int? id);

		//Retorna os produtos pelo id da categoria
		Task<Product> GetProductCategoryAsync(int? id);

		//Irá criar um novo produto
		Task<Product> CreateAsync(Product product);

		//Irá atualizar um produto
		Task<Product> UpdateAsync(Product product);

		//Irá deletar um produto
		Task<Product> RemoveAsync(Product product);
	}
}
