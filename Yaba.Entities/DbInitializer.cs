using System.Linq;
using Yaba.Entities.Budget;

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

            var myBudget = new Budget.BudgetEntity {Name = "My Budget"};
            var companyBudget = new Budget.BudgetEntity {Name = "Company Budget"};
            context.Budgets.AddRange(myBudget, companyBudget);

            var foodCat = new CategoryEntity
            {
                Name = "Food",
                BudgetEntity = myBudget,
            };
            var billCat = new CategoryEntity
            {
                Name = "Bills",
                BudgetEntity = myBudget,
            };
            var officeCat = new CategoryEntity
            {
                Name = "Office Supplies",
                BudgetEntity = companyBudget,
            };
            context.BudgetCategories.AddRange(foodCat, billCat, officeCat);

            var entries = new[]
            {
                new EntryEntity {Amount = 65.0m, Description = "Hawaii Pizza", CategoryEntity = foodCat},
                new EntryEntity {Amount = 120.75m, Description = "Indkøb Netto", CategoryEntity = foodCat},
                new EntryEntity {Amount = 30.0m, Description = "Kebab", CategoryEntity = foodCat},
                new EntryEntity {Amount = 82.50m, Description = "Bland-selv slik", CategoryEntity = foodCat},

                new EntryEntity {Amount = 150.0m, Description = "Mobilregning", CategoryEntity = billCat},
                new EntryEntity {Amount = 200.0m, Description = "A-kasse", CategoryEntity = billCat},
                new EntryEntity {Amount = 300.0m, Description = "Afbetaling (narkogæld)", CategoryEntity = billCat},

                new EntryEntity {Amount = 150.0m, Description = "Post-its", CategoryEntity = officeCat},
                new EntryEntity {Amount = 531.5m, Description = "Personalemad", CategoryEntity = officeCat},
                new EntryEntity {Amount = 20.0m, Description = "Kuglepen", CategoryEntity = officeCat},
            };
            context.BudgetEntries.AddRange(entries);
            
            context.SaveChangesAsync().Wait();
        }
    }
}