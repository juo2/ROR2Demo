using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000607 RID: 1543
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : ISettable, IDisposable
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000280BC File Offset: 0x000262BC
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x000280D8 File Offset: 0x000262D8
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

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000280E8 File Offset: 0x000262E8
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x00028104 File Offset: 0x00026304
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00028114 File Offset: 0x00026314
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x00028130 File Offset: 0x00026330
		public string Description
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Description, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Description, value);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00028140 File Offset: 0x00026340
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x0002815C File Offset: 0x0002635C
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

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0002816C File Offset: 0x0002636C
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x00028188 File Offset: 0x00026388
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

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00028198 File Offset: 0x00026398
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x000281B4 File Offset: 0x000263B4
		public string HiddenDescription
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_HiddenDescription, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_HiddenDescription, value);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000281C4 File Offset: 0x000263C4
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x000281E0 File Offset: 0x000263E0
		public string CompletionDescription
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CompletionDescription, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_CompletionDescription, value);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000281F0 File Offset: 0x000263F0
		// (set) Token: 0x060025B8 RID: 9656 RVA: 0x0002820C File Offset: 0x0002640C
		public string UnlockedIconId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UnlockedIconId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockedIconId, value);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x0002821C File Offset: 0x0002641C
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x00028238 File Offset: 0x00026438
		public string LockedIconId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LockedIconId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LockedIconId, value);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x00028248 File Offset: 0x00026448
		// (set) Token: 0x060025BC RID: 9660 RVA: 0x00028264 File Offset: 0x00026464
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

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x00028274 File Offset: 0x00026474
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x00028296 File Offset: 0x00026496
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

		// Token: 0x060025BF RID: 9663 RVA: 0x000282AC File Offset: 0x000264AC
		public void Set(Definition other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.AchievementId;
				this.DisplayName = other.DisplayName;
				this.Description = other.Description;
				this.LockedDisplayName = other.LockedDisplayName;
				this.LockedDescription = other.LockedDescription;
				this.HiddenDescription = other.HiddenDescription;
				this.CompletionDescription = other.CompletionDescription;
				this.UnlockedIconId = other.UnlockedIconId;
				this.LockedIconId = other.LockedIconId;
				this.IsHidden = other.IsHidden;
				this.StatThresholds = other.StatThresholds;
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0002834A File Offset: 0x0002654A
		public void Set(object other)
		{
			this.Set(other as Definition);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00028358 File Offset: 0x00026558
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
			Helper.TryMarshalDispose(ref this.m_Description);
			Helper.TryMarshalDispose(ref this.m_LockedDisplayName);
			Helper.TryMarshalDispose(ref this.m_LockedDescription);
			Helper.TryMarshalDispose(ref this.m_HiddenDescription);
			Helper.TryMarshalDispose(ref this.m_CompletionDescription);
			Helper.TryMarshalDispose(ref this.m_UnlockedIconId);
			Helper.TryMarshalDispose(ref this.m_LockedIconId);
			Helper.TryMarshalDispose(ref this.m_StatThresholds);
		}

		// Token: 0x040011DF RID: 4575
		private int m_ApiVersion;

		// Token: 0x040011E0 RID: 4576
		private IntPtr m_AchievementId;

		// Token: 0x040011E1 RID: 4577
		private IntPtr m_DisplayName;

		// Token: 0x040011E2 RID: 4578
		private IntPtr m_Description;

		// Token: 0x040011E3 RID: 4579
		private IntPtr m_LockedDisplayName;

		// Token: 0x040011E4 RID: 4580
		private IntPtr m_LockedDescription;

		// Token: 0x040011E5 RID: 4581
		private IntPtr m_HiddenDescription;

		// Token: 0x040011E6 RID: 4582
		private IntPtr m_CompletionDescription;

		// Token: 0x040011E7 RID: 4583
		private IntPtr m_UnlockedIconId;

		// Token: 0x040011E8 RID: 4584
		private IntPtr m_LockedIconId;

		// Token: 0x040011E9 RID: 4585
		private int m_IsHidden;

		// Token: 0x040011EA RID: 4586
		private int m_StatThresholdsCount;

		// Token: 0x040011EB RID: 4587
		private IntPtr m_StatThresholds;
	}
}
