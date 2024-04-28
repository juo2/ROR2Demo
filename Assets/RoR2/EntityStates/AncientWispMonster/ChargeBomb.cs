using System;
using RoR2;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x02000497 RID: 1175
	public class ChargeBomb : BaseState
	{
		// Token: 0x0600150C RID: 5388 RVA: 0x0005D5F4 File Offset: 0x0005B7F4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeBomb.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture", "ChargeBomb", "ChargeBomb.playbackRate", this.duration);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component && ChargeBomb.effectPrefab)
				{
					Transform transform = component.FindChild("MuzzleLeft");
					Transform transform2 = component.FindChild("MuzzleRight");
					if (transform)
					{
						this.chargeEffectLeft = UnityEngine.Object.Instantiate<GameObject>(ChargeBomb.effectPrefab, transform.position, transform.rotation);
						this.chargeEffectLeft.transform.parent = transform;
					}
					if (transform2)
					{
						this.chargeEffectRight = UnityEngine.Object.Instantiate<GameObject>(ChargeBomb.effectPrefab, transform2.position, transform2.rotation);
						this.chargeEffectRight.transform.parent = transform2;
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(base.GetAimRay(), out raycastHit, (float)LayerIndex.world.mask))
			{
				this.startLine = raycastHit.point;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0005D73C File Offset: 0x0005B93C
		public override void OnExit()
		{
			base.OnExit();
			EntityState.Destroy(this.chargeEffectLeft);
			EntityState.Destroy(this.chargeEffectRight);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005D75C File Offset: 0x0005B95C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float num = 0f;
			if (base.fixedAge >= num && !this.hasFired)
			{
				this.hasFired = true;
				Ray aimRay = base.GetAimRay();
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, (float)LayerIndex.world.mask))
				{
					this.endLine = raycastHit.point;
				}
				Vector3 normalized = (this.endLine - this.startLine).normalized;
				normalized.y = 0f;
				normalized.Normalize();
				for (int i = 0; i < 1; i++)
				{
					Vector3 vector = this.endLine;
					Ray ray = default(Ray);
					ray.origin = aimRay.origin;
					ray.direction = vector - aimRay.origin;
					Debug.DrawLine(ray.origin, vector, Color.red, 5f);
					if (Physics.Raycast(ray, out raycastHit, 500f, LayerIndex.world.mask))
					{
						Vector3 point = raycastHit.point;
						Quaternion rotation = Util.QuaternionSafeLookRotation(raycastHit.normal);
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeBomb.delayPrefab, point, rotation);
						DelayBlast component = gameObject.GetComponent<DelayBlast>();
						component.position = point;
						component.baseDamage = base.characterBody.damage * ChargeBomb.damageCoefficient;
						component.baseForce = 2000f;
						component.bonusForce = Vector3.up * 1000f;
						component.radius = ChargeBomb.radius;
						component.attacker = base.gameObject;
						component.inflictor = null;
						component.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
						component.maxTimer = this.duration;
						gameObject.GetComponent<TeamFilter>().teamIndex = TeamComponent.GetObjectTeam(component.attacker);
						gameObject.transform.localScale = new Vector3(ChargeBomb.radius, ChargeBomb.radius, 1f);
						ScaleParticleSystemDuration component2 = gameObject.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireBomb());
				return;
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001ADF RID: 6879
		public static float baseDuration = 3f;

		// Token: 0x04001AE0 RID: 6880
		public static GameObject effectPrefab;

		// Token: 0x04001AE1 RID: 6881
		public static GameObject delayPrefab;

		// Token: 0x04001AE2 RID: 6882
		public static float radius = 10f;

		// Token: 0x04001AE3 RID: 6883
		public static float damageCoefficient = 1f;

		// Token: 0x04001AE4 RID: 6884
		private float duration;

		// Token: 0x04001AE5 RID: 6885
		private GameObject chargeEffectLeft;

		// Token: 0x04001AE6 RID: 6886
		private GameObject chargeEffectRight;

		// Token: 0x04001AE7 RID: 6887
		private Vector3 startLine = Vector3.zero;

		// Token: 0x04001AE8 RID: 6888
		private Vector3 endLine = Vector3.zero;

		// Token: 0x04001AE9 RID: 6889
		private bool hasFired;
	}
}
