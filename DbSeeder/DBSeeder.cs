using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace Enterprise_Expense_Tracker.DbSeeder
{
    public class DBSeeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            var executionStrategy = dbContext.Database.CreateExecutionStrategy();
            executionStrategy.Execute(
                () =>
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Seed Users
                            if (!dbContext.Users.Any())
                            {
                                var usersData = File.ReadAllText("./Rescource/NewUser.json");
                                var parsedUsers = JsonConvert.DeserializeObject<User[]>(usersData);
                                foreach (var user in parsedUsers)
                                {
                                    user.Password = user.Password;
                                }
                                dbContext.Users.AddRange(parsedUsers);
                                dbContext.SaveChanges();
                            }
                            transaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                });
                }
    }
}
