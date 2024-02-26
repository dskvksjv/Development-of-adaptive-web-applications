using System;
using System.Reflection;
using System.Collections.Generic;

public class Pizza
{
    public string Name { get; }
    public decimal Price { get; }

    public Pizza(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

public class PizzaOrder
{
    private readonly string _customerName;
    public int OrderNumber { get; }
    public bool IsDelivery { get; }
    private readonly string _deliveryAddress;
    private readonly decimal _totalCost;
    private readonly List<string> _options;

    public PizzaOrder(string customerName, int orderNumber, bool isDelivery, string deliveryAddress, decimal totalCost, List<string> options)
    {
        _customerName = customerName;
        OrderNumber = orderNumber;
        IsDelivery = isDelivery;
        _deliveryAddress = deliveryAddress;
        _totalCost = totalCost;
        _options = options;
    }

    public void DisplayOrderInfo()
    {
        Console.WriteLine($"Order Number: {OrderNumber}");
        Console.WriteLine($"Customer: {_customerName}");
        Console.WriteLine($"Delivery: {(IsDelivery ? "Yes" : "No")}");
        if (IsDelivery)
        {
            Console.WriteLine($"Address: {_deliveryAddress}");
        }
        Console.WriteLine($"Total: {_totalCost:C}");
        Console.WriteLine("Options:");
        foreach (var option in _options)
        {
            Console.WriteLine(option);
        }
    }

    public decimal CalculateTotalCost(decimal basePrice)
    {
        decimal deliveryCharge = IsDelivery ? 5.00m : 0.00m;
        return basePrice + deliveryCharge;
    }

    //Робота з Type і TypeInfo
    public void DisplayTypeInfo()
    {
        Type orderType = typeof(PizzaOrder);
        TypeInfo orderTypeInfo = orderType.GetTypeInfo();
        Console.WriteLine($"Type Name: {orderType.Name}");
        Console.WriteLine($"Is Class: {orderTypeInfo.IsClass}");
    }

    //Робота з MemberInfo
    public void DisplayMemberInfo()
    {
        MemberInfo[] members = typeof(PizzaOrder).GetMembers();
        foreach (var member in members)
        {
            Console.WriteLine($"Member Name: {member.Name}, Member Type: {member.MemberType}");
        }
    }

    //Робота з FieldInfo
    public void DisplayFieldInfo()
    {
        FieldInfo[] fields = typeof(PizzaOrder).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            Console.WriteLine($"Field Name: {field.Name}, Field Type: {field.FieldType}");
        }
    }

    //Робота з MethodInfo
    public void DisplayMethodInfo()
    {
        MethodInfo displayMethodInfo = typeof(PizzaOrder).GetMethod("DisplayOrderInfo");
        if (displayMethodInfo != null)
        {
            Console.WriteLine($"Method Name: {displayMethodInfo.Name}");
            displayMethodInfo.Invoke(this, null);
        }
        else
        {
            Console.WriteLine("Method not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Pizza> pizzas = new List<Pizza>
        {
            new Pizza("Margherita", 9.99m),
            new Pizza("Pepperoni", 10.99m),
            new Pizza("Vegetarian", 11.99m)
        };

        Console.WriteLine("Pizzas:");
        for (int i = 0; i < pizzas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pizzas[i].Name} - {pizzas[i].Price:C}");
        }
        Console.Write("Choose a pizza: ");
        int pizzaChoice = int.Parse(Console.ReadLine()) - 1;
        Pizza chosenPizza = pizzas[pizzaChoice];

        // Додавання опцій
        List<string> options = new List<string>();
        bool addingOptions = true;
        while (addingOptions)
        {
            Console.Write("Add an option (type 'done' to finish): ");
            string option = Console.ReadLine();
            if (option.ToLower() == "done")
            {
                addingOptions = false;
            }
            else
            {
                options.Add(option);
            }
        }

        decimal totalCost = chosenPizza.Price;
        int cheeseCount = 0;
        foreach (var option in options)
        {
            if (option.ToLower().Contains("cheese"))
            {
                cheeseCount++;
            }
            else if (option.ToLower().Contains("extra cheese"))
            {
                cheeseCount += 2;
            }
        }
        totalCost += cheeseCount * 2.00m;

        PizzaOrder order = new PizzaOrder("Victoria Rekonvald", 101, true, "Mykolaiv", totalCost, options);

        // Reflection
        order.DisplayTypeInfo();
        Console.WriteLine();
        order.DisplayMemberInfo();
        Console.WriteLine();
        order.DisplayFieldInfo();
        Console.WriteLine();
        order.DisplayMethodInfo();
    }
}

