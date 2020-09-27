using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using LifeIsShort.Mongodb;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LifeIsShort.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddTransient(_ => new Repository(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")));

            return services;
        }

        public static IServiceCollection ResolveIdentity(this IServiceCollection services)
        {
            services.AddIdentityMongoDbProvider<MongoUser, MongoRole>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 8;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireDigit = false;
            }, mongoIdentityOptions =>
            {
                mongoIdentityOptions.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
                mongoIdentityOptions.RolesCollection = "Roles";
                mongoIdentityOptions.UsersCollection = "Users";
            });

            return services;
        }
    }
}
