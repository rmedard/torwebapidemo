using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TORWebAPIDemo.Model;

namespace TORWebAPIDemo
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
            services.AddDbContext<TorDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            DbInitializer.Initialize(app);
            app.UseMvc();
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TorDbContext>();
                context.Database.EnsureCreated();
                if (context.Levels.Any())
                {
                    return;
                }

                var province = context.Levels.Add(new Level {Weight = 1, Name = "Province"}).Entity;
                var district = context.Levels.Add(new Level {Weight = 1, Name = "District"}).Entity;
                var sector = context.Levels.Add(new Level {Weight = 1, Name = "Sector"}).Entity;
                var cell = context.Levels.Add(new Level {Weight = 1, Name = "Cell"}).Entity;
                var village = context.Levels.Add(new Level {Weight = 1, Name = "Village"}).Entity;

                var location =
                    new Location
                    {
                        Level = village,
                        ParentLocation = new Location
                        {
                            Level = cell,
                            ParentLocation = new Location
                            {
                                Level = sector,
                                ParentLocation = new Location
                                {
                                    Level = district,
                                    ParentLocation = new Location
                                    {
                                        Level = province,
                                        ParentLocation = null,
                                        Name = "Kigali City"
                                    },
                                    Name = "Gasabo"
                                },
                                Name = "Kimironko"
                            },
                            Name = "Gabiro"
                        },
                        Name = "Bibare"
                    };
                
                var kigaliCity = context.Locations.Add(location).Entity;

                var cncMember = context.CncMembers.Add(new CncMember{Location = kigaliCity, Names = "Johnny Depp", Birthdate = new DateTime(1980, 5, 20), 
                    Gender = "F", EducationLevel = "primary", Position = "uvuga rikijyana", Contact = "123456543", Trained = true}).Entity;
                
                context.CncMembers.Add(cncMember);
                
                context.SaveChanges();
            }
        }
    }
}