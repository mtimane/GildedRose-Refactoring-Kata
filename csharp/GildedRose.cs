using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        private const string AGEDBRIE = "Aged Brie"; // AGED_BRIE ??
        private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        private const string TAFKAL80ETC = "Backstage passes to a TAFKAL80ETC concert";
        private IList<Item> Items;

        public GildedRose(IList<Item> Items) => this.Items = Items;

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                bool isNotAgedbrie = item.Name != AGEDBRIE; // Very very great
                bool isNotSulfuras = item.Name != SULFURAS; // GOOD good

                if (isNotAgedbrie)
                {
                    QualityLessThan50(item);
                }
                SulfurasQualityCase(item);

                if (isNotSulfuras)
                {
                    item.SellIn -= 1; // Ajouter une fonction qui update la qualité
                }

                if (item.SellIn < 0)
                {
                    if (isNotAgedbrie)
                    {
                        if (!SulfurasQualityCase(item))
                        {
                            item.Quality = 0; // Ajouter une fonction qui update la qualité
                        }
                    }
                    else
                    {
                        QualityLessThan50(item);
                    }
                }
            }
        }

        private void QualityLessThan50(Item pItem)
        {
            if (pItem.Quality < 50)
            {
                Items[Items.IndexOf(pItem)].Quality += 1; // Ajouter une fonction qui update la qualité
            }
        }

        private bool SulfurasQualityCase(Item pItem)
        {
            bool isNotTafAndQMoreThanZero = pItem.Name != TAFKAL80ETC && pItem.Quality > 0;
            if (isNotTafAndQMoreThanZero)
            {
                if (pItem.Name != SULFURAS)
                {
                    Items[Items.IndexOf(pItem)].Quality -= 1; // Ajouter une fonction qui update la qualité
                }
            }
            return isNotTafAndQMoreThanZero;
        }
    }
}