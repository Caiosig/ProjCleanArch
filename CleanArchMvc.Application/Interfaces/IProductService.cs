using CleanArchMvc.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
		//Retornar uma lista de produtos, assincrona
		Task<IEnumerable<ProductDTO>> GetProduts();

		//Retorna apenas uma produtos, assincrona
		Task<ProductDTO> GetById(int? id);

		//Retorna os produtos pelo id da categoria
		Task<ProductDTO> GetProductCategory(int? id);

		//Irá criar um novo produto
		Task Add(ProductDTO productDTO);

		//Irá atualizar um produto
		Task Update(ProductDTO productDTO);

		//Irá deletar um produto
		Task Delete(int? id);
    }
}
