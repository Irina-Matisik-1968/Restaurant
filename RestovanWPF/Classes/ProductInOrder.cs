using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestovanWPF.Classes
{
    /// <summary>
    /// Позиция блюда в заказе 
    /// </summary>
    public class ProductInOrder
    {
        public string ProductName { get; set; }     //Название выбранного блюда
        public double ProductCost { get; set; }     //Его стоимость
        public double ProductCount { get; set; }    //Кол-во в заказе
        public double ProductTotalCost { get; set; }    //Итоговая его стоимость
    }
}
