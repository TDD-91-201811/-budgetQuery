using BudgetQueryService.Entities;
using System.Collections.Generic;

namespace BudgetQueryService.Interfaces
{
    public interface IBudgetRepository
    {
        List<Budget> GetAll();
    }
}
