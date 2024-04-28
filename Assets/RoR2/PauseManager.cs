using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000985 RID: 2437
	public static class PauseManager
	{
		// Token: 0x0600374F RID: 14159 RVA: 0x000E9864 File Offset: 0x000E7A64
		[ConCommand(commandName = "pause", flags = ConVarFlags.None, helpText = "Toggles game pause state.")]
		private static void CCTogglePause(ConCommandArgs args)
		{
			if (PauseManager.pauseScreenInstance)
			{
				UnityEngine.Object.Destroy(PauseManager.pauseScreenInstance);
				PauseManager.pauseScreenInstance = null;
				return;
			}
			if (NetworkManager.singleton.isNetworkActive)
			{
				PauseManager.pauseScreenInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/PauseScreen"), RoR2Application.instance.transform);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000E98B8 File Offset: 0x000E7AB8
		public static bool isPaused
		{
			get
			{
				return PauseManager.pauseScreenInstance;
			}
		}

		// Token: 0x040037AD RID: 14253
		private static GameObject pauseScreenInstance;

		// Token: 0x040037AE RID: 14254
		public static Action onPauseStartGlobal;

		// Token: 0x040037AF RID: 14255
		public static Action onPauseEndGlobal;
	}
}
