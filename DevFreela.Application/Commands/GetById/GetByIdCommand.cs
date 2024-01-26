using System;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Commands.GetById
{
    public class GetByIdCommand : IRequest<ProjectDetailsViewModel>
    {
        public GetByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ClientFullName { get; private set; }
        public string FreelancerFullName { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
    }
}