using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services
{
	public class ProductService : IProductService
	{
		private IMediator _mediator;
		private IMapper _mapper;

		public ProductService(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ProductDTO>> GetProduts()
		{
			var productsQuery = new GetProductQuery();

			if (productsQuery == null)
				throw new ApplicationException($"Etity could not be loaded");

			var result = await _mediator.Send(productsQuery);

			return _mapper.Map<IEnumerable<ProductDTO>>(result);
		}

		public async Task<ProductDTO> GetById(int? id)
		{
			var produtsByIdQuery = new GetProductByIdQuery(id.Value);

			if (produtsByIdQuery == null)
				throw new ApplicationException($"Entity could not be loaded");

			var result = await _mediator.Send(produtsByIdQuery);

			return _mapper.Map<ProductDTO>(result);
		}

		public async Task<ProductDTO> GetProductCategory(int? id)
		{
			var produtsByIdQuery = new GetProductByIdQuery(id.Value);

			if (produtsByIdQuery == null)
				throw new ApplicationException($"Entity could not be loaded");

			var result = await _mediator.Send(produtsByIdQuery);

			return _mapper.Map<ProductDTO>(result);
		}

		public async Task Add(ProductDTO productDTO)
		{
			var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
			await _mediator.Send(productCreateCommand);
		}

		public async Task Update(ProductDTO productDTO)
		{
			var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
			await _mediator.Send(productUpdateCommand);
		}

		public async Task Delete(int? id)
		{
			var productDeleteCommand = new ProductRemoveCommand(id.Value);

			if (productDeleteCommand == null)
				throw new ApplicationException($"Entity could not be loaded");

			await _mediator.Send(productDeleteCommand);
		}
	}
}
