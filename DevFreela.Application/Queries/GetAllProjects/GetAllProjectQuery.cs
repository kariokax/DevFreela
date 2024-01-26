using System;
using System.Collections.Generic;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQuery : IRequest<List<ProjectViewModel>>
    {
        public GetAllProjectQuery(string query)
        {
            Query = query;
        }

        public string Query { get; private set; }
    }
}