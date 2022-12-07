using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWinFormsApp2
{
    class PriceListReader
    {
        private const int COL_NAME = 1;
        private const int COL_ID = 2;
        private const int COL_STOCK = 4;
        private const int COL_PRICE = 6;
        private static readonly string[] NOT_IN_STOCK = {"нет в наличии", "транзит" };

        public static List<PriceItem> Read(List<List<string>> dataList)
        {
            List<PriceItem> priceItems = new();

            for (int i = 0; i < dataList.Count; i++)
            {
                if (int.TryParse(dataList[i][0], out int _))
                {
                    var priceItem = new PriceItem
                    {
                        NumberStringInitial = i,
                        Name = dataList[i][COL_NAME],
                        Id = dataList[i][COL_ID],
                        Stock = true
                    };

                    if (dataList[i][COL_PRICE] != string.Empty) priceItem.Price = Convert.ToDecimal(dataList[i][COL_PRICE]);

                    var stock = dataList[i][COL_STOCK].ToLower();
                    if (NOT_IN_STOCK.Any(s => s == stock)) 
                        priceItem.Stock = false;

                    priceItems.Add(priceItem);
                }
            }
            return priceItems;
        }
    }
}
