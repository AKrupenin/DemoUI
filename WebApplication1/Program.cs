using Microsoft.Extensions.DependencyInjection;
using WSSC.V5.SYS.Admin.UI;
using WSSC.V5.SYS.UICore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorPages();

builder.Services.ConfigureOptions(typeof(EditorRCLConfigureOptions));
builder.Services.ConfigureOptions(typeof(CustomControl.EditorRCLConfigureOptions));

builder.Services.AddMvcCore(o =>
{
    o.ModelBinderProviders.Insert(0,
        new InterfacesModelBinderProvider());
    //o.EnableEndpointRouting = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
