using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to update the account activation status of a user.
    /// </summary>
    public class UpdateUserIsAccountActivatedCommand : IRequest<bool>
    {
        public string Email { get; private set; }

        public UpdateUserIsAccountActivatedCommand(string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email), "El correo electrónico no puede ser nulo.");
        }
    }
}
