using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Create
{
    public class CreateFeedbackCommand : IRequest<string>
    {
       
        public string ReaderId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
