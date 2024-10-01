using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public string Search { get; set; } = "";
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 3;

    }
}
