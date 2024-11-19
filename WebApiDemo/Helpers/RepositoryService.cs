using System.Data;
using WebApiDemo.Models;

//using System.Security.Policy;
using WebApiDemo.Services.Approach_Simple;
using WebApiDemo.Services.First_Approach;
using WebApiDemo.Services.Second_Approach;


namespace WebApiDemo.Helpers
{
    public static class RepositoryService
    {
        // Đăng ký các RepositoryService
        public static IServiceCollection MyConfigureRepositoryService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


            //Simple Repository
            services.AddScoped<IAuthorService, AuthorService>();

            //First Approach (way 1) about Repository Patten in Powerpoint file   
            services.AddScoped<IService<Publisher>, PublisherService>();

            //Second approach (way 2) about Repository Patten in Powerpoint file
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

            return services;
        }
    }
}
