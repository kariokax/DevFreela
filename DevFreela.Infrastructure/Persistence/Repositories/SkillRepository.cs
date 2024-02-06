using System.Linq;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillsRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        
        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }
        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _dbContext.Skills.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}