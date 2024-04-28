using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GolemMonster
{
	// Token: 0x0200036C RID: 876
	public class FireLaser : BaseState
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00045B34 File Offset: 0x00043D34
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLaser.baseDuration / this.attackSpeedStat;
			this.modifiedAimRay = base.GetAimRay();
			this.modifiedAimRay.direction = this.laserDirection;
			base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			Util.PlaySound(FireLaser.attackSoundString, base.gameObject);
			string text = "MuzzleLaser";
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
			base.PlayAnimation("Gesture", "FireLaser", "FireLaser.playbackRate", this.duration);
			if (FireLaser.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireLaser.effectPrefab, base.gameObject, text, false);
			}
			if (base.isAuthority)
			{
				float num = 1000f;
				Vector3 vector = this.modifiedAimRay.origin + this.modifiedAimRay.direction * num;
				RaycastHit raycastHit;
				if (Physics.Raycast(this.modifiedAimRay, out raycastHit, num, LayerIndex.world.mask | LayerIndex.defaultLayer.mask | LayerIndex.entityPrecise.mask))
				{
					vector = raycastHit.point;
				}
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * FireLaser.damageCoefficient,
					baseForce = FireLaser.force * 0.2f,
					position = vector,
					radius = FireLaser.blastRadius,
					falloffModel = BlastAttack.FalloffModel.SweetSpot,
					bonusForce = FireLaser.force * this.modifiedAimRay.direction
				}.Fire();
				Vector3 origin = this.modifiedAimRay.origin;
				if (modelTransform)
				{
					ChildLocator component = modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						int childIndex = component.FindChildIndex(text);
						if (FireLaser.tracerEffectPrefab)
						{
							EffectData effectData = new EffectData
							{
								origin = vector,
								start = this.modifiedAimRay.origin
							};
							effectData.SetChildLocatorTransformReference(base.gameObject, childIndex);
							EffectManager.SpawnEffect(FireLaser.tracerEffectPrefab, effectData, true);
							EffectManager.SpawnEffect(FireLaser.hitEffectPrefab, effectData, true);
						}
					}
				}
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00045D89 File Offset: 0x00043F89
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001421 RID: 5153
		public static GameObject effectPrefab;

		// Token: 0x04001422 RID: 5154
		public static GameObject hitEffectPrefab;

		// Token: 0x04001423 RID: 5155
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001424 RID: 5156
		public static float damageCoefficient;

		// Token: 0x04001425 RID: 5157
		public static float blastRadius;

		// Token: 0x04001426 RID: 5158
		public static float force;

		// Token: 0x04001427 RID: 5159
		public static float minSpread;

		// Token: 0x04001428 RID: 5160
		public static float maxSpread;

		// Token: 0x04001429 RID: 5161
		public static int bulletCount;

		// Token: 0x0400142A RID: 5162
		public static float baseDuration = 2f;

		// Token: 0x0400142B RID: 5163
		public static string attackSoundString;

		// Token: 0x0400142C RID: 5164
		public Vector3 laserDirection;

		// Token: 0x0400142D RID: 5165
		private float duration;

		// Token: 0x0400142E RID: 5166
		private Ray modifiedAimRay;
	}
}
