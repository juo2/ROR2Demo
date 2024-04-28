using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000050 RID: 80
	public class OnDisplaySettingsUpdatedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00004C90 File Offset: 0x00002E90
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00004C98 File Offset: 0x00002E98
		public object ClientData { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00004CA1 File Offset: 0x00002EA1
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public bool IsVisible { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00004CB2 File Offset: 0x00002EB2
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00004CBA File Offset: 0x00002EBA
		public bool IsExclusiveInput { get; private set; }

		// Token: 0x060003D5 RID: 981 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00004CDC File Offset: 0x00002EDC
		internal void Set(OnDisplaySettingsUpdatedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.IsVisible = other.Value.IsVisible;
				this.IsExclusiveInput = other.Value.IsExclusiveInput;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00004D31 File Offset: 0x00002F31
		public void Set(object other)
		{
			this.Set(other as OnDisplaySettingsUpdatedCallbackInfoInternal?);
		}
	}
}
