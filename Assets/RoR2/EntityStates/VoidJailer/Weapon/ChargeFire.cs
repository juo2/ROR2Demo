using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidJailer.Weapon
{
	// Token: 0x02000156 RID: 342
	public class ChargeFire : BaseState
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00019C48 File Offset: 0x00017E48
		public override void OnEnter()
		{
			base.OnEnter();
			this._totalDuration = ChargeFire.baseDuration / this.attackSpeedStat;
			this._crossFadeDuration = this._totalDuration * 0.25f;
			this._chargingDuration = this._totalDuration - this._crossFadeDuration;
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ChargeFire.attackSoundEffect, base.gameObject, this.attackSpeedStat);
			if (modelTransform != null)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("ClawMuzzle");
					if (transform && ChargeFire.chargeVfxPrefab)
					{
						this._chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFire.chargeVfxPrefab, transform.position, transform.rotation, transform);
						ScaleParticleSystemDuration component2 = this._chargeVfxInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this._totalDuration;
						}
					}
				}
			}
			base.PlayCrossfade(ChargeFire.animationLayerName, ChargeFire.animationStateName, ChargeFire.animationPlaybackRateName, this._chargingDuration, this._crossFadeDuration);
			base.characterBody.SetAimTimer(this._totalDuration + 3f);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00019D60 File Offset: 0x00017F60
		public override void Update()
		{
			base.Update();
			if (this._chargeVfxInstance)
			{
				Ray aimRay = base.GetAimRay();
				this._chargeVfxInstance.transform.forward = aimRay.direction;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this._totalDuration && base.isAuthority)
			{
				Fire nextState = new Fire();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019DDC File Offset: 0x00017FDC
		public override void OnExit()
		{
			base.OnExit();
			if (this._chargeVfxInstance)
			{
				EntityState.Destroy(this._chargeVfxInstance);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000744 RID: 1860
		public static string attackSoundEffect;

		// Token: 0x04000745 RID: 1861
		public static string animationLayerName;

		// Token: 0x04000746 RID: 1862
		public static string animationStateName;

		// Token: 0x04000747 RID: 1863
		public static string animationPlaybackRateName;

		// Token: 0x04000748 RID: 1864
		public static float baseDuration;

		// Token: 0x04000749 RID: 1865
		public static GameObject chargeVfxPrefab;

		// Token: 0x0400074A RID: 1866
		private float _totalDuration;

		// Token: 0x0400074B RID: 1867
		private float _crossFadeDuration;

		// Token: 0x0400074C RID: 1868
		private float _chargingDuration;

		// Token: 0x0400074D RID: 1869
		private GameObject _chargeVfxInstance;
	}
}
