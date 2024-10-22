using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
	public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
	{
		//Injeção de dependencia
		private readonly IProductRepository _productRepository;

		public ProductRemoveCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}


		public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
		{
			//Será pego o Id do produto via metodo do repository
			var product = await _productRepository.GetByIdAsync(request.Id);

			if (product == null)
				throw new ApplicationException($"Error  could not be found");
			else
			{
				//Caso retorne o Id do produto, será deletado o mesmo conforme a requisição do usuário
				var result = await _productRepository.RemoveAsync(product);
				return result;
			}
		}
	}
}
