using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void UpdateQuality_WithItemName_NameDoesNotChange()
        {
            // Arrange
            string expectedName = "foo";
            IList<Item> expectedItems = new List<Item> { new Item { Name = expectedName, SellIn = 0, Quality = 0 } }; 
            GildedRose app = new GildedRose(expectedItems);
            
            // Apply
            app.UpdateQuality();
            IList<Item> appItems = app.GetItems();

            // Assert
            Assert.AreEqual(expectedName, appItems[0].Name);
        }
    }
}
