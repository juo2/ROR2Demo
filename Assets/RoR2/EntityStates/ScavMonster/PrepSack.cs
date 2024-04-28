using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DF RID: 479
	public class PrepSack : SackBaseState
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x000240B4 File Offset: 0x000222B4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepSack.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "PrepSack", "PrepSack.playbackRate", this.duration, 0.1f);
			Util.PlaySound(PrepSack.sound, base.gameObject);
			base.StartAimMode(this.duration, false);
			if (this.muzzleTransform && PrepSack.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(PrepSack.chargeEffectPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
				this.chargeInstance.transform.parent = this.muzzleTransform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002418C File Offset: 0x0002238C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new ThrowSack());
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000241BA File Offset: 0x000223BA
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x04000A04 RID: 2564
		public static float baseDuration;

		// Token: 0x04000A05 RID: 2565
		public static string sound;

		// Token: 0x04000A06 RID: 2566
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000A07 RID: 2567
		private GameObject chargeInstance;

		// Token: 0x04000A08 RID: 2568
		private float duration;
	}
}
