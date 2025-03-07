﻿using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.States
{
    /// <summary>
    /// Query to retrieve a state by its ID.
    /// </summary>
    public class GetStateByIdQuery : IRequest<StateDto>
    {
        public int Id { get; set; }

        public GetStateByIdQuery(int id)
        {
            Id = id;
        }
    }
}
