﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace C4FWebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           // .AddJwtBearer(options =>
           //{
           //    options.RequireHttpsMetadata = false;
           //    options.Authority = "webapi";
           //    options.Audience = "http://localhost:8080/auth/realms/coding4fun";
           //})

           //services.AddAuthentication(options =>
           //{
           //    options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
           //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           //})
           .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = "http://localhost:8080/auth/realms/coding4fun";
                options.MetadataAddress = "http://localhost:8080/auth/realms/coding4fun/.well-known/openid-configuration"; 
                options.Audience = "webapi";

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:8080/auth/realms/coding4fun",
                    ValidateLifetime = true
                };
            });
            //.AddCookie()
            //.AddOpenIdConnect(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.MetadataAddress = "http://localhost:8080/auth/realms/coding4fun/.well-known/openid-configuration";
            //    options.Authority = "http://localhost:8080/auth/realms/coding4fun";
            //    options.ClientId = "webapi";
            //    options.ClientSecret = "66776d4c-63c7-4f70-8251-c0a2fb67c7d3";
            //});

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Administrator", policy => policy.RequireClaim("webapi_admin", "[Administrator]"));
                options.AddPolicy("Administrator", policy => policy.RequireRole("administrator"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
