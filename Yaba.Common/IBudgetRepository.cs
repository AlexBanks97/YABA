using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Common
{
    public interface IBudgetRepository : IDisposable
    {
        Task<BudgetDTO> FindBudget(int id);
    }
}
