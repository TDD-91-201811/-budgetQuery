using System;
using System.Linq;
using BudgetQueryService.Interfaces;

namespace BudgetQueryService
{
    public class BudgetService
    {
        private IBudgetRepository _budgetRepository;
        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public double TotalAmount(DateTime start, DateTime end)
        {
            if (end < start)
                return 0;

            var resultAmount = 0;


            if (IsSameYearMonth(start, end))
            {
                resultAmount = GetDailyAmount(start.Year, start.Month) * GetCalcDays(start.Date, end.Date);
            }
            else
            {
                var monthes = end.Subtract(start).Days / (365.25 / 12);

                

                if (monthes > 1)
                {
                    for (var i = 0; i < monthes; i++)
                    {
                        var date = start.AddMonths(i);
                        if (!IsSameYearMonth(date, end))
                        {
                            resultAmount += GetDailyAmount(date.Year, date.Month) *
                                            GetCalcDays(date.Date, GetEndOfMonth(date));
                        }
                        else
                        {
                            resultAmount += GetDailyAmount(date.Year, date.Month) *
                                            GetCalcDays(date.Date, end.Date);
                        }
                    }
                }
                else
                {
                    resultAmount += GetDailyAmount(start.Year, start.Month) * GetCalcDays(start.Date, GetEndOfMonth(start));
                    resultAmount += GetDailyAmount(end.Year, end.Month) * GetCalcDays(end.Date, end.Date);
                }
            }
            return resultAmount;
        }

        private DateTime GetEndOfMonth(DateTime date)
        {
            var end = new DateTime(date.Year, date.Month + 1, 1);

            return end.AddDays(-1);
        }

        private string GetYearMonth(int year, int month)
        {
            return $"{year}{month.ToString().PadLeft(2, '0')}";
        }

        private int GetMonthAmount(string yearMonth)
        {
            var totalAmount = _budgetRepository.GetAll();
            return totalAmount.Where(x => x.YearMonth == yearMonth).SingleOrDefault().Amount;
        }

        private int GetDailyAmount(int year, int month)
        {
            var totalAmount = GetMonthAmount(GetYearMonth(year, month));
            return totalAmount / DateTime.DaysInMonth(year, month);
        }

        private bool IsSameYearMonth(DateTime start, DateTime end)
        {
            var startMonth = GetYearMonth(start.Year, start.Month);
            var endMonth = GetYearMonth(end.Year, end.Month);

            return startMonth.Equals(endMonth);
        }

        private int GetCalcDays(DateTime startDate, DateTime endDate)
        {
            return ((endDate - startDate).Days + 1);
        }

    }
}
