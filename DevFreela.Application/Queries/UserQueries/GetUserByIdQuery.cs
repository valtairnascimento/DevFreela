using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries
{
    public class GetUserByIdQuery:IRequest<ResultViewModel<UserViewModel>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }


    }
}
