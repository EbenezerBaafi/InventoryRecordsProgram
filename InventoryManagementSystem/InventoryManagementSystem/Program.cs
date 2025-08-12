
namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the inventory application with a log file path
            var app = new InventoryApp("inventory_log.json");
            // Seed sample data
            app.SeedSampleData();
            // Save the data to file
            app.SaveData();
            // Load the data from file
            app.LoadData();
            // Print all items from loaded data
            app.PrintAllItems();
        }
    }
}