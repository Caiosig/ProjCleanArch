using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
	public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
	{
		//Injeção de dependencia
		private readonly IProductRepository _productRepository;

		public ProductCreateCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
		{
			//Será criado um novo produto conforme o cliente informou nos campos
			var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image);

			if (product == null)
				throw new ApplicationException($"Error creating entity");
			else
			{   //Caso o produto não seja nulo, será feito o cadastro do produto conforme as informações do usuário
				product.CategoryId = request.CategoryId;
				return await _productRepository.CreateAsync(product);
			}
		}
	}
}
