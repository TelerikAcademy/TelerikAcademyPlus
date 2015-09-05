using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceConditionSalesSumWithLocking
{
    struct SoldItem
    {
        public int PriceCents { get; private set; }
        public string Name { get; private set; }

        public SoldItem(int priceCents, string name)
        {
            this.PriceCents = priceCents;
            this.Name = name;
        }
    }

    struct DaySales
    {
        public int TotalCents { get; private set; }
        public Dictionary<string, int> SoldItemCounts { get; private set; }

        public DaySales(int totalCents, Dictionary<string, int> soldItemCounts)
        {
            this.TotalCents = totalCents;
            this.SoldItemCounts = soldItemCounts;
        }

        public override string ToString()
        {
            StringBuilder soldItemCountsBuilder = new StringBuilder();

            foreach (var entry in this.SoldItemCounts)
            {
                soldItemCountsBuilder.Append(entry.Key + ": " + entry.Value + System.Environment.NewLine);
            }

            //Note: This should really be a formatted string
            return "Total Sales Sum: " + (TotalCents / 100.0) + "$" + System.Environment.NewLine +
                "Sales count by item:" + System.Environment.NewLine +
                soldItemCountsBuilder.ToString();
        }
    }

    class RaceConditionsSalesSumWithLocking
    {
        static DaySales AggregateDaySalesParallel(List<SoldItem> allSales)
        {
            object aggregationLock = new object();
            Dictionary<string, int> soldItemCounts = new Dictionary<string, int>();
            int totalCents = 0;

            Parallel.ForEach(allSales, (soldItem) =>
            {
                //Note: we could also lock on soldItemCounts, as it is a reference type. However, that binds our locking to our implementation details - 
                //i.e. what happens if we want to compute the counts by item name in a different data structure, or without a data structure at all?
                lock (aggregationLock)
                {
                    IncludeItemInSoldCounts(soldItem, soldItemCounts);
                    totalCents += soldItem.PriceCents;
                }
            });

            return new DaySales(totalCents, soldItemCounts);
        }

        static DaySales AggregateDaySales(List<SoldItem> allSales)
        {
            Dictionary<string, int> soldItemCounts = new Dictionary<string, int>();
            int totalCents = 0;
            foreach (SoldItem soldItem in allSales)
            {
                IncludeItemInSoldCounts(soldItem, soldItemCounts);
                totalCents += soldItem.PriceCents;
            }

            return new DaySales(totalCents, soldItemCounts);
        }

        private static void IncludeItemInSoldCounts(SoldItem soldItem, Dictionary<string, int> soldItemCounts)
        {
            if (!soldItemCounts.ContainsKey(soldItem.Name))
            {
                soldItemCounts[soldItem.Name] = 1;
            }
            else
            {
                soldItemCounts[soldItem.Name]++;
            }
        }

        static List<SoldItem> GenerateSoldItemsData()
        {
            List<SoldItem> soldItems = new List<SoldItem>();

            //The more items we have, the more often race conditions will arise and the larger the error will be
            for (int i = 0; i < 100; i++)
            {
                soldItems.Add(new SoldItem(100, "Coke"));
                soldItems.Add(new SoldItem(200, "Beer"));
                soldItems.Add(new SoldItem(300, "Beer Nuts"));
                soldItems.Add(new SoldItem(400, "Deer Nuts"));
                soldItems.Add(new SoldItem(500, "HealthyNot Chips"));

                soldItems.Add(new SoldItem(600, "Aisis Pork"));
                soldItems.Add(new SoldItem(700, "Pengiun Stake"));
                soldItems.Add(new SoldItem(800, "Ustillfat Diet Coke"));
                soldItems.Add(new SoldItem(900, "ClimateChangeHoax Aerosol"));
                soldItems.Add(new SoldItem(1000, "McDon'talds Iscream"));
            }

            return soldItems;
        }

        static void Main(string[] args)
        {
            var soldItemsForADay = GenerateSoldItemsData();

            Console.WriteLine();
            Console.WriteLine("--DAY-SALES-SEQUENTIAL-----------------------------------");
            Console.WriteLine();

            var daySalesSequential = AggregateDaySales(soldItemsForADay);
            Console.WriteLine(daySalesSequential);

            Console.WriteLine();
            Console.WriteLine("---DAY-SALES-PARALLEL------------------------------------");
            Console.WriteLine();

            var daySalesParallel = AggregateDaySalesParallel(soldItemsForADay);
            Console.WriteLine(daySalesParallel);
        }
    }
}
