﻿using FSOLID.Commom.Notification;
using FSOLID.Commom.Publisher;
using FSOLID.Database.Context;
using FSOLID.Database.Repositories;
using FSOLID.Database.UnitOfWork;
using FSOLID.Domain.Commands;
using FSOLID.Domain.Events;
using FSOLID.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FSOLID.Cross.IoC
{
    public class DependencyInjectRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Publisher 
            services.AddScoped<IPublisher, Publisher>();

            //Repository
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Context
            services.AddDbContext<FSolidContext>(opt => opt.UseInMemoryDatabase("fsolidDatabase", null));

            //Commands
            services.AddTransient<NewPessoaCommandHandler, NewPessoaCommandHandler>();
            services.AddTransient<UpdatePessoaCommandHandler, UpdatePessoaCommandHandler>();

            services.AddTransient<NewEnderecoCommandHandler, NewEnderecoCommandHandler>();
            services.AddTransient<UpdateEnderecoCommandHandler, UpdateEnderecoCommandHandler>();

            services.AddTransient<NewEstadoCommandHandler, NewEstadoCommandHandler>();
            services.AddTransient<UpdateEstadoCommandHandler, UpdateEstadoCommandHandler>();

            //Events
            services.AddScoped<INotificationHandler<NewPessoaEvent>, NewPessoaEventHandler>();
            services.AddScoped<INotificationHandler<NewEnderecoEvent>, NewEnderecoEventHandler>();
            services.AddScoped<INotificationHandler<NewEstadoEvent>, NewEstadoEventHandler>();
            services.AddScoped<DomainNotificationHandler>();

        }
    }
}
