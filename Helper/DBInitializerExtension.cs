using Enterprise_Expense_Tracker.DbSeeder;
using Enterprise_Expense_Tracker.ModelContext;

namespace Enterprise_Expense_Tracker.Helper
{
    public static class DBInitializerExtension
    {
        public static IApplicationBuilder UseSeedDB(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            DBSeeder.Seed(context);
            return app;
        }
    }
}
