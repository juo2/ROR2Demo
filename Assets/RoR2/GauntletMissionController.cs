using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006F5 RID: 1781
	public class GauntletMissionController : NetworkBehaviour
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x0009B705 File Offset: 0x00099905
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x0009B70C File Offset: 0x0009990C
		public static GauntletMissionController instance { get; private set; }

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002445 RID: 9285 RVA: 0x0009B714 File Offset: 0x00099914
		// (remove) Token: 0x06002446 RID: 9286 RVA: 0x0009B748 File Offset: 0x00099948
		public static event Action onInstanceChangedGlobal;

		// Token: 0x06002447 RID: 9287 RVA: 0x0009B77B File Offset: 0x0009997B
		private void Awake()
		{
			this.mainStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Main");
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0009B793 File Offset: 0x00099993
		private void OnEnable()
		{
			GauntletMissionController.instance = SingletonHelper.Assign<GauntletMissionController>(GauntletMissionController.instance, this);
			Action action = GauntletMissionController.onInstanceChangedGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0009B7B4 File Offset: 0x000999B4
		private void OnDisable()
		{
			GauntletMissionController.instance = SingletonHelper.Unassign<GauntletMissionController>(GauntletMissionController.instance, this);
			Action action = GauntletMissionController.onInstanceChangedGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0009B7D5 File Offset: 0x000999D5
		public void GauntletMissionTimesUp()
		{
			Debug.Log("GauntletMissionController : GauntletMissionTimesUp()");
			this.gauntletEnd = true;
			this.mainStateMachine.SetNextState(new GauntletMissionController.MissionCompleted());
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0009B7F8 File Offset: 0x000999F8
		private void OnPreGeneratePlayerSpawnPointsServer(SceneDirector sceneDirector, ref Action generationMethod)
		{
			generationMethod = new Action(this.GenerateGauntletShards);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0009B808 File Offset: 0x00099A08
		private void GenerateGauntletShards()
		{
			int num = this.gauntletShards.Length;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0009B812 File Offset: 0x00099A12
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0009B824 File Offset: 0x00099A24
		[Server]
		private void FixedUpdateServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GauntletMissionController::FixedUpdateServer()' called on client");
				return;
			}
			if (this.gauntletEnd)
			{
				this.degenTimer += Time.fixedDeltaTime;
				if (this.degenTimer > 1f / this.degenTickFrequency)
				{
					this.degenTimer -= 1f / this.degenTickFrequency;
					foreach (TeamComponent teamComponent in TeamComponent.GetTeamMembers(TeamIndex.Player))
					{
						float damage = this.percentDegenPerSecond / 100f / this.degenTickFrequency * teamComponent.body.healthComponent.combinedHealth;
						teamComponent.body.healthComponent.TakeDamage(new DamageInfo
						{
							damage = damage,
							position = teamComponent.body.corePosition,
							damageType = DamageType.Silent
						});
					}
				}
			}
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0009B92C File Offset: 0x00099B2C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002896 RID: 10390
		[Header("Behavior Values")]
		public float dleayBeforeStart;

		// Token: 0x04002897 RID: 10391
		public float timeLimit;

		// Token: 0x04002898 RID: 10392
		public float degenTickFrequency;

		// Token: 0x04002899 RID: 10393
		public float percentDegenPerSecond;

		// Token: 0x0400289A RID: 10394
		[Header("Cached Components")]
		private EntityStateMachine mainStateMachine;

		// Token: 0x0400289B RID: 10395
		public GameObject[] gauntletShards;

		// Token: 0x0400289C RID: 10396
		public GameObject clearedEffect;

		// Token: 0x0400289D RID: 10397
		public GameObject gauntletPortal;

		// Token: 0x0400289E RID: 10398
		private bool gauntletEnd;

		// Token: 0x0400289F RID: 10399
		private bool slowDeathEffectActive;

		// Token: 0x040028A2 RID: 10402
		private float degenTimer;

		// Token: 0x020006F6 RID: 1782
		public class GauntletMissionBaseState : EntityState
		{
			// Token: 0x17000301 RID: 769
			// (get) Token: 0x06002454 RID: 9300 RVA: 0x0009B93A File Offset: 0x00099B3A
			protected GauntletMissionController gauntletMissionController
			{
				get
				{
					return GauntletMissionController.instance;
				}
			}
		}

		// Token: 0x020006F7 RID: 1783
		public class MissionCompleted : GauntletMissionController.GauntletMissionBaseState
		{
			// Token: 0x06002456 RID: 9302 RVA: 0x0009B941 File Offset: 0x00099B41
			public override void OnEnter()
			{
				base.OnEnter();
				base.gauntletMissionController.clearedEffect.SetActive(true);
			}
		}
	}
}
