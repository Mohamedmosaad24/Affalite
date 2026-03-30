
using AffaliteBL.IServices;
using AffaliteBL.Services;
using AffaliteDAL.Data;
using AffaliteDAL.IRepo;
using AffaliteDAL.Repo;
using Microsoft.EntityFrameworkCore;

namespace AffalitePL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AffaliteDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
         

            // Register Services islam
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICommissionService, CommissionService>();
            /******************************************************************************/

            builder.Services.AddScoped<IAffiliateRepo, AffiliateRepo>();
            builder.Services.AddScoped<IAffiliateService, AffiliateService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
