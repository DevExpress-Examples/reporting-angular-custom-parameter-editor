using CustomParameterEditor;
using CustomParameterEditor.Data;
using CustomParameterEditor.Server.Services;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Security.Resources;
using DevExpress.Utils.Serializing;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.EntityFrameworkCore;


DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(CustomParameterType));
DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(CustomParameterType[]));
ObjectDataSerializer.Register(CustomDataSerializer.Name, new CustomDataSerializer());

var builder = WebApplication.CreateBuilder(args);

AppDomain.CurrentDomain.SetData("DataDirectory", builder.Environment.ContentRootPath);
builder.Services.AddDevExpressControls();
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
builder.Services.AddMvc();
builder.Services.ConfigureReportingServices(configurator => {
    if(builder.Environment.IsDevelopment())
        configurator.UseDevelopmentMode();
    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
        viewerConfigurator.UseCachedReportSourceBuilder();
    });
});
builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

var app = builder.Build();
using(var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;    
    services.GetService<ReportDbContext>().InitializeDatabase();
}
var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(app.Environment.ContentRootPath, "Content")).FullName);
AccessSettings.ReportingSpecificResources.SetRules(contentDirectoryAllowRule, UrlAccessRule.Deny());

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseDevExpressControls();
System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html");

app.Run();