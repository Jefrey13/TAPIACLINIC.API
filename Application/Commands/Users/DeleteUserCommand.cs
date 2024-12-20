﻿using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to delete a user by ID.
    /// </summary>
    public class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}