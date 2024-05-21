using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pri.Api.Music.Core.Entities;
using Pri.Api.Music.Core.Interfaces.Repositories;
using Pri.Api.Music.Core.Interfaces.Services;
using Pri.Api.Music.Core.Services;
using Pri.Api.Music.Infrastructure.Repositories;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services;
using Pri.CleanArchitecture.Music.Infrastructure.Data;
using Pri.CleanArchitecture.Music.Infrastructure.Repositories;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace Pri.Api.Music.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Add database service
            builder.Services.AddDbContext<ApplicationDbContext>
                (options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));
            //register identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //ONLY FOR TESTING PURPOSES!!!!
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            //configure jwt bearer token
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = builder.Configuration["JWTConfiguration:Audience"],
                ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"],
                IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:SecretKey"]))
            });
            //Add authorisation policies
            //admin claim
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                }
                );
                //userclaim
                options.AddPolicy("User", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        if(context.User.HasClaim(ClaimTypes.Role,"Admin")
                            || context.User.HasClaim(ClaimTypes.Role,"User"))
                        {
                            return true;
                        }
                        return false;
                    });
                });
                //18+
                options.AddPolicy("AdultOnly", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        //check if claims are present(if there is a token)
                        if (context.User.Claims.Count() != 0)
                        {
                            //get dob claim
                            var claimValue = context.User.Claims.FirstOrDefault(c =>
                            c.Type.Equals(ClaimTypes.DateOfBirth));

                            //parse to date
                            var dateOfBirth = DateTime.Parse(claimValue.Value);
                            //calculate age
                            if (DateTime.Now.Year - dateOfBirth.Year >= 18)
                            {
                                return true;
                            }
                            return false;
                        }
                        return false;
                     });
                });
            });
            //addcors
            builder.Services.AddCors();
            // Add services to the container.
            builder.Services.AddScoped<IRecordRepository, RecordRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IRecordService, RecordService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IArtistService, ArtistService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
