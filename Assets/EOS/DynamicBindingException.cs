using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000009 RID: 9
	internal class DynamicBindingException : Exception
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000218F File Offset: 0x0000038F
		public DynamicBindingException(string bindingName) : base(string.Format("Failed to hook dynamic binding for '{0}'", bindingName))
		{
		}
	}
}
