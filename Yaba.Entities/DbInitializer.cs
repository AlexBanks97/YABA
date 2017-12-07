using System.Linq;
using Yaba.Entities.Budget;
using Yaba.Entities.Tab;

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

			var myBudget = new Budget.BudgetEntity { Name = "My Budget" };
			var companyBudget = new Budget.BudgetEntity { Name = "Company Budget" };
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

			var tabs = new[]
			{
				new Tab.TabEntity()
				{

				},
				new Tab.TabEntity()
				{

				}

			};

			context.Tabs.AddRange(tabs);

			var tabItems = new[]
			{
				new ItemEntity()
				{
					Amount = 200,
					Description = "For food and drinks",
					TabEntity = tabs[0],
				},

				new ItemEntity()
				{
					Amount = 100,
					Description = "Entrance",
					TabEntity = tabs[1],
				},

				new ItemEntity()
				{
					Amount = 500,
					Description = "Grocieries",
					TabEntity = tabs[1],
				}

			};

			context.TabItems.AddRange(tabItems);

			context.SaveChangesAsync().Wait();
		}

	}
}
