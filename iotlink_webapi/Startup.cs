using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace iotlink_webapi
{
    public class Startup
    {
        private Response<PlaceEntity> _response;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(m => m.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
            services.AddSingleton<PlaceService>();
            services.AddSingleton<AccountService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iotlink_webapi", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iotlink_webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                await next();

                await CheckStatusCode(context);
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async Task CheckStatusCode(HttpContext context)
        {
            if (context.Response.StatusCode != 200 && context.Response.StatusCode != 201)
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "bad_request",
                        Message = "Máy chủ không thể hiểu yêu cầu do cú pháp không hợp lệ",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "not_authen",
                        Message = "Chưa có authen",
                        Data = null
                    };  
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "not_have_role",
                        Message = "Bạn không có quyền sử dụng chức năng này",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "not_found",
                        Message = "Không tìm thấy trang này",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.RequestTimeout)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "requesr_timeout",
                        Message = "Hết thời gian request",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "internal_server_error",
                        Message = "",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.BadGateway)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "bad_gateway",
                        Message = "502 Bad Gateway",
                        Data = null
                    };
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.GatewayTimeout)
                {
                    _response = new Response<PlaceEntity>()
                    {
                        Status = "gateway_time_out",
                        Message = "Máy chủ mất quá nhiều thời gian để phản hồi",
                        Data = null
                    };
                }

                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_response));
            }
            
        }
    }
}
