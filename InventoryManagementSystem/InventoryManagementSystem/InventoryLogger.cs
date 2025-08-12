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

        public void LoadFrmoFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"File not found: {_filePath}, Starting with empty log.");
                    _log = new List<T>();
                    return;
                }
                // Read json string from file
                string fileContent = "";

                // Read the file using StreamReader with using statement
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    fileContent = sr.ReadToEnd();
                }

                // Check if file is empty
                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    Console.WriteLine("File is empty, starting with empty log.");
                    _log = new List<T>();
                    return;
                }
                // Deserialize json string to list of T
                List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(fileContent);

                // Handle null case
                if (loadedItems == null)
                {
                    Console.WriteLine("Failed to load data, starting with empty log.");
                    _log = new List<T>();
                    return;
                }
                // Assign loaded items to _log
                _log = loadedItems;
                Console.WriteLine($"Successfully loaded {_log.Count} items from {_filePath}");

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found. {ex.Message}");
                _log = new List<T>();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Permission denied. {ex.Message}");
                _log = new List<T>();
            }
            catch(IOException ex)
            {
                Console.WriteLine($"IO error occured: {ex.Message}");
                _log = new List<T>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error converting from Json: {ex.Message}");
                _log = new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occured: {ex.Message}");
                _log = new List<T>();
            }
        }

    }
}
