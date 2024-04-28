using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000006 RID: 6
	internal class ExternalAllocationException : AllocationException
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002126 File Offset: 0x00000326
		public ExternalAllocationException(IntPtr address, Type type) : base(string.Format("Attempting to allocate '{0}' over externally allocated memory at {1}", type, address.ToString("X")))
		{
		}
	}
}
