using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            ValidateDomain(name);

            //Id não pode ser negativo
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
        }

        //Relacionamento "Propriedade de navegação"
        public ICollection<Product> Products { get; set; }

        //Metodo que irá validar o nome do produto
        private void ValidateDomain(string name)
        {
            //Nome do produto não pode ser null
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), 
                "Invalid name. Name is required");

            //Nome do produto deve ter mais que 3 caracteres
            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name. too short, minimum 3 characters");

            Name = name;
        }

        //Metodo que irá ser responsavel por fazer a alteração do nome
        public void Update(string name)
        {
            ValidateDomain(name);
        }

    }
}
