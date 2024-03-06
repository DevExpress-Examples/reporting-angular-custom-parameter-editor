using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.Security.Resources;
using Microsoft.EntityFrameworkCore;
using CustomParameterEditorAngularExample.Services;
using CustomParameterEditorAngularExample.Data;
using CustomParameterEditorAngularExample;
using DevExpress.XtraReports.Native;

DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(CustomParameterType));
DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(CustomParameterType[]));
SerializationService.RegisterSerializer(CustomDataSerializer.Name, new CustomDataSerializer());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDevExpressControls();
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
builder.Services
    .AddMvc()
    .AddNewtonsoftJson();
builder.Services.ConfigureReportingServices(configurator => {
    if(builder.Environment.IsDevelopment())
        configurator.UseDevelopmentMode();

    configurator.ConfigureReportDesigner(designerConfigurator => {
                            });
    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
        viewerConfigurator.UseCachedReportSourceBuilder();
    });
    configurator.UseAsyncEngine();
});
builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "ClientApp/dist";
});
builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

var app = builder.Build();
using(var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetService<ReportDbContext>();
    db.InitializeDatabase();
}
var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(app.Environment.ContentRootPath, "..", "Content")).FullName);
AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());
if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
} else {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
if(!app.Environment.IsDevelopment()) {
    app.UseSpaStaticFiles();
}
app.UseRouting();


app.UseDevExpressControls();
System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa => {
    // To learn more about options for serving an Angular SPA from ASP.NET Core,
    // see https://go.microsoft.com/fwlink/?linkid=864501

    spa.Options.SourcePath = "ClientApp";

    if(app.Environment.IsDevelopment()) {
        spa.UseAngularCliServer(npmScript: "start");
        spa.Options.StartupTimeout = TimeSpan.FromSeconds(240);
    }
});

app.Run();
