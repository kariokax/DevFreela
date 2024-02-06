using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillsRepository
    {
        Task<Skill> GetByIdAsync(int id);
    }
}