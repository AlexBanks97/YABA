using System;
using System.Linq;
using Yaba.Common;
using Yaba.Common.User;
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
			context.SaveChangesAsync().Wait();

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
			context.SaveChangesAsync().Wait();


			var recurrings = new[]
            {
                new RecurringEntity{Name = "Paycheck", Amount = 8000, Recurrence = Common.Recurrence.Monthly, BudgetEntity = myBudget},
                new RecurringEntity{Name = "Netflix", Amount = -89, Recurrence = Common.Recurrence.Monthly, BudgetEntity = myBudget},
                new RecurringEntity{Name = "Spotify", Amount = -50, Recurrence = Common.Recurrence.Monthly, BudgetEntity = myBudget},

                new RecurringEntity{Name = "SpotiftyOffice", Amount = 89, Recurrence = Common.Recurrence.Monthly, BudgetEntity = companyBudget},
                new RecurringEntity{Name = "Rent", Amount = -7000, Recurrence = Common.Recurrence.Monthly, BudgetEntity = companyBudget}
            };

            context.BudgetRecurrings.AddRange(recurrings);
			context.SaveChangesAsync().Wait();

			var goals = new[]
            {
                new GoalEntity {Amount = 3000, Recurrence = Common.Recurrence.Monthly, CategoryEntity = foodCat},
                new GoalEntity {Amount = 500, Recurrence = Common.Recurrence.Monthly, CategoryEntity = billCat},
                new GoalEntity {Amount = 1000, Recurrence = Common.Recurrence.Monthly, CategoryEntity = officeCat},

            };

            context.BudgetGoals.AddRange(goals);
			context.SaveChangesAsync().Wait();

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

			var phil = new UserEntity
			{
				FacebookId = Guid.NewGuid().ToString(),
				Name = "Dr. Phil"
			};
			var myQueen = new UserEntity
			{
				FacebookId = Guid.NewGuid().ToString(),
				Name = "Lady Sylvanas"
			};

			context.Users.AddRange(phil, myQueen);
			context.SaveChangesAsync().Wait();

			var tabs = new[]
			{
				new Tab.TabEntity()
				{
					UserOne = phil,
					UserTwo = myQueen,
					State = State.Active,
				},

			};

			context.Tabs.AddRange(tabs);
			context.SaveChangesAsync().Wait();

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
					TabEntity = tabs[0],
				},

				new ItemEntity()
				{
					Amount = 500,
					Description = "Grocieries",
					TabEntity = tabs[0],
				}

			};

			context.TabItems.AddRange(tabItems);

			context.SaveChangesAsync().Wait();
		}

        public static void InitializeTestData(IYabaDBContext context)
        {
            if (context.Budgets.Any() || context.Tabs.Any())
            {
                return;
            }

            var tabs = new[]
            {
                new Tab.TabEntity()
                {

                }

            };
			context.SaveChangesAsync().Wait();

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
                    TabEntity = tabs[0],
                },

                new ItemEntity()
                {
                    Amount = 500,
                    Description = "Grocieries",
                    TabEntity = tabs[0],
                }

            };

            context.TabItems.AddRange(tabItems);

            context.SaveChangesAsync().Wait();
        }

	}
}
