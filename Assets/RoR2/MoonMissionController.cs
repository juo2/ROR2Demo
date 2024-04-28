using System;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007BD RID: 1981
	public class MoonMissionController : NetworkBehaviour
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000B516D File Offset: 0x000B336D
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x000B5174 File Offset: 0x000B3374
		public static MoonMissionController instance { get; private set; }

		// Token: 0x060029F9 RID: 10745 RVA: 0x000B517C File Offset: 0x000B337C
		private void OnEnable()
		{
			MoonMissionController.instance = SingletonHelper.Assign<MoonMissionController>(MoonMissionController.instance, this);
			SceneDirector.onPreGeneratePlayerSpawnPointsServer += this.OnPreGeneratePlayerSpawnPointsServer;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000B519F File Offset: 0x000B339F
		private void OnDisable()
		{
			SceneDirector.onPreGeneratePlayerSpawnPointsServer -= this.OnPreGeneratePlayerSpawnPointsServer;
			MoonMissionController.instance = SingletonHelper.Unassign<MoonMissionController>(MoonMissionController.instance, this);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000B51C2 File Offset: 0x000B33C2
		[Server]
		public override void OnStartServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.MoonMissionController::OnStartServer()' called on client");
				return;
			}
			base.OnStartServer();
			this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000B51FA File Offset: 0x000B33FA
		private void OnPreGeneratePlayerSpawnPointsServer(SceneDirector sceneDirector, ref Action generationMethod)
		{
			generationMethod = new Action(this.GeneratePlayerSpawnPointsServer);
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000B520C File Offset: 0x000B340C
		private void GeneratePlayerSpawnPointsServer()
		{
			if (!SceneInfo.instance)
			{
				return;
			}
			ChildLocator component = SceneInfo.instance.GetComponent<ChildLocator>();
			if (!component)
			{
				return;
			}
			Transform transform = component.FindChild("PlayerSpawnOrigin");
			Vector3 position = transform.position;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			if (!groundNodes)
			{
				Debug.LogError("MoonMissionController.GeneratePlayerSpawnPointsServer: No ground nodegraph found to place spawn points.", this);
				return;
			}
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(groundNodes, HullMask.Human);
			nodeGraphSpider.AddNodeForNextStep(groundNodes.FindClosestNode(position, HullClassification.Human, float.PositiveInfinity));
			for (int i = 0; i < 4; i++)
			{
				nodeGraphSpider.PerformStep();
				if (nodeGraphSpider.collectedSteps.Count > 16)
				{
					break;
				}
			}
			for (int j = 0; j < nodeGraphSpider.collectedSteps.Count; j++)
			{
				NodeGraphSpider.StepInfo stepInfo = nodeGraphSpider.collectedSteps[j];
				Vector3 position2;
				groundNodes.GetNodePosition(stepInfo.node, out position2);
				Quaternion rotation = transform.rotation;
				SpawnPoint.AddSpawnPoint(position2, rotation);
			}
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000B5300 File Offset: 0x000B3500
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D3D RID: 11581
		private Xoroshiro128Plus rng;
	}
}
