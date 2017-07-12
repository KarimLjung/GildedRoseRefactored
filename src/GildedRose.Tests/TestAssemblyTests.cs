using GildedRose.Console;
using System.Collections.Generic;
using Xunit;
using System;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        private ItemManager itemManager;
        private IList<Item> items;

        [Fact]
        public void BackStageQualityAtZeroSellInTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(15);
            Then_Backstage_Passes_Quality_Is_Zero();
        }

         [Fact]
        public void ConjuringItemQualityZeroTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(3);
            Then_Conjuring_Items_Are_Zero();
        }

        [Fact]
        public void ConjuringItemQualityFourTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(1);
            Then_Conjuring_Items_Are_Four();
        }

        private void Then_Conjuring_Items_Are_Four()
        {
            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(2, items[5].SellIn);
            Assert.Equal(4, items[5].Quality);
        }

        private void Then_Conjuring_Items_Are_Zero()
        {
            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(0, items[5].SellIn);
            Assert.Equal(0, items[5].Quality);
        }

        private void When_Items_Are_Updated_N_Times(int n)
        {
            for (int i = 0; i < n; i++)
            {
                itemManager.UpdateQuality();
            }
        }

        [Fact]
        public void BackStageQualityAboveTenSellInTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(5);
            Then_Backstage_Passes_Quality_Is_25();
        }

        private void Then_Backstage_Passes_Quality_Is_25()
        {
            Assert.Equal(25, items[4].Quality);
        }

        [Fact]
        public void BackStageQualityBelowTenSellInTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(6);
            Then_Backstage_Passes_Quality_Is_27();
        }

        private void Then_Backstage_Passes_Quality_Is_27()
        {
            Assert.Equal(27, items[4].Quality);
        }

        [Fact]
        public void BackStageQualityBelowFiveSellInTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(11);
            Then_Backstage_Passes_Quality_Is_38();
        }

        private void Then_Backstage_Passes_Quality_Is_38()
        {
            Assert.Equal(38, items[4].Quality);
        }


        private void Then_Backstage_Passes_Quality_Is_Zero()
        {
            Assert.Equal(0, items[5].Quality);
        }

        [Fact]
        public void UpdateQualityOnceTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            Then_Items_Have_Valid_Initial_Values();

            When_Items_Are_Updated_N_Times(1);
            Then_Items_Are_Valid_After_One_Update();
        }

        [Fact]
        public void UpdateQualityTwentyTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            Then_Items_Have_Valid_Initial_Values();

            When_Items_Are_Updated_N_Times(20);
            Then_Items_Are_Valid_After_Twenty_Updates();
        }

        private void Then_Items_Are_Valid_After_Twenty_Updates()
        {
            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-10, items[0].SellIn);

            Assert.Equal("Aged Brie", items[1].Name);
            Assert.Equal(38, items[1].Quality);
            Assert.Equal(-18, items[1].SellIn);

            Assert.Equal("Elixir of the Mongoose", items[2].Name);
            Assert.Equal(0, items[2].Quality);
            Assert.Equal(-15, items[2].SellIn);

            Assert.Equal("Sulfuras, Hand of Ragnaros", items[3].Name);
            Assert.Equal(80, items[3].Quality);
            Assert.Equal(0, items[3].SellIn);

            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[4].Name);
            Assert.Equal(0, items[4].Quality);
            Assert.Equal(-5, items[4].SellIn);

            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(0, items[5].Quality);
            Assert.Equal(-17, items[5].SellIn);
        }

        [Fact]
        public void QualityDegradeTwiceAsFastTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(6);
            Then_Item_Elixir_Quality_Is_Zero();
        }

        [Fact]
        public void QualityBelowZeroTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(21);
            Then_All_Items_Qualities_Are_Non_Negative();
        }

        [Fact]
        public void AgedBrieQualityMaxTest()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(30);
            Then_Aged_Brie_Is_Max_Quality_50();
        }

        [Fact]
        public void Test_BackStage_Passes_Are_Max_Quality_50()
        {
            Given_A_Clone_Of_ItemManager();
            Given_A_List_Of_Items();
            When_Items_Are_Updated_N_Times(15);
            Then_Backstage_Passes_Quality_Is_50();

        }

        private void Then_Backstage_Passes_Quality_Is_50()
        {
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[4].Name);
            Assert.Equal(50, items[4].Quality);
            Assert.Equal(0, items[4].SellIn);
        }

        private void Then_Aged_Brie_Is_Max_Quality_50()
        {
            //Initial condition for item +5 Aged Brie.
            Assert.Equal("Aged Brie", items[1].Name);
            Assert.Equal(50, items[1].Quality);
            Assert.Equal(-28, items[1].SellIn);
        }

        private void Then_All_Items_Qualities_Are_Non_Negative()
        {
            //Initial condition for item +5 Dexterity Vest.
            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-11, items[0].SellIn);

            //Initial condition for item +5 Aged Brie.
            Assert.Equal("Aged Brie", items[1].Name);
            Assert.Equal(40, items[1].Quality);
            Assert.Equal(-19, items[1].SellIn);

            ////Initial condition for item Elixir of the Mongoose.
            Assert.Equal("Elixir of the Mongoose", items[2].Name);
            Assert.Equal(0, items[2].Quality);
            Assert.Equal(-16, items[2].SellIn);

            ////Initial condition for item Sulfuras, Hand of Ragnaros.
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[3].Name);
            Assert.Equal(80, items[3].Quality);
            Assert.Equal(0, items[3].SellIn);

            ////Initial condition for item Backstage passes to a TAFKAL80ETC concert.
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[4].Name);
            Assert.Equal(0, items[4].Quality);
            Assert.Equal(-6, items[4].SellIn);

            ////Initial condition for item Conjured Mana Cake.
            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(0, items[5].Quality);
            Assert.Equal(-18, items[5].SellIn);
        }

        private void Then_Item_Elixir_Quality_Is_Zero()
        {
            Assert.Equal(0, items[2].Quality);
            Assert.Equal(-1, items[2].SellIn);
        }

        private void Then_Items_Are_Valid_After_One_Update()
        {
            // After update.

            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(19, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);

            Assert.Equal("Aged Brie", items[1].Name);
            Assert.Equal(1, items[1].Quality);
            Assert.Equal(1, items[1].SellIn);

            Assert.Equal("Elixir of the Mongoose", items[2].Name);
            Assert.Equal(6, items[2].Quality);
            Assert.Equal(4, items[2].SellIn);

            Assert.Equal("Sulfuras, Hand of Ragnaros", items[3].Name);
            Assert.Equal(80, items[3].Quality);
            Assert.Equal(0, items[3].SellIn);

            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[4].Name);
            Assert.Equal(21, items[4].Quality);
            Assert.Equal(14, items[4].SellIn);

            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(4, items[5].Quality);
            Assert.Equal(2, items[5].SellIn);
        }

        private void Then_Items_Have_Valid_Initial_Values()
        {
            //Initial condition for item +5 Dexterity Vest.
            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(20, items[0].Quality);
            Assert.Equal(10, items[0].SellIn);

            //Initial condition for item +5 Aged Brie.
            Assert.Equal("Aged Brie", items[1].Name);
            Assert.Equal(0, items[1].Quality);
            Assert.Equal(2, items[1].SellIn);

            //Initial condition for item Elixir of the Mongoose.
            Assert.Equal("Elixir of the Mongoose", items[2].Name);
            Assert.Equal(7, items[2].Quality);
            Assert.Equal(5, items[2].SellIn);

            //Initial condition for item Sulfuras, Hand of Ragnaros.
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[3].Name);
            Assert.Equal(80, items[3].Quality);
            Assert.Equal(0, items[3].SellIn);

            //Initial condition for item Sulfuras, Hand of Ragnaros.
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[4].Name);
            Assert.Equal(20, items[4].Quality);
            Assert.Equal(15, items[4].SellIn);

            //Initial condition for item Conjured Mana Cake.
            Assert.Equal("Conjured Mana Cake", items[5].Name);
            Assert.Equal(6, items[5].Quality);
            Assert.Equal(3, items[5].SellIn);
        }

        private void Given_A_Clone_Of_ItemManager()
        {
            itemManager = new ItemManager();
        }

        private void Given_A_List_Of_Items()
        {
            items = itemManager.GetItems();
        }
    }
}