using MediatR;
using Application.Models;
using System.Collections.Generic;

namespace Application.Queries.Menus
{
    /// <summary>
    /// Query to get all menus.
    /// </summary>
    public class GetAllMenusQuery : IRequest<IEnumerable<MenuDto>>
    {
    }
}