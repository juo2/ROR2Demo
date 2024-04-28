using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GolemMonster
{
	// Token: 0x02000369 RID: 873
	public class ChargeLaser : BaseState
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x000454D8 File Offset: 0x000436D8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeLaser.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			this.chargePlayID = Util.PlayAttackSpeedSound(ChargeLaser.attackSoundString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleLaser");
					if (transform)
					{
						if (ChargeLaser.effectPrefab)
						{
							this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeLaser.effectPrefab, transform.position, transform.rotation);
							this.chargeEffect.transform.parent = transform;
							ScaleParticleSystemDuration component2 = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
							if (component2)
							{
								component2.newDuration = this.duration;
							}
						}
						if (ChargeLaser.laserPrefab)
						{
							this.laserEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeLaser.laserPrefab, transform.position, transform.rotation);
							this.laserEffect.transform.parent = transform;
							this.laserLineComponent = this.laserEffect.GetComponent<LineRenderer>();
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
			this.flashTimer = 0f;
			this.laserOn = true;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00045628 File Offset: 0x00043828
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.chargePlayID);
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			if (this.laserEffect)
			{
				EntityState.Destroy(this.laserEffect);
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00045678 File Offset: 0x00043878
		public override void Update()
		{
			base.Update();
			if (this.laserEffect && this.laserLineComponent)
			{
				float num = 1000f;
				Ray aimRay = base.GetAimRay();
				Vector3 position = this.laserEffect.transform.parent.position;
				Vector3 point = aimRay.GetPoint(num);
				this.laserDirection = point - position;
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, num, LayerIndex.world.mask | LayerIndex.entityPrecise.mask))
				{
					point = raycastHit.point;
				}
				this.laserLineComponent.SetPosition(0, position);
				this.laserLineComponent.SetPosition(1, point);
				float num2;
				if (this.duration - base.age > 0.5f)
				{
					num2 = base.age / this.duration;
				}
				else
				{
					this.flashTimer -= Time.deltaTime;
					if (this.flashTimer <= 0f)
					{
						this.laserOn = !this.laserOn;
						this.flashTimer = 0.033333335f;
					}
					num2 = (this.laserOn ? 1f : 0f);
				}
				num2 *= ChargeLaser.laserMaxWidth;
				this.laserLineComponent.startWidth = num2;
				this.laserLineComponent.endWidth = num2;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000457D4 File Offset: 0x000439D4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireLaser fireLaser = new FireLaser();
				fireLaser.laserDirection = this.laserDirection;
				this.outer.SetNextState(fireLaser);
				return;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001407 RID: 5127
		public static float baseDuration = 3f;

		// Token: 0x04001408 RID: 5128
		public static float laserMaxWidth = 0.2f;

		// Token: 0x04001409 RID: 5129
		public static GameObject effectPrefab;

		// Token: 0x0400140A RID: 5130
		public static GameObject laserPrefab;

		// Token: 0x0400140B RID: 5131
		public static string attackSoundString;

		// Token: 0x0400140C RID: 5132
		private float duration;

		// Token: 0x0400140D RID: 5133
		private uint chargePlayID;

		// Token: 0x0400140E RID: 5134
		private GameObject chargeEffect;

		// Token: 0x0400140F RID: 5135
		private GameObject laserEffect;

		// Token: 0x04001410 RID: 5136
		private LineRenderer laserLineComponent;

		// Token: 0x04001411 RID: 5137
		private Vector3 laserDirection;

		// Token: 0x04001412 RID: 5138
		private Vector3 visualEndPosition;

		// Token: 0x04001413 RID: 5139
		private float flashTimer;

		// Token: 0x04001414 RID: 5140
		private bool laserOn;
	}
}
