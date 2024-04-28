using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A5 RID: 1445
	public class RegisterEventParamDef : ISettable
	{
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0002524A File Offset: 0x0002344A
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x00025252 File Offset: 0x00023452
		public string ParamName { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0002525B File Offset: 0x0002345B
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x00025263 File Offset: 0x00023463
		public AntiCheatCommonEventParamType ParamType { get; set; }

		// Token: 0x06002327 RID: 8999 RVA: 0x0002526C File Offset: 0x0002346C
		internal void Set(RegisterEventParamDefInternal? other)
		{
			if (other != null)
			{
				this.ParamName = other.Value.ParamName;
				this.ParamType = other.Value.ParamType;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000252AC File Offset: 0x000234AC
		public void Set(object other)
		{
			this.Set(other as RegisterEventParamDefInternal?);
		}
	}
}
