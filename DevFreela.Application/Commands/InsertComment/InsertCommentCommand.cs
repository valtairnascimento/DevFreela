using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentCommand :IRequest<ResultViewModel>
    {
        public InsertCommentCommand(string content, int projectId, int idUser)
        {
            Content = content;
            ProjectId = projectId;
            IdUser = idUser;
        }

        public string Content { get; set; }
        public int ProjectId { get; set; }
        public int IdUser { get; set; }
    }
}

