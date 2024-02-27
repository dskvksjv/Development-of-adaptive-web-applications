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
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying order info: {ex.Message}");
        }
    }

    //Робота з Type і TypeInfo
    public void DisplayTypeInfo()
    {
        try
        {
            Type orderType = typeof(PizzaOrder);
            TypeInfo orderTypeInfo = orderType.GetTypeInfo();
            Console.WriteLine($"Type Name: {orderType.Name}");
            Console.WriteLine($"Is Class: {orderTypeInfo.IsClass}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying type info: {ex.Message}");
        }
    }

    //MemberInfo
    public void DisplayDynamicInfo(string memberName)
    {
        try
        {
            MemberInfo memberInfo = typeof(PizzaOrder).GetMember(memberName)[0];
            Console.WriteLine($"Name: {memberInfo.Name}, Member Type: {memberInfo.MemberType}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying dynamic info: {ex.Message}");
        }
    }

    //кешування викликів через Reflection
    private static Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();

    public void DisplayMethodInfo(string methodName)
    {
        try
        {
            if (!_methodCache.ContainsKey(methodName))
            {
                MethodInfo methodInfo = typeof(PizzaOrder).GetMethod(methodName);
                if (methodInfo != null)
                {
                    _methodCache[methodName] = methodInfo;
                }
                else
                {
                    Console.WriteLine("Method not found.");
                    return;
                }
            }

            Console.WriteLine($"Method Name: {_methodCache[methodName].Name}");
            _methodCache[methodName].Invoke(this, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying method info: {ex.Message}");
        }
    }

    //динамічне створення об'єктів на основі даних Reflection
    public static object CreateInstance(string className)
    {
        try
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                Console.WriteLine("Class not found.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating an instance: {ex.Message}");
            return null;
        }
    }

    //MemberInfo nf FieldInfo
    public void SetAttribute(string memberName, string attributeName, object value)
    {
        try
        {
            MemberInfo memberInfo = typeof(PizzaOrder).GetMember(memberName)[0];
            if (memberInfo != null)
            {
                switch (memberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        FieldInfo fieldInfo = (FieldInfo)memberInfo;
                        fieldInfo.SetValue(this, value);
                        break;
                    case MemberTypes.Property:
                        PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                        propertyInfo.SetValue(this, value);
                        break;
                    default:
                        Console.WriteLine("Error member type.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error setting attribute: {ex.Message}");
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

        //виклик методів через Reflection
        order.DisplayOrderInfo();
        Console.WriteLine();

        //інформація про клас через Reflection
        order.DisplayTypeInfo();
        Console.WriteLine();

        //динамічнв інформація Reflection
        order.DisplayDynamicInfo("OrderNumber");
        order.DisplayDynamicInfo("IsDelivery");
        Console.WriteLine();

        //кешуванням через Reflection
        order.DisplayMethodInfo("DisplayOrderInfo");
        Console.WriteLine();

        //створення об'єкту за допомогою Reflection
        object newOrder = PizzaOrder.CreateInstance("PizzaOrder");
        Console.WriteLine($"New instance created: {newOrder}");
        Console.WriteLine();

        //встановлення атрибуту Reflection
        order.SetAttribute("_customerName", "NewCustomer", "John Doe");
        order.DisplayOrderInfo();
    }
}


