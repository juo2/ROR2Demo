using System;
using System.Collections.ObjectModel;
using RoR2.WwiseUtils;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007C6 RID: 1990
	public class MusicController : MonoBehaviour
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000B62A2 File Offset: 0x000B44A2
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x000B62AA File Offset: 0x000B44AA
		private MusicTrackDef currentTrack
		{
			get
			{
				return this._currentTrack;
			}
			set
			{
				if (this._currentTrack == value)
				{
					return;
				}
				if (this._currentTrack != null)
				{
					this._currentTrack.Stop();
				}
				this._currentTrack = value;
				if (this._currentTrack != null)
				{
					this._currentTrack.Play();
				}
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000B62E4 File Offset: 0x000B44E4
		private void InitializeEngineDependentValues()
		{
			this.rtpcPlayerHealthValue = new RtpcSetter("playerHealth", null);
			this.rtpcEnemyValue = new RtpcSetter("enemyValue", null);
			this.rtpcTeleporterProximityValue = new RtpcSetter("teleporterProximity", null);
			this.rtpcTeleporterDirectionValue = new RtpcSetter("teleporterDirection", null);
			this.rtpcTeleporterPlayerStatus = new RtpcSetter("teleporterPlayerStatus", null);
			this.stBossStatus = new StateSetter("bossStatus");
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000B6356 File Offset: 0x000B4556
		private void Start()
		{
			this.enemyInfoBuffer = new NativeArray<MusicController.EnemyInfo>(64, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.InitializeEngineDependentValues();
			if (this.enableMusicSystem)
			{
				AkSoundEngine.PostEvent("Play_Music_System", base.gameObject);
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000B6388 File Offset: 0x000B4588
		private void Update()
		{
			this.UpdateState();
			this.targetCamera = ((CameraRigController.readOnlyInstancesList.Count > 0) ? CameraRigController.readOnlyInstancesList[0] : null);
			this.target = (this.targetCamera ? this.targetCamera.target : null);
			this.ScheduleIntensityCalculation(this.target);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000B63EC File Offset: 0x000B45EC
		private void RecalculateHealth(GameObject playerObject)
		{
			this.rtpcPlayerHealthValue.value = 100f;
			if (this.target)
			{
				CharacterBody component = this.target.GetComponent<CharacterBody>();
				if (component)
				{
					if (component.HasBuff(JunkContent.Buffs.Deafened))
					{
						this.rtpcPlayerHealthValue.value = -100f;
						return;
					}
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						this.rtpcPlayerHealthValue.value = healthComponent.combinedHealthFraction * 100f;
					}
				}
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000B6470 File Offset: 0x000B4670
		private void UpdateTeleporterParameters(TeleporterInteraction teleporter, Transform cameraTransform, CharacterBody targetBody)
		{
			float value = float.PositiveInfinity;
			float value2 = 0f;
			bool flag = true;
			this.stBossStatus.valueId = CommonWwiseIds.alive;
			if (teleporter)
			{
				if (cameraTransform)
				{
					Vector3 position = cameraTransform.position;
					Vector3 forward = cameraTransform.forward;
					Vector3 vector = teleporter.transform.position - position;
					float num = Vector2.SignedAngle(new Vector2(vector.x, vector.z), new Vector2(forward.x, forward.z));
					if (num < 0f)
					{
						num += 360f;
					}
					value = vector.magnitude;
					value2 = num;
				}
				if (TeleporterInteraction.instance.isIdleToCharging || TeleporterInteraction.instance.isCharging)
				{
					this.stBossStatus.valueId = CommonWwiseIds.alive;
				}
				else if (TeleporterInteraction.instance.isCharged)
				{
					this.stBossStatus.valueId = CommonWwiseIds.dead;
				}
				if (teleporter.isCharging && targetBody)
				{
					flag = teleporter.holdoutZoneController.IsBodyInChargingRadius(targetBody);
				}
			}
			value = Mathf.Clamp(value, 20f, 250f);
			this.rtpcTeleporterProximityValue.value = Util.Remap(value, 20f, 250f, 0f, 10000f);
			this.rtpcTeleporterDirectionValue.value = value2;
			this.rtpcTeleporterPlayerStatus.value = (flag ? 1f : 0f);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000B65DC File Offset: 0x000B47DC
		private void LateUpdate()
		{
			bool flag = Time.timeScale == 0f;
			if (this.wasPaused != flag)
			{
				AkSoundEngine.PostEvent(flag ? "Pause_Music" : "Unpause_Music", base.gameObject);
				this.wasPaused = flag;
			}
			this.RecalculateHealth(this.target);
			this.UpdateTeleporterParameters(TeleporterInteraction.instance, this.targetCamera ? this.targetCamera.transform : null, this.target ? this.target.GetComponent<CharacterBody>() : null);
			this.calculateIntensityJobHandle.Complete();
			float num;
			float num2;
			this.calculateIntensityJob.CalculateSum(out num, out num2);
			float num3 = 0.025f;
			Mathf.Clamp(1f - this.rtpcPlayerHealthValue.value * 0.01f, 0.25f, 0.75f);
			float value = (num * 0.75f + num2 * 0.25f) * num3;
			this.rtpcEnemyValue.value = value;
			this.FlushValuesToEngine();
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000B66DC File Offset: 0x000B48DC
		private void FlushValuesToEngine()
		{
			this.stBossStatus.FlushIfChanged();
			this.rtpcPlayerHealthValue.FlushIfChanged();
			this.rtpcTeleporterProximityValue.FlushIfChanged();
			this.rtpcTeleporterDirectionValue.FlushIfChanged();
			this.rtpcTeleporterPlayerStatus.FlushIfChanged();
			this.rtpcEnemyValue.FlushIfChanged();
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000B672C File Offset: 0x000B492C
		private void PickCurrentTrack(ref MusicTrackDef newTrack)
		{
			SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
			bool flag = false;
			if (TeleporterInteraction.instance && !TeleporterInteraction.instance.isIdle)
			{
				flag = true;
			}
			if (mostRecentSceneDef)
			{
				if (flag && mostRecentSceneDef.bossTrack)
				{
					newTrack = mostRecentSceneDef.bossTrack;
				}
				else if (mostRecentSceneDef.mainTrack)
				{
					newTrack = mostRecentSceneDef.mainTrack;
				}
			}
			MusicController.PickTrackDelegate pickTrackDelegate = MusicController.pickTrackHook;
			if (pickTrackDelegate == null)
			{
				return;
			}
			pickTrackDelegate(this, ref newTrack);
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06002A3C RID: 10812 RVA: 0x000B67A4 File Offset: 0x000B49A4
		// (remove) Token: 0x06002A3D RID: 10813 RVA: 0x000B67D8 File Offset: 0x000B49D8
		public static event MusicController.PickTrackDelegate pickTrackHook;

		// Token: 0x06002A3E RID: 10814 RVA: 0x000B680C File Offset: 0x000B4A0C
		private void UpdateState()
		{
			MusicTrackDef currentTrack = this.currentTrack;
			this.PickCurrentTrack(ref currentTrack);
			this.currentTrack = currentTrack;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000B682F File Offset: 0x000B4A2F
		private void EnsureEnemyBufferSize(int requiredSize)
		{
			if (this.enemyInfoBuffer.Length < requiredSize)
			{
				if (this.enemyInfoBuffer.Length != 0)
				{
					this.enemyInfoBuffer.Dispose();
				}
				this.enemyInfoBuffer = new NativeArray<MusicController.EnemyInfo>(requiredSize, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000B6865 File Offset: 0x000B4A65
		private void OnDestroy()
		{
			this.enemyInfoBuffer.Dispose();
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000B6874 File Offset: 0x000B4A74
		private void ScheduleIntensityCalculation(GameObject targetBodyObject)
		{
			if (!targetBodyObject)
			{
				return;
			}
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Monster);
			int count = teamMembers.Count;
			this.EnsureEnemyBufferSize(count);
			int num = 0;
			int i = 0;
			int num2 = count;
			while (i < num2)
			{
				TeamComponent teamComponent = teamMembers[i];
				InputBankTest component = teamComponent.GetComponent<InputBankTest>();
				CharacterBody component2 = teamComponent.GetComponent<CharacterBody>();
				if (component)
				{
					this.enemyInfoBuffer[num++] = new MusicController.EnemyInfo
					{
						aimRay = new Ray(component.aimOrigin, component.aimDirection),
						threatScore = (component2.master ? component2.GetNormalizedThreatValue() : 0f)
					};
				}
				i++;
			}
			this.calculateIntensityJob = new MusicController.CalculateIntensityJob
			{
				enemyInfoBuffer = this.enemyInfoBuffer,
				elementCount = num,
				targetPosition = targetBodyObject.transform.position,
				nearDistance = 20f,
				farDistance = 75f
			};
			this.calculateIntensityJobHandle = this.calculateIntensityJob.Schedule(num, 32, default(JobHandle));
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000B6994 File Offset: 0x000B4B94
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			RoR2Application.onLoad = (Action)Delegate.Combine(RoR2Application.onLoad, new Action(delegate()
			{
				UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/MusicController"), RoR2Application.instance.transform);
			}));
		}

		// Token: 0x04002D75 RID: 11637
		public GameObject target;

		// Token: 0x04002D76 RID: 11638
		public bool enableMusicSystem = true;

		// Token: 0x04002D77 RID: 11639
		private CameraRigController targetCamera;

		// Token: 0x04002D78 RID: 11640
		private MusicTrackDef _currentTrack;

		// Token: 0x04002D79 RID: 11641
		private RtpcSetter rtpcPlayerHealthValue;

		// Token: 0x04002D7A RID: 11642
		private RtpcSetter rtpcEnemyValue;

		// Token: 0x04002D7B RID: 11643
		private RtpcSetter rtpcTeleporterProximityValue;

		// Token: 0x04002D7C RID: 11644
		private RtpcSetter rtpcTeleporterDirectionValue;

		// Token: 0x04002D7D RID: 11645
		private RtpcSetter rtpcTeleporterPlayerStatus;

		// Token: 0x04002D7E RID: 11646
		private StateSetter stBossStatus;

		// Token: 0x04002D7F RID: 11647
		private bool wasPaused;

		// Token: 0x04002D81 RID: 11649
		private NativeArray<MusicController.EnemyInfo> enemyInfoBuffer;

		// Token: 0x04002D82 RID: 11650
		private MusicController.CalculateIntensityJob calculateIntensityJob;

		// Token: 0x04002D83 RID: 11651
		private JobHandle calculateIntensityJobHandle;

		// Token: 0x020007C7 RID: 1991
		// (Invoke) Token: 0x06002A45 RID: 10821
		public delegate void PickTrackDelegate(MusicController musicController, ref MusicTrackDef newTrack);

		// Token: 0x020007C8 RID: 1992
		private struct EnemyInfo
		{
			// Token: 0x04002D84 RID: 11652
			public Ray aimRay;

			// Token: 0x04002D85 RID: 11653
			public float lookScore;

			// Token: 0x04002D86 RID: 11654
			public float proximityScore;

			// Token: 0x04002D87 RID: 11655
			public float threatScore;
		}

		// Token: 0x020007C9 RID: 1993
		private struct CalculateIntensityJob : IJobParallelFor
		{
			// Token: 0x06002A48 RID: 10824 RVA: 0x000B69D8 File Offset: 0x000B4BD8
			public void Execute(int i)
			{
				MusicController.EnemyInfo enemyInfo = this.enemyInfoBuffer[i];
				Vector3 a = this.targetPosition - enemyInfo.aimRay.origin;
				float magnitude = a.magnitude;
				float num = Mathf.Clamp01(Vector3.Dot(a / magnitude, enemyInfo.aimRay.direction));
				float num2 = Mathf.Clamp01(Mathf.InverseLerp(this.farDistance, this.nearDistance, magnitude));
				enemyInfo.lookScore = num * enemyInfo.threatScore;
				enemyInfo.proximityScore = num2 * enemyInfo.threatScore;
				this.enemyInfoBuffer[i] = enemyInfo;
			}

			// Token: 0x06002A49 RID: 10825 RVA: 0x000B6A78 File Offset: 0x000B4C78
			public void CalculateSum(out float proximityScore, out float lookScore)
			{
				proximityScore = 0f;
				lookScore = 0f;
				for (int i = 0; i < this.elementCount; i++)
				{
					proximityScore += this.enemyInfoBuffer[i].proximityScore;
					lookScore += this.enemyInfoBuffer[i].lookScore;
				}
			}

			// Token: 0x04002D88 RID: 11656
			[global::ReadOnly]
			public Vector3 targetPosition;

			// Token: 0x04002D89 RID: 11657
			[global::ReadOnly]
			public int elementCount;

			// Token: 0x04002D8A RID: 11658
			public NativeArray<MusicController.EnemyInfo> enemyInfoBuffer;

			// Token: 0x04002D8B RID: 11659
			[global::ReadOnly]
			public float nearDistance;

			// Token: 0x04002D8C RID: 11660
			[global::ReadOnly]
			public float farDistance;
		}
	}
}
