using Microsoft.Extensions.DependencyInjection;

using Module_1_Abstraction;

using Module_2_Abstraction;

using Module_3_Abstraction;

namespace Module_2_Implementation;
public class Module_2 : IModule_2
{
	private readonly IServiceProvider serviceProvider;

	public Module_2(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}
	public void Call_Module_1()
	{
		serviceProvider.GetService<IModule_1>()!.Respond();
	}

	public void Call_Module_3()
	{
		serviceProvider.GetService<IModule_3>()!.Respond();
	}

	public void Respond()
	{
		Console.WriteLine("this is module 2 getting called");
	}
}