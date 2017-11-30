using System.Linq;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public class DbInitializer
    {
        public static void Initialize(IYabaDBContext context)
        {
            if (context.Budgets.Any() || context.Tabs.Any())
            {
                return;
            }
            
            var budgets = new []
            {
                new Budget { Name = "My Budget" },
                new Budget { Name = "Company Budget" },
            };
            context.Budgets.AddRange(budgets);

            var categories = new[]
            {
                new BudgetCategory
                {
                    Name = "Food",
                    Budget = budgets.SingleOrDefault(b => b.Name == "My Budget"),
                },
            };
            context.BudgetCategories.AddRange(categories);
            
            context.SaveChangesAsync().Wait();
        }
    }
}