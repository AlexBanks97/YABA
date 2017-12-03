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

			var tabs = new[]
			{
				new TabEntitites.Tab()
				{

				},
				new TabEntitites.Tab()
				{

				}

			};

			context.Tabs.AddRange(tabs);

			var TabItemCategories = new[]
			{
				new TabEntitites.TabItemCategory
				{
					Name = "Food"
				},
				new TabEntitites.TabItemCategory
				{
					Name = "Transportaion"
				},
				new TabEntitites.TabItemCategory
				{
					Name = "Nightout"
				}
			};

			context.TabItemCategories.AddRange(TabItemCategories);

			var tabItems = new[]
			{
				new TabEntitites.TabItem()
				{
					Amount = 200,
					Description = "For food and drinks",
					Tab = tabs[0],
					Category = TabItemCategories[2]
				},

				new TabEntitites.TabItem()
				{
					Amount = 100,
					Description = "Entrance",
					Tab = tabs[1],
					Category = TabItemCategories[2]
				},

				new TabEntitites.TabItem()
				{
					Amount = 500,
					Description = "Grocieries",
					Tab = tabs[1],
					Category = TabItemCategories[0]
				}

			};

			context.TabItems.AddRange(tabItems);

			context.SaveChangesAsync().Wait();
		}

	}
}
