using System.Collections.Generic;
using System;
using csharp;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        private const int QUALITE_MAX = 50;
        private const string SULFRAS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE = "Backstage passes to a TAFKAL80ETC concert";
        private const string BRIE = "Aged Brie";


        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }


        public void UpdateQuality()
        {
            foreach(Item item in Items) 
            {
                UpdateQualiteItem(item);

                UpdateSellin(item);
            }
        }


        
        private void UpdateQualiteItem(Item item)
        {
            bool isSpecialItem = (item.Name != BRIE) && (item.Name != BACKSTAGE) && (item.Name != SULFRAS);

            if (isSpecialItem  && (item.Quality > 0))
            {
                UpdateQualiteIItemNormal(item);
            }

            else
            {
                UpdateQualiteIItemSpecial(item);
            }
        }

        public void UpdateQualiteIItemNormal(Item item)
        {
            item.Quality--;
            if(item.Name == "Conjured")
            { 
                item.Quality--; 
            }
        }


        public void UpdateQualiteIItemSpecial(Item item)
        {
            if (item.Quality < QUALITE_MAX && item.Name != SULFRAS)
            {
                item.Quality++;

                if (item.Name == BACKSTAGE)
                {
                    UpdateBackstageValue(item);
                }
            }
        }


        public void UpdateBackstageValue(Item item)
        {
            if (item.SellIn < 11 && item.Quality < QUALITE_MAX)
            {
                item.Quality++;
            }

            if (item.SellIn < 6 && item.Quality < QUALITE_MAX)
            {
                item.Quality++;
            }
        }


        public void UpdateSellin(Item item)
        {

            if (item.Name != SULFRAS && item.SellIn > 0)
            {
                item.SellIn--;
            }

            if (item.SellIn < 0)
            {
                HandleExpiredItem(item);
            }
        }

        public void HandleExpiredItem(Item item)
        {
            if (item.Name != BRIE && item.Name != BACKSTAGE)
            {
                if (item.Quality > 0 && item.Name != SULFRAS)
                {
                    item.Quality--;
                }
                else
                {
                    item.Quality = 0;
                }
            }
            else if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }
}
