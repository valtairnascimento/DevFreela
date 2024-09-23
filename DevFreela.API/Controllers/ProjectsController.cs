﻿using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IMediator _mediator;
        public ProjectsController(IProjectService service, IMediator mediator) 
        {
            _service = service;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "", int page = 0, int size =3)
        {

            var query = new GetAllProjectsQuery();

            var result = await _mediator.Send(query);   

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess) 
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertProjectCommand command) 
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) 
            {
                return BadRequest(result.Message);
            }
           
            return CreatedAtAction(nameof(GetById), new {id = result.Data}, command); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {

            var result = await _mediator.Send(command);

            if (!result.IsSuccess) 
            {
                return BadRequest(result.Message);
            }          

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {

            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {

            var result = await _mediator.Send(new StartProjectCommand(id));   

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {

            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment (int id, InsertCommentCommand command)
        {

         var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
