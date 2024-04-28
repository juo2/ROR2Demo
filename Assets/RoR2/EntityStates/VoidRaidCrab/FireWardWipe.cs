using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011C RID: 284
	public class FireWardWipe : BaseWardWipeState
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00015904 File Offset: 0x00013B04
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.muzzleFlashPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			}
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
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

		// Token: 0x06000500 RID: 1280 RVA: 0x00015A3C File Offset: 0x00013C3C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x040005D2 RID: 1490
		[SerializeField]
		public float duration;

		// Token: 0x040005D3 RID: 1491
		[SerializeField]
		public string muzzleName;

		// Token: 0x040005D4 RID: 1492
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x040005D5 RID: 1493
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005D6 RID: 1494
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005D7 RID: 1495
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005D8 RID: 1496
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040005D9 RID: 1497
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x040005DA RID: 1498
		[SerializeField]
		public SkillDef nextSkillDef;

		// Token: 0x040005DB RID: 1499
		[SerializeField]
		public BuffDef requiredBuffToKill;
	}
}
