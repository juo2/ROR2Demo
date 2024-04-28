using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x0200049E RID: 1182
	public class Throw : BaseState
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x0005E184 File Offset: 0x0005C384
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Throw.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.rightMuzzleTransform = component.FindChild("MuzzleRight");
				}
			}
			if (this.modelAnimator)
			{
				int layerIndex = this.modelAnimator.GetLayerIndex("Gesture");
				if (this.modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("Throw1"))
				{
					base.PlayCrossfade("Gesture", "Throw2", "Throw.playbackRate", this.duration / (1f - Throw.returnToIdlePercentage), 0.2f);
				}
				else
				{
					base.PlayCrossfade("Gesture", "Throw1", "Throw.playbackRate", this.duration / (1f - Throw.returnToIdlePercentage), 0.2f);
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005E298 File Offset: 0x0005C498
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Throw.activate") > 0f && !this.hasSwung)
			{
				Ray aimRay = base.GetAimRay();
				Vector3 forward = aimRay.direction;
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, (float)LayerIndex.world.mask))
				{
					forward = raycastHit.point - this.rightMuzzleTransform.position;
				}
				ProjectileManager.instance.FireProjectile(Throw.projectilePrefab, this.rightMuzzleTransform.position, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * Throw.damageCoefficient, Throw.forceMagnitude, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				EffectManager.SimpleMuzzleFlash(Throw.swingEffectPrefab, base.gameObject, "RightSwingCenter", true);
				this.hasSwung = true;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
		private static void PullEnemies(Vector3 position, Vector3 direction, float coneAngle, float maxDistance, float force, TeamIndex excludedTeam)
		{
			float num = Mathf.Cos(coneAngle * 0.5f * 0.017453292f);
			foreach (Collider collider in Physics.OverlapSphere(position, maxDistance))
			{
				Vector3 position2 = collider.transform.position;
				Vector3 normalized = (position - position2).normalized;
				if (Vector3.Dot(-normalized, direction) >= num)
				{
					TeamComponent component = collider.GetComponent<TeamComponent>();
					if (component)
					{
						TeamIndex teamIndex = component.teamIndex;
						if (teamIndex != excludedTeam)
						{
							CharacterMotor component2 = collider.GetComponent<CharacterMotor>();
							if (component2)
							{
								component2.ApplyForce(normalized * force, false, false);
							}
							Rigidbody component3 = collider.GetComponent<Rigidbody>();
							if (component3)
							{
								component3.AddForce(normalized * force, ForceMode.Impulse);
							}
						}
					}
				}
			}
		}

		// Token: 0x04001B08 RID: 6920
		public static float baseDuration = 3.5f;

		// Token: 0x04001B09 RID: 6921
		public static float returnToIdlePercentage;

		// Token: 0x04001B0A RID: 6922
		public static float damageCoefficient = 4f;

		// Token: 0x04001B0B RID: 6923
		public static float forceMagnitude = 16f;

		// Token: 0x04001B0C RID: 6924
		public static float radius = 3f;

		// Token: 0x04001B0D RID: 6925
		public static GameObject projectilePrefab;

		// Token: 0x04001B0E RID: 6926
		public static GameObject swingEffectPrefab;

		// Token: 0x04001B0F RID: 6927
		private Transform rightMuzzleTransform;

		// Token: 0x04001B10 RID: 6928
		private Animator modelAnimator;

		// Token: 0x04001B11 RID: 6929
		private float duration;

		// Token: 0x04001B12 RID: 6930
		private bool hasSwung;
	}
}
