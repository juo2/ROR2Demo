using System;
using RoR2.Audio;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000556 RID: 1366
	[CreateAssetMenu(menuName = "RoR2/NetworkSoundEventDef")]
	public class NetworkSoundEventDef : ScriptableObject
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0006BCE1 File Offset: 0x00069EE1
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0006BCE9 File Offset: 0x00069EE9
		public NetworkSoundEventIndex index { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0006BCF2 File Offset: 0x00069EF2
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x0006BCFA File Offset: 0x00069EFA
		public uint akId { get; set; }

		// Token: 0x04001E69 RID: 7785
		public string eventName;
	}
}
