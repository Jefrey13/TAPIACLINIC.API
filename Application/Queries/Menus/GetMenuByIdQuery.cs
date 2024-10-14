using MediatR;
using Application.Models;

namespace Application.Queries.Menus
{
    /// <summary>
    /// Query to get a menu by its ID.
    /// </summary>
    public class GetMenuByIdQuery : IRequest<MenuDto>
    {
        public int Id { get; set; }

        public GetMenuByIdQuery(int id)
        {
            Id = id;
        }
    }
}