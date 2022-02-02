using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SendEmailWebApi.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SendEmailWebApi
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        /// <summary>
        /// ��������������� �������� ���������������� ��������
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("ConfigurationSMTP.json");
            AppConfiguration = builder.Build();
        }

        /// <summary>
        /// ConfigureServices() ������������ �������, ������� ������������ �����������:
        /// 1. �� ������ ������������ (ConfigurationSMTP.json) �� �������� AppConfiguration ��������� ������ Person;
        /// 2. �������� ����������� � ���� ������;
        /// 3. ������ �������� ���������;
        /// 4. ����� ������������� �����������, ���������� ����� services.AddControllers().
        /// </summary>
        /// <param name="services">������������ ��������� �������� � ����������</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpConfig>(AppConfiguration);
            services.AddDbContext<ApplicationContext>();
            services.AddTransient<ServiceSendMail>();
            services.AddControllers();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
