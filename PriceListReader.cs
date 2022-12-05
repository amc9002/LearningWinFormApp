﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWinFormsApp2
{
    class PriceListReader
    {
        public List<PriceItem> Read(List<List<string>> dataList)
        {
            List<PriceItem> priceItems = new List<PriceItem>();

            for(int i=0; i < dataList.Count; i++) 
            {
                if (int.TryParse(dataList[i][0], out int number))
                {
                    PriceItem priceItem = new PriceItem();

                    priceItem.Name = dataList[i][1];
                    priceItem.Id = dataList[i][2];

                    if(dataList[i][6] != string.Empty)
                        priceItem.Price = Convert.ToDecimal(dataList[i][6]);

                    priceItem.Stock = true;
                    var stock = dataList[i][4].ToLower();
                    if (stock == "нет в наличии" || stock == "транзит") priceItem.Stock = false;

                    priceItems.Add(priceItem);
                }
            }
            return priceItems;

        }
    }
}
