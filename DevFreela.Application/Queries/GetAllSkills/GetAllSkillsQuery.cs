using System.Collections.Generic;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<SkillViewModel>
    {
        public GetAllSkillsQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}