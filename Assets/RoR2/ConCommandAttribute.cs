using System;
using HG.Reflection;

namespace RoR2
{
	// Token: 0x0200066D RID: 1645
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public class ConCommandAttribute : SearchableAttribute
	{
		// Token: 0x04002586 RID: 9606
		public string commandName;

		// Token: 0x04002587 RID: 9607
		public ConVarFlags flags;

		// Token: 0x04002588 RID: 9608
		public string helpText = "";
	}
}
