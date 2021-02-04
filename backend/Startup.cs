
using backend.Configurers;
using backend.Security;
using backend.Services.Implementations;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace backend
{
    public class Startup
    {

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion Properties


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ��������� �������������� ���������������� ������ ����������� ����������
            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });

            // ����������� �����������
            services.AddControllers().AddJsonOptions(jsonOptions => {
                jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
            });

            // ������� ��������� ��� �����-�������� ��������
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:5001");
                });
            });

            // �������� ������������� �������� �����
            services.AddMvc(option => option.EnableEndpointRouting = false);

            //  ��������� � ���������� ������������
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            RepositoryConfigurer.ConfigureAdd(services, connectionString);
            //  ��������� � ���������� �������� ������ � ����������
            DbServicesConfigurer.ConfigureAdd(services);
            // ��������� ������� ������������
            SecurityConfigurer.ConfigureAdd(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // ������� ����������� � ��������������
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("VueCorsPolicy");
            app.UseMvc();
            app.UseRouting();
            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            //app.UseSpaStaticFiles();
            app.UseSpa(configuration: builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });
        }
    }
}
