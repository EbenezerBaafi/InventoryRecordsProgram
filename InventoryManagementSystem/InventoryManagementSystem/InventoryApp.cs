using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class InventoryApp
    {
        InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string logFilePath)
        {
            _logger = new InventoryLogger<InventoryItem>(logFilePath);
            Console.WriteLine("Inventory application initialized");
        }

        public void SeedSampleData()
        {
            // Create sample inventory items
            var item1 = new InventoryItem(1, "Laptop", 10, new DateTime(2025, 01, 22));
            var item2 = new InventoryItem(2, "Smartphone", 20, new DateTime(2025, 01, 23));
            var item3 = new InventoryItem(3, "Tablet", 15, new DateTime(2025, 01, 24));
            // Add items to the logger
            _logger.Add(item1);
            _logger.Add(item2);
            _logger.Add(item3);
            Console.WriteLine("Sample data seeded");
        }

        public void SaveData()
        {
            // Save the inventory log to file
            _logger.SaveToFile();
            Console.WriteLine("Data saved to file");
        }

        public void LoadData()
        {
            // Load the inventory log from file
            var items = _logger.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found in the log");
            }
            else
            {
                Console.WriteLine("Items loaded from log:");
                foreach (var item in items)
                {
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
                }
            }
        }

        public void PrintAllItems()
        {
            // Print each item from loaded data
            var items = _logger.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
            }

            if (items.Count == 0)
            {
                Console.WriteLine("No items to display");
            }
            else
            {
                Console.WriteLine("All items printed successfully");
            }

        }
    }
}
