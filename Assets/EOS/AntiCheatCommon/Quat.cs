using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A1 RID: 1441
	public class Quat : ISettable
	{
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x00025022 File Offset: 0x00023222
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x0002502A File Offset: 0x0002322A
		public float w { get; set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00025033 File Offset: 0x00023233
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x0002503B File Offset: 0x0002323B
		public float x { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00025044 File Offset: 0x00023244
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0002504C File Offset: 0x0002324C
		public float y { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00025055 File Offset: 0x00023255
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0002505D File Offset: 0x0002325D
		public float z { get; set; }

		// Token: 0x06002305 RID: 8965 RVA: 0x00025068 File Offset: 0x00023268
		internal void Set(QuatInternal? other)
		{
			if (other != null)
			{
				this.w = other.Value.w;
				this.x = other.Value.x;
				this.y = other.Value.y;
				this.z = other.Value.z;
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000250D2 File Offset: 0x000232D2
		public void Set(object other)
		{
			this.Set(other as QuatInternal?);
		}
	}
}
