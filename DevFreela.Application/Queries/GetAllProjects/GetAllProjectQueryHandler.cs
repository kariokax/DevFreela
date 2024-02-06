using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery,List<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;
        
        public GetAllProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync();
                
           var projectViewModel = projects.Select(p => new ProjectViewModel(p.Id,p.Title,p.CreatedAt,p.Status)).ToList();

            return projectViewModel;
        }
    }
}