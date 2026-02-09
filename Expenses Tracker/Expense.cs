using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker
{
    internal class Expense
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }

        // Метод для збереження у форматі рядка: "Опис|Сума"
        public override string ToString()
        {
            return $"{Description}|{Amount}";
        }
    }
}