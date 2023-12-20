using System;
using System.Collections.Generic;

class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}

class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void DisplayItems()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public Item FindItem(int id)
    {
        return items.Find(item => item.Id == id);
    }

    public bool UpdateItem(int id, Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id == id)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool DeleteItem(int id)
    {
        return items.Remove(items.Find(item => item.Id == id));
    }
}

class Program
{
    static void Main()
    {
        Inventory inventory = new Inventory();

        inventory.AddItem(new Item { Id = 1, Name = "Apple", Price = 1.0, Quantity = 10 });
        inventory.AddItem(new Item { Id = 2, Name = "Banana", Price = 1.5, Quantity = 5 });

        inventory.DisplayItems();

        Console.WriteLine("Updated Apple's quantity:");
        inventory.UpdateItem(1, new Item { Id = 1, Name = "Apple", Price = 1.0, Quantity = 15 });
        inventory.DisplayItems();

        Console.WriteLine("Deleted Banana:");
        inventory.DeleteItem(2);
        inventory.DisplayItems();
    }
}