using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E2 RID: 1506
	public class InitializeThreadAffinity : ISettable
	{
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00026C7D File Offset: 0x00024E7D
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x00026C85 File Offset: 0x00024E85
		public ulong NetworkWork { get; set; }

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00026C8E File Offset: 0x00024E8E
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x00026C96 File Offset: 0x00024E96
		public ulong StorageIo { get; set; }

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x00026C9F File Offset: 0x00024E9F
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x00026CA7 File Offset: 0x00024EA7
		public ulong WebSocketIo { get; set; }

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x00026CB0 File Offset: 0x00024EB0
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x00026CB8 File Offset: 0x00024EB8
		public ulong P2PIo { get; set; }

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00026CC1 File Offset: 0x00024EC1
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x00026CC9 File Offset: 0x00024EC9
		public ulong HttpRequestIo { get; set; }

		// Token: 0x0600249A RID: 9370 RVA: 0x00026CD4 File Offset: 0x00024ED4
		internal void Set(InitializeThreadAffinityInternal? other)
		{
			if (other != null)
			{
				this.NetworkWork = other.Value.NetworkWork;
				this.StorageIo = other.Value.StorageIo;
				this.WebSocketIo = other.Value.WebSocketIo;
				this.P2PIo = other.Value.P2PIo;
				this.HttpRequestIo = other.Value.HttpRequestIo;
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00026D53 File Offset: 0x00024F53
		public void Set(object other)
		{
			this.Set(other as InitializeThreadAffinityInternal?);
		}
	}
}
