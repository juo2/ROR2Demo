using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000067 RID: 103
	public class DeleteCacheCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000053AE File Offset: 0x000035AE
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000053B6 File Offset: 0x000035B6
		public Result ResultCode { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000053BF File Offset: 0x000035BF
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x000053C7 File Offset: 0x000035C7
		public object ClientData { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000053D0 File Offset: 0x000035D0
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000053D8 File Offset: 0x000035D8
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000448 RID: 1096 RVA: 0x000053E1 File Offset: 0x000035E1
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000053F0 File Offset: 0x000035F0
		internal void Set(DeleteCacheCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00005445 File Offset: 0x00003645
		public void Set(object other)
		{
			this.Set(other as DeleteCacheCallbackInfoInternal?);
		}
	}
}
