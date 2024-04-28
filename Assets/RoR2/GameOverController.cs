using System;
using System.Collections.Generic;
using HG;
using JetBrains.Annotations;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006F3 RID: 1779
	[RequireComponent(typeof(VoteController))]
	public class GameOverController : NetworkBehaviour
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0009B265 File Offset: 0x00099465
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x0009B26C File Offset: 0x0009946C
		public static GameOverController instance { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0009B274 File Offset: 0x00099474
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x0009B27C File Offset: 0x0009947C
		public bool shouldDisplayGameEndReportPanels { get; set; }

		// Token: 0x0600242E RID: 9262 RVA: 0x0009B288 File Offset: 0x00099488
		[Server]
		public void SetRunReport([NotNull] RunReport newRunReport)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GameOverController::SetRunReport(RoR2.RunReport)' called on client");
				return;
			}
			base.SetDirtyBit(1U);
			this.runReport = newRunReport;
			if (this.runReport.gameEnding)
			{
				EntityStateMachine.FindByCustomName(base.gameObject, "Main").initialStateType = this.runReport.gameEnding.gameOverControllerState;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x0009B2EF File Offset: 0x000994EF
		// (set) Token: 0x06002430 RID: 9264 RVA: 0x0009B2F7 File Offset: 0x000994F7
		public RunReport runReport { get; private set; }

		// Token: 0x06002431 RID: 9265 RVA: 0x0009B300 File Offset: 0x00099500
		private int FindPlayerIndex(LocalUser localUser)
		{
			int i = 0;
			int playerInfoCount = this.runReport.playerInfoCount;
			while (i < playerInfoCount)
			{
				if (this.runReport.GetPlayerInfo(i).localUser == localUser)
				{
					return i;
				}
				i++;
			}
			Debug.Log("Found no valid player index. Falling back to 0.");
			return 0;
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0009B348 File Offset: 0x00099548
		private void UpdateReportScreens()
		{
			if (this.shouldDisplayGameEndReportPanels)
			{
				int i = 0;
				int count = HUD.readOnlyInstanceList.Count;
				while (i < count)
				{
					HUD hud = HUD.readOnlyInstanceList[i];
					if (!this.reportPanels.ContainsKey(hud))
					{
						this.reportPanels[hud] = this.GenerateReportScreen(hud);
					}
					i++;
				}
			}
			List<HUD> list = CollectionPool<HUD, List<HUD>>.RentCollection();
			foreach (HUD hud2 in this.reportPanels.Keys)
			{
				if (!hud2 || !this.shouldDisplayGameEndReportPanels)
				{
					list.Add(hud2);
				}
			}
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				this.reportPanels.Remove(list[j]);
				j++;
			}
			CollectionPool<HUD, List<HUD>>.ReturnCollection(list);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0009B43C File Offset: 0x0009963C
		private GameEndReportPanelController GenerateReportScreen(HUD hud)
		{
			LocalUser localUser = hud.localUserViewer;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gameEndReportPanelPrefab, hud.transform);
			gameObject.transform.parent = hud.transform;
			gameObject.GetComponent<MPEventSystemProvider>().eventSystem = localUser.eventSystem;
			GameEndReportPanelController component = gameObject.GetComponent<GameEndReportPanelController>();
			GameEndReportPanelController.DisplayData displayData = new GameEndReportPanelController.DisplayData
			{
				runReport = this.runReport,
				playerIndex = this.FindPlayerIndex(localUser)
			};
			component.SetDisplayData(displayData);
			component.SetContinueButtonAction(delegate
			{
				if (localUser.currentNetworkUser)
				{
					localUser.currentNetworkUser.CallCmdSubmitVote(this.voteController.gameObject, 0);
				}
			});
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/VoteInfoPanel"), (RectTransform)component.continueButton.transform.parent);
			gameObject2.transform.SetAsFirstSibling();
			gameObject2.GetComponent<VoteInfoPanelController>().voteController = this.voteController;
			return component;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0009B523 File Offset: 0x00099723
		private void Awake()
		{
			this.runReport = new RunReport();
			this.voteController = base.GetComponent<VoteController>();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0009B53C File Offset: 0x0009973C
		private void OnEnable()
		{
			GameOverController.instance = SingletonHelper.Assign<GameOverController>(GameOverController.instance, this);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0009B54E File Offset: 0x0009974E
		private void OnDisable()
		{
			GameOverController.instance = SingletonHelper.Unassign<GameOverController>(GameOverController.instance, this);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0009B560 File Offset: 0x00099760
		private void Update()
		{
			this.UpdateReportScreens();
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0009B568 File Offset: 0x00099768
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 1U;
			}
			bool flag = (num & 1U) > 0U;
			if (!initialState)
			{
				writer.Write((byte)num);
			}
			if (flag)
			{
				this.runReport.Write(writer);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0009B5A9 File Offset: 0x000997A9
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (((initialState ? 1 : reader.ReadByte()) & 1) > 0)
			{
				this.runReport.Read(reader);
			}
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0009B5CA File Offset: 0x000997CA
		[ClientRpc]
		public void RpcClientGameOver()
		{
			if (Run.instance)
			{
				Run.instance.OnClientGameOver(this.runReport);
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0009B606 File Offset: 0x00099806
		protected static void InvokeRpcRpcClientGameOver(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcClientGameOver called on server.");
				return;
			}
			((GameOverController)obj).RpcClientGameOver();
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0009B62C File Offset: 0x0009982C
		public void CallRpcClientGameOver()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcClientGameOver called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)GameOverController.kRpcRpcClientGameOver);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcClientGameOver");
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0009B695 File Offset: 0x00099895
		static GameOverController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(GameOverController), GameOverController.kRpcRpcClientGameOver, new NetworkBehaviour.CmdDelegate(GameOverController.InvokeRpcRpcClientGameOver));
			NetworkCRC.RegisterBehaviour("GameOverController", 0);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400288B RID: 10379
		[Tooltip("The prefab to use for the end-of-game report panel.")]
		public GameObject gameEndReportPanelPrefab;

		// Token: 0x0400288C RID: 10380
		public float appearanceDelay = 1f;

		// Token: 0x0400288D RID: 10381
		private VoteController voteController;

		// Token: 0x04002890 RID: 10384
		private Dictionary<HUD, GameEndReportPanelController> reportPanels = new Dictionary<HUD, GameEndReportPanelController>();

		// Token: 0x04002891 RID: 10385
		private const uint runReportDirtyBit = 1U;

		// Token: 0x04002892 RID: 10386
		private const uint allDirtyBits = 1U;

		// Token: 0x04002893 RID: 10387
		private static int kRpcRpcClientGameOver = 1518660169;
	}
}
