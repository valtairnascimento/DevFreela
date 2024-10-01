using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries
{
    public class GetAllSkillsHandler :IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _repository;
        public GetAllSkillsHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        async Task<ResultViewModel<List<SkillViewModel>>> IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>.Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetAll();

            var model = skill.Select(SkillViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);    
        }
    }
}
