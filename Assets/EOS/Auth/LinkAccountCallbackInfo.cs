using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200051E RID: 1310
	public class LinkAccountCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x00021817 File Offset: 0x0001FA17
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x0002181F File Offset: 0x0001FA1F
		public Result ResultCode { get; private set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00021828 File Offset: 0x0001FA28
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00021830 File Offset: 0x0001FA30
		public object ClientData { get; private set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00021839 File Offset: 0x0001FA39
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x00021841 File Offset: 0x0001FA41
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0002184A File Offset: 0x0001FA4A
		// (set) Token: 0x06001FAE RID: 8110 RVA: 0x00021852 File Offset: 0x0001FA52
		public PinGrantInfo PinGrantInfo { get; private set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0002185B File Offset: 0x0001FA5B
		// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00021863 File Offset: 0x0001FA63
		public EpicAccountId SelectedAccountId { get; private set; }

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0002186C File Offset: 0x0001FA6C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0002187C File Offset: 0x0001FA7C
		internal void Set(LinkAccountCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PinGrantInfo = other.Value.PinGrantInfo;
				this.SelectedAccountId = other.Value.SelectedAccountId;
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000218FB File Offset: 0x0001FAFB
		public void Set(object other)
		{
			this.Set(other as LinkAccountCallbackInfoInternal?);
		}
	}
}
