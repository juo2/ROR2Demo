using System;
using System.Linq;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.ImpBossMonster
{
	// Token: 0x02000305 RID: 773
	public class BlinkState : BaseState
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x0003A340 File Offset: 0x00038540
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
				this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			}
			if (this.disappearWhileBlinking)
			{
				if (this.characterModel)
				{
					this.characterModel.invisibilityCount++;
				}
				if (this.hurtboxGroup)
				{
					HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
					int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
					hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
				}
				if (this.childLocator)
				{
					this.childLocator.FindChild("DustCenter").gameObject.SetActive(false);
				}
			}
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			base.gameObject.layer = LayerIndex.fakeActor.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			this.CalculateBlinkDestination();
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003A48C File Offset: 0x0003868C
		private void CalculateBlinkDestination()
		{
			Vector3 vector = Vector3.zero;
			Ray aimRay = base.GetAimRay();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.maxDistanceFilter = this.blinkDistance;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.teamMaskFilter.RemoveTeam(TeamComponent.GetObjectTeam(base.gameObject));
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
			bullseyeSearch.RefreshCandidates();
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				vector = hurtBox.transform.position - base.transform.position;
			}
			this.blinkDestination = base.transform.position;
			this.blinkStart = base.transform.position;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(base.transform.position + vector, base.characterBody.hullClassification, float.PositiveInfinity);
			groundNodes.GetNodePosition(nodeIndex, out this.blinkDestination);
			this.blinkDestination += base.transform.position - base.characterBody.footPosition;
			base.characterDirection.forward = vector;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003A5D0 File Offset: 0x000387D0
		private void CreateBlinkEffect(Vector3 origin)
		{
			if (this.blinkPrefab)
			{
				EffectData effectData = new EffectData();
				effectData.rotation = Util.QuaternionSafeLookRotation(this.blinkDestination - this.blinkStart);
				effectData.origin = origin;
				EffectManager.SpawnEffect(this.blinkPrefab, effectData, false);
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002837E File Offset: 0x0002657E
		private void SetPosition(Vector3 newPosition)
		{
			if (base.characterMotor)
			{
				base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003A620 File Offset: 0x00038820
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterMotor)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (!this.hasBlinked)
			{
				this.SetPosition(Vector3.Lerp(this.blinkStart, this.blinkDestination, base.fixedAge / this.duration));
			}
			if (base.fixedAge >= this.duration - this.destinationAlertDuration && !this.hasBlinked)
			{
				this.hasBlinked = true;
				if (this.blinkDestinationPrefab)
				{
					this.blinkDestinationInstance = UnityEngine.Object.Instantiate<GameObject>(this.blinkDestinationPrefab, this.blinkDestination, Quaternion.identity);
					this.blinkDestinationInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.destinationAlertDuration;
				}
				this.SetPosition(this.blinkDestination);
			}
			if (base.fixedAge >= this.duration)
			{
				this.ExitCleanup();
			}
			if (base.fixedAge >= this.duration + this.exitDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003A728 File Offset: 0x00038928
		private void ExitCleanup()
		{
			if (this.isExiting)
			{
				return;
			}
			this.isExiting = true;
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			Util.PlaySound(this.endSoundString, base.gameObject);
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			this.modelTransform = base.GetModelTransform();
			if (this.blastAttackDamageCoefficient > 0f)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * this.blastAttackDamageCoefficient,
					baseForce = this.blastAttackForce,
					position = this.blinkDestination,
					radius = this.blastAttackRadius,
					falloffModel = BlastAttack.FalloffModel.Linear,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
			}
			if (this.disappearWhileBlinking)
			{
				if (this.modelTransform && this.destealthMaterial)
				{
					TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = 1f;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = this.destealthMaterial;
					temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
				}
				if (this.characterModel)
				{
					this.characterModel.invisibilityCount--;
				}
				if (this.hurtboxGroup)
				{
					HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
					int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
					hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
				}
				if (this.childLocator)
				{
					this.childLocator.FindChild("DustCenter").gameObject.SetActive(true);
				}
				base.PlayAnimation("Gesture, Additive", "BlinkEnd", "BlinkEnd.playbackRate", this.exitDuration);
			}
			if (this.blinkDestinationInstance)
			{
				EntityState.Destroy(this.blinkDestinationInstance);
			}
			if (base.characterMotor)
			{
				base.characterMotor.enabled = true;
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003A963 File Offset: 0x00038B63
		public override void OnExit()
		{
			base.OnExit();
			this.ExitCleanup();
		}

		// Token: 0x040010ED RID: 4333
		private Transform modelTransform;

		// Token: 0x040010EE RID: 4334
		[SerializeField]
		public bool disappearWhileBlinking;

		// Token: 0x040010EF RID: 4335
		[SerializeField]
		public GameObject blinkPrefab;

		// Token: 0x040010F0 RID: 4336
		[SerializeField]
		public GameObject blinkDestinationPrefab;

		// Token: 0x040010F1 RID: 4337
		[SerializeField]
		public Material destealthMaterial;

		// Token: 0x040010F2 RID: 4338
		private Vector3 blinkDestination = Vector3.zero;

		// Token: 0x040010F3 RID: 4339
		private Vector3 blinkStart = Vector3.zero;

		// Token: 0x040010F4 RID: 4340
		[SerializeField]
		public float duration = 0.3f;

		// Token: 0x040010F5 RID: 4341
		[SerializeField]
		public float exitDuration;

		// Token: 0x040010F6 RID: 4342
		[SerializeField]
		public float destinationAlertDuration;

		// Token: 0x040010F7 RID: 4343
		[SerializeField]
		public float blinkDistance = 25f;

		// Token: 0x040010F8 RID: 4344
		[SerializeField]
		public string beginSoundString;

		// Token: 0x040010F9 RID: 4345
		[SerializeField]
		public string endSoundString;

		// Token: 0x040010FA RID: 4346
		[SerializeField]
		public float blastAttackRadius;

		// Token: 0x040010FB RID: 4347
		[SerializeField]
		public float blastAttackDamageCoefficient;

		// Token: 0x040010FC RID: 4348
		[SerializeField]
		public float blastAttackForce;

		// Token: 0x040010FD RID: 4349
		[SerializeField]
		public float blastAttackProcCoefficient;

		// Token: 0x040010FE RID: 4350
		private Animator animator;

		// Token: 0x040010FF RID: 4351
		private CharacterModel characterModel;

		// Token: 0x04001100 RID: 4352
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04001101 RID: 4353
		private ChildLocator childLocator;

		// Token: 0x04001102 RID: 4354
		private GameObject blinkDestinationInstance;

		// Token: 0x04001103 RID: 4355
		private bool isExiting;

		// Token: 0x04001104 RID: 4356
		private bool hasBlinked;
	}
}
