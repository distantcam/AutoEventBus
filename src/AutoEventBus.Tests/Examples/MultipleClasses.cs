using AutoEventBus;

public class ClassA
{
    [Subscriber]
    public void Do(int value)
    {
    }
}

public class ClassB
{
    [Subscriber]
    public void Do(int value)
    {
    }
}
