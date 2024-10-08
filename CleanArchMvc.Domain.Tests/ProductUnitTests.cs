using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTests
    {
        [Fact(DisplayName = "Create product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Create Product", "Create Products", 10, 10, "image with products");
            action.Should().NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName ="Create product with id invalid")]
        public void CreateProduct_WithInvalidId_DomainExceptionId()
        {
            Action action = () => new Product(-1, "Create Product", "Create Products", 10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }


        [Fact(DisplayName = "Create product whit null name")]
        public void CreateProduct_NullNameProduct_DomainExceptionName()
        {
            Action action = () => new Product(1, "", "Create Products", 10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }

        [Fact(DisplayName = "Create product with short name")]
        public void CreateProduct_ShortNameProduct_DomainExceptionName()
        {
            Action action = () => new Product(1, "Ca", "Create Products", 10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create product shot description")]
        public void CreateProduct_ShortDescriptionProduct_DomainExceptionDescription()
        {
            Action action = () => new Product(1, "Create Product", "Cr", 10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid descrition. too short, minimum 5 characters");
        }

        [Fact(DisplayName = "Create product with null description")]
        public void CreateProduct_NullDescriptionProduct_DomainExceptionDescription()
        {
            Action action = () => new Product(1, "Create Product", null, 10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid descrition. Description is required");
        }

        [Fact(DisplayName = "Create Product with price invalid value")]
        public void CreateProduct_NegativeProductPrice_DomainExceptionPrice()
        {
            Action action = () => new Product(1, "Create Product", "Description product", -10, 10, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_NegativeProductStock_DomainExceptionStock(int value)
        {
            Action action = () => new Product(1, "Create Product", "Description product", 10, value, "image with products");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }

        [Fact(DisplayName ="Create Product with image maximum 250 characters")]
        public void CreateProduct_MaximumCharactersImageProduct_DomainExceptionImage()
        {
            Action action = () => new Product(1, "Create Product", "Description product", 10, 10, "A importância da leitura na formação pessoal e profissional é inegável. Ler amplia nosso vocabulário, aprimora a escrita e nos apresenta novas ideias e perspectivas. Além disso, a leitura estimula a criatividade, permitindo que exploremos mundos diferentes e compreendamos melhor a complexidade das emoções humanas. Livros de ficção nos transportam para realidades alternativas, enquanto não-ficção nos oferece conhecimento prático e teórico sobre diversos assuntos. Em um mundo cada vez mais digital, dedicar tempo à leitura pode ser um desafio, mas é um investimento que traz benefícios a longo prazo, enriquecendo nossas vidas de maneiras que muitas vezes não conseguimos medir. Portanto, é fundamental cultivar o hábito da leitura desde cedo, incentivando crianças e jovens a se tornarem leitores ávidos e críticos.");
            action.Should().Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters");
        }

        [Fact(DisplayName ="Create Product with image null")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Create Product", "Description product", 10, 10, null);
            action.Should().NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with image null exception")]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Create Product", "Description product", 10, 10, null);
            action.Should().NotThrow<NullReferenceException>();
        }

    }
}
