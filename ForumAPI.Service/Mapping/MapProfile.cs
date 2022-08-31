using AutoMapper;
using ForumAPI.Contract.AnswerContract;
using ForumAPI.Contract.LoginContract;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Contract.UserContract;
using ForumAPI.Contract.VoteContract;
using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddUserContract, User>().ReverseMap();
            CreateMap<AddQuestionContract, Question>().ReverseMap();
            CreateMap<User, UserLoginContract>().ReverseMap();
            CreateMap<Answer, AddAnswerContract>().ReverseMap();
            CreateMap<AddQuestionToFavContract, Favorite>().ReverseMap();
            CreateMap<Vote, AddVoteContract>().ReverseMap();
            CreateMap<Question, GetAllQuestionsContract>().ReverseMap();
        }

    }
}
