using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class ItemManager
    {
        private IList<Item> items;

        public ItemManager()
        {
            InitializeItems();
        }

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

        public IList<Item> GetItems()
        {
            return items;
        }
        public void UpdateQuality()
        {

            foreach (var item in items)
            {
                // Decrease an item quality if it is not aged brie or Backstage passses.
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    DecrementItemQuality(item);
                }
                else
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
                DecrementSellInForItem(item);

                // In this case aged brie increases in quality but all other items do not.
                if (IsItemBelowSellIn(item))
                {
                    UpdateItemAfterSellIn(item);
                }
            }
        }

        private void UpdateItemAfterSellIn(Item item)
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

        private bool IsItemBelowSellIn(Item item)
        {
            return (item.SellIn < 0 ? true : false);
        }

        private void ClearItemQuality(Item item)
        {
            item.Quality = 0;
        }

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

        private void IncrementItemQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        private void DecrementSellInForItem(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
            }
        }
    }
}
