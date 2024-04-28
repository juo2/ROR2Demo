using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000007 RID: 7
	internal class CachedTypeAllocationException : AllocationException
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		public CachedTypeAllocationException(IntPtr address, Type foundType, Type expectedType) : base(string.Format("Cached allocation is '{0}' but expected '{1}' at {2}", foundType, expectedType, address.ToString("X")))
		{
		}
	}
}
