using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D6 RID: 470
	public class PrepEnergyCannon : EnergyCannonState
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x000237F0 File Offset: 0x000219F0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepEnergyCannon.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "PrepEnergyCannon", "PrepEnergyCannon.playbackRate", this.duration, 0.1f);
			Util.PlaySound(PrepEnergyCannon.sound, base.gameObject);
			if (this.muzzleTransform && PrepEnergyCannon.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(PrepEnergyCannon.chargeEffectPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
				this.chargeInstance.transform.parent = this.muzzleTransform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000238BB File Offset: 0x00021ABB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.StartAimMode(0.5f, false);
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireEnergyCannon());
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000238F5 File Offset: 0x00021AF5
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x040009D6 RID: 2518
		public static float baseDuration;

		// Token: 0x040009D7 RID: 2519
		public static string sound;

		// Token: 0x040009D8 RID: 2520
		public static GameObject chargeEffectPrefab;

		// Token: 0x040009D9 RID: 2521
		private GameObject chargeInstance;

		// Token: 0x040009DA RID: 2522
		private float duration;
	}
}
