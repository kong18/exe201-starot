using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Delete
{
    public class DeleteTarotCardCommandValidator : AbstractValidator<DeleteTarotCardCommand>    
    {
        public DeleteTarotCardCommandValidator()
        {
            RuleFor(x=> x.Id).NotEmpty().WithMessage("ID not empty.");
        }
    }
}
