using System;

// generic інтерфейс з generic методом
public interface IGenericInterface<T>
{
    T Process(T input);
}

// звичайний інтерфейс з одним методом
public interface ISimpleInterface
{
    void ShowMessage(string message);
}

// абстрактний клас з абстрактним і звичайним методами
public abstract class AbstractBase
{
    public abstract void AbstractMethod();

    public void NormalMethod()
    {
        Console.WriteLine("This is a normal method in AbstractBase");
    }
}

// клас, який наслідує абстрактний клас і реалізує обидва інтерфейси
public class ConcreteClass : AbstractBase, IGenericInterface<int>, ISimpleInterface
{
    public override void AbstractMethod()
    {
        Console.WriteLine("Implementation of abstract method");
    }

    public int Process(int input)
    {
        return input * 2;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine("Message: " + message);
    }
}
