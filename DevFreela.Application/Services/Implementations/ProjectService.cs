using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Cancel();
            _dbContext.SaveChanges();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();
            _dbContext.SaveChanges();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Title, CreatedAt FROM Projects";

                return sqlConnection.Query<ProjectViewModel>(script).ToList();
            }
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "Select p.Id, p.Title, p.Description, p.TotalCost, p.StartedAt, p.FinishedAt, f.FullName as freela_name, u.FullName as client_name from Projects p " +
                    "inner join Users u on u.Id = p.IdClient " +
                    "inner join Users f on f.Id = p.IdFreelancer " +
                    "where p.id = 2";

                 var projectSelect =  sqlConnection.Query(script).FirstOrDefault();

                if (projectSelect == null) return null;

                var projectDetailsViewModel = new ProjectDetailsViewModel(
                projectSelect.Id,
                projectSelect.Title,
                projectSelect.Description,
                projectSelect.TotalCost,
                projectSelect.StartedAt,
                projectSelect.FinishedAt,
                projectSelect.client_name,
                projectSelect.freela_name
                );

                return projectDetailsViewModel;
            }
            //var project = _dbContext.Projects
            //    .Include(p => p.Client)
            //    .Include(p => p.Freelancer)
            //    .SingleOrDefault(p => p.Id == id);

            //if (project == null) return null;

            //var projectDetailsViewModel = new ProjectDetailsViewModel(
            //    project.Id,
            //    project.Title,
            //    project.Description,
            //    project.TotalCost,
            //    project.StartedAt,
            //    project.FinishedAt,
            //    project.Client.FullName,
            //    project.Freelancer.FullName
            //    );

        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE id = @id";

                sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });

            }

            //_dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            _dbContext.SaveChanges();
        }
    }
}
