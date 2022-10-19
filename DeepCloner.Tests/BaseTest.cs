using System.Reflection;
using DeepCloner.Core.Helpers;

namespace DeepCloner.Tests.Core;

public class BaseTest
{
	public BaseTest(bool isSafeInit)
	{
		SwitchTo(isSafeInit);
	}

	public static void SwitchTo(bool isSafeInit)
	{
		typeof(ShallowObjectCloner).GetMethod("SwitchTo", BindingFlags.NonPublic | BindingFlags.Static)
			.Invoke(null, new object[] { isSafeInit });
	}
}