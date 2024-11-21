
using GridHub.API.Configuration;
using GridHub.Repository.Interface;
using GridHub.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GridHub.Database;
using GridHub.Database.Models;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace GridHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;

            APPConfiguration appConfiguration = new APPConfiguration();

            builder.Services.Configure<APPConfiguration>(configuration);

            configuration.Bind(appConfiguration);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(swagger =>
            {
                // Carregar o arquivo XML de comentários
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);

                //Adiciona a possibilidade de enviar token para o controller
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                //Codigo para mudar a documentação do Swagger
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration.GetSection("Swagger:Title").Value,
                    Description = builder.Configuration.GetSection("Swagger:Description").Value,
                    Contact = new OpenApiContact()
                    {
                        Email = builder.Configuration.GetSection("Swagger:Email").Value,
                        Name = builder.Configuration.GetSection("Swagger:Name").Value
                    }
                });
            });

            builder.Services.AddDbContext<FIAPDBContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("FIAPDatabase"),
                    b => b.MigrationsAssembly("GridHub.Database"));
            });

            builder.Services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            builder.Services.AddScoped<IRepository<Espaco>, Repository<Espaco>>();
            builder.Services.AddScoped<IRepository<Microgrid>, Repository<Microgrid>>();
            builder.Services.AddScoped<IRepository<Investimento>, Repository<Investimento>>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseRouting();

            app.Run();
        }
    }
}
