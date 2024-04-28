using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBruiser.Weapon
{
	// Token: 0x02000400 RID: 1024
	public class MinigunSpinUp : MinigunState
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x000522EC File Offset: 0x000504EC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = MinigunSpinUp.baseDuration / this.attackSpeedStat;
			Util.PlaySound(MinigunSpinUp.sound, base.gameObject);
			base.GetModelAnimator().SetBool("WeaponIsReady", true);
			if (this.muzzleTransform && MinigunSpinUp.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(MinigunSpinUp.chargeEffectPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
				this.chargeInstance.transform.parent = this.muzzleTransform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000523A8 File Offset: 0x000505A8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new MinigunFire());
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000523D6 File Offset: 0x000505D6
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x040017AE RID: 6062
		public static float baseDuration;

		// Token: 0x040017AF RID: 6063
		public static string sound;

		// Token: 0x040017B0 RID: 6064
		public static GameObject chargeEffectPrefab;

		// Token: 0x040017B1 RID: 6065
		private GameObject chargeInstance;

		// Token: 0x040017B2 RID: 6066
		private float duration;
	}
}
