using System.Text;
using AutoMapper;
using FilmsAPI.Core;
using FilmsAPI.Core.Helpers;
using FilmsAPI.Core.Services;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace FilmsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // ----- Automapper configuration -----
            // Configure AutoMapper with profiles
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            // Configure Geometry Factory
            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

            // Configure AutoMapper with GeometryFactory
            services.AddSingleton(provider =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                return new MapperConfiguration(config =>
                {
                    config.AddProfile(new AutoMapperProfiles(geometryFactory));
                }).CreateMapper();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    o => o.UseNetTopologySuite()
                );
            });

            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<IStorageFiles, StorageFileImpl>();
            _loadServices(services);
            _loadRepositories(services);
            services.AddEndpointsApiExplorer();
            _configSwagger(services);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("").AllowAnyMethod().AllowAnyHeader();
                });
            });
            _authConfig(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmsAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void _authConfig(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["jwt:key"])
                        ),
                        ClockSkew = TimeSpan.Zero
                    }
                );
        }

        private void _loadServices(IServiceCollection services)
        {
            services.AddScoped<ExistMovieAttribute>();
            services.AddScoped<IGenreServices, GenreServicesImpl>();
            services.AddScoped<IActorServices, ActorServicesImpl>();
            services.AddScoped<IMovieServices, MovieServicesImpl>();
            services.AddScoped<ICinemaServices, CinemaServicesImpl>();
        }

        private void _loadRepositories(IServiceCollection services)
        {
            services.AddScoped<IGenreRepository, GenreRepositoryImpl>();
            services.AddScoped<IActorRepository, ActorRepositoryImpl>();
            services.AddScoped<IMovieRepository, MovieRepositoryImpl>();
            services.AddScoped<ICinemaRepository, CinemaRepositoryImpl>();
        }

        private void _configSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmsAPI", Version = "v1" });
            });
        }
    }
}