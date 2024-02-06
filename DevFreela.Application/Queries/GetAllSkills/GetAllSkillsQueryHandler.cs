using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, SkillViewModel>
    {
        private readonly ISkillsRepository _skillsRepository;

        public GetAllSkillsQueryHandler(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
        
        public async Task<SkillViewModel> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skill = await _skillsRepository.GetByIdAsync(request.Id);
            var skillViewModel = new SkillViewModel(skill.Id, skill.Description);
            return skillViewModel;
        }
    }
}