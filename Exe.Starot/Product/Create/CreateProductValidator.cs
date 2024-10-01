using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(command => command.ProductName)
               .NotEmpty().WithMessage("Name can't be empty or null")
               .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.Content)
              .NotEmpty().WithMessage("Content can't be empty or null")
              .MaximumLength(150).WithMessage("Content can't be over 150 words");

            RuleFor(command => command.Price)
            .NotEmpty().WithMessage("Price can't be empty or null")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL can't be empty");
            RuleFor(x => x.ProductDescription)
             .NotEmpty().WithMessage("Description can't be empty or null")
              .MaximumLength(150).WithMessage("Description can't be over 150 words");

        }
    }
}
