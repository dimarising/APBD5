using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW.DAL;
using CW.Middlewares;
using CW.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CW.Tokens;
using System.Text;

namespace CW
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

              services.AddDbContext<StudentDBContext>(options =>
            {
                options.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16541;Integrated Security=True");
            });
            services.AddControllers();


            /*           services.AddControllers();

                       services.AddSwaggerGen(c =>
                       {
                           c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students App API", Version = "v1" });
                       });*/

            //--
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllersWithViews();
            //--

            /*  //HTTP Basic
              services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.TokenValidationParameters = new TokenValidationParameters
                          {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidIssuer = "Gakko",
                              ValidAudience = "Students",
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                          };
                      });*/



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoggingMiddleware>();

            


            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students App API");
            });

            //index
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Write indeks");
                    return;
                }

                string index = context.Request.Headers["Index"].ToString();

                await next();
            });*/

            //--
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            //--

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




        }
    }
