using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005AB RID: 1451
	public class Vec3f : ISettable
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x00025423 File Offset: 0x00023623
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x0002542B File Offset: 0x0002362B
		public float x { get; set; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00025434 File Offset: 0x00023634
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x0002543C File Offset: 0x0002363C
		public float y { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00025445 File Offset: 0x00023645
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x0002544D File Offset: 0x0002364D
		public float z { get; set; }

		// Token: 0x0600234B RID: 9035 RVA: 0x00025458 File Offset: 0x00023658
		internal void Set(Vec3fInternal? other)
		{
			if (other != null)
			{
				this.x = other.Value.x;
				this.y = other.Value.y;
				this.z = other.Value.z;
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000254AD File Offset: 0x000236AD
		public void Set(object other)
		{
			this.Set(other as Vec3fInternal?);
		}
	}
}
