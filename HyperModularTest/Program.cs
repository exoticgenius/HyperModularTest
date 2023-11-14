
using Microsoft.Extensions.DependencyInjection;

using Module_1_Abstraction;

using Module_2_Abstraction;

using Module_3_Abstraction;

using Newtonsoft.Json;

ServiceCollection sc = new ServiceCollection();

Scanner.RegisterServices(sc);

var provider = sc.BuildServiceProvider();



var m1 = provider.GetService<IModule_1>();
var m2 = provider.GetService<IModule_2>();
var m3 = provider.GetService<IModule_3>();


try
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("m1 calling m2:");
    Console.ResetColor();
    m1.Call_Module_2();
}
catch(Exception e)
{
    Console.WriteLine("implementation not found");
}
try
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine("m1 calling m3:");
	Console.ResetColor();
	m1.Call_Module_3();
}
catch (Exception e)
{
	Console.WriteLine("implementation not found");
}


try
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine("m2 calling m1:");
	Console.ResetColor();
	m2.Call_Module_1();
}
catch (Exception e)
{
	Console.WriteLine("implementation not found");
}
try
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine("m2 calling m3:");
	Console.ResetColor();
	m2.Call_Module_3();
}
catch (Exception e)
{
	Console.WriteLine("implementation not found");
}


try
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine("m3 calling m1:");
	Console.ResetColor();
	m3.Call_Module_1();
}
catch (Exception e)
{
	Console.WriteLine("implementation not found");
}
try
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine("m3 calling m2:");
	Console.ResetColor();
	m3.Call_Module_2();
}
catch (Exception e)
{
	Console.WriteLine("implementation not found");
}

Console.ReadLine();