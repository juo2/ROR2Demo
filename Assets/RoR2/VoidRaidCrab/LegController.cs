using System;
using System.Linq;
using System.Runtime.CompilerServices;
using EntityStates.VoidRaidCrab.Joint;
using EntityStates.VoidRaidCrab.Leg;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.VoidRaidCrab
{
	// Token: 0x02000B6D RID: 2925
	public class LegController : MonoBehaviour
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x0011336D File Offset: 0x0011156D
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x00113375 File Offset: 0x00111575
		public CharacterBody mainBody { get; private set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0011337E File Offset: 0x0011157E
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x00113386 File Offset: 0x00111586
		public GameObject mainBodyGameObject { get; private set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x0011338F File Offset: 0x0011158F
		public bool mainBodyHasEffectiveAuthority
		{
			get
			{
				CharacterBody mainBody = this.mainBody;
				return mainBody != null && mainBody.hasEffectiveAuthority;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x001133A2 File Offset: 0x001115A2
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x001133AA File Offset: 0x001115AA
		public ChildLocator legChildLocator { get; private set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x001133B3 File Offset: 0x001115B3
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x001133BB File Offset: 0x001115BB
		public ChildLocator childLocator { get; private set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x001133C4 File Offset: 0x001115C4
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x001133CC File Offset: 0x001115CC
		public Transform toeTransform { get; private set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x001133D5 File Offset: 0x001115D5
		// (set) Token: 0x0600427F RID: 17023 RVA: 0x001133DD File Offset: 0x001115DD
		public Transform toeTipTransform { get; private set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x001133E6 File Offset: 0x001115E6
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x001133EE File Offset: 0x001115EE
		public Transform footTranform { get; private set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x001133F7 File Offset: 0x001115F7
		// (set) Token: 0x06004283 RID: 17027 RVA: 0x001133FF File Offset: 0x001115FF
		public CharacterMaster jointMaster { get; private set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x00113408 File Offset: 0x00111608
		public CharacterBody jointBody
		{
			get
			{
				if (!this.jointMaster)
				{
					return null;
				}
				return this.jointMaster.GetBody();
			}
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x00113424 File Offset: 0x00111624
		public bool IsBusy()
		{
			return !(this.stateMachine.state is Idle);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x0011343C File Offset: 0x0011163C
		public bool IsStomping()
		{
			return this.stateMachine.state is BaseStompState;
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x00113454 File Offset: 0x00111654
		private void OnEnable()
		{
			CharacterModel componentInParent = base.GetComponentInParent<CharacterModel>();
			this.mainBody = (componentInParent ? componentInParent.body : null);
			this.mainBodyGameObject = (this.mainBody ? this.mainBody.gameObject : null);
			this.childLocator = base.GetComponent<ChildLocator>();
			this.footTranform = this.childLocator.FindChild("Foot");
			this.toeTransform = this.childLocator.FindChild("Toe");
			this.toeTipTransform = this.childLocator.FindChild("ToeTip");
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x001134F0 File Offset: 0x001116F0
		private void FixedUpdate()
		{
			this.desiredRetractionWeight = (this.shouldRetract ? 1f : 0f);
			this.currentRetractionWeight = Mathf.Clamp01(Mathf.MoveTowards(this.currentRetractionWeight, this.desiredRetractionWeight, Time.fixedDeltaTime * this.retractionBlendTransitionRate));
			int layerIndex = this.animator.GetLayerIndex(this.retractionLayerName);
			float weight = this.retractionCurve.Evaluate(this.currentRetractionWeight);
			this.animator.SetLayerWeight(layerIndex, weight);
			if (!this.wasJointBodyDead && this.IsBreakPending())
			{
				this.wasJointBodyDead = true;
				this.isJointBodyCurrentlyDying = true;
			}
			if (this.mainBodyHasEffectiveAuthority)
			{
				this.UpdateStompTargetPositionAuthority(Time.fixedDeltaTime);
			}
			if (NetworkServer.active && !this.jointBody)
			{
				this.jointRegenStopwatchServer += Time.fixedDeltaTime;
				if (this.jointRegenStopwatchServer >= this.jointRegenDuration)
				{
					this.RegenerateServer();
				}
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x001135DB File Offset: 0x001117DB
		public bool RequestStomp(GameObject target)
		{
			if (!this.IsBusy())
			{
				this.stateMachine.SetNextState(new PreStompLegRaise
				{
					target = target
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x001135FF File Offset: 0x001117FF
		public void SetStompTargetWorldPosition(Vector3? newStompTargetWorldPosition)
		{
			this.stompTargetWorldPosition = newStompTargetWorldPosition;
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x00113608 File Offset: 0x00111808
		public bool SetJointMaster(CharacterMaster master, ChildLocator legChildLocator)
		{
			this.legChildLocator = legChildLocator;
			if (!this.jointMaster)
			{
				this.jointMaster = master;
				if (this.jointMaster)
				{
					this.jointMaster.onBodyDestroyed += this.OnJointBodyDestroyed;
					this.jointMaster.onBodyStart += this.OnJointBodyStart;
					this.MirrorLegJoints();
				}
				return true;
			}
			Debug.LogError("LegController on " + base.gameObject.name + " already has a jointMaster set!");
			return false;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x00113693 File Offset: 0x00111893
		private void OnJointBodyStart(CharacterBody body)
		{
			this.wasJointBodyDead = false;
			this.isJointBodyCurrentlyDying = false;
			this.MirrorLegJoints();
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x001136A9 File Offset: 0x001118A9
		private void OnJointBodyDestroyed(CharacterBody body)
		{
			this.isJointBodyCurrentlyDying = false;
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x001136B2 File Offset: 0x001118B2
		public bool IsSupportingWeight()
		{
			return !this.IsBroken() && !this.IsBreakPending() && !this.IsBusy();
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x001136CF File Offset: 0x001118CF
		public bool CanBreak()
		{
			return this.jointBody;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x001136DC File Offset: 0x001118DC
		public bool IsBreakPending()
		{
			if (this.jointBody)
			{
				HealthComponent healthComponent = this.jointBody.healthComponent;
				return healthComponent && !healthComponent.alive;
			}
			return false;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x00113717 File Offset: 0x00111917
		public bool IsBroken()
		{
			return this.isJointBodyCurrentlyDying || !this.jointBody;
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x001136CF File Offset: 0x001118CF
		public bool DoesJointExist()
		{
			return this.jointBody;
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x00113734 File Offset: 0x00111934
		public void CompleteBreakAuthority()
		{
			if (this.jointMaster)
			{
				CharacterBody jointBody = this.jointBody;
				if (jointBody)
				{
					HealthComponent healthComponent = jointBody.healthComponent;
					if (healthComponent)
					{
						if (NetworkServer.active)
						{
							DamageInfo damageInfo = new DamageInfo();
							damageInfo.crit = false;
							damageInfo.damage = healthComponent.fullCombinedHealth;
							damageInfo.procCoefficient = 0f;
							this.mainBody.healthComponent.TakeDamage(damageInfo);
							GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
							GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
						}
						this.mainBody.healthComponent.UpdateLastHitTime(0f, Vector3.zero, true, healthComponent.lastHitAttacker);
					}
					PreDeathState preDeathState = EntityStateMachine.FindByCustomName(jointBody.gameObject, "Body").state as PreDeathState;
					if (preDeathState != null)
					{
						preDeathState.canProceed = true;
					}
				}
			}
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x00113818 File Offset: 0x00111A18
		public void RegenerateServer()
		{
			if (this.jointMaster)
			{
				this.jointRegenStopwatchServer = 0f;
				if (!this.jointMaster.GetBody())
				{
					this.jointMaster.Respawn(this.mainBody.transform.position, this.mainBody.transform.rotation);
				}
			}
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x0011387C File Offset: 0x00111A7C
		private void MirrorLegJoints()
		{
			GameObject bodyObject = this.jointMaster.GetBodyObject();
			if (bodyObject && this.legChildLocator)
			{
				ChildLocatorMirrorController component = bodyObject.GetComponent<ChildLocatorMirrorController>();
				if (component)
				{
					component.referenceLocator = this.legChildLocator;
				}
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x001138C8 File Offset: 0x00111AC8
		private Vector2 WorldPointToLocalStompPoint(Vector3 worldPoint)
		{
			Vector3 vector = this.originTransform.InverseTransformVector(worldPoint);
			return new Vector2(vector.x, vector.z);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x001138F4 File Offset: 0x00111AF4
		private Vector2 LocalStompPointToStompParams(Vector2 stompPoint)
		{
			float x = this.originTransform.InverseTransformVector(this.stompRangeLeftMarker.position).x;
			float x2 = this.originTransform.InverseTransformVector(this.stompRangeRightMarker.position).x;
			float z = this.originTransform.InverseTransformVector(this.stompRangeNearMarker.position).z;
			float z2 = this.originTransform.InverseTransformVector(this.stompRangeFarMarker.position).z;
			float x3 = Util.Remap(this.currentLocalStompPosition.x, x, x2, -1f, 1f);
			float y = Util.Remap(this.currentLocalStompPosition.y, z, z2, -1f, 1f);
			return new Vector2(x3, y);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x001139B4 File Offset: 0x00111BB4
		private void UpdateStompTargetPositionAuthority(float deltaTime)
		{
			Vector3 worldPoint = this.toeTipTransform.position;
			if (this.stompTargetWorldPosition != null)
			{
				worldPoint = this.stompTargetWorldPosition.Value;
			}
			Vector2 target = this.WorldPointToLocalStompPoint(worldPoint);
			this.currentLocalStompPosition = Vector2.MoveTowards(this.currentLocalStompPosition, target, this.stompAimSpeed * deltaTime);
			Vector2 vector = this.LocalStompPointToStompParams(this.currentLocalStompPosition);
			this.animator.SetFloat(this.stompXParameter, vector.x);
			this.animator.SetFloat(this.stompYParameter, vector.y);
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x00113A44 File Offset: 0x00111C44
		private bool GetKneeToToeTipRaycast(out Vector3 hitPosition, out Vector3 hitNormal, out Vector3 rayNormal)
		{
			Vector3 position = this.footTranform.position;
			Vector3 position2 = this.toeTipTransform.position;
			Vector3 a = position2 - position;
			float magnitude = a.magnitude;
			if (magnitude <= Mathf.Epsilon)
			{
				hitPosition = position2;
				hitNormal = Vector3.up;
				rayNormal = Vector3.down;
				return false;
			}
			Vector3 vector = a / magnitude;
			RaycastHit[] array = Physics.RaycastAll(new Ray(position, vector), magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
			float num = float.PositiveInfinity;
			int num2 = -1;
			for (int i = 0; i < array.Length; i++)
			{
				ref RaycastHit ptr = ref array[i];
				if (ptr.distance < num && !(ptr.collider.transform.root == base.transform.root))
				{
					num2 = i;
					num = ptr.distance;
				}
			}
			rayNormal = vector;
			if (num2 != -1)
			{
				ref RaycastHit ptr2 = ref array[num2];
				hitPosition = ptr2.point;
				hitNormal = ptr2.normal;
				return true;
			}
			hitPosition = this.toeTipTransform.position;
			hitNormal = -rayNormal;
			return false;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x00113B88 File Offset: 0x00111D88
		public void DoToeConcussionBlastAuthority(Vector3? positionOverride = null, bool useEffect = true)
		{
			if (!this.mainBodyHasEffectiveAuthority)
			{
				throw new Exception("Caller does not have authority.");
			}
			Vector3 value;
			if (positionOverride != null)
			{
				value = positionOverride.Value;
			}
			else
			{
				Vector3 vector;
				Vector3 vector2;
				this.GetKneeToToeTipRaycast(out value, out vector, out vector2);
			}
			if (useEffect)
			{
				EffectData effectData = new EffectData();
				effectData.origin = value;
				effectData.scale = this.footstepBlastRadius;
				effectData.rotation = Quaternion.identity;
				EffectManager.SpawnEffect(this.footstepBlastEffectPrefab, effectData, true);
			}
			new BlastAttack
			{
				attacker = this.mainBodyGameObject,
				teamIndex = (this.mainBody ? this.mainBody.teamComponent.teamIndex : TeamIndex.None),
				attackerFiltering = AttackerFiltering.NeverHitSelf,
				inflictor = this.mainBodyGameObject,
				radius = this.footstepBlastRadius,
				position = value,
				losType = BlastAttack.LoSType.None,
				procCoefficient = 0f,
				procChainMask = default(ProcChainMask),
				baseDamage = 0f,
				baseForce = this.footstepBlastForce,
				bonusForce = this.footstepBonusForce,
				canRejectForce = false,
				crit = false,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Silent,
				impactEffect = EffectIndex.Invalid,
				falloffModel = this.footstepFalloffModel
			}.Fire();
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00113CD4 File Offset: 0x00111ED4
		public GameObject CheckForStompTarget()
		{
			Vector3 position = this.stompRangeLeftMarker.position;
			Vector3 position2 = this.stompRangeRightMarker.position;
			Vector3 position3 = this.stompRangeNearMarker.position;
			Vector3 position4 = this.stompRangeFarMarker.position;
			Vector3 vector = LegController.<CheckForStompTarget>g__Average|89_0(position, position2, position3, position4);
			float a = Vector3.Distance(vector, position);
			float b = Vector3.Distance(vector, position2);
			float c = Vector3.Distance(vector, position3);
			float d = Vector3.Distance(vector, position4);
			float num = LegController.<CheckForStompTarget>g__Min|89_1(a, b, c, d);
			float num2 = Mathf.Sqrt(2f);
			float num3 = 1f / num2;
			float maxDistanceFilter = num * num3;
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = vector;
			bullseyeSearch.minDistanceFilter = 0f;
			bullseyeSearch.maxDistanceFilter = maxDistanceFilter;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.viewer = this.mainBody;
			bullseyeSearch.teamMaskFilter = TeamMask.AllExcept(this.mainBody.teamComponent.teamIndex);
			bullseyeSearch.filterByDistinctEntity = true;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.RefreshCandidates();
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox == null)
			{
				return null;
			}
			HealthComponent healthComponent = hurtBox.healthComponent;
			if (healthComponent == null)
			{
				return null;
			}
			return healthComponent.gameObject;
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x00113E68 File Offset: 0x00112068
		[CompilerGenerated]
		internal static Vector3 <CheckForStompTarget>g__Average|89_0(in Vector3 a, in Vector3 b, in Vector3 c, in Vector3 d)
		{
			Vector3 vector = Vector3Utils.Average(a, b);
			Vector3 vector2 = Vector3Utils.Average(c, d);
			return Vector3Utils.Average(vector, vector2);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x00113E8E File Offset: 0x0011208E
		[CompilerGenerated]
		internal static float <CheckForStompTarget>g__Min|89_1(float a, float b, float c, float d)
		{
			return Mathf.Min(Mathf.Min(a, b), Mathf.Min(c, d));
		}

		// Token: 0x0400406F RID: 16495
		private const string jointDeathStateMachineName = "Body";

		// Token: 0x04004070 RID: 16496
		public EntityStateMachine stateMachine;

		// Token: 0x04004071 RID: 16497
		public Animator animator;

		// Token: 0x04004072 RID: 16498
		public string primaryLayerName;

		// Token: 0x04004073 RID: 16499
		[Tooltip("The transform that should be considered the origin of this leg, points outward from the base, and provides a transform for consistent local space conversions. This field must always be set, and is available for the case that the object this component is attached to is not a bone meeting the metioned criteria.")]
		public Transform originTransform;

		// Token: 0x04004074 RID: 16500
		[Header("Regeneration")]
		public float jointRegenDuration = 15f;

		// Token: 0x04004075 RID: 16501
		[Header("Footstep Concussive Blast")]
		public float footstepBlastRadius = 20f;

		// Token: 0x04004076 RID: 16502
		public BlastAttack.FalloffModel footstepFalloffModel = BlastAttack.FalloffModel.SweetSpot;

		// Token: 0x04004077 RID: 16503
		public float footstepBlastForce = 500f;

		// Token: 0x04004078 RID: 16504
		public Vector3 footstepBonusForce;

		// Token: 0x04004079 RID: 16505
		public GameObject footstepBlastEffectPrefab;

		// Token: 0x0400407A RID: 16506
		[Header("Stomp")]
		public Transform stompRangeNearMarker;

		// Token: 0x0400407B RID: 16507
		public Transform stompRangeFarMarker;

		// Token: 0x0400407C RID: 16508
		public Transform stompRangeLeftMarker;

		// Token: 0x0400407D RID: 16509
		public Transform stompRangeRightMarker;

		// Token: 0x0400407E RID: 16510
		public float stompAimSpeed = 15f;

		// Token: 0x0400407F RID: 16511
		public string stompXParameter;

		// Token: 0x04004080 RID: 16512
		public string stompYParameter;

		// Token: 0x04004081 RID: 16513
		public string stompPlaybackRateParam;

		// Token: 0x04004082 RID: 16514
		[Header("Retraction")]
		public AnimationCurve retractionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04004083 RID: 16515
		public string retractionLayerName;

		// Token: 0x04004084 RID: 16516
		private float currentRetractionWeight;

		// Token: 0x04004085 RID: 16517
		private float desiredRetractionWeight;

		// Token: 0x04004086 RID: 16518
		public float retractionBlendTransitionRate = 1f;

		// Token: 0x04004087 RID: 16519
		public bool shouldRetract;

		// Token: 0x04004090 RID: 16528
		[NonSerialized]
		public bool isBreakSuppressed;

		// Token: 0x04004091 RID: 16529
		private bool wasJointBodyDead;

		// Token: 0x04004092 RID: 16530
		private bool isJointBodyCurrentlyDying;

		// Token: 0x04004093 RID: 16531
		private float jointRegenStopwatchServer;

		// Token: 0x04004094 RID: 16532
		private Vector3? stompTargetWorldPosition;

		// Token: 0x04004095 RID: 16533
		private Vector2 currentLocalStompPosition = Vector2.zero;
	}
}
