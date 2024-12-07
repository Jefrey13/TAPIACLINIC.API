using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Handles the command to update the account activation status of a user.
    /// </summary>
    public class UpdateUserIsAccountActivatedCommandHandler : IRequestHandler<UpdateUserIsAccountActivatedCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserIsAccountActivatedCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(UpdateUserIsAccountActivatedCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new ValidationException("Se debe proporcionar un correo electrónico válido.");
            }

            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Email);
            }

            bool result = await _userRepository.UpdateUserIsAccountActivated(request.Email);

            if (!result)
            {
                throw new OperationFailedException("La operación para actualizar el estado de activación del usuario falló.");
            }

            return true;
        }
    }
}