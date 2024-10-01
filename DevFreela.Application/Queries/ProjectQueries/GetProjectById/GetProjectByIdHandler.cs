﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;
        public GetProjectByIdHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetDetailsById(request.Id);

            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto nao existe");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
