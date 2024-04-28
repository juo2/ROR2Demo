using System;
using RoR2;
using RoR2.Networking;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000279 RID: 633
	public class FocusedAssaultDash : BasicMeleeAttack
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002E27D File Offset: 0x0002C47D
		private Vector3 dashVelocity
		{
			get
			{
				return this.dashVector * this.moveSpeedStat * this.speedCoefficient;
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002E29C File Offset: 0x0002C49C
		public override void OnEnter()
		{
			base.OnEnter();
			this.dashVector = base.inputBank.aimDirection;
			base.gameObject.layer = LayerIndex.fakeActor.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			base.characterMotor.Motor.ForceUnground();
			base.characterMotor.velocity = Vector3.zero;
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = this.enterOverlayDuration;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = this.enterOverlayMaterial;
				temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
			}
			base.PlayCrossfade(this.enterAnimationLayerName, this.enterAnimationStateName, this.enterAnimationCrossfadeDuration);
			base.characterDirection.forward = base.characterMotor.velocity.normalized;
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002E3D0 File Offset: 0x0002C5D0
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			base.characterMotor.velocity *= this.speedCoefficientOnExit;
			base.SmallHop(base.characterMotor, this.exitSmallHop);
			Util.PlaySound(this.endSoundString, base.gameObject);
			this.PlayAnimation(this.exitAnimationLayerName, this.exitAnimationStateName);
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			base.OnExit();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002E471 File Offset: 0x0002C671
		protected override void PlayAnimation()
		{
			base.PlayAnimation();
			base.PlayCrossfade(this.enterAnimationLayerName, this.enterAnimationStateName, this.enterAnimationCrossfadeDuration);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002E494 File Offset: 0x0002C694
		protected override void AuthorityFixedUpdate()
		{
			base.AuthorityFixedUpdate();
			if (!base.authorityInHitPause)
			{
				base.characterMotor.rootMotion += this.dashVelocity * Time.fixedDeltaTime;
				base.characterDirection.forward = this.dashVelocity;
				base.characterDirection.moveVector = this.dashVelocity;
				base.characterBody.isSprinting = true;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002E503 File Offset: 0x0002C703
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			overlapAttack.damage = this.damageCoefficient * this.damageStat;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002E520 File Offset: 0x0002C720
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			float num = this.hitPauseDuration / this.attackSpeedStat;
			if (this.selfOnHitOverlayEffectPrefab && num > 0.033333335f)
			{
				EffectData effectData = new EffectData
				{
					origin = base.transform.position,
					genericFloat = this.hitPauseDuration / this.attackSpeedStat
				};
				effectData.SetNetworkedObjectReference(base.gameObject);
				EffectManager.SpawnEffect(this.selfOnHitOverlayEffectPrefab, effectData, true);
			}
			foreach (HurtBox victimHurtBox in this.hitResults)
			{
				this.currentHitCount++;
				float damageValue = base.characterBody.damage * this.delayedDamageCoefficient;
				float num2 = this.delay + this.delayPerHit * (float)this.currentHitCount;
				bool isCrit = base.RollCrit();
				FocusedAssaultDash.HandleHit(base.gameObject, victimHurtBox, damageValue, this.delayedProcCoefficient, isCrit, num2, this.orbEffect, this.delayedEffectPrefab);
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002E640 File Offset: 0x0002C840
		private static void HandleHit(GameObject attackerObject, HurtBox victimHurtBox, float damageValue, float procCoefficient, bool isCrit, float delay, GameObject orbEffectPrefab, GameObject orbImpactEffectPrefab)
		{
			if (!NetworkServer.active)
			{
				NetworkWriter networkWriter = new NetworkWriter();
				networkWriter.StartMessage(77);
				networkWriter.Write(attackerObject);
				networkWriter.Write(HurtBoxReference.FromHurtBox(victimHurtBox));
				networkWriter.Write(damageValue);
				networkWriter.Write(procCoefficient);
				networkWriter.Write(isCrit);
				networkWriter.Write(delay);
				networkWriter.WriteEffectIndex(EffectCatalog.FindEffectIndexFromPrefab(orbEffectPrefab));
				networkWriter.WriteEffectIndex(EffectCatalog.FindEffectIndexFromPrefab(orbImpactEffectPrefab));
				networkWriter.FinishMessage();
				NetworkConnection readyConnection = ClientScene.readyConnection;
				if (readyConnection == null)
				{
					return;
				}
				readyConnection.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
				return;
			}
			else
			{
				if (!victimHurtBox || !victimHurtBox.healthComponent)
				{
					return;
				}
				SetStateOnHurt.SetStunOnObject(victimHurtBox.healthComponent.gameObject, delay);
				OrbManager.instance.AddOrb(new DelayedHitOrb
				{
					attacker = attackerObject,
					target = victimHurtBox,
					damageColorIndex = DamageColorIndex.Default,
					damageValue = damageValue,
					damageType = DamageType.ApplyMercExpose,
					isCrit = isCrit,
					procChainMask = default(ProcChainMask),
					procCoefficient = procCoefficient,
					delay = delay,
					orbEffect = orbEffectPrefab,
					delayedEffectPrefab = orbImpactEffectPrefab
				});
				return;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002E764 File Offset: 0x0002C964
		[NetworkMessageHandler(msgType = 77, client = false, server = true)]
		private static void HandleReportMercFocusedAssaultHitReplaceMeLater(NetworkMessage netMsg)
		{
			GameObject attackerObject = netMsg.reader.ReadGameObject();
			HurtBox victimHurtBox = netMsg.reader.ReadHurtBoxReference().ResolveHurtBox();
			float damageValue = netMsg.reader.ReadSingle();
			float procCoefficient = netMsg.reader.ReadSingle();
			bool isCrit = netMsg.reader.ReadBoolean();
			float num = netMsg.reader.ReadSingle();
			EffectDef effectDef = EffectCatalog.GetEffectDef(netMsg.reader.ReadEffectIndex());
			GameObject orbEffectPrefab = ((effectDef != null) ? effectDef.prefab : null) ?? null;
			EffectDef effectDef2 = EffectCatalog.GetEffectDef(netMsg.reader.ReadEffectIndex());
			GameObject orbImpactEffectPrefab = ((effectDef2 != null) ? effectDef2.prefab : null) ?? null;
			FocusedAssaultDash.HandleHit(attackerObject, victimHurtBox, damageValue, procCoefficient, isCrit, num, orbEffectPrefab, orbImpactEffectPrefab);
		}

		// Token: 0x04000CE3 RID: 3299
		[SerializeField]
		public float speedCoefficientOnExit;

		// Token: 0x04000CE4 RID: 3300
		[SerializeField]
		public float speedCoefficient;

		// Token: 0x04000CE5 RID: 3301
		[SerializeField]
		public string endSoundString;

		// Token: 0x04000CE6 RID: 3302
		[SerializeField]
		public float exitSmallHop;

		// Token: 0x04000CE7 RID: 3303
		[SerializeField]
		public float delayedDamageCoefficient;

		// Token: 0x04000CE8 RID: 3304
		[SerializeField]
		public float delayedProcCoefficient;

		// Token: 0x04000CE9 RID: 3305
		[SerializeField]
		public float delay;

		// Token: 0x04000CEA RID: 3306
		[SerializeField]
		public string enterAnimationLayerName = "FullBody, Override";

		// Token: 0x04000CEB RID: 3307
		[SerializeField]
		public string enterAnimationStateName = "AssaulterLoop";

		// Token: 0x04000CEC RID: 3308
		[SerializeField]
		public float enterAnimationCrossfadeDuration = 0.1f;

		// Token: 0x04000CED RID: 3309
		[SerializeField]
		public string exitAnimationLayerName = "FullBody, Override";

		// Token: 0x04000CEE RID: 3310
		[SerializeField]
		public string exitAnimationStateName = "EvisLoopExit";

		// Token: 0x04000CEF RID: 3311
		[SerializeField]
		public Material enterOverlayMaterial;

		// Token: 0x04000CF0 RID: 3312
		[SerializeField]
		public float enterOverlayDuration = 0.7f;

		// Token: 0x04000CF1 RID: 3313
		[SerializeField]
		public GameObject delayedEffectPrefab;

		// Token: 0x04000CF2 RID: 3314
		[SerializeField]
		public GameObject orbEffect;

		// Token: 0x04000CF3 RID: 3315
		[SerializeField]
		public float delayPerHit;

		// Token: 0x04000CF4 RID: 3316
		[SerializeField]
		public GameObject selfOnHitOverlayEffectPrefab;

		// Token: 0x04000CF5 RID: 3317
		private Transform modelTransform;

		// Token: 0x04000CF6 RID: 3318
		private Vector3 dashVector;

		// Token: 0x04000CF7 RID: 3319
		private int currentHitCount;
	}
}
