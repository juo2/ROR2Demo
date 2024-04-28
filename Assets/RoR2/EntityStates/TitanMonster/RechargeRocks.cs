using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000367 RID: 871
	public class RechargeRocks : BaseState
	{
		// Token: 0x06000FA7 RID: 4007 RVA: 0x000452B8 File Offset: 0x000434B8
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = RechargeRocks.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			Util.PlaySound(RechargeRocks.attackSoundString, base.gameObject);
			base.PlayCrossfade("Body", "RechargeRocks", "RechargeRocks.playbackRate", this.duration, 0.2f);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("LeftFist");
					if (transform && RechargeRocks.effectPrefab)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(RechargeRocks.effectPrefab, transform.position, transform.rotation);
						this.chargeEffect.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
			if (NetworkServer.active)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(RechargeRocks.rockControllerPrefab);
				gameObject.GetComponent<TitanRockController>().SetOwner(base.gameObject);
				NetworkServer.Spawn(gameObject);
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000453CB File Offset: 0x000435CB
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000453EB File Offset: 0x000435EB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040013FA RID: 5114
		public static float baseDuration = 3f;

		// Token: 0x040013FB RID: 5115
		public static float baseRechargeDuration = 2f;

		// Token: 0x040013FC RID: 5116
		public static GameObject effectPrefab;

		// Token: 0x040013FD RID: 5117
		public static string attackSoundString;

		// Token: 0x040013FE RID: 5118
		public static GameObject rockControllerPrefab;

		// Token: 0x040013FF RID: 5119
		private int rocksFired;

		// Token: 0x04001400 RID: 5120
		private float duration;

		// Token: 0x04001401 RID: 5121
		private float stopwatch;

		// Token: 0x04001402 RID: 5122
		private float rechargeStopwatch;

		// Token: 0x04001403 RID: 5123
		private GameObject chargeEffect;
	}
}
