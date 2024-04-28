using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026A RID: 618
	public class ReadFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00010DEC File Offset: 0x0000EFEC
		public Result ResultCode { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00010DF5 File Offset: 0x0000EFF5
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00010DFD File Offset: 0x0000EFFD
		public object ClientData { get; private set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00010E06 File Offset: 0x0000F006
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00010E0E File Offset: 0x0000F00E
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00010E17 File Offset: 0x0000F017
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00010E1F File Offset: 0x0000F01F
		public string Filename { get; private set; }

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00010E28 File Offset: 0x0000F028
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00010E38 File Offset: 0x0000F038
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

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00010EA2 File Offset: 0x0000F0A2
		public void Set(object other)
		{
			this.Set(other as ReadFileCallbackInfoInternal?);
		}
	}
}
