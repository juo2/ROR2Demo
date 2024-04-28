using System;
using RoR2;
using UnityEngine;

namespace EntityStates.UrchinTurret.Weapon
{
	// Token: 0x0200016F RID: 367
	public class MinigunSpinUp : MinigunState
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = MinigunSpinUp.baseDuration / this.attackSpeedStat;
			Util.PlaySound(MinigunSpinUp.sound, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "EnterShootLoop", 0.2f);
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

		// Token: 0x06000666 RID: 1638 RVA: 0x0001B788 File Offset: 0x00019988
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new MinigunFire());
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001B7B6 File Offset: 0x000199B6
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x040007C1 RID: 1985
		public static float baseDuration;

		// Token: 0x040007C2 RID: 1986
		public static string sound;

		// Token: 0x040007C3 RID: 1987
		public static GameObject chargeEffectPrefab;

		// Token: 0x040007C4 RID: 1988
		private GameObject chargeInstance;

		// Token: 0x040007C5 RID: 1989
		private float duration;
	}
}
