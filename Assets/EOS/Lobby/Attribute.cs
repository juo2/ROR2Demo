using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FC RID: 764
	public class Attribute : ISettable
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0001435B File Offset: 0x0001255B
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00014363 File Offset: 0x00012563
		public AttributeData Data { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0001436C File Offset: 0x0001256C
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00014374 File Offset: 0x00012574
		public LobbyAttributeVisibility Visibility { get; set; }

		// Token: 0x06001306 RID: 4870 RVA: 0x00014380 File Offset: 0x00012580
		internal void Set(AttributeInternal? other)
		{
			if (other != null)
			{
				this.Data = other.Value.Data;
				this.Visibility = other.Value.Visibility;
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000143C0 File Offset: 0x000125C0
		public void Set(object other)
		{
			this.Set(other as AttributeInternal?);
		}
	}
}
