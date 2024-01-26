using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery,List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;
        
        public GetAllProjectQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects.Select(
                                        p => new ProjectViewModel(p.Id,p.Title,p.CreatedAt,p.Status)).ToListAsync();

            return projects;
        }
    }
}