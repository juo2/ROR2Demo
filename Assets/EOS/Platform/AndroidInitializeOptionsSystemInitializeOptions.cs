using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005DA RID: 1498
	public class AndroidInitializeOptionsSystemInitializeOptions : ISettable
	{
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x00026219 File Offset: 0x00024419
		// (set) Token: 0x0600242A RID: 9258 RVA: 0x00026221 File Offset: 0x00024421
		public IntPtr Reserved { get; set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0002622A File Offset: 0x0002442A
		// (set) Token: 0x0600242C RID: 9260 RVA: 0x00026232 File Offset: 0x00024432
		public string OptionalInternalDirectory { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x0002623B File Offset: 0x0002443B
		// (set) Token: 0x0600242E RID: 9262 RVA: 0x00026243 File Offset: 0x00024443
		public string OptionalExternalDirectory { get; set; }

		// Token: 0x0600242F RID: 9263 RVA: 0x0002624C File Offset: 0x0002444C
		internal void Set(AndroidInitializeOptionsSystemInitializeOptionsInternal? other)
		{
			if (other != null)
			{
				this.Reserved = other.Value.Reserved;
				this.OptionalInternalDirectory = other.Value.OptionalInternalDirectory;
				this.OptionalExternalDirectory = other.Value.OptionalExternalDirectory;
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000262A1 File Offset: 0x000244A1
		public void Set(object other)
		{
			this.Set(other as AndroidInitializeOptionsSystemInitializeOptionsInternal?);
		}
	}
}
