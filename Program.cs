using Microsoft.EntityFrameworkCore;
using Enterprise_Expense_Tracker.ModelContext;
using System;
using Enterprise_Expense_Tracker.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Enterprise_Expense_Tracker.ServiceRepository;
using Microsoft.OpenApi.Models;
using Enterprise_Expense_Tracker.Helper;
using Serilog;

namespace Enterprise_Expense_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
            var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            // Add services to the container.
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                        // Adding Jwt Bearer
                        .AddJwtBearer(x =>
                        {
                            x.SaveToken = true;
                            x.RequireHttpsMetadata = false;
                            x.TokenValidationParameters = new TokenValidationParameters()
                            {

                                //ValidateIssuer = true,
                                //ValidateAudience = true,
                                //ValidateLifetime = true,
                                ValidateAudience = false,
                                ValidateIssuer = false,
                                ValidateIssuerSigningKey = true,
                                //ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                //ValidAudience = builder.Configuration["Jwt:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(key),

                                //ValidAudience = false,
                                //ValidIssuer = false
                                //IssuerSigningKey =false)
                            };
                        });

            /*builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });*/
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("employee", policy => policy.RequireRole("employee"));
                options.AddPolicy("manager", policy => policy.RequireRole("manager"));

            });
            builder.Services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);


            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiler));

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IEmployee_Expenses,Employee_Expenses>();
            builder.Services.AddScoped<IManager_Expenses, Manager_Expenses>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                /*swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET Core 6 Web API",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });*/
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSeedDB();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(options => options.WithOrigins("http://localhost:4200 ")
            .AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.UseMiddleware<Log_Error_HandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
