
using IPChecker.Repository.Interfaces;
using IPChecker.Repository.Repositories;
using IPChecker.Service.Interfaces;
using IPChecker.Service.Mapping;
using IPChecker.Service.Services;

namespace IPChecker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IBlockedCountryRepository, BlockedCountryRepository>();
            builder.Services.AddSingleton<IBlockedAttemptsRepository, BlockedAttemptsRepository>();
            builder.Services.AddSingleton<IBlockService, BlockService>();
            builder.Services.AddHttpClient<IIPLookupService, IPLookupService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
