using System.Reflection;
using System.Reflection.Emit;

using Microsoft.Extensions.DependencyInjection;

using Module_X_Abstraction;

public static class Scanner
{
	public static void RegisterServices(ServiceCollection sc)
	{
		var asses = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*Module_*_Implementation.dll").ToList();

		asses.ForEach(x => AppDomain.CurrentDomain.Load(File.ReadAllBytes(x.FullName)));
		var ass = AppDomain.CurrentDomain.GetAssemblies().Where(x=>x.FullName.Contains("Module_")).ToList();
		var types = ass.SelectMany(x => x.GetTypes()).ToList();

		foreach (var item in types.Where(x=>x.IsInterface && x != typeof(Module_X) && typeof(Module_X).IsAssignableFrom(x)))
		{
			var declaringType = types.FirstOrDefault(x => x.GetInterfaces().Contains(item));

			if (declaringType is not null)
				sc.AddTransient(item, declaringType);
			else
				sc.AddTransient(item, TypeMixer.ExtendWith(item));
		}
	}
}



public static class TypeMixer
{
	public static readonly BindingFlags visibilityFlags = BindingFlags.Public | BindingFlags.Instance;
	public static Type ExtendWith(Type i)
	{
		var assemblyName = Guid.NewGuid().ToString();
		var assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
		var module = assembly.DefineDynamicModule("Module");
		var type = module.DefineType("DefaultImplementation_" + i.Name, TypeAttributes.Public);
		type.AddInterfaceImplementation(i);

		HashSet<MethodInfo> methods = new HashSet<MethodInfo>(i.GetMethods());
        foreach (var item in i.GetInterfaces().SelectMany(x => x.GetMethods()))
        {
			methods.Add(item);
        };

		foreach (var pm in methods)
		{
			var method = type.DefineMethod(pm.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.Virtual, pm.ReturnType, pm.GetParameters().Select(x => x.ParameterType).ToArray());
			var methodGen = method.GetILGenerator();
			methodGen.ThrowException(typeof(NotAvailableModule));
			methodGen.Emit(OpCodes.Ret);
			type.DefineMethodOverride(method, pm);
		}
		return type.CreateType();
	}
}

public class NotAvailableModule : Exception
{
	
}