using System;
using System.Collections.Generic;
using System.Xml;

namespace csharp
{
    public class GildedRose
    {
        public const int MAX_QUALITY = 50;

        public List<String> SPECIAL_ITEMS = new List<string>()
        {
            "Backstage passes to a TAFKAL80ETC concert", "Conjured Mana Cake", "Aged Brie", "Sulfuras, Hand of Ragnaros"
        };


        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
                MiseUnJourItem(item);

            #region Rose

            //for (var i = 0; i < Items.Count; i++)
            //{
            //    if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //    {
            //        if (Items[i].Quality > 0)
            //        {
            //            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //            {
            //                Items[i].Quality = Items[i].Quality - 1;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (Items[i].Quality < 50)
            //        {
            //            Items[i].Quality = Items[i].Quality + 1;

            //            if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            //            {
            //                if (Items[i].SellIn < 11)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }

            //                if (Items[i].SellIn < 6)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //    {
            //        Items[i].SellIn = Items[i].SellIn - 1;
            //    }

            //    if (Items[i].SellIn < 0)
            //    {
            //        if (Items[i].Name != "Aged Brie")
            //        {
            //            if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //            {
            //                if (Items[i].Quality > 0)
            //                {
            //                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //                    {
            //                        Items[i].Quality = Items[i].Quality - 1;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Items[i].Quality = Items[i].Quality - Items[i].Quality;
            //            }
            //        }
            //        else
            //        {
            //            if (Items[i].Quality < 50)
            //            {
            //                Items[i].Quality = Items[i].Quality + 1;
            //            }
            //        }
            //    }
            //}

            #endregion
        }


        private void MiseUnJourItem(Item item)
        {
            if (!SPECIAL_ITEMS.Contains(item.Name))
            {
                AjusterQuality(item);
            }

            bool backStageWithSellInAboveTen = item.SellIn > 10 && item.Name.Contains("Backstage passes");

            if (item.Name.Contains("Aged Brie") || backStageWithSellInAboveTen)
            {
                AjusterQuality(item, 1);
            }
            else if (item.Name.Contains("Backstage passes"))
                AjusterBackStage(item);

            else if (item.Name.Contains("Conjured"))
            {
                AjusterQuality(item, -2);
            }

            // Diminiuer les jours restant.
            if (!item.Name.Contains("Sulfuras"))
                item.SellIn--;
        }


        private void AjusterQuality(Item item, int ajustment = -1)
        {
            if (item.Name.Contains("Backstage passes") && item.SellIn <= 0)
            {
                item.Quality = 0;
                return;
            }

            if (item.SellIn > 0)
            {
                int newQuality = item.Quality + ajustment;

                if (ajustment > 0)
                    item.Quality = newQuality < GildedRose.MAX_QUALITY ? newQuality : GildedRose.MAX_QUALITY;
                else
                    item.Quality = newQuality > 0 ? newQuality : 0;
            }
            else
            {
                item.Quality -= 2;
            }
          
        }

        private void AjusterBackStage(Item item)
        {
            if (item.SellIn <= 5)
                AjusterQuality(item, 3);
            else if (item.SellIn <= 10)
                AjusterQuality(item, 2);
        }
    }
}