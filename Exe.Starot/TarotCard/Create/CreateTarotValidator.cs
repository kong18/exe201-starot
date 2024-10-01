using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Create
{
    public class CreateTarotValidator : AbstractValidator<CreateTarotCardCommand>
    {
        public CreateTarotValidator() {
            RuleFor(command => command.Name)
                  .NotEmpty().WithMessage("Name can't be empty or null")
                  .MaximumLength(100).WithMessage("Name can't be over 100 words");


            RuleFor(command => command.Content)
              .NotEmpty().WithMessage("Content can't be empty or null")
              .MaximumLength(300).WithMessage("Content can't be over 300 words");

            RuleFor(x => x.Image)
        .NotEmpty().WithMessage("URL can't be empty");
            
        }
    }
}
