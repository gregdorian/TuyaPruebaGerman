using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;

namespace TuyaPruebaGerman.Application.Features.Productos.Commands.CreateProductos
{
    public class CreateProductoCommandValidator : AbstractValidator<CreateProductoCommand>
    {
        private readonly IProductoRepositoryAsync productoRepository;


        public CreateProductoCommandValidator(IProductoRepositoryAsync productoRepository)
        {
            this.productoRepository = productoRepository;

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueDescripcion).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.Precio)
                 .NotEmpty()
                 .GreaterThanOrEqualTo(0m)
                 .WithMessage("Price is required when enum is of value");

        }

        private async Task<bool> IsUniqueDescripcion(string descrip, CancellationToken arg2)
        {
            return await productoRepository.IsProductoDescriptionAsync(descrip);
        }
    }
}
