using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000875 RID: 2165
	public class SceneExitController : MonoBehaviour
	{
		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06002F70 RID: 12144 RVA: 0x000CA4B4 File Offset: 0x000C86B4
		// (remove) Token: 0x06002F71 RID: 12145 RVA: 0x000CA4E8 File Offset: 0x000C86E8
		public static event Action<SceneExitController> onBeginExit;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06002F72 RID: 12146 RVA: 0x000CA51C File Offset: 0x000C871C
		// (remove) Token: 0x06002F73 RID: 12147 RVA: 0x000CA550 File Offset: 0x000C8750
		public static event Action<SceneExitController> onFinishExit;

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000CA583 File Offset: 0x000C8783
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000CA58A File Offset: 0x000C878A
		public static bool isRunning { get; private set; }

		// Token: 0x06002F76 RID: 12150 RVA: 0x000CA592 File Offset: 0x000C8792
		public void Begin()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (this.exitState == SceneExitController.ExitState.Idle)
			{
				this.SetState(Run.instance.ruleBook.keepMoneyBetweenStages ? SceneExitController.ExitState.TeleportOut : SceneExitController.ExitState.ExtractExp);
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000CA5C0 File Offset: 0x000C87C0
		public void SetState(SceneExitController.ExitState newState)
		{
			if (newState == this.exitState)
			{
				return;
			}
			this.exitState = newState;
			switch (this.exitState)
			{
			case SceneExitController.ExitState.Idle:
				break;
			case SceneExitController.ExitState.ExtractExp:
				if (!SceneExitController.isRunning)
				{
					Action<SceneExitController> action = SceneExitController.onBeginExit;
					if (action != null)
					{
						action(this);
					}
				}
				SceneExitController.isRunning = true;
				this.experienceCollector = base.gameObject.AddComponent<ConvertPlayerMoneyToExperience>();
				return;
			case SceneExitController.ExitState.TeleportOut:
			{
				ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
				for (int i = 0; i < readOnlyInstancesList.Count; i++)
				{
					CharacterMaster component = readOnlyInstancesList[i].GetComponent<CharacterMaster>();
					if (component.GetComponent<SetDontDestroyOnLoad>())
					{
						GameObject bodyObject = component.GetBodyObject();
						if (bodyObject)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/TeleportOutController"), bodyObject.transform.position, Quaternion.identity);
							gameObject.GetComponent<TeleportOutController>().Networktarget = bodyObject;
							NetworkServer.Spawn(gameObject);
						}
					}
				}
				this.teleportOutTimer = 4f;
				return;
			}
			case SceneExitController.ExitState.Finished:
			{
				Action<SceneExitController> action2 = SceneExitController.onFinishExit;
				if (action2 != null)
				{
					action2(this);
				}
				if (Run.instance && Run.instance.isGameOverServer)
				{
					return;
				}
				if (this.useRunNextStageScene)
				{
					Stage.instance.BeginAdvanceStage(Run.instance.nextStageScene);
					return;
				}
				if (this.destinationScene)
				{
					Stage.instance.BeginAdvanceStage(this.destinationScene);
					return;
				}
				Debug.Log("SceneExitController: destinationScene not set!");
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000CA71B File Offset: 0x000C891B
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.UpdateServer();
			}
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000CA72C File Offset: 0x000C892C
		private void UpdateServer()
		{
			switch (this.exitState)
			{
			case SceneExitController.ExitState.Idle:
			case SceneExitController.ExitState.Finished:
				break;
			case SceneExitController.ExitState.ExtractExp:
				if (!this.experienceCollector)
				{
					this.SetState(SceneExitController.ExitState.TeleportOut);
					return;
				}
				break;
			case SceneExitController.ExitState.TeleportOut:
				this.teleportOutTimer -= Time.fixedDeltaTime;
				if (this.teleportOutTimer <= 0f)
				{
					this.SetState(SceneExitController.ExitState.Finished);
					return;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000CA79A File Offset: 0x000C899A
		private void OnDestroy()
		{
			SceneExitController.isRunning = false;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000CA7A2 File Offset: 0x000C89A2
		private void OnEnable()
		{
			InstanceTracker.Add<SceneExitController>(this);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000CA7AA File Offset: 0x000C89AA
		private void OnDisable()
		{
			InstanceTracker.Remove<SceneExitController>(this);
		}

		// Token: 0x0400313F RID: 12607
		public bool useRunNextStageScene;

		// Token: 0x04003140 RID: 12608
		public SceneDef destinationScene;

		// Token: 0x04003141 RID: 12609
		private const float teleportOutDuration = 4f;

		// Token: 0x04003142 RID: 12610
		private float teleportOutTimer;

		// Token: 0x04003143 RID: 12611
		private SceneExitController.ExitState exitState;

		// Token: 0x04003144 RID: 12612
		private ConvertPlayerMoneyToExperience experienceCollector;

		// Token: 0x02000876 RID: 2166
		public enum ExitState
		{
			// Token: 0x04003146 RID: 12614
			Idle,
			// Token: 0x04003147 RID: 12615
			ExtractExp,
			// Token: 0x04003148 RID: 12616
			TeleportOut,
			// Token: 0x04003149 RID: 12617
			Finished
		}
	}
}
