using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.EngiTurret
{
	// Token: 0x02000389 RID: 905
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x00047450 File Offset: 0x00045650
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.PlayInFixedTime("Death", layerIndex);
				modelAnimator.Update(0f);
				this.deathDuration = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).length;
				if (this.initialExplosion)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.initialExplosion, base.transform.position, base.transform.rotation, base.transform);
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000474DC File Offset: 0x000456DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.deathDuration && NetworkServer.active && this.deathExplosion)
			{
				EffectManager.SpawnEffect(this.deathExplosion, new EffectData
				{
					origin = base.transform.position,
					scale = 2f
				}, true);
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x0400149F RID: 5279
		[SerializeField]
		public GameObject initialExplosion;

		// Token: 0x040014A0 RID: 5280
		[SerializeField]
		public GameObject deathExplosion;

		// Token: 0x040014A1 RID: 5281
		private float deathDuration;
	}
}
