using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    /// <summary>
    /// The item manager is responsible for updating the quality and sellin values
    /// for all items.
    /// </summary>
    public class ItemManager
    {
        private IList<Item> items;
        private Dictionary<Item, int> itemsToDecrease;

        public ItemManager()
        {
            InitializeItems();
            InitializeDictionary();
        }

        /// <summary>
        /// Initializes a dictionary that contains items with a
        /// decrease factor. The decrease factor corresponds to
        /// the amount of times we should decrease the value of
        /// an item after each sellin.
        /// </summary>
        private void InitializeDictionary()
        {
            itemsToDecrease = new Dictionary<Item, int>()
            {
                { items[0], 1 },
                { items[2], 1 },
                { items[5], 2 }
            };
        }

        /// <summary>
        /// Initializing all items in the item manager.
        /// </summary>
        public void InitializeItems()
        {
            items = new List<Item>

                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          };
        }

        /// <summary>
        /// Returns a list of items.
        /// </summary>
        /// <returns>A list of items</returns>
        public IList<Item> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Updates the quality of an item. Depending on the item type,
        /// the item's quality and decrease or increase.
        /// All items except Backstage passes and Aged Brie decreases
        /// by 2 once the SellIn is less than zero.
        /// </summary>
        public void UpdateQuality()
        {

            foreach (var item in items)
            {
                // Decrease an item quality if it is not aged brie or Backstage passses.
                if (IsItemToDecrease(item))
                {
                    DecreaseItemQuality(item);
                }
                else
                {
                    IncreaseItemValue(item);
                }

                DecrementItemSellIn(item);

                // In this case aged brie increases in quality but all other items do not.
                if (IsItemBelowSellIn(item))
                {
                    UpdateItemQualityAfterSellIn(item);
                }
            }
        }

        /// <summary>
        /// Increase item value for an item. For a Backstage passes item
        /// the increase factor gets larger when approaching zero for the
        /// value of SellIn.
        /// </summary>
        /// <param name="item"></param>
        private void IncreaseItemValue(Item item)
        {
            IncrementItemQuality(item);

            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.SellIn < 11)
                {
                    IncrementItemQuality(item);
                }

                if (item.SellIn < 6)
                {
                    IncrementItemQuality(item);
                }
            }
        }

        /// <summary>
        /// Checks if the item is a type of item that can be decreased.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsItemToDecrease(Item item)
        {
            return itemsToDecrease.ContainsKey(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void DecreaseItemQuality(Item item)
        {
            int decreaseFactor = 1;
            itemsToDecrease.TryGetValue(item, out decreaseFactor);

            for (int i = 0; i < decreaseFactor; i++)
            {
                DecrementItemQuality(item);
            }
        }

        /// <summary>
        /// Increases or decreases an item faster than before
        /// because the SellIn value is less than zero.
        /// </summary>
        /// <param name="item"></param>
        private void UpdateItemQualityAfterSellIn(Item item)
        {
            if (item.SellIn < 0)
            {
                switch (item.Name)
                {
                    case "Aged Brie":
                        IncrementItemQuality(item);
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        ClearItemQuality(item);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    default:
                        DecrementItemQuality(item);
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if an item's SellIn value is below zero.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if an item's SellIn value is below zero.</returns>
        private bool IsItemBelowSellIn(Item item)
        {
            return (item.SellIn < 0 ? true : false);
        }

        /// <summary>
        /// Sets the item quality to zero.
        /// </summary>
        /// <param name="item">The item to clear.</param>
        private void ClearItemQuality(Item item)
        {
            item.Quality = 0;
        }

        /// <summary>
        /// Decrements the quality of an item by one.
        /// </summary>
        /// <param name="item">The item to decrement..</param>
        private void DecrementItemQuality(Item item)
        {
            if (item.Quality > 0)
            {
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.Quality -= 1;
                }
            }
        }

        /// <summary>
        /// Increments the item quality.
        /// </summary>
        /// <param name="item">The item to increment.</param>
        private void IncrementItemQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        /// <summary>
        /// Decrements the SellIn value of an item.
        /// </summary>
        /// <param name="item">The item to decrement.</param>
        private void DecrementItemSellIn(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
            }
        }
    }
}
