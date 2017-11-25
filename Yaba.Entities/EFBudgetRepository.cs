using System;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Entities
{
    public class EFBudgetRepository : IBudgetRepository
    {
        private readonly IYabaDBContext _context;

        public EFBudgetRepository(IYabaDBContext context)
        {
            _context = context;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<BudgetDTO> FindBudget(Guid id)
        {
            var budget = _context.Budgets.FirstOrDefault(b => b.Id == id);
            if (budget == null) return null;
            return new BudgetDTO
            {
                Name = budget.Name,
            };
        }
    }
}
