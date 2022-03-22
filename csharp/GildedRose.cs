using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        public const string AGED_BRIE = "Aged Brie";
        public const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        public const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        public const string CONJURED = "Conjured Mana Cake";    // new feature
        public const int MAX_QUALITY = 50;
        public const int MIN_QUALITY = 0;

         IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public IList<Item> GetItems()
        {
            return Items;
        }

        /**/
        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                UpdateQualityItem(item);
            }
        }

        private void UpdateQualityItem(Item item)
        {
            bool isItemExpired = item.SellIn <= 0;
            bool isNotSpecialItem = (item.Name != AGED_BRIE) && (item.Name != BACKSTAGE_PASSES)
                                        && (item.Name != SULFURAS) && (item.Name != CONJURED);

            if (isNotSpecialItem)
            {
                int NormalItemDegradeRate = CalculateDegradeRate(item, isItemExpired);
                AdjustQuality(item, NormalItemDegradeRate);
            }
            if (item.Name == AGED_BRIE)
            {
                int adjustment = isItemExpired ? 2 : 1;
                AdjustQuality(item, adjustment);
            }
            if (item.Name == BACKSTAGE_PASSES)
            {
                UpdateQualityBackstagePasses(item, isItemExpired);
            }
            if (item.Name == CONJURED)
            {
                int ConjuredDegradeRate = 2 * CalculateDegradeRate(item, isItemExpired);
                AdjustQuality(item, ConjuredDegradeRate);
            }
            bool hasExpireDate = item.Name != SULFURAS;
            if (hasExpireDate)
            {
                item.SellIn = item.SellIn - 1;
            }
        }

        private void AdjustQuality(Item item, int adjustment)
        {
            int newQuality = item.Quality + adjustment;
            bool isQualityInRange = (newQuality <= 50) && (newQuality >= 0);
            if (isQualityInRange)
            {
                item.Quality = newQuality;
            }
        }

        private int CalculateDegradeRate(Item item, bool isExpired)
        {
            return isExpired ? -2 : -1;
        }

        private void UpdateQualityBackstagePasses(Item item, bool isExpired)
        {
            int adjustment;
            if (isExpired) 
            {
                adjustment = - item.Quality;
            }
            else
            {
                adjustment = item.SellIn < 6 ? 3 : item.SellIn < 11 ? 2 : 1;
            }
            AdjustQuality(item, adjustment);
        }
    }
}