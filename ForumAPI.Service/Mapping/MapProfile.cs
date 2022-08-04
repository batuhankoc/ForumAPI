using AutoMapper;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Contract.UserContract;
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
        }
        
    }
}
