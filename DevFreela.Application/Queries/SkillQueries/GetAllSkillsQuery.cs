using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries
{
    public class GetAllSkillsQuery :IRequest<ResultViewModel<List<SkillViewModel>>>
    {
    }
}
