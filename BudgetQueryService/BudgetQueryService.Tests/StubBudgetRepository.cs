using System;
using System.Collections.Generic;
using BudgetQueryService.Entities;
using BudgetQueryService.Interfaces;

namespace BudgetQueryService.Tests
{
    public class StubBudgetRepository : IBudgetRepository
    {
        public List<Budget> GetAll()
        {
            return new List<Budget>()
            {
                new Budget()
                {
                    YearMonth = "201801",
                    Amount =  310
                },
                new Budget()
                {
                    YearMonth = "201802",
                    Amount = 560
                },
                new Budget()
                {
                    YearMonth = "201803",
                    Amount = 465
                }
            };
        }
    }
}