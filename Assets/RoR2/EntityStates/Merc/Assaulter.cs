using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000274 RID: 628
	public class Assaulter : BaseState
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002CDA5 File Offset: 0x0002AFA5
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x0002CDAD File Offset: 0x0002AFAD
		public bool hasHit { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x0002CDBE File Offset: 0x0002AFBE
		public int dashIndex { private get; set; }

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002CDC8 File Offset: 0x0002AFC8
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(Assaulter.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
				if (this.childLocator)
				{
					this.childLocator.FindChild("PreDashEffect").gameObject.SetActive(true);
				}
			}
			base.SmallHop(base.characterMotor, Assaulter.smallHopVelocity);
			base.PlayAnimation("FullBody, Override", "AssaulterPrep", "AssaulterPrep.playbackRate", Assaulter.dashPrepDuration);
			this.dashVector = base.inputBank.aimDirection;
			this.overlapAttack = base.InitMeleeOverlap(Assaulter.damageCoefficient, Assaulter.hitEffectPrefab, this.modelTransform, "Assaulter");
			this.overlapAttack.damageType = DamageType.Stun1s;
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002CF18 File Offset: 0x0002B118
		private void CreateDashEffect()
		{
			Transform transform = this.childLocator.FindChild("DashCenter");
			if (transform && Assaulter.dashPrefab)
			{
				UnityEngine.Object.Instantiate<GameObject>(Assaulter.dashPrefab, transform.position, Util.QuaternionSafeLookRotation(this.dashVector), transform);
			}
			if (this.childLocator)
			{
				this.childLocator.FindChild("PreDashEffect").gameObject.SetActive(false);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002CF90 File Offset: 0x0002B190
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterDirection.forward = this.dashVector;
			if (this.stopwatch > Assaulter.dashPrepDuration / this.attackSpeedStat && !this.isDashing)
			{
				this.isDashing = true;
				this.dashVector = base.inputBank.aimDirection;
				this.CreateDashEffect();
				base.PlayCrossfade("FullBody, Override", "AssaulterLoop", 0.1f);
				base.gameObject.layer = LayerIndex.fakeActor.intVal;
				base.characterMotor.Motor.RebuildCollidableLayers();
				if (this.modelTransform)
				{
					TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = 0.7f;
					temporaryOverlay.animateShaderAlpha = true;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEnergized");
					temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
				}
			}
			if (!this.isDashing)
			{
				this.stopwatch += Time.fixedDeltaTime;
			}
			else if (base.isAuthority)
			{
				base.characterMotor.velocity = Vector3.zero;
				if (!this.inHitPause)
				{
					bool flag = this.overlapAttack.Fire(null);
					this.stopwatch += Time.fixedDeltaTime;
					if (flag)
					{
						if (!this.hasHit)
						{
							this.hasHit = true;
						}
						this.inHitPause = true;
						this.hitPauseTimer = Assaulter.hitPauseDuration / this.attackSpeedStat;
						if (this.modelTransform)
						{
							TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
							temporaryOverlay2.duration = Assaulter.hitPauseDuration / this.attackSpeedStat;
							temporaryOverlay2.animateShaderAlpha = true;
							temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
							temporaryOverlay2.destroyComponentOnEnd = true;
							temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEvisTarget");
							temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
						}
					}
					base.characterMotor.rootMotion += this.dashVector * this.moveSpeedStat * Assaulter.speedCoefficient * Time.fixedDeltaTime;
				}
				else
				{
					this.hitPauseTimer -= Time.fixedDeltaTime;
					if (this.hitPauseTimer < 0f)
					{
						this.inHitPause = false;
					}
				}
			}
			if (this.stopwatch >= Assaulter.dashDuration + Assaulter.dashPrepDuration / this.attackSpeedStat && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002D240 File Offset: 0x0002B440
		public override void OnExit()
		{
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			Util.PlaySound(Assaulter.endSoundString, base.gameObject);
			if (base.isAuthority)
			{
				base.characterMotor.velocity *= 0.1f;
				base.SmallHop(base.characterMotor, Assaulter.smallHopVelocity);
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (this.childLocator)
			{
				this.childLocator.FindChild("PreDashEffect").gameObject.SetActive(false);
			}
			this.PlayAnimation("FullBody, Override", "EvisLoopExit");
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex);
			}
			base.OnExit();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002D322 File Offset: 0x0002B522
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.dashIndex);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002D338 File Offset: 0x0002B538
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.dashIndex = (int)reader.ReadByte();
		}

		// Token: 0x04000C94 RID: 3220
		private Transform modelTransform;

		// Token: 0x04000C95 RID: 3221
		public static GameObject dashPrefab;

		// Token: 0x04000C96 RID: 3222
		public static float smallHopVelocity;

		// Token: 0x04000C97 RID: 3223
		public static float dashPrepDuration;

		// Token: 0x04000C98 RID: 3224
		public static float dashDuration = 0.3f;

		// Token: 0x04000C99 RID: 3225
		public static float speedCoefficient = 25f;

		// Token: 0x04000C9A RID: 3226
		public static string beginSoundString;

		// Token: 0x04000C9B RID: 3227
		public static string endSoundString;

		// Token: 0x04000C9C RID: 3228
		public static float damageCoefficient;

		// Token: 0x04000C9D RID: 3229
		public static float procCoefficient;

		// Token: 0x04000C9E RID: 3230
		public static GameObject hitEffectPrefab;

		// Token: 0x04000C9F RID: 3231
		public static float hitPauseDuration;

		// Token: 0x04000CA0 RID: 3232
		private float stopwatch;

		// Token: 0x04000CA1 RID: 3233
		private Vector3 dashVector = Vector3.zero;

		// Token: 0x04000CA2 RID: 3234
		private Animator animator;

		// Token: 0x04000CA3 RID: 3235
		private CharacterModel characterModel;

		// Token: 0x04000CA4 RID: 3236
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04000CA5 RID: 3237
		private OverlapAttack overlapAttack;

		// Token: 0x04000CA6 RID: 3238
		private ChildLocator childLocator;

		// Token: 0x04000CA7 RID: 3239
		private bool isDashing;

		// Token: 0x04000CA8 RID: 3240
		private bool inHitPause;

		// Token: 0x04000CA9 RID: 3241
		private float hitPauseTimer;

		// Token: 0x04000CAA RID: 3242
		private CameraTargetParams.AimRequest aimRequest;
	}
}
