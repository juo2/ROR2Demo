using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000609 RID: 1545
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionV2Internal : ISettable, IDisposable
	{
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00028588 File Offset: 0x00026788
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x000285A4 File Offset: 0x000267A4
		public string AchievementId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AchievementId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x000285B4 File Offset: 0x000267B4
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x000285D0 File Offset: 0x000267D0
		public string UnlockedDisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UnlockedDisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockedDisplayName, value);
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000285E0 File Offset: 0x000267E0
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x000285FC File Offset: 0x000267FC
		public string UnlockedDescription
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UnlockedDescription, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockedDescription, value);
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x0002860C File Offset: 0x0002680C
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x00028628 File Offset: 0x00026828
		public string LockedDisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LockedDisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LockedDisplayName, value);
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00028638 File Offset: 0x00026838
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x00028654 File Offset: 0x00026854
		public string LockedDescription
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LockedDescription, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LockedDescription, value);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00028664 File Offset: 0x00026864
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x00028680 File Offset: 0x00026880
		public string FlavorText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_FlavorText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_FlavorText, value);
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00028690 File Offset: 0x00026890
		// (set) Token: 0x060025E6 RID: 9702 RVA: 0x000286AC File Offset: 0x000268AC
		public string UnlockedIconURL
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UnlockedIconURL, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockedIconURL, value);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000286BC File Offset: 0x000268BC
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x000286D8 File Offset: 0x000268D8
		public string LockedIconURL
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LockedIconURL, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LockedIconURL, value);
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000286E8 File Offset: 0x000268E8
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x00028704 File Offset: 0x00026904
		public bool IsHidden
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsHidden, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IsHidden, value);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x00028714 File Offset: 0x00026914
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x00028736 File Offset: 0x00026936
		public StatThresholds[] StatThresholds
		{
			get
			{
				StatThresholds[] result;
				Helper.TryMarshalGet<StatThresholdsInternal, StatThresholds>(this.m_StatThresholds, out result, this.m_StatThresholdsCount);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<StatThresholdsInternal, StatThresholds>(ref this.m_StatThresholds, value, out this.m_StatThresholdsCount);
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0002874C File Offset: 0x0002694C
		public void Set(DefinitionV2 other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.AchievementId;
				this.UnlockedDisplayName = other.UnlockedDisplayName;
				this.UnlockedDescription = other.UnlockedDescription;
				this.LockedDisplayName = other.LockedDisplayName;
				this.LockedDescription = other.LockedDescription;
				this.FlavorText = other.FlavorText;
				this.UnlockedIconURL = other.UnlockedIconURL;
				this.LockedIconURL = other.LockedIconURL;
				this.IsHidden = other.IsHidden;
				this.StatThresholds = other.StatThresholds;
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000287DB File Offset: 0x000269DB
		public void Set(object other)
		{
			this.Set(other as DefinitionV2);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000287EC File Offset: 0x000269EC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
			Helper.TryMarshalDispose(ref this.m_UnlockedDisplayName);
			Helper.TryMarshalDispose(ref this.m_UnlockedDescription);
			Helper.TryMarshalDispose(ref this.m_LockedDisplayName);
			Helper.TryMarshalDispose(ref this.m_LockedDescription);
			Helper.TryMarshalDispose(ref this.m_FlavorText);
			Helper.TryMarshalDispose(ref this.m_UnlockedIconURL);
			Helper.TryMarshalDispose(ref this.m_LockedIconURL);
			Helper.TryMarshalDispose(ref this.m_StatThresholds);
		}

		// Token: 0x040011F6 RID: 4598
		private int m_ApiVersion;

		// Token: 0x040011F7 RID: 4599
		private IntPtr m_AchievementId;

		// Token: 0x040011F8 RID: 4600
		private IntPtr m_UnlockedDisplayName;

		// Token: 0x040011F9 RID: 4601
		private IntPtr m_UnlockedDescription;

		// Token: 0x040011FA RID: 4602
		private IntPtr m_LockedDisplayName;

		// Token: 0x040011FB RID: 4603
		private IntPtr m_LockedDescription;

		// Token: 0x040011FC RID: 4604
		private IntPtr m_FlavorText;

		// Token: 0x040011FD RID: 4605
		private IntPtr m_UnlockedIconURL;

		// Token: 0x040011FE RID: 4606
		private IntPtr m_LockedIconURL;

		// Token: 0x040011FF RID: 4607
		private int m_IsHidden;

		// Token: 0x04001200 RID: 4608
		private uint m_StatThresholdsCount;

		// Token: 0x04001201 RID: 4609
		private IntPtr m_StatThresholds;
	}
}
