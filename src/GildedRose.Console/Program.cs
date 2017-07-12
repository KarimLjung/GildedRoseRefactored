using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var itemManager = new ItemManager();

            itemManager.UpdateQuality();

            System.Console.ReadKey();

        }



    }

}
