using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GrandParent
{
	// Token: 0x02000358 RID: 856
	public class ChannelSun : ChannelSunBase
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x00043600 File Offset: 0x00041800
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(ChannelSun.animLayerName, ChannelSun.animStateName);
			if (NetworkServer.active)
			{
				Vector3? vector = this.sunSpawnPosition;
				this.sunSpawnPosition = ((vector != null) ? vector : ChannelSun.FindSunSpawnPosition(base.transform.position));
				if (this.sunSpawnPosition != null)
				{
					this.sunInstance = this.CreateSun(this.sunSpawnPosition.Value);
				}
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00043677 File Offset: 0x00041877
		public override void OnExit()
		{
			if (NetworkServer.active && this.sunInstance)
			{
				this.sunInstance.GetComponent<GenericOwnership>().ownerObject = null;
				this.sunInstance = null;
			}
			base.OnExit();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000436AB File Offset: 0x000418AB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && !base.IsKeyDownAuthority())
			{
				this.outer.SetNextState(new ChannelSunEnd());
			}
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000436D3 File Offset: 0x000418D3
		private GameObject CreateSun(Vector3 sunSpawnPosition)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ChannelSun.sunPrefab, sunSpawnPosition, Quaternion.identity);
			gameObject.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
			NetworkServer.Spawn(gameObject);
			return gameObject;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000436FC File Offset: 0x000418FC
		private static Vector3? FindSunNodePosition(Vector3 searchOrigin)
		{
			NodeGraph airNodes = SceneInfo.instance.airNodes;
			NodeGraph.NodeIndex nodeIndex = airNodes.FindClosestNodeWithFlagConditions(searchOrigin, HullClassification.Golem, NodeFlags.None, NodeFlags.None, false);
			if (nodeIndex == NodeGraph.NodeIndex.invalid)
			{
				return null;
			}
			float num = ChannelSun.sunPlacementMinDistance;
			float num2 = num * num;
			NodeGraph.NodeIndex invalid = NodeGraph.NodeIndex.invalid;
			float num3 = 0f;
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(airNodes, HullMask.Golem);
			nodeGraphSpider.AddNodeForNextStep(nodeIndex);
			int num4 = 0;
			int i = 0;
			while (nodeGraphSpider.PerformStep())
			{
				num4++;
				while (i < nodeGraphSpider.collectedSteps.Count)
				{
					NodeGraphSpider.StepInfo stepInfo = nodeGraphSpider.collectedSteps[i];
					Vector3 vector;
					airNodes.GetNodePosition(stepInfo.node, out vector);
					float sqrMagnitude = (vector - searchOrigin).sqrMagnitude;
					if (sqrMagnitude > num3)
					{
						num3 = sqrMagnitude;
						NodeGraph.NodeIndex node = stepInfo.node;
						if (num3 >= num2)
						{
							return new Vector3?(vector);
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000437E8 File Offset: 0x000419E8
		public static Vector3? FindSunSpawnPosition(Vector3 searchOrigin)
		{
			Vector3? vector = ChannelSun.FindSunNodePosition(searchOrigin);
			if (vector != null)
			{
				Vector3 value = vector.Value;
				float num = ChannelSun.sunPlacementIdealAltitudeBonus;
				float num2 = ChannelSun.sunPrefabDiameter * 0.5f;
				RaycastHit raycastHit;
				if (Physics.Raycast(value, Vector3.up, out raycastHit, ChannelSun.sunPlacementIdealAltitudeBonus + num2, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					num = Mathf.Clamp(raycastHit.distance - num2, 0f, num);
				}
				value.y += num;
				return new Vector3?(value);
			}
			return null;
		}

		// Token: 0x0400137E RID: 4990
		public static string animLayerName;

		// Token: 0x0400137F RID: 4991
		public static string animStateName;

		// Token: 0x04001380 RID: 4992
		public static GameObject sunPrefab;

		// Token: 0x04001381 RID: 4993
		public static float sunPrefabDiameter = 10f;

		// Token: 0x04001382 RID: 4994
		public static float sunPlacementMinDistance = 100f;

		// Token: 0x04001383 RID: 4995
		public static float sunPlacementIdealAltitudeBonus = 200f;

		// Token: 0x04001384 RID: 4996
		private GameObject sunInstance;

		// Token: 0x04001385 RID: 4997
		public Vector3? sunSpawnPosition;
	}
}
