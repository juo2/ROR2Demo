using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003622 File Offset: 0x00001822
		public MonoPInvokeCallbackAttribute(Type type)
		{
		}
	}
}
