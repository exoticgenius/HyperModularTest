using Microsoft.Extensions.DependencyInjection;

using Module_1_Abstraction;

using Module_2_Abstraction;

using Module_3_Abstraction;

namespace Module_3_Implementation;
public class Module_3 : IModule_3
{
	private readonly IServiceProvider serviceProvider;

	public Module_3(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}
	public void Call_Module_1()
	{
		serviceProvider.GetService<IModule_1>()!.Respond();
	}

	public void Call_Module_2()
	{
		serviceProvider.GetService<IModule_2>()!.Respond();
	}

	public void Respond()
	{
		Console.WriteLine("this is module 3 getting called");
	}
}