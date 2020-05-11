using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Sample.Api.InfraMongo;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sample.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // TODO Add MongoDb settings in appsettings
        // TODO Add ExceptionMiddleware
        public void ConfigureServices(IServiceCollection services)
        {

            #region Api Configurations

            services
                .AddCors(options => options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));

            services
                .AddControllers()
                .AddJsonOptions(op =>
                {
                    op.JsonSerializerOptions.IgnoreNullValues = true;
                    op.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                });

            #endregion


            #region Swagger Configurations

            var assemblyPath = typeof(Startup).Assembly.Location;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Sample Api MongoDB v1",
                        Version = "v1",
                        Description = "Sample Api MongoDB v1"
                    });
                c.ExampleFilters();
                c.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
                c.UseInlineDefinitionsForEnums();
                c.AddSecurityDefinition("api_key", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "x-api-key"
                });
                c.IncludeXmlComments($"{Path.GetDirectoryName(assemblyPath)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(assemblyPath)}.xml");
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            #endregion


            #region Mongo Configurations

            var mongoClientSettings = MongoClientSettings.FromConnectionString(_configuration.GetConnectionString("Mongo"));

            services.AddSingleton(mongoClientSettings);

            services.AddSingleton<IConventionPack>(new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfNullConvention(true)
            });

            services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

            services.AddTransient(typeof(IMongoBaseRepository<>), typeof(MongoBaseRepository<,>));

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Api MongoDB v1");
                c.EnableFilter();
                c.DocExpansion(DocExpansion.List);
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}