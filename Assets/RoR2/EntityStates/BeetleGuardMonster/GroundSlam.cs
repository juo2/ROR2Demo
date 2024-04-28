using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleGuardMonster
{
	// Token: 0x0200046D RID: 1133
	public class GroundSlam : BaseState
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x0005A7F8 File Offset: 0x000589F8
		private void EnableIndicator(Transform indicator)
		{
			if (indicator)
			{
				indicator.gameObject.SetActive(true);
				ObjectScaleCurve component = indicator.gameObject.GetComponent<ObjectScaleCurve>();
				if (component)
				{
					component.time = 0f;
				}
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0005A838 File Offset: 0x00058A38
		private void DisableIndicator(Transform indicator)
		{
			if (indicator)
			{
				indicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0005A850 File Offset: 0x00058A50
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlaySound(GroundSlam.initialAttackSoundString, base.gameObject);
			base.characterDirection;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = GroundSlam.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = GroundSlam.hitEffectPrefab;
			this.attack.forceVector = Vector3.up * GroundSlam.forceMagnitude;
			if (this.modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "GroundSlam");
			}
			this.duration = GroundSlam.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "GroundSlam", "GroundSlam.playbackRate", this.duration, 0.2f);
			if (this.modelTransform)
			{
				this.modelChildLocator = this.modelTransform.GetComponent<ChildLocator>();
				if (this.modelChildLocator)
				{
					GameObject original = GroundSlam.chargeEffectPrefab;
					Transform transform = this.modelChildLocator.FindChild("HandL");
					Transform transform2 = this.modelChildLocator.FindChild("HandR");
					if (transform)
					{
						this.leftHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform);
					}
					if (transform2)
					{
						this.rightHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform2);
					}
					this.groundSlamIndicatorInstance = this.modelChildLocator.FindChild("GroundSlamIndicator");
					this.EnableIndicator(this.groundSlamIndicatorInstance);
				}
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0005AA3C File Offset: 0x00058C3C
		public override void OnExit()
		{
			EntityState.Destroy(this.leftHandChargeEffect);
			EntityState.Destroy(this.rightHandChargeEffect);
			this.DisableIndicator(this.groundSlamIndicatorInstance);
			base.characterDirection;
			base.OnExit();
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005AA74 File Offset: 0x00058C74
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("GroundSlam.hitBoxActive") > 0.5f && !this.hasAttacked)
			{
				if (NetworkServer.active)
				{
					this.attack.Fire(null);
				}
				if (base.isAuthority && this.modelTransform)
				{
					this.DisableIndicator(this.groundSlamIndicatorInstance);
					EffectManager.SimpleMuzzleFlash(GroundSlam.slamEffectPrefab, base.gameObject, "SlamZone", true);
				}
				this.hasAttacked = true;
				EntityState.Destroy(this.leftHandChargeEffect);
				EntityState.Destroy(this.rightHandChargeEffect);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A11 RID: 6673
		public static float baseDuration = 3.5f;

		// Token: 0x04001A12 RID: 6674
		public static float damageCoefficient = 4f;

		// Token: 0x04001A13 RID: 6675
		public static float forceMagnitude = 16f;

		// Token: 0x04001A14 RID: 6676
		private OverlapAttack attack;

		// Token: 0x04001A15 RID: 6677
		public static string initialAttackSoundString;

		// Token: 0x04001A16 RID: 6678
		public static GameObject chargeEffectPrefab;

		// Token: 0x04001A17 RID: 6679
		public static GameObject slamEffectPrefab;

		// Token: 0x04001A18 RID: 6680
		public static GameObject hitEffectPrefab;

		// Token: 0x04001A19 RID: 6681
		private Animator modelAnimator;

		// Token: 0x04001A1A RID: 6682
		private Transform modelTransform;

		// Token: 0x04001A1B RID: 6683
		private bool hasAttacked;

		// Token: 0x04001A1C RID: 6684
		private float duration;

		// Token: 0x04001A1D RID: 6685
		private GameObject leftHandChargeEffect;

		// Token: 0x04001A1E RID: 6686
		private GameObject rightHandChargeEffect;

		// Token: 0x04001A1F RID: 6687
		private ChildLocator modelChildLocator;

		// Token: 0x04001A20 RID: 6688
		private Transform groundSlamIndicatorInstance;
	}
}
