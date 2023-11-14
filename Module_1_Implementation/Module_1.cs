using Microsoft.Extensions.DependencyInjection;

using Module_1_Abstraction;

using Module_2_Abstraction;

using Module_3_Abstraction;

namespace Module_1_Implementation;
public class Module_1 : IModule_1
{
	private readonly IServiceProvider serviceProvider;

	public Module_1(IServiceProvider serviceProvider)
    {
		this.serviceProvider = serviceProvider;
	}
	public void Call_Module_2()
	{
		serviceProvider.GetService<IModule_2>()!.Respond();
	}

	public void Call_Module_3()
	{
		serviceProvider.GetService<IModule_3>()!.Respond();
	}

	public void Respond()
	{
        Console.WriteLine("this is module 1 getting called");
    }
}