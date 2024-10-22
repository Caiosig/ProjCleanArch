using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
	{
		//Injeção de dependencia
		private readonly IProductRepository _productRepository;

		public GetProductByIdQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			//Irá retornar o produto conforme o Id do mesmo
			return await _productRepository.GetByIdAsync(request.Id);
		}
	}
}
