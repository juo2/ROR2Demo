using System;
using RoR2;
using UnityEngine;

namespace EntityStates.NewtMonster
{
	// Token: 0x02000236 RID: 566
	public class KickFromShop : BaseState
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x000298AC File Offset: 0x00027AAC
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(KickFromShop.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Body", "Stomp", "Stomp.playbackRate", KickFromShop.duration, 0.1f);
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("StompMuzzle");
					if (transform)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(KickFromShop.chargeEffectPrefab, transform);
					}
				}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00029953 File Offset: 0x00027B53
		public override void OnExit()
		{
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00029974 File Offset: 0x00027B74
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("Stomp.hitBoxActive") > 0.5f && !this.hasAttacked)
			{
				Util.PlayAttackSpeedSound(KickFromShop.stompSoundString, base.gameObject, this.attackSpeedStat);
				EffectManager.SimpleMuzzleFlash(KickFromShop.stompEffectPrefab, base.gameObject, "HealthBarOrigin", false);
				if (SceneInfo.instance)
				{
					GameObject gameObject = SceneInfo.instance.transform.Find("KickOutOfShop").gameObject;
					if (gameObject)
					{
						gameObject.gameObject.SetActive(true);
					}
				}
				if (base.isAuthority)
				{
					HurtBoxGroup component = this.modelTransform.GetComponent<HurtBoxGroup>();
					if (component)
					{
						HurtBoxGroup hurtBoxGroup = component;
						int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
						hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
					}
				}
				this.hasAttacked = true;
				EntityState.Destroy(this.chargeEffectInstance);
			}
			if (base.fixedAge >= KickFromShop.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000BAF RID: 2991
		public static float duration = 3.5f;

		// Token: 0x04000BB0 RID: 2992
		public static string attackSoundString;

		// Token: 0x04000BB1 RID: 2993
		public static string stompSoundString;

		// Token: 0x04000BB2 RID: 2994
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000BB3 RID: 2995
		public static GameObject stompEffectPrefab;

		// Token: 0x04000BB4 RID: 2996
		private Animator modelAnimator;

		// Token: 0x04000BB5 RID: 2997
		private Transform modelTransform;

		// Token: 0x04000BB6 RID: 2998
		private bool hasAttacked;

		// Token: 0x04000BB7 RID: 2999
		private GameObject chargeEffectInstance;
	}
}
