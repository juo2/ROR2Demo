using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000777 RID: 1911
	public class IntroCutsceneController : MonoBehaviour
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000AC88E File Offset: 0x000AAA8E
		public static bool shouldSkip
		{
			get
			{
				return IntroCutsceneController.cvIntroSkip.value;
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000AC89A File Offset: 0x000AAA9A
		public void Finish()
		{
			Console.instance.SubmitCmd(null, "set_scene title", false);
		}

		// Token: 0x04002BA1 RID: 11169
		private static BoolConVar cvIntroSkip = new BoolConVar("intro_skip", ConVarFlags.Archive, "0", "Whether or not to skip the opening cutscene.");
	}
}
