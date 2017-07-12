using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class ItemManager
    {
        IList<Item> Items;

        public ItemManager()
        {
            InitializeItems();
        }

        public void InitializeItems()
        {
            Items = new List<Item>
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
            return Items;
        }
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                // Decrease an item quality if it is not aged brie or Backstage passses.
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    DecreaseItemQuality(Items[i]);
                }
                else
                {
                    IncreaseItemQuality(Items[i]);

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 11)
                        {
                            IncreaseItemQuality(Items[i]);
                        }

                        if (Items[i].SellIn < 6)
                        {
                            IncreaseItemQuality(Items[i]);
                        }
                    }
                }
                UpdateSellInForItem(Items[i]);

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        IncreaseItemQuality(Items[i]);
                    }
                }
            }
        }

        private void DecreaseItemQuality(Item item)
        {
            if (item.Quality > 0)
            {
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.Quality -= 1;
                }
            }
        }

        private void IncreaseItemQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        private void UpdateSellInForItem(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
            }
        }
    }
}
