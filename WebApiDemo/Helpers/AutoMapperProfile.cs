using AutoMapper;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using WebApiDemo.DTO;
using WebApiDemo.Models;

namespace WebApiDemo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<Title, TitleDTO>().ReverseMap();
            CreateMap<TitleAuthor, TitleAuthorDTO>().ReverseMap();
        }
    }
}
