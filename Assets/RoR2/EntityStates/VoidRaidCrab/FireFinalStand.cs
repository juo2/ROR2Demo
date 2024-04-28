using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011B RID: 283
	public class FireFinalStand : BaseWardWipeState
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x000157D8 File Offset: 0x000139D8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.muzzleFlashPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			}
			if (this.fogDamageController)
			{
				if (NetworkServer.active)
				{
					foreach (CharacterBody characterBody in this.fogDamageController.GetAffectedBodies())
					{
						if (!this.requiredBuffToKill || characterBody.HasBuff(this.requiredBuffToKill))
						{
							characterBody.healthComponent.Suicide(base.gameObject, base.gameObject, DamageType.VoidDeath);
						}
					}
				}
				this.fogDamageController.enabled = false;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000158D8 File Offset: 0x00013AD8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x040005CA RID: 1482
		[SerializeField]
		public float duration;

		// Token: 0x040005CB RID: 1483
		[SerializeField]
		public string muzzleName;

		// Token: 0x040005CC RID: 1484
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x040005CD RID: 1485
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005CE RID: 1486
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005CF RID: 1487
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005D0 RID: 1488
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040005D1 RID: 1489
		[SerializeField]
		public BuffDef requiredBuffToKill;
	}
}
