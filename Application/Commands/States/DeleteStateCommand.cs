using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.States
{
    /// <summary>
    /// Command to delete a state by ID.
    /// </summary>
    public class DeleteStateCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteStateCommand(int id)
        {
            Id = id;
        }
    }
}
