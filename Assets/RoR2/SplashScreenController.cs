using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A4 RID: 2212
	public class SplashScreenController : MonoBehaviour
	{
		// Token: 0x06003101 RID: 12545 RVA: 0x000D029A File Offset: 0x000CE49A
		private void Start()
		{
			if (SplashScreenController.cvSplashSkip.value)
			{
				this.Finish();
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000D02AE File Offset: 0x000CE4AE
		public void Finish()
		{
			Console.instance.SubmitCmd(null, IntroCutsceneController.shouldSkip ? "set_scene title" : "set_scene intro", false);
		}

		// Token: 0x040032A0 RID: 12960
		private static BoolConVar cvSplashSkip = new BoolConVar("splash_skip", ConVarFlags.Archive, "0", "Whether or not to skip startup splash screens.");
	}
}
