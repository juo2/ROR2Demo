using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000373 RID: 883
	public class LocalRTCOptions : ISettable
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00017954 File Offset: 0x00015B54
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0001795C File Offset: 0x00015B5C
		public uint Flags { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00017965 File Offset: 0x00015B65
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0001796D File Offset: 0x00015B6D
		public bool UseManualAudioInput { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00017976 File Offset: 0x00015B76
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x0001797E File Offset: 0x00015B7E
		public bool UseManualAudioOutput { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00017987 File Offset: 0x00015B87
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x0001798F File Offset: 0x00015B8F
		public bool LocalAudioDeviceInputStartsMuted { get; set; }

		// Token: 0x060015CA RID: 5578 RVA: 0x00017998 File Offset: 0x00015B98
		internal void Set(LocalRTCOptionsInternal? other)
		{
			if (other != null)
			{
				this.Flags = other.Value.Flags;
				this.UseManualAudioInput = other.Value.UseManualAudioInput;
				this.UseManualAudioOutput = other.Value.UseManualAudioOutput;
				this.LocalAudioDeviceInputStartsMuted = other.Value.LocalAudioDeviceInputStartsMuted;
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00017A02 File Offset: 0x00015C02
		public void Set(object other)
		{
			this.Set(other as LocalRTCOptionsInternal?);
		}
	}
}
