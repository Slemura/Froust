using System;

namespace RpDev.Services.GenericFactories.VContainer
{
	public interface IPlainClassFactory
	{
		T Create<T> (params object[] extraArgs) where T : class;

		object Create (Type type, params object[] extraArgs);
	}
}