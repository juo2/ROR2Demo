using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Wisp1Monster
{
	// Token: 0x020000DA RID: 218
	public class ChargeEmbers : BaseState
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00010368 File Offset: 0x0000E568
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = ChargeEmbers.baseDuration / this.attackSpeedStat;
			this.soundID = Util.PlayAttackSpeedSound(ChargeEmbers.attackString, base.gameObject, this.attackSpeedStat);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("Muzzle");
					if (transform)
					{
						if (ChargeEmbers.chargeEffectPrefab)
						{
							this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeEmbers.chargeEffectPrefab, transform.position, transform.rotation);
							this.chargeEffectInstance.transform.parent = transform;
							ScaleParticleSystemDuration component2 = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
							if (component2)
							{
								component2.newDuration = this.duration;
							}
						}
						if (ChargeEmbers.laserEffectPrefab)
						{
							this.laserEffectInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeEmbers.laserEffectPrefab, transform.position, transform.rotation);
							this.laserEffectInstance.transform.parent = transform;
							this.laserEffectInstanceLineRenderer = this.laserEffectInstance.GetComponent<LineRenderer>();
						}
					}
				}
			}
			base.PlayAnimation("Body", "ChargeAttack1", "ChargeAttack1.playbackRate", this.duration);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000104CC File Offset: 0x0000E6CC
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.soundID);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			if (this.laserEffectInstance)
			{
				EntityState.Destroy(this.laserEffectInstance);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001051C File Offset: 0x0000E71C
		public override void Update()
		{
			base.Update();
			Ray aimRay = base.GetAimRay();
			float distance = 50f;
			Vector3 origin = aimRay.origin;
			Vector3 point = aimRay.GetPoint(distance);
			this.laserEffectInstanceLineRenderer.SetPosition(0, origin);
			this.laserEffectInstanceLineRenderer.SetPosition(1, point);
			Color startColor = new Color(1f, 1f, 1f, this.stopwatch / this.duration);
			Color clear = Color.clear;
			this.laserEffectInstanceLineRenderer.startColor = startColor;
			this.laserEffectInstanceLineRenderer.endColor = clear;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000105AC File Offset: 0x0000E7AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireEmbers());
				return;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040003EE RID: 1006
		public static float baseDuration = 3f;

		// Token: 0x040003EF RID: 1007
		public static GameObject chargeEffectPrefab;

		// Token: 0x040003F0 RID: 1008
		public static GameObject laserEffectPrefab;

		// Token: 0x040003F1 RID: 1009
		public static string attackString;

		// Token: 0x040003F2 RID: 1010
		private float duration;

		// Token: 0x040003F3 RID: 1011
		private float stopwatch;

		// Token: 0x040003F4 RID: 1012
		private uint soundID;

		// Token: 0x040003F5 RID: 1013
		private GameObject chargeEffectInstance;

		// Token: 0x040003F6 RID: 1014
		private GameObject laserEffectInstance;

		// Token: 0x040003F7 RID: 1015
		private LineRenderer laserEffectInstanceLineRenderer;
	}
}
