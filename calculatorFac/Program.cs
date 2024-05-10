using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Inventory Management System");

            Inventory inventory = new Inventory();

            while (true)
            {
                Console.WriteLine("\n1. Add Item");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Display Inventory");
                Console.WriteLine("4. Save Inventory to File");
                Console.WriteLine("5. Load Inventory from File");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter item name: ");
                        string itemName = Console.ReadLine();
                        Console.Write("Enter item quantity: ");
                        int itemQuantity = int.Parse(Console.ReadLine());
                        inventory.AddItem(itemName, itemQuantity);
                        break;
                    case 2:
                        Console.Write("Enter item name to remove: ");
                        itemName = Console.ReadLine();
                        inventory.RemoveItem(itemName);
                        break;
                    case 3:
                        inventory.DisplayInventory();
                        break;
                    case 4:
                        Console.Write("Enter file name to save: ");
                        string saveFileName = Console.ReadLine();
                        inventory.SaveInventoryToFile(saveFileName);
                        break;
                    case 5:
                        Console.Write("Enter file name to load: ");
                        string loadFileName = Console.ReadLine();
                        inventory.LoadInventoryFromFile(loadFileName);
                        break;
                    case 6:
                        Console.WriteLine("Exiting program...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    class Inventory
    {
        private Dictionary<string, int> items;

        public Inventory()
        {
            items = new Dictionary<string, int>();
        }

        public void AddItem(string name, int quantity)
        {
            if (items.ContainsKey(name))
            {
                items[name] += quantity;
            }
            else
            {
                items.Add(name, quantity);
            }
            Console.WriteLine($"{quantity} {name}(s) added to inventory.");
        }

        public void RemoveItem(string name)
        {
            if (items.ContainsKey(name))
            {
                items.Remove(name);
                Console.WriteLine($"{name} removed from inventory.");
            }
            else
            {
                Console.WriteLine($"{name} not found in inventory.");
            }
        }

        public void DisplayInventory()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
            }
            else
            {
                Console.WriteLine("Inventory:");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
        }

        public void SaveInventoryToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var item in items)
                {
                    writer.WriteLine($"{item.Key},{item.Value}");
                }
            }
            Console.WriteLine("Inventory saved to file.");
        }

        public void LoadInventoryFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found.");
                return;
            }

            items.Clear();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                    {
                        items.Add(parts[0], quantity);
                    }
                }
            }

            Console.WriteLine("Inventory loaded from file.");
        }
    }
}
