using System.Linq;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public static class DbInitializer
    {
        public static void Initialize(IYabaDBContext context)
        {
            if (context.Budgets.Any() || context.Tabs.Any())
            {
                return;
            }

            var myBudget = new Budget {Name = "My Budget"};
            var companyBudget = new Budget {Name = "Company Budget"};
            context.Budgets.AddRange(myBudget, companyBudget);

            var foodCat = new BudgetCategory
            {
                Name = "Food",
                Budget = myBudget,
            };
            var billCat = new BudgetCategory
            {
                Name = "Bills",
                Budget = myBudget,
            };
            var officeCat = new BudgetCategory
            {
                Name = "Office Supplies",
                Budget = companyBudget,
            };
            context.BudgetCategories.AddRange(foodCat, billCat, officeCat);

            var entries = new[]
            {
                new BudgetEntry {Amount = 65.0m, Description = "Hawaii Pizza", BudgetCategory = foodCat},
                new BudgetEntry {Amount = 120.75m, Description = "Indkøb Netto", BudgetCategory = foodCat},
                new BudgetEntry {Amount = 30.0m, Description = "Kebab", BudgetCategory = foodCat},
                new BudgetEntry {Amount = 82.50m, Description = "Bland-selv slik", BudgetCategory = foodCat},

                new BudgetEntry {Amount = 150.0m, Description = "Mobilregning", BudgetCategory = billCat},
                new BudgetEntry {Amount = 200.0m, Description = "A-kasse", BudgetCategory = billCat},
                new BudgetEntry {Amount = 300.0m, Description = "Afbetaling (narkogæld)", BudgetCategory = billCat},

                new BudgetEntry {Amount = 150.0m, Description = "Post-its", BudgetCategory = officeCat},
                new BudgetEntry {Amount = 531.5m, Description = "Personalemad", BudgetCategory = officeCat},
                new BudgetEntry {Amount = 20.0m, Description = "Kuglepen", BudgetCategory = officeCat},
            };
            context.BudgetEntries.AddRange(entries);
            
            context.SaveChangesAsync().Wait();
        }
    }
}