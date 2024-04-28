using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000085 RID: 133
	public class ReadFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00005B8A File Offset: 0x00003D8A
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00005B92 File Offset: 0x00003D92
		public Result ResultCode { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00005B9B File Offset: 0x00003D9B
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00005BA3 File Offset: 0x00003DA3
		public object ClientData { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00005BAC File Offset: 0x00003DAC
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00005BBD File Offset: 0x00003DBD
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00005BC5 File Offset: 0x00003DC5
		public string Filename { get; private set; }

		// Token: 0x060004F3 RID: 1267 RVA: 0x00005BCE File Offset: 0x00003DCE
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal void Set(ReadFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00005C46 File Offset: 0x00003E46
		public void Set(object other)
		{
			this.Set(other as ReadFileCallbackInfoInternal?);
		}
	}
}
