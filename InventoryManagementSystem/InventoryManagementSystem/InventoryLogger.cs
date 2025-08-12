using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log;
        private string _filePath;

        // constructor to initialize the logger with a file path
        public InventoryLogger(string filePath)
        {
            _log = new List<T>();
            _filePath = filePath;
            Console.WriteLine("Inventory logger initialized");
        }

        public void Add(T item)
        {
            if (item != null)
            {
                _log.Add(item);
                Console.WriteLine($"Item added: {item.Id}");
            }

            else
            {
                Console.WriteLine("Cannot add null item to the log.");
            }
        }

        public  List<T> GetAll()
        {
            return _log;
        }

        public void SaveToFile()
        {
            try
            {
                // check if there are items  to save
                if (_log.Count == 0)
                {
                    Console.WriteLine("No items to save");
                    return;
                }

                // convert _log to json string
                string jsonString = JsonSerializer.Serialize(_log, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write json string to file
                File.WriteAllText(_filePath, jsonString);

                Console.WriteLine($"Successfully saved {_log.Count} to {_filePath}");

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission denied: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error converting to Json: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occured: {ex.Message}");
            }

        }



    }
}
