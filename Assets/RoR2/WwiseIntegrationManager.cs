using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000AAB RID: 2731
	public static class WwiseIntegrationManager
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public static bool noAudio
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00103124 File Offset: 0x00101324
		public static void Init()
		{
			if (!WwiseIntegrationManager.noAudio)
			{
				if (UnityEngine.Object.FindObjectOfType<AkInitializer>())
				{
					Debug.LogError("Attempting to initialize wwise when AkInitializer already exists! This will cause a crash!");
					return;
				}
				WwiseIntegrationManager.wwiseGlobalObjectInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/WwiseGlobal"));
				WwiseIntegrationManager.wwiseAudioObjectInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/AudioManager"));
			}
		}

		// Token: 0x04003D0F RID: 15631
		private static GameObject wwiseGlobalObjectInstance;

		// Token: 0x04003D10 RID: 15632
		private static GameObject wwiseAudioObjectInstance;
	}
}
