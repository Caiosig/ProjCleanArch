using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<Category> Update (Category category);

        //Irá deletar uma categoria
        Task<Category> Remove(Category category);
    }
}
