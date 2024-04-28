using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000427 RID: 1063
	public class CallSupplyDropBase : BaseSkillState
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0005525B File Offset: 0x0005345B
		private float duration
		{
			get
			{
				return CallSupplyDropBase.baseDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0005526C File Offset: 0x0005346C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.placementInfo = SetupSupplyDrop.GetPlacementInfo(base.GetAimRay(), base.gameObject);
				if (this.placementInfo.ok)
				{
					base.activatorSkillSlot.DeductStock(1);
				}
			}
			if (this.placementInfo.ok)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffect, base.gameObject, CallSupplyDropBase.muzzleString, false);
				base.characterBody.SetAimTimer(3f);
				base.PlayAnimation("Gesture, Override", "CallSupplyDrop", "CallSupplyDrop.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Additive", "CallSupplyDrop", "CallSupplyDrop.playbackRate", this.duration);
				if (NetworkServer.active)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.supplyDropPrefab, this.placementInfo.position, this.placementInfo.rotation);
					gameObject.GetComponent<TeamFilter>().teamIndex = base.teamComponent.teamIndex;
					gameObject.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
					Deployable component = gameObject.GetComponent<Deployable>();
					if (component && base.characterBody.master)
					{
						base.characterBody.master.AddDeployable(component, DeployableSlot.CaptainSupplyDrop);
					}
					ProjectileDamage component2 = gameObject.GetComponent<ProjectileDamage>();
					component2.crit = base.RollCrit();
					component2.damage = this.damageStat * CallSupplyDropBase.impactDamageCoefficient;
					component2.damageColorIndex = DamageColorIndex.Default;
					component2.force = CallSupplyDropBase.impactDamageForce;
					component2.damageType = DamageType.Generic;
					NetworkServer.Spawn(gameObject);
				}
			}
			else
			{
				base.PlayCrossfade("Gesture, Override", "BufferEmpty", 0.1f);
				base.PlayCrossfade("Gesture, Additive", "BufferEmpty", 0.1f);
			}
			EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Skillswap");
			if (entityStateMachine)
			{
				entityStateMachine.SetNextStateToMain();
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00055436 File Offset: 0x00053636
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0005545F File Offset: 0x0005365F
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			this.placementInfo.Serialize(writer);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00055474 File Offset: 0x00053674
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.placementInfo.Deserialize(reader);
		}

		// Token: 0x04001889 RID: 6281
		[SerializeField]
		public GameObject muzzleflashEffect;

		// Token: 0x0400188A RID: 6282
		[SerializeField]
		public GameObject supplyDropPrefab;

		// Token: 0x0400188B RID: 6283
		public static string muzzleString;

		// Token: 0x0400188C RID: 6284
		public static float baseDuration;

		// Token: 0x0400188D RID: 6285
		public static float impactDamageCoefficient;

		// Token: 0x0400188E RID: 6286
		public static float impactDamageForce;

		// Token: 0x0400188F RID: 6287
		public SetupSupplyDrop.PlacementInfo placementInfo;
	}
}
