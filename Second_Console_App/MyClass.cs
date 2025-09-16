using System;

public class MyClass
{
    // Field
    private string _name;

    // Property
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    // Конструктор без параметрів
    public MyClass()
    {
        _name = "User";
    }

    // Конструктор з параметром
    public MyClass(string name)
    {
        _name = name;
    }

    // Публічний метод
    public void PublicMethod()
    {
        Console.WriteLine($"Hello, my name is {_name}");
        PrivateMethod();
    }

    // Приватний метод
    private void PrivateMethod()
    {
        Console.WriteLine("This is a private method!");
    }
}
