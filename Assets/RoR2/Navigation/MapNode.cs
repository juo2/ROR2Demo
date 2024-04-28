using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B3A RID: 2874
	[ExecuteInEditMode]
	public class MapNode : MonoBehaviour
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0010EE29 File Offset: 0x0010D029
		public static ReadOnlyCollection<MapNode> instances
		{
			get
			{
				return MapNode.instancesReadOnly;
			}
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0010EE30 File Offset: 0x0010D030
		public void OnEnable()
		{
			MapNode._instances.Add(this);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x0010EE3D File Offset: 0x0010D03D
		public void OnDisable()
		{
			MapNode._instances.Remove(this);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x0010EE4C File Offset: 0x0010D04C
		private void AddLink(MapNode nodeB, float distanceScore, float minJumpHeight, HullClassification hullClassification)
		{
			int num = this.links.FindIndex((MapNode.Link item) => item.nodeB == nodeB);
			if (num == -1)
			{
				this.links.Add(new MapNode.Link
				{
					nodeB = nodeB
				});
				num = this.links.Count - 1;
			}
			MapNode.Link link = this.links[num];
			link.distanceScore = Mathf.Max(link.distanceScore, distanceScore);
			link.minJumpHeight = Mathf.Max(link.minJumpHeight, minJumpHeight);
			link.hullMask |= 1 << (int)hullClassification;
			if (minJumpHeight > 0f)
			{
				link.jumpHullMask |= 1 << (int)hullClassification;
			}
			if (string.IsNullOrEmpty(link.gateName))
			{
				link.gateName = nodeB.gateName;
			}
			this.links[num] = link;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x0010EF3C File Offset: 0x0010D13C
		private void BuildGroundLinks(ReadOnlyCollection<MapNode> nodes, MapNode.MoveProbe moveProbe)
		{
			Vector3 position = base.transform.position;
			for (int i = 0; i < nodes.Count; i++)
			{
				MapNode mapNode = nodes[i];
				if (!(mapNode == this))
				{
					Vector3 position2 = mapNode.transform.position;
					float num = MapNode.maxConnectionDistance;
					float num2 = num * num;
					float sqrMagnitude = (position2 - position).sqrMagnitude;
					if (sqrMagnitude < num2)
					{
						float distanceScore = Mathf.Sqrt(sqrMagnitude);
						for (int j = 0; j < 3; j++)
						{
							moveProbe.SetHull((HullClassification)j);
							if ((this.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None && (mapNode.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None)
							{
								Vector3 b = Vector3.up * (moveProbe.testCharacterController.height * 0.5f);
								Vector3 b2 = Vector3.up * 0.01f;
								Vector3 a = moveProbe.GetGroundPosition(position) + b2;
								Vector3 a2 = moveProbe.GetGroundPosition(position2) + b2;
								Vector3 vector = a + b;
								Vector3 vector2 = a2 + b;
								if (moveProbe.CapsuleOverlapTest(vector) && moveProbe.CapsuleOverlapTest(vector2))
								{
									bool flag = moveProbe.GroundTest(vector, vector2, 6f);
									float num3 = (!flag) ? moveProbe.JumpTest(vector, vector2, 7.5f) : 0f;
									if (flag || (num3 > 0f && num3 < 10f))
									{
										this.AddLink(mapNode, distanceScore, num3, (HullClassification)j);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x0010F0BC File Offset: 0x0010D2BC
		private void BuildAirLinks(ReadOnlyCollection<MapNode> nodes, MapNode.MoveProbe moveProbe)
		{
			Vector3 position = base.transform.position;
			for (int i = 0; i < nodes.Count; i++)
			{
				MapNode mapNode = nodes[i];
				if (!(mapNode == this))
				{
					Vector3 position2 = mapNode.transform.position;
					float num = MapNode.maxConnectionDistance * 2f;
					float num2 = num * num;
					float sqrMagnitude = (position2 - position).sqrMagnitude;
					if (sqrMagnitude < num2)
					{
						float distanceScore = Mathf.Sqrt(sqrMagnitude);
						for (int j = 0; j < 3; j++)
						{
							if ((this.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None && (mapNode.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None)
							{
								moveProbe.SetHull((HullClassification)j);
								Vector3 vector = position;
								Vector3 vector2 = position2;
								if (moveProbe.CapsuleOverlapTest(vector) && moveProbe.CapsuleOverlapTest(vector2) && moveProbe.FlyTest(vector, vector2, 6f))
								{
									this.AddLink(mapNode, distanceScore, 0f, (HullClassification)j);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0010F1B0 File Offset: 0x0010D3B0
		private void BuildRailLinks(ReadOnlyCollection<MapNode> nodes, MapNode.MoveProbe moveProbe)
		{
			Vector3 position = base.transform.position;
			for (int i = 0; i < nodes.Count; i++)
			{
				MapNode mapNode = nodes[i];
				if (!(mapNode == this))
				{
					Vector3 position2 = mapNode.transform.position;
					float num = MapNode.maxConnectionDistance * 2f;
					float num2 = num * num;
					float sqrMagnitude = (position2 - position).sqrMagnitude;
					if (sqrMagnitude < num2)
					{
						float distanceScore = Mathf.Sqrt(sqrMagnitude);
						for (int j = 0; j < 3; j++)
						{
							HullDef hullDef = HullDef.Find((HullClassification)j);
							if ((this.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None && (mapNode.forbiddenHulls & (HullMask)(1 << j)) == HullMask.None)
							{
								moveProbe.SetHull((HullClassification)j);
								Vector3 vector = position;
								Vector3 vector2 = position2;
								if (Vector3.Angle(Vector3.up, vector2 - vector) > 50f)
								{
									vector.y += hullDef.height;
									vector2.y += hullDef.height;
									if (moveProbe.CapsuleOverlapTest(vector) && moveProbe.CapsuleOverlapTest(vector2) && moveProbe.FlyTest(vector, vector2, 6f))
									{
										this.AddLink(mapNode, distanceScore, 0f, (HullClassification)j);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0010F2FC File Offset: 0x0010D4FC
		public void BuildLinks(ReadOnlyCollection<MapNode> nodes, MapNodeGroup.GraphType graphType)
		{
			this.links.Clear();
			Vector3 position = base.transform.position;
			MapNode.MoveProbe moveProbe = new MapNode.MoveProbe();
			moveProbe.Init();
			switch (graphType)
			{
			case MapNodeGroup.GraphType.Ground:
				this.BuildGroundLinks(nodes, moveProbe);
				break;
			case MapNodeGroup.GraphType.Air:
				this.BuildAirLinks(nodes, moveProbe);
				break;
			case MapNodeGroup.GraphType.Rail:
				this.BuildRailLinks(nodes, moveProbe);
				break;
			}
			foreach (MapNodeLink mapNodeLink in base.GetComponents<MapNodeLink>())
			{
				if (mapNodeLink.other)
				{
					MapNode.Link link = new MapNode.Link
					{
						nodeB = mapNodeLink.other,
						distanceScore = Vector3.Distance(position, mapNodeLink.other.transform.position),
						minJumpHeight = mapNodeLink.minJumpHeight,
						gateName = mapNodeLink.gateName,
						hullMask = -1
					};
					bool flag = false;
					for (int j = 0; j < this.links.Count; j++)
					{
						if (this.links[j].nodeB == mapNodeLink.other)
						{
							this.links[j] = link;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.links.Add(link);
					}
				}
			}
			moveProbe.Destroy();
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0010F454 File Offset: 0x0010D654
		public bool TestLineOfSight(MapNode other)
		{
			return !Physics.Linecast(base.transform.position + Vector3.up, other.transform.position + Vector3.up, LayerIndex.world.mask);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0010F4A8 File Offset: 0x0010D6A8
		public bool TestNoCeiling()
		{
			return !Physics.Raycast(new Ray(base.transform.position, Vector3.up), float.PositiveInfinity, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0010F4EC File Offset: 0x0010D6EC
		public bool TestTeleporterOK()
		{
			float d = 15f;
			int num = 20;
			float num2 = 7f;
			float num3 = 3f;
			float num4 = 360f / (float)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 b = Quaternion.AngleAxis(num4 * (float)i, Vector3.up) * (Vector3.forward * d);
				Vector3 origin = base.transform.position + b + Vector3.up * num2;
				RaycastHit raycastHit;
				if (!Physics.Raycast(new Ray(origin, Vector3.down), out raycastHit, num3 + num2, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					return false;
				}
			}
			Debug.DrawRay(base.transform.position, base.transform.up * 20f, Color.green, 15f);
			return true;
		}

		// Token: 0x04003FC8 RID: 16328
		private static List<MapNode> _instances = new List<MapNode>();

		// Token: 0x04003FC9 RID: 16329
		private static ReadOnlyCollection<MapNode> instancesReadOnly = MapNode._instances.AsReadOnly();

		// Token: 0x04003FCA RID: 16330
		public static readonly float maxConnectionDistance = 15f;

		// Token: 0x04003FCB RID: 16331
		public List<MapNode.Link> links = new List<MapNode.Link>();

		// Token: 0x04003FCC RID: 16332
		[EnumMask(typeof(HullMask))]
		public HullMask forbiddenHulls;

		// Token: 0x04003FCD RID: 16333
		[EnumMask(typeof(NodeFlags))]
		public NodeFlags flags;

		// Token: 0x04003FCE RID: 16334
		[Tooltip("The name of the nodegraph gate associated with this node. If the named gate is closed this node will be treated as though it does not exist.")]
		public string gateName = "";

		// Token: 0x02000B3B RID: 2875
		public struct Link
		{
			// Token: 0x04003FCF RID: 16335
			public MapNode nodeB;

			// Token: 0x04003FD0 RID: 16336
			public float distanceScore;

			// Token: 0x04003FD1 RID: 16337
			public float minJumpHeight;

			// Token: 0x04003FD2 RID: 16338
			public int hullMask;

			// Token: 0x04003FD3 RID: 16339
			public int jumpHullMask;

			// Token: 0x04003FD4 RID: 16340
			public string gateName;
		}

		// Token: 0x02000B3C RID: 2876
		public class MoveProbe
		{
			// Token: 0x06004186 RID: 16774 RVA: 0x0010F610 File Offset: 0x0010D810
			public void Init()
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "NodeGraphProbe";
				Transform transform = gameObject.transform;
				this.testCharacterController = gameObject.AddComponent<CharacterController>();
				this.testCharacterController.stepOffset = 0.5f;
				this.testCharacterController.slopeLimit = 60f;
			}

			// Token: 0x06004187 RID: 16775 RVA: 0x0010F664 File Offset: 0x0010D864
			public void SetHull(HullClassification hullClassification)
			{
				HullDef hullDef = HullDef.Find(hullClassification);
				this.testCharacterController.radius = hullDef.radius;
				this.testCharacterController.height = hullDef.height;
			}

			// Token: 0x06004188 RID: 16776 RVA: 0x0010F69A File Offset: 0x0010D89A
			public void Destroy()
			{
				UnityEngine.Object.DestroyImmediate(this.testCharacterController.gameObject);
			}

			// Token: 0x06004189 RID: 16777 RVA: 0x000E8EE5 File Offset: 0x000E70E5
			private static float DistanceXZ(Vector3 a, Vector3 b)
			{
				a.y = 0f;
				b.y = 0f;
				return Vector3.Distance(a, b);
			}

			// Token: 0x0600418A RID: 16778 RVA: 0x0010F6AC File Offset: 0x0010D8AC
			public static Vector3 GetGroundPosition(Vector3 footPosition, float height, float radius)
			{
				Vector3 b = Vector3.up * (height * 0.5f - radius);
				Vector3 b2 = Vector3.up * (height * 0.5f);
				Vector3 a = footPosition + b2;
				float num = radius * 0.5f + 0.005f;
				Vector3 a2 = footPosition + Vector3.up * num;
				Vector3 a3 = a + Vector3.up * num;
				RaycastHit raycastHit;
				if (Physics.CapsuleCast(a3 + b, a3 - b, radius, Vector3.down, out raycastHit, num * 2f + 0.005f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					Vector3 b3 = raycastHit.distance * Vector3.down;
					return a2 + b3;
				}
				return footPosition;
			}

			// Token: 0x0600418B RID: 16779 RVA: 0x0010F775 File Offset: 0x0010D975
			public Vector3 GetGroundPosition(Vector3 footPosition)
			{
				return MapNode.MoveProbe.GetGroundPosition(footPosition, this.testCharacterController.height, this.testCharacterController.radius);
			}

			// Token: 0x0600418C RID: 16780 RVA: 0x0010F794 File Offset: 0x0010D994
			public bool CapsuleOverlapTest(Vector3 centerOfCapsule)
			{
				Vector3 b = Vector3.up * (this.testCharacterController.height * 0.5f - this.testCharacterController.radius);
				Vector3.up * (this.testCharacterController.height * 0.5f);
				return Physics.OverlapCapsule(centerOfCapsule + b, centerOfCapsule - b, this.testCharacterController.radius, LayerIndex.world.mask | LayerIndex.defaultLayer.mask, QueryTriggerInteraction.Ignore).Length == 0;
			}

			// Token: 0x0600418D RID: 16781 RVA: 0x0010F830 File Offset: 0x0010DA30
			public bool FlyTest(Vector3 startPos, Vector3 endPos, float flySpeed)
			{
				Vector3 b = Vector3.up * (this.testCharacterController.height * 0.5f - this.testCharacterController.radius);
				return !Physics.CapsuleCast(startPos + b, startPos - b, this.testCharacterController.radius, (endPos - startPos).normalized, (endPos - startPos).magnitude, LayerIndex.world.mask);
			}

			// Token: 0x0600418E RID: 16782 RVA: 0x0010F8B6 File Offset: 0x0010DAB6
			private void MoveCapsule(Vector3 displacement)
			{
				this.testCharacterController.Move(displacement);
			}

			// Token: 0x0600418F RID: 16783 RVA: 0x0010F8C5 File Offset: 0x0010DAC5
			private void SetCapsulePosition(Vector3 position)
			{
				this.testCharacterController.transform.position = position;
				Physics.SyncTransforms();
			}

			// Token: 0x06004190 RID: 16784 RVA: 0x0010F8E0 File Offset: 0x0010DAE0
			public bool GroundTest(Vector3 startCenterOfCapsulePos, Vector3 endCenterOfCapsulePos, float hSpeed)
			{
				this.MoveCapsule(Vector3.zero);
				Vector3 a = Vector3.zero;
				float num = MapNode.MoveProbe.DistanceXZ(startCenterOfCapsulePos, endCenterOfCapsulePos);
				this.SetCapsulePosition(startCenterOfCapsulePos + Vector3.up);
				int num2 = Mathf.CeilToInt(num * 1.5f / hSpeed / this.testTimeStep);
				Vector3 rhs = this.testCharacterController.transform.position;
				for (int i = 0; i < num2; i++)
				{
					Vector3 vector = endCenterOfCapsulePos - this.testCharacterController.transform.position;
					if (vector.sqrMagnitude <= 0.25f)
					{
						return true;
					}
					Vector3 vector2 = vector;
					vector2.y = 0f;
					vector2.Normalize();
					a.x = vector2.x * hSpeed;
					a.z = vector2.z * hSpeed;
					a += Physics.gravity * this.testTimeStep;
					this.MoveCapsule(a * this.testTimeStep);
					Vector3 position = this.testCharacterController.transform.position;
					if (position == rhs)
					{
						return false;
					}
					rhs = position;
				}
				return false;
			}

			// Token: 0x06004191 RID: 16785 RVA: 0x0010F9F8 File Offset: 0x0010DBF8
			public float JumpTest(Vector3 startCenterOfCapsulePos, Vector3 endCenterOfCapsulePos, float hSpeed)
			{
				float y = Trajectory.CalculateInitialYSpeed(Trajectory.CalculateGroundTravelTime(hSpeed, MapNode.MoveProbe.DistanceXZ(startCenterOfCapsulePos, endCenterOfCapsulePos)), endCenterOfCapsulePos.y - startCenterOfCapsulePos.y);
				this.testCharacterController.Move(Vector3.zero);
				Vector3 a = endCenterOfCapsulePos - startCenterOfCapsulePos;
				a.y = 0f;
				a.Normalize();
				a *= hSpeed;
				a.y = y;
				float num = MapNode.MoveProbe.DistanceXZ(startCenterOfCapsulePos, endCenterOfCapsulePos);
				this.SetCapsulePosition(startCenterOfCapsulePos);
				int num2 = Mathf.CeilToInt(num * 1.5f / hSpeed / this.testTimeStep);
				float num3 = float.NegativeInfinity;
				Vector3 rhs = this.testCharacterController.transform.position;
				for (int i = 0; i < num2; i++)
				{
					Vector3 vector = endCenterOfCapsulePos - this.testCharacterController.transform.position;
					if (vector.sqrMagnitude <= 4f)
					{
						return num3 - startCenterOfCapsulePos.y;
					}
					num3 = Mathf.Max(this.testCharacterController.transform.position.y, num3);
					Vector3 vector2 = vector;
					vector2.y = 0f;
					vector2.Normalize();
					a.x = vector2.x * hSpeed;
					a.z = vector2.z * hSpeed;
					a += Physics.gravity * this.testTimeStep;
					this.testCharacterController.Move(a * this.testTimeStep);
					Vector3 position = this.testCharacterController.transform.position;
					if (position == rhs)
					{
						return 0f;
					}
					rhs = position;
				}
				return 0f;
			}

			// Token: 0x04003FD5 RID: 16341
			public CharacterController testCharacterController;

			// Token: 0x04003FD6 RID: 16342
			private float testTimeStep = 0.06666667f;
		}
	}
}
