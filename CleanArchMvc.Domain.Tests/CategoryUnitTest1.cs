using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
	public class CategoryUnitTest1
	{
		[Fact(DisplayName = "Create Category with valida state")]
		public void CreateCategory_WithValidParameters_ResultObjectValidState()
		{
			Action action = () => new Category(1, "Category Name");
			action.Should().NotThrow<Validation.DomainExceptionValidation>();
		}

		[Fact(DisplayName = "Create Category with negativa Id")]
		public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
		{
			Action action = () => new Category(-1, "Category name");
			action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");

		}

		[Fact(DisplayName = "Create Category with short name")]
		public void CreateCategory_ShortNameCategory_DomainExceptionShortName()
		{
			Action action = () => new Category(1, "Ca");
			action.Should().Throw<Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. too short, minimum 3 characters");
		}

		[Fact(DisplayName = "Create Category with null name")]
		public void CreateCategory_NullNameCategory_DomainExceptionName()
		{
			Action action = () => new Category(1, "");
			action.Should().Throw<Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. Name is required");
		}
	}
}