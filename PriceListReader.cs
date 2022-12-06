using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWinFormsApp2
{
    class PriceListReader
    {
        public static List<PriceItem> Read(List<List<string>> dataList)
        {
            List<PriceItem> priceItems = new();

            foreach (var d in dataList)
            {
                if (int.TryParse(d[0], out int number))
                {
                    var priceItem = new PriceItem
                    {
                        Name = d[1],
                        Id = d[2],
                        Stock = true
                    };

                    if (d[6] != string.Empty) priceItem.Price = Convert.ToDecimal(d[6]);

                    var stock = d[4].ToLower();
                    if (stock == "нет в наличии" || stock == "транзит") priceItem.Stock = false;

                    priceItems.Add(priceItem);
                }
            }
            return priceItems;

        }
    }
}
