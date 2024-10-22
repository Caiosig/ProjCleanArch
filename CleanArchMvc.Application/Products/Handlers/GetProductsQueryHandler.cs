using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
	public class GetProductsQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<Product>>
	{
		//Injeção de dependencia
		private readonly IProductRepository _productRepository;

		public GetProductsQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<IEnumerable<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			//Irá retornar um lista de produtos conforme cadastrado no banco de dados
			return await _productRepository.GetProductsAsync();
		}
	}
}
