using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000008 RID: 8
	internal class CachedArrayAllocationException : AllocationException
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002165 File Offset: 0x00000365
		public CachedArrayAllocationException(IntPtr address, int foundLength, int expectedLength) : base(string.Format("Cached array allocation has length {0} but expected {1} at {2}", foundLength, expectedLength, address.ToString("X")))
		{
		}
	}
}
