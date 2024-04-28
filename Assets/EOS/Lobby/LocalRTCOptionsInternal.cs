using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000374 RID: 884
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LocalRTCOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00017A15 File Offset: 0x00015C15
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x00017A1D File Offset: 0x00015C1D
		public uint Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00017A28 File Offset: 0x00015C28
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00017A44 File Offset: 0x00015C44
		public bool UseManualAudioInput
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_UseManualAudioInput, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UseManualAudioInput, value);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00017A54 File Offset: 0x00015C54
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00017A70 File Offset: 0x00015C70
		public bool UseManualAudioOutput
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_UseManualAudioOutput, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UseManualAudioOutput, value);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00017A80 File Offset: 0x00015C80
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00017A9C File Offset: 0x00015C9C
		public bool LocalAudioDeviceInputStartsMuted
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_LocalAudioDeviceInputStartsMuted, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalAudioDeviceInputStartsMuted, value);
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00017AAB File Offset: 0x00015CAB
		public void Set(LocalRTCOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Flags = other.Flags;
				this.UseManualAudioInput = other.UseManualAudioInput;
				this.UseManualAudioOutput = other.UseManualAudioOutput;
				this.LocalAudioDeviceInputStartsMuted = other.LocalAudioDeviceInputStartsMuted;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00017AE7 File Offset: 0x00015CE7
		public void Set(object other)
		{
			this.Set(other as LocalRTCOptions);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A79 RID: 2681
		private int m_ApiVersion;

		// Token: 0x04000A7A RID: 2682
		private uint m_Flags;

		// Token: 0x04000A7B RID: 2683
		private int m_UseManualAudioInput;

		// Token: 0x04000A7C RID: 2684
		private int m_UseManualAudioOutput;

		// Token: 0x04000A7D RID: 2685
		private int m_LocalAudioDeviceInputStartsMuted;
	}
}
