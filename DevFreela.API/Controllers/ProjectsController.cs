using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.Entity;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context) 
        {
            _context = context;
        }
        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted).ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
            return Ok(model);
        }

        //Get api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        //Post api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model) 
        { 
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

           
            return CreatedAtAction(nameof(GetById), new {id = 1}, model); 
        }

        //PUT api/projects/123
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {

            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null) 
            {
                return NotFound();
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }


        //DELETE api/projects/123
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {

            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {

            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();


            return NoContent();
        }

        //PUT api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {

            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //Post api/projects/123/comment
        [HttpPost("{id}/comments")]
        public IActionResult PostComment (int id, CreateProjectCommentInputModel model)
        {

            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            var comment = new ProjectComment(model.Content, model.ProjectId, model.IdUser);

            _context.ProjectComments.Add(comment);  
            _context.SaveChanges();

            return Ok();
        }
    }
}
