using System;
using System.Collections.Generic;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Commands.GetAll
{
    public class GetAllCommand : IRequest<List<ProjectViewModel>>
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
    }
}