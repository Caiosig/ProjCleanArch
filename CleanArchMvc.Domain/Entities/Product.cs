using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
	public sealed class Product : Entity
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
		public int Stock { get; private set; }
		public string Image { get; private set; }

		//Relacionamento "Propriedade de navegação"
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public Product(string name, string description, decimal price, int stock, string image)
		{
			ValidateDomain(name, description, price, stock, image);
		}

		public Product(int id, string name, string description, decimal price, int stock, string image)
		{
			DomainExceptionValidation.When(id < 0, "Invalid Id value");
			Id = id;
			ValidateDomain(name, description, price, stock, image);
		}

		//Metodo que irá permitir a atualização dos dados do produto
		public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
		{
			ValidateDomain(name, description, price, stock, image);
			CategoryId = categoryId;
		}

		//Metodo que irá verificar a validação dos dados do produto
		private void ValidateDomain(string name, string description, decimal price, int stock, string image)
		{
			//Nome do produto não pode ser null
			DomainExceptionValidation.When(string.IsNullOrEmpty(name),
				"Invalid name. Name is required");

			//Nome do produto deve ter mais que 3 caracteres
			DomainExceptionValidation.When(name.Length < 3,
				"Invalid name. too short, minimum 3 characters");

			//Descrição do produto não pode ser null
			DomainExceptionValidation.When(string.IsNullOrEmpty(description),
				"Invalid descrition. Description is required");

			//Descrição do produto não pode ter menos que 5 caracteres
			DomainExceptionValidation.When(description.Length < 5,
				"Invalid descrition. too short, minimum 5 characters");

			//Valor do produto não pode ser negativo
			DomainExceptionValidation.When(price < 0, "Invalid price value");

			//Valor do produto em estoque, não pode ser negativo
			DomainExceptionValidation.When(stock < 0, "Invalid stock value");

			//O tamanho da imagem não pode exceder 250 caracteres
			DomainExceptionValidation.When(image?.Length > 250,
				"Invalid image name, too long, maximum 250 characters");

			Name = name;
			Description = description;
			Price = price;
			Stock = stock;
			Image = image;
		}
	}
}
