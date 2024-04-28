using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A6 RID: 422
	public class ToolbotDualWield : ToolbotDualWieldBase
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldAllowPrimarySkills
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00020A90 File Offset: 0x0001EC90
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(ToolbotDualWield.animLayer, ToolbotDualWield.animStateName);
			Util.PlaySound(ToolbotDualWield.enterSoundString, base.gameObject);
			this.loopSoundID = Util.PlaySound(ToolbotDualWield.startLoopSoundString, base.gameObject);
			if (ToolbotDualWield.coverPrefab)
			{
				Transform transform = base.FindModelChild("LowerArmL");
				Transform transform2 = base.FindModelChild("LowerArmR");
				if (transform)
				{
					this.coverLeftInstance = UnityEngine.Object.Instantiate<GameObject>(ToolbotDualWield.coverPrefab, transform.position, transform.rotation, transform);
				}
				if (transform2)
				{
					this.coverRightInstance = UnityEngine.Object.Instantiate<GameObject>(ToolbotDualWield.coverPrefab, transform2.position, transform2.rotation, transform2);
				}
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00020B48 File Offset: 0x0001ED48
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				bool flag = this.IsKeyDownAuthority(base.skillLocator, base.inputBank);
				this.keyReleased |= !flag;
				if (this.keyReleased && flag)
				{
					this.outer.SetNextState(new ToolbotDualWieldEnd
					{
						activatorSkillSlot = base.activatorSkillSlot
					});
				}
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		public override void OnExit()
		{
			Util.PlaySound(ToolbotDualWield.exitSoundString, base.gameObject);
			Util.PlaySound(ToolbotDualWield.stopLoopSoundString, base.gameObject);
			AkSoundEngine.StopPlayingID(this.loopSoundID);
			if (this.coverLeftInstance)
			{
				EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "LowerArmL", false);
				EntityState.Destroy(this.coverLeftInstance);
			}
			if (this.coverRightInstance)
			{
				EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "LowerArmR", false);
				EntityState.Destroy(this.coverRightInstance);
			}
			base.OnExit();
		}

		// Token: 0x04000929 RID: 2345
		public static string animLayer;

		// Token: 0x0400092A RID: 2346
		public static string animStateName;

		// Token: 0x0400092B RID: 2347
		public static GameObject coverPrefab;

		// Token: 0x0400092C RID: 2348
		public static GameObject coverEjectEffect;

		// Token: 0x0400092D RID: 2349
		public static string enterSoundString;

		// Token: 0x0400092E RID: 2350
		public static string exitSoundString;

		// Token: 0x0400092F RID: 2351
		public static string startLoopSoundString;

		// Token: 0x04000930 RID: 2352
		public static string stopLoopSoundString;

		// Token: 0x04000931 RID: 2353
		private bool keyReleased;

		// Token: 0x04000932 RID: 2354
		private GameObject coverLeftInstance;

		// Token: 0x04000933 RID: 2355
		private GameObject coverRightInstance;

		// Token: 0x04000934 RID: 2356
		private uint loopSoundID;
	}
}
