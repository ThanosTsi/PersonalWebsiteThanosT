using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DataAccess.Data;
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using PersonalWebsite.DataAccess.Repository.IRepository;
using PersonalWebsite.DataAccess.Repository;

/*Creates an instance of a WebApplication builder using the CreateBuilder method. This builder is used to configure the application.
The args parameter typically comes from the main method and contains the command-line arguments that were passed to the application*/
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();
// Make sure a JS engine is registered, or you will get an error!
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();

/*Add services to the container.
This line adds MVC services to the DI container. Specifically, it adds services required for controllers and views. This is necessary
because in an MVC application, controllers handle incoming HTTP requests and views are used to generate the HTML response.*/
builder.Services.AddControllersWithViews();

// Registers and configures ApplicationDBContext to use SQL Server with a connection string from the app configuration.
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// This line builds the web application that was configured using the builder.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// This line sets up the HTTPS Redirection Middleware, which redirects HTTP requests to HTTPS.
app.UseHttpsRedirection();


// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
    // If you want to use server-side rendering of React components,
    // add all the necessary JavaScript files here. This includes
    // your components as well as all of their dependencies.
    // See http://reactjs.net/ for more information. Example:
    //config
    //    .AddScript("~/js/First.jsx")
    //    .AddScript("~/js/Second.jsx");

    // If you use an external build too (for example, Babel, Webpack,
    // Browserify or Gulp), you can improve performance by disabling
    // ReactJS.NET's version of Babel and loading the pre-transpiled
    // scripts. Example:
    //config
    //    .SetLoadBabel(false)
    //    .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
});


// This line enables the application to serve static files, such as images, CSS files, and JavaScript files.
app.UseStaticFiles();

// This line sets up the routing middleware, which is responsible for mapping incoming HTTP requests to the correct route handlers.
app.UseRouting();

// This line adds authorization middleware to the pipeline, enabling the application to authenticate and authorize users.
app.UseAuthorization();

/*This line sets up a default route that the routing middleware will use if no other route matches the incoming request.
The pattern parameter defines the format of the URL. If no controller and action are specified in the URL, it will
default to Home controller and Index action. The id is optional (? denotes optional).*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
