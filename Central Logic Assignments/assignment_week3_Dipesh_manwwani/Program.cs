class

Item
{
    private

int id;
    private

string name;
    private

double price;
    private

int quantity;

    public

Item(int id, string name, double price, int quantity)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.quantity = quantity;
    }

    public

int ID
    {
        get { return id; }
        set { id = value; }
    }

    public

string Name
    {
        get { return name; }
        set { name = value; }
    }

    public

double

Price
    {
        get { return price; }
        set { price = value; }
    }

    public

int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public

override

string

ToString()
    {
        return $"ID: {id}, Name: {name}, Price: {price}, Quantity: {quantity}";
    }
}

class

Inventory
{
    private List<Item> items;

    public

Inventory()
    {
        items = new List<Item>();
    }

    public

void

AddItem(Item item)
    {
        items.Add(item);
    }

    public void DisplayItems()
    {
        foreach (Item item in items)
        {
            Console.WriteLine(item);
        }
    }

    public Item FindItemById(int id)
    {
        return items.Find(item => item.ID == id);
    }

    public void UpdateItem(Item item)
    {
        Item existingItem = items.Find(item => item.ID == item.ID);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Price = item.Price;
            existingItem.Quantity = item.Quantity;
        }
    }

    public void DeleteItem(int id)
    {
        items.RemoveAll(item => item.ID == id);
    }
}