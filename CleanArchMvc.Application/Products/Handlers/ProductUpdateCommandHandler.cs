using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
	public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
	{
		//Injeção de dependencia
		private readonly IProductRepository _productRepository;

        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
		{
			//Será pego o Id do produto via metodo do repository
			var product = await _productRepository.GetByIdAsync(request.Id);

			if (product == null)
				throw new ApplicationException($"Error could not be found");
			else
			{
				//Caso retorne o Id do produto, será feito o update conforme os dados passado pelo usuário
				product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId);
				return await _productRepository.UpdateAsync(product);
			}
		}
	}
}
