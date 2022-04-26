using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RusMProjectApplication.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RusMProjectApplication.Statics
{
    public static class ServiceRegistrationFromApplication
    {
        public static IServiceCollection GenerateStartupFromApplication(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            var mappingConfig = new MapperConfiguration(i => i.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMediatR(assm);

            return services;


        }
    }
}
