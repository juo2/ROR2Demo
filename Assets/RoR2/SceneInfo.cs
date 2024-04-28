using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000877 RID: 2167
	public class SceneInfo : MonoBehaviour
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000CA7B2 File Offset: 0x000C89B2
		public static SceneInfo instance
		{
			get
			{
				return SceneInfo._instance;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000CA7B9 File Offset: 0x000C89B9
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x000CA7C1 File Offset: 0x000C89C1
		public NodeGraph groundNodes { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000CA7CA File Offset: 0x000C89CA
		// (set) Token: 0x06002F82 RID: 12162 RVA: 0x000CA7D2 File Offset: 0x000C89D2
		public NodeGraph airNodes { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000CA7DB File Offset: 0x000C89DB
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000CA7E3 File Offset: 0x000C89E3
		public NodeGraph railNodes { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000CA7EC File Offset: 0x000C89EC
		// (set) Token: 0x06002F86 RID: 12166 RVA: 0x000CA7F4 File Offset: 0x000C89F4
		public SceneDef sceneDef { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000CA7FD File Offset: 0x000C89FD
		public bool countsAsStage
		{
			get
			{
				return this.sceneDef && this.sceneDef.sceneType == SceneType.Stage;
			}
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000CA81C File Offset: 0x000C8A1C
		private void Awake()
		{
			if (this.groundNodesAsset)
			{
				this.groundNodes = UnityEngine.Object.Instantiate<NodeGraph>(this.groundNodesAsset);
			}
			else
			{
				Debug.LogWarning(base.gameObject.scene.name + " has no groundNodesAsset");
			}
			if (this.airNodesAsset)
			{
				this.airNodes = UnityEngine.Object.Instantiate<NodeGraph>(this.airNodesAsset);
			}
			else
			{
				Debug.LogWarning(base.gameObject.scene.name + " has no airNodesAsset");
			}
			this.sceneDef = SceneCatalog.GetSceneDefFromSceneName(base.gameObject.scene.name);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000CA8CB File Offset: 0x000C8ACB
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.groundNodes);
			UnityEngine.Object.Destroy(this.airNodes);
			UnityEngine.Object.Destroy(this.railNodes);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000CA8EE File Offset: 0x000C8AEE
		public MapNodeGroup GetNodeGroup(MapNodeGroup.GraphType nodeGraphType)
		{
			switch (nodeGraphType)
			{
			case MapNodeGroup.GraphType.Ground:
				return this.groundNodeGroup;
			case MapNodeGroup.GraphType.Air:
				return this.airNodeGroup;
			case MapNodeGroup.GraphType.Rail:
				return this.railNodeGroup;
			default:
				return null;
			}
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000CA91A File Offset: 0x000C8B1A
		public NodeGraph GetNodeGraph(MapNodeGroup.GraphType nodeGraphType)
		{
			switch (nodeGraphType)
			{
			case MapNodeGroup.GraphType.Ground:
				return this.groundNodes;
			case MapNodeGroup.GraphType.Air:
				return this.airNodes;
			case MapNodeGroup.GraphType.Rail:
				return this.railNodes;
			default:
				return null;
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000CA948 File Offset: 0x000C8B48
		public void SetGateState(string gateName, bool gateEnabled)
		{
			bool flag = false;
			if (this.groundNodes)
			{
				flag = (this.groundNodes.TrySetGateState(gateName, gateEnabled) || flag);
			}
			if (this.airNodes)
			{
				flag = (this.airNodes.TrySetGateState(gateName, gateEnabled) || flag);
			}
			if (this.railNodes)
			{
				flag = (this.railNodes.TrySetGateState(gateName, gateEnabled) || flag);
			}
			if (!flag)
			{
				Debug.LogError(string.Format("Unable to set gate state for {0}: {1}={2}", base.gameObject.scene.name, gateName, gateEnabled));
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000CA9DA File Offset: 0x000C8BDA
		private void OnEnable()
		{
			if (!SceneInfo._instance)
			{
				SceneInfo._instance = this;
			}
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000CA9EE File Offset: 0x000C8BEE
		private void OnDisable()
		{
			if (SceneInfo._instance == this)
			{
				SceneInfo._instance = null;
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000CAA03 File Offset: 0x000C8C03
		private void OnValidate()
		{
			if (this.groundNodeGroup)
			{
				this.groundNodesAsset = this.groundNodeGroup.nodeGraph;
			}
			if (this.airNodeGroup)
			{
				this.airNodesAsset = this.airNodeGroup.nodeGraph;
			}
		}

		// Token: 0x0400314A RID: 12618
		private static SceneInfo _instance;

		// Token: 0x0400314B RID: 12619
		[FormerlySerializedAs("groundNodes")]
		public MapNodeGroup groundNodeGroup;

		// Token: 0x0400314C RID: 12620
		[FormerlySerializedAs("airNodes")]
		public MapNodeGroup airNodeGroup;

		// Token: 0x0400314D RID: 12621
		[FormerlySerializedAs("railNodes")]
		public MapNodeGroup railNodeGroup;

		// Token: 0x0400314E RID: 12622
		public MeshRenderer approximateMapBoundMesh;

		// Token: 0x0400314F RID: 12623
		[SerializeField]
		private NodeGraph groundNodesAsset;

		// Token: 0x04003150 RID: 12624
		[SerializeField]
		private NodeGraph airNodesAsset;

		// Token: 0x02000878 RID: 2168
		private static class NodeGraphOverlay
		{
			// Token: 0x06002F91 RID: 12177 RVA: 0x000CAA44 File Offset: 0x000C8C44
			private static void StaticUpdate()
			{
				foreach (KeyValuePair<ValueTuple<MapNodeGroup.GraphType, HullMask>, ValueTuple<DebugOverlay.MeshDrawer, Action>> keyValuePair in SceneInfo.NodeGraphOverlay.drawers)
				{
					keyValuePair.Value.Item2();
				}
			}

			// Token: 0x06002F92 RID: 12178 RVA: 0x000CAAA0 File Offset: 0x000C8CA0
			private static void SetGraphDrawEnabled(bool shouldDraw, MapNodeGroup.GraphType graphType, HullMask hullMask)
			{
				SceneInfo.NodeGraphOverlay.<>c__DisplayClass2_0 CS$<>8__locals1 = new SceneInfo.NodeGraphOverlay.<>c__DisplayClass2_0();
				CS$<>8__locals1.graphType = graphType;
				CS$<>8__locals1.hullMask = hullMask;
				ValueTuple<MapNodeGroup.GraphType, HullMask> key = new ValueTuple<MapNodeGroup.GraphType, HullMask>(CS$<>8__locals1.graphType, CS$<>8__locals1.hullMask);
				if (shouldDraw)
				{
					if (SceneInfo.NodeGraphOverlay.drawers == null)
					{
						SceneInfo.NodeGraphOverlay.drawers = new Dictionary<ValueTuple<MapNodeGroup.GraphType, HullMask>, ValueTuple<DebugOverlay.MeshDrawer, Action>>();
						RoR2Application.onUpdate += SceneInfo.NodeGraphOverlay.StaticUpdate;
					}
					if (!SceneInfo.NodeGraphOverlay.drawers.ContainsKey(key))
					{
						SceneInfo.NodeGraphOverlay.<>c__DisplayClass2_1 CS$<>8__locals2 = new SceneInfo.NodeGraphOverlay.<>c__DisplayClass2_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.drawer = DebugOverlay.GetMeshDrawer();
						CS$<>8__locals2.drawer.hasMeshOwnership = true;
						CS$<>8__locals2.drawer.material = DebugOverlay.defaultWireMaterial;
						CS$<>8__locals2.getNodeGraphMeshMemoizer = new GenericMemoizer<ValueTuple<DebugOverlay.MeshDrawer, SceneInfo, MapNodeGroup.GraphType, HullMask>, Mesh>(new GenericMemoizer<ValueTuple<DebugOverlay.MeshDrawer, SceneInfo, MapNodeGroup.GraphType, HullMask>, Mesh>.Func(SceneInfo.NodeGraphOverlay.<>c.<>9.<SetGraphDrawEnabled>g__GetNodeGraphMesh|2_0));
						SceneInfo.NodeGraphOverlay.drawers.Add(key, new ValueTuple<DebugOverlay.MeshDrawer, Action>(CS$<>8__locals2.drawer, new Action(CS$<>8__locals2.<SetGraphDrawEnabled>g__Updater|1)));
						return;
					}
				}
				else if (SceneInfo.NodeGraphOverlay.drawers != null)
				{
					ValueTuple<DebugOverlay.MeshDrawer, Action> valueTuple;
					if (SceneInfo.NodeGraphOverlay.drawers.TryGetValue(key, out valueTuple))
					{
						valueTuple.Item1.Dispose();
						SceneInfo.NodeGraphOverlay.drawers.Remove(key);
					}
					if (SceneInfo.NodeGraphOverlay.drawers.Count == 0)
					{
						SceneInfo.NodeGraphOverlay.drawers = null;
						RoR2Application.onUpdate -= SceneInfo.NodeGraphOverlay.StaticUpdate;
					}
				}
			}

			// Token: 0x06002F93 RID: 12179 RVA: 0x000CABD0 File Offset: 0x000C8DD0
			[ConCommand(commandName = "debug_scene_draw_nodegraph", flags = ConVarFlags.Cheat, helpText = "Enables/disables overlay drawing of the specified nodegraph. Format: {shouldDraw} {graphType} {hullClassification, ...}")]
			private static void CCDebugSceneDrawNodegraph(ConCommandArgs args)
			{
				bool argBool = args.GetArgBool(0);
				MapNodeGroup.GraphType argEnum = args.GetArgEnum<MapNodeGroup.GraphType>(1);
				HullMask hullMask = (HullMask)(1 << (int)args.GetArgEnum<HullClassification>(2));
				if (hullMask == HullMask.None)
				{
					throw new ConCommandException("Cannot use HullMask.None.");
				}
				for (int i = 3; i < args.Count; i++)
				{
					HullClassification? hullClassification = args.TryGetArgEnum<HullClassification>(i);
					if (hullClassification != null)
					{
						hullMask |= (HullMask)(1 << (int)hullClassification.Value);
					}
				}
				SceneInfo.NodeGraphOverlay.SetGraphDrawEnabled(argBool, argEnum, hullMask);
			}

			// Token: 0x04003155 RID: 12629
			[TupleElementNames(new string[]
			{
				"graphType",
				"hullMask",
				"drawer",
				"updater"
			})]
			private static Dictionary<ValueTuple<MapNodeGroup.GraphType, HullMask>, ValueTuple<DebugOverlay.MeshDrawer, Action>> drawers;
		}
	}
}
