using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000884 RID: 2180
	public class SeerStationController : NetworkBehaviour
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000CB799 File Offset: 0x000C9999
		private void SetTargetSceneDefIndex(int newTargetSceneDefIndex)
		{
			this.NetworktargetSceneDefIndex = newTargetSceneDefIndex;
			this.OnTargetSceneChanged(SceneCatalog.GetSceneDef((SceneIndex)this.targetSceneDefIndex));
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000CB7B3 File Offset: 0x000C99B3
		[Server]
		public void SetTargetScene(SceneDef sceneDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SeerStationController::SetTargetScene(RoR2.SceneDef)' called on client");
				return;
			}
			this.NetworktargetSceneDefIndex = (int)sceneDef.sceneDefIndex;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000CB7D8 File Offset: 0x000C99D8
		public override void OnStartClient()
		{
			base.OnStartClient();
			SceneDef targetScene = null;
			if ((ulong)this.targetSceneDefIndex < (ulong)((long)SceneCatalog.sceneDefCount))
			{
				targetScene = SceneCatalog.GetSceneDef((SceneIndex)this.targetSceneDefIndex);
			}
			this.OnTargetSceneChanged(targetScene);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000CB810 File Offset: 0x000C9A10
		private void OnTargetSceneChanged(SceneDef targetScene)
		{
			Material portalMaterial = null;
			if (targetScene)
			{
				portalMaterial = targetScene.portalMaterial;
			}
			this.SetPortalMaterial(portalMaterial);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000CB835 File Offset: 0x000C9A35
		private void SetPortalMaterial(Material portalMaterial)
		{
			this.targetRenderer.GetSharedMaterials(SeerStationController.sharedSharedMaterialsList);
			SeerStationController.sharedSharedMaterialsList[this.materialIndexToAssign] = portalMaterial;
			this.targetRenderer.SetSharedMaterials(SeerStationController.sharedSharedMaterialsList);
			SeerStationController.sharedSharedMaterialsList.Clear();
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000CB874 File Offset: 0x000C9A74
		[Server]
		public void SetRunNextStageToTarget()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SeerStationController::SetRunNextStageToTarget()' called on client");
				return;
			}
			SceneDef sceneDef = SceneCatalog.GetSceneDef((SceneIndex)this.targetSceneDefIndex);
			if (sceneDef)
			{
				SceneExitController sceneExitController = this.explicitTargetSceneExitController;
				if (!sceneExitController && this.fallBackToFirstActiveExitController)
				{
					sceneExitController = InstanceTracker.FirstOrNull<SceneExitController>();
				}
				if (sceneExitController)
				{
					sceneExitController.destinationScene = sceneDef;
					sceneExitController.useRunNextStageScene = false;
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = sceneDef.portalSelectionMessageString
					});
				}
			}
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000CB910 File Offset: 0x000C9B10
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000CB923 File Offset: 0x000C9B23
		public int NetworktargetSceneDefIndex
		{
			get
			{
				return this.targetSceneDefIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetTargetSceneDefIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<int>(value, ref this.targetSceneDefIndex, 1U);
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000CB964 File Offset: 0x000C9B64
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.targetSceneDefIndex);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.targetSceneDefIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000CB9D0 File Offset: 0x000C9BD0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.targetSceneDefIndex = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetTargetSceneDefIndex((int)reader.ReadPackedUInt32());
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400318C RID: 12684
		[SyncVar(hook = "SetTargetSceneDefIndex")]
		private int targetSceneDefIndex = -1;

		// Token: 0x0400318D RID: 12685
		public SceneExitController explicitTargetSceneExitController;

		// Token: 0x0400318E RID: 12686
		public bool fallBackToFirstActiveExitController;

		// Token: 0x0400318F RID: 12687
		public Renderer targetRenderer;

		// Token: 0x04003190 RID: 12688
		public int materialIndexToAssign;

		// Token: 0x04003191 RID: 12689
		private static List<Material> sharedSharedMaterialsList = new List<Material>();
	}
}
