using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernelPluginDemov4.Models;
using SemanticKernelPluginDemov4.Services;


namespace SemanticKernelPluginDemov4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add DbContext
            builder.Services.AddDbContextFactory<NorthwindContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IOpenAIChatcompletionService, OpenAIChatcompletionService>();

            builder.Services.AddScoped<NorthwindSemanticKernelPlugin>();

            builder.Services.AddScoped(sp =>
            {
                var kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.AddOpenAIChatCompletion(modelId: builder.Configuration.GetSection("OpenAI").GetValue<string>("ModelId")!,
                    apiKey: builder.Configuration.GetSection("OpenAI").GetValue<string>("ApiKey")!);

                return kernelBuilder.Build();
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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
