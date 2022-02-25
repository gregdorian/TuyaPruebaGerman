using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;

namespace TuyaPruebaGerman.Application.Features.OrdenCompras.Commands.CreateOrden
{
    public class CreateOrdenCompraCommandValidator : AbstractValidator<CreateOrdenCompraCommand>
    {
        private readonly IOrdenCompraRepositoryAsync ordenCompraRepository;

        public CreateOrdenCompraCommandValidator(IOrdenCompraRepositoryAsync ordenCompraRepository)
        {
            this.ordenCompraRepository = ordenCompraRepository;

            RuleFor(p => p.OrdenCompraNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueOrdenCompraNumber).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.DireccionEnvio)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }


        private async Task<bool> IsUniqueOrdenCompraNumber(string OrdenCompraNumber, CancellationToken cancellationToken)
        {
            return await ordenCompraRepository.IsOrdenCompranNumberAsync(int.Parse(OrdenCompraNumber));
        }
    }
}
