using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidBarnacle.Weapon
{
	// Token: 0x02000163 RID: 355
	public class ChargeFire : BaseState
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public override void OnEnter()
		{
			base.OnEnter();
			this._totalDuration = this.baseDuration / this.attackSpeedStat;
			this._crossFadeDuration = this._totalDuration * 0.25f;
			this._chargingDuration = this._totalDuration - this._crossFadeDuration;
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(this.attackSoundEffect, base.gameObject, this.attackSpeedStat);
			if (modelTransform != null)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleMouth");
					if (transform && this.chargeVfxPrefab)
					{
						this._chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeVfxPrefab, transform.position, transform.rotation, transform);
						ScaleParticleSystemDuration component2 = this._chargeVfxInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this._totalDuration;
						}
					}
				}
			}
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackRateName, this._chargingDuration, this._crossFadeDuration);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001AE04 File Offset: 0x00019004
		public override void Update()
		{
			base.Update();
			if (this._chargeVfxInstance)
			{
				Ray aimRay = base.GetAimRay();
				this._chargeVfxInstance.transform.forward = aimRay.direction;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001AE44 File Offset: 0x00019044
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

		// Token: 0x06000637 RID: 1591 RVA: 0x0001AE80 File Offset: 0x00019080
		public override void OnExit()
		{
			base.OnExit();
			if (this._chargeVfxInstance)
			{
				EntityState.Destroy(this._chargeVfxInstance);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400079A RID: 1946
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400079B RID: 1947
		[SerializeField]
		public GameObject chargeVfxPrefab;

		// Token: 0x0400079C RID: 1948
		[SerializeField]
		public string attackSoundEffect;

		// Token: 0x0400079D RID: 1949
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400079E RID: 1950
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400079F RID: 1951
		[SerializeField]
		public string animationPlaybackRateName;

		// Token: 0x040007A0 RID: 1952
		private float _chargingDuration;

		// Token: 0x040007A1 RID: 1953
		private float _totalDuration;

		// Token: 0x040007A2 RID: 1954
		private float _crossFadeDuration;

		// Token: 0x040007A3 RID: 1955
		private GameObject _chargeVfxInstance;
	}
}
