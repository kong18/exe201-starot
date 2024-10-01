using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Get
{
    public class GetRandomTarotCardQuery : IRequest<TarotCardDto>
    {
    }
}
