using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C7 RID: 711
	public class ModIdentifier : ISettable
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0001326C File Offset: 0x0001146C
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x00013274 File Offset: 0x00011474
		public string NamespaceId { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0001327D File Offset: 0x0001147D
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x00013285 File Offset: 0x00011485
		public string ItemId { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0001328E File Offset: 0x0001148E
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x00013296 File Offset: 0x00011496
		public string ArtifactId { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0001329F File Offset: 0x0001149F
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x000132A7 File Offset: 0x000114A7
		public string Title { get; set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000132B0 File Offset: 0x000114B0
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x000132B8 File Offset: 0x000114B8
		public string Version { get; set; }

		// Token: 0x06001208 RID: 4616 RVA: 0x000132C4 File Offset: 0x000114C4
		internal void Set(ModIdentifierInternal? other)
		{
			if (other != null)
			{
				this.NamespaceId = other.Value.NamespaceId;
				this.ItemId = other.Value.ItemId;
				this.ArtifactId = other.Value.ArtifactId;
				this.Title = other.Value.Title;
				this.Version = other.Value.Version;
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00013343 File Offset: 0x00011543
		public void Set(object other)
		{
			this.Set(other as ModIdentifierInternal?);
		}
	}
}
