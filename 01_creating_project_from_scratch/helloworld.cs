using System;

class HelloWorld
{
    static void Main()
    {
#if DebugConfig
        Console.WriteLine("WE ARE IN THE DEBUG CONFIGURATION");
#endif
        MyMath my = new MyMath();

        Console.WriteLine("Hello, world!");
        Console.WriteLine("2 + 3: " + my.Add(2,3));
    }
}