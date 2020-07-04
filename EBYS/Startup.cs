using Business.Abstract;
using Business.Concrete.EntityFramework;
using DataAcces.Abstract;
using DataAcces.Concrete.EntityFramework;
using Services.NativeServices.Abstract;
using Services.NativeServices.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Unicode;
using Services.Adapter.Abstract;
using Services.Adapter.Concrete;
using Hangfire;
using Utilities.Abstract;
using Utilities.Concrete;

namespace EBYS
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
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(1440));

            services.AddControllersWithViews();
            
            services.AddDbContext<EfContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")
                ));
            
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            //Special Turkish Characters (ð,ü,þ,ç,ö,i) support. https://stackoverflow.com/questions/13451209/charset-utf-8-in-asp-net-for-special-turkish-characters
            services.AddWebEncoders(o => {
                o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddHangfire( options =>
            {
                options.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            
            services.TryAddTransient<IAboutMeDal, AboutMeDal>();
            services.TryAddTransient<IChairDal, ChairDal>();
            services.TryAddTransient<ICorrespondenceDal, CorrespondenceDal>();
            services.TryAddTransient<IDocumentAttachmentDal, DocumentAttachmentDal>();
            services.TryAddTransient<IDocumentDal, DocumentDal>();
            services.TryAddTransient<IDocumentRelatedDal, DocumentRelatedDal>();
            services.TryAddTransient<IDraftDal, DraftDal>();
            services.TryAddTransient<IDutyDal, DutyDal>();
            services.TryAddTransient<IInboxDal, InboxDal>();
            services.TryAddTransient<IInstitutionDal, InstitutionDal>();
            services.TryAddTransient<IOfficeDal, OfficeDal>();
            services.TryAddTransient<ISentDal, SentDal>();
            services.TryAddTransient<IStaffDal, StaffDal>();
            services.TryAddTransient<ISubmissionDal, SubmissionDal>();
            services.TryAddTransient<IToDal, ToDal>();            
            services.TryAddTransient<ITrashDal, TrashDal>();
            services.TryAddTransient<IUnitDal, UnitDal>();

            services.TryAddTransient<IAboutMeManager, AboutMeManager>();
            services.TryAddTransient<IChairManager, ChairManager>();
            services.TryAddTransient<ICorrespondenceManager, CorrespondenceManager>();
            services.TryAddTransient<IDocumentAttachmentManager, DocumentAttachmentManager>();
            services.TryAddTransient<IDocumentManager, DocumentManager>();
            services.TryAddTransient<IDocumentRelatedManager, DocumentRelatedManager>();
            services.TryAddTransient<IDraftManager, DraftManager>();
            services.TryAddTransient<IDutyManager, DutyManager>();
            services.TryAddTransient<IInboxManager, InboxManager>();
            services.TryAddTransient<IInstitutionManager, InstitutionManager>();
            services.TryAddTransient<IOfficeManager, OfficeManager>();
            services.TryAddTransient<ISentManager, SentManager>();
            services.TryAddTransient<IStaffManager, StaffManager>();
            services.TryAddTransient<ISubmissionManager, SubmissionManager>();
            services.TryAddTransient<IToManager, ToManager>();
            services.TryAddTransient<ITrashManager, TrashManager>();
            services.TryAddTransient<IUnitManager, UnitManager>();
            
            services.TryAddTransient<ISystemService, SystemSevice>();
            services.TryAddTransient<IFileCompressionService, CSharpCodeAdapter>();
            services.TryAddTransient<IMessageService, MessageService>();

            services.TryAddTransient<IUtility, Utility>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
