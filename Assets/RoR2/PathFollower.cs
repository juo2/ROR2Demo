using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000981 RID: 2433
	public class PathFollower
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000E8E50 File Offset: 0x000E7050
		// (set) Token: 0x06003733 RID: 14131 RVA: 0x000E8E58 File Offset: 0x000E7058
		public NodeGraph nodeGraph { get; private set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000E8E61 File Offset: 0x000E7061
		// (set) Token: 0x06003735 RID: 14133 RVA: 0x000E8E69 File Offset: 0x000E7069
		public NodeGraph.NodeIndex nextNode { get; private set; } = NodeGraph.NodeIndex.invalid;

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000E8E72 File Offset: 0x000E7072
		public bool isFinished
		{
			get
			{
				return this.currentWaypoint >= this.waypoints.Count;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool isOnJumpLink
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000E8E8A File Offset: 0x000E708A
		public bool hasPassedFirstWaypoint
		{
			get
			{
				return this.currentWaypoint > 0;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000E8E98 File Offset: 0x000E7098
		public bool nextWaypointNeedsJump
		{
			get
			{
				return this.waypoints.Count > 0 && this.currentWaypoint < this.waypoints.Count && this.waypoints[this.currentWaypoint].minJumpHeight > 0f;
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000E8EE5 File Offset: 0x000E70E5
		private static float DistanceXZ(Vector3 a, Vector3 b)
		{
			a.y = 0f;
			b.y = 0f;
			return Vector3.Distance(a, b);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000E8F08 File Offset: 0x000E7108
		public float CalculateJumpVelocityNeededToReachNextWaypoint(float moveSpeed)
		{
			if (!this.nextWaypointNeedsJump)
			{
				return 0f;
			}
			Vector3 vector = this.currentPosition;
			Vector3 vector2;
			if (!this.GetNextNodePosition(out vector2))
			{
				return 0f;
			}
			return Trajectory.CalculateInitialYSpeed(Trajectory.CalculateGroundTravelTime(moveSpeed, PathFollower.DistanceXZ(vector, vector2)), vector2.y - vector.y);
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000E8F5C File Offset: 0x000E715C
		public void UpdatePosition(Vector3 newPosition)
		{
			Vector3 vector = this.currentPosition;
			this.currentPosition = newPosition;
			Vector3 vector2;
			if (this.GetNextNodePosition(out vector2))
			{
				Vector3 vector3 = vector2 - this.currentPosition;
				Vector3 vector4 = vector3;
				vector4.y = 0f;
				float num = 2f;
				Vector3 inNormal = this.currentPosition - vector;
				Plane plane = new Plane(inNormal, vector2);
				if (plane.GetSide(this.currentPosition) != plane.GetSide(vector))
				{
					num += 2f;
				}
				if (this.waypoints.Count > this.currentWaypoint + 1 && this.waypoints[this.currentWaypoint + 1].minJumpHeight > 0f)
				{
					num = 0.5f;
				}
				this.debugPassDistance = num;
				if (num * num >= vector4.sqrMagnitude && Mathf.Abs(vector3.y) <= 2f)
				{
					this.SetWaypoint(this.currentWaypoint + 1);
				}
			}
			this.nextNode != NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000E906C File Offset: 0x000E726C
		private void SetWaypoint(int newWaypoint)
		{
			this.currentWaypoint = Math.Min(newWaypoint, this.waypoints.Count);
			if (this.currentWaypoint == this.waypoints.Count)
			{
				this.nextNode = NodeGraph.NodeIndex.invalid;
				this.previousNode = NodeGraph.NodeIndex.invalid;
				return;
			}
			this.nextNode = this.waypoints[this.currentWaypoint].nodeIndex;
			this.previousNode = ((this.currentWaypoint > 0) ? this.waypoints[this.currentWaypoint - 1].nodeIndex : NodeGraph.NodeIndex.invalid);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000E9104 File Offset: 0x000E7304
		public void Reset()
		{
			this.nodeGraph = null;
			this.nextNode = NodeGraph.NodeIndex.invalid;
			this.previousNode = NodeGraph.NodeIndex.invalid;
			this.waypoints.Clear();
			this.currentWaypoint = 0;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000E9138 File Offset: 0x000E7338
		public void SetPath(Path newPath)
		{
			if (this.nodeGraph != newPath.nodeGraph)
			{
				this.Reset();
				this.nodeGraph = newPath.nodeGraph;
			}
			this.waypoints.Clear();
			newPath.WriteWaypointsToList(this.waypoints);
			this.currentWaypoint = 0;
			for (int i = 1; i < this.waypoints.Count; i++)
			{
				if (this.waypoints[i].nodeIndex == this.nextNode && this.waypoints[i - 1].nodeIndex == this.previousNode)
				{
					this.currentWaypoint = i;
					break;
				}
			}
			this.SetWaypoint(this.currentWaypoint);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000E91F4 File Offset: 0x000E73F4
		private void GetPreviousAndNextNodePositions(out Vector3? previousPosition, out Vector3? nextPosition)
		{
			previousPosition = null;
			nextPosition = null;
			if (this.nodeGraph == null)
			{
				return;
			}
			Vector3 value;
			if (this.nodeGraph.GetNodePosition(this.previousNode, out value))
			{
				previousPosition = new Vector3?(value);
			}
			if (this.nodeGraph.GetNodePosition(this.nextNode, out value))
			{
				nextPosition = new Vector3?(value);
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000E9260 File Offset: 0x000E7460
		private bool GetNextNodePosition(out Vector3 nextPosition)
		{
			if (this.nodeGraph != null && this.nextNode != NodeGraph.NodeIndex.invalid && this.nodeGraph.GetNodePosition(this.nextNode, out nextPosition))
			{
				return true;
			}
			nextPosition = this.currentPosition;
			return false;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000E92B0 File Offset: 0x000E74B0
		public Vector3? GetNextPosition()
		{
			Vector3 value;
			if (this.GetNextNodePosition(out value))
			{
				return new Vector3?(value);
			}
			return null;
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000E92D8 File Offset: 0x000E74D8
		public void DebugDrawPath(Color color, float duration)
		{
			for (int i = 1; i < this.waypoints.Count; i++)
			{
				Vector3 start;
				this.nodeGraph.GetNodePosition(this.waypoints[i].nodeIndex, out start);
				Vector3 end;
				this.nodeGraph.GetNodePosition(this.waypoints[i - 1].nodeIndex, out end);
				Debug.DrawLine(start, end, color, duration);
			}
			for (int j = 0; j < this.waypoints.Count; j++)
			{
				Vector3 a;
				this.nodeGraph.GetNodePosition(this.waypoints[j].nodeIndex, out a);
				Debug.DrawLine(a + Vector3.up, a - Vector3.up, color, duration);
			}
		}

		// Token: 0x04003797 RID: 14231
		private Vector3 currentPosition;

		// Token: 0x04003798 RID: 14232
		private const float waypointPassDistance = 2f;

		// Token: 0x04003799 RID: 14233
		private const float waypointPassYTolerance = 2f;

		// Token: 0x0400379B RID: 14235
		private List<Path.Waypoint> waypoints = new List<Path.Waypoint>();

		// Token: 0x0400379C RID: 14236
		private int currentWaypoint;

		// Token: 0x0400379D RID: 14237
		private NodeGraph.NodeIndex previousNode = NodeGraph.NodeIndex.invalid;

		// Token: 0x0400379F RID: 14239
		private float debugPassDistance;

		// Token: 0x02000982 RID: 2434
		public class Drawer : IDisposable
		{
			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x06003745 RID: 14149 RVA: 0x000E93C1 File Offset: 0x000E75C1
			// (set) Token: 0x06003746 RID: 14150 RVA: 0x000E93CC File Offset: 0x000E75CC
			public PathFollower target
			{
				get
				{
					return this._target;
				}
				set
				{
					if (this._target == value)
					{
						return;
					}
					this._target = value;
					this.pathDrawer.enabled = (this._target != null);
					this.passDistanceDrawer.enabled &= (this._target != null);
					this.waypointCache.Clear();
				}
			}

			// Token: 0x06003747 RID: 14151 RVA: 0x000E9424 File Offset: 0x000E7624
			public Drawer(PathFollower target)
			{
				this.cachedCurrentWaypoint = -1;
				this.waypointCache = new List<Path.Waypoint>();
				this.wireMeshBuilder = new WireMeshBuilder();
				this.passDistanceDrawer = DebugOverlay.GetMeshDrawer();
				this.pathDrawer = DebugOverlay.GetMeshDrawer();
				int num = 32;
				PathFollower.Drawer.<>c__DisplayClass9_0 CS$<>8__locals1;
				CS$<>8__locals1.sliceSize = 360f / (float)num;
				CS$<>8__locals1.halfForward = Vector3.forward * 0.5f;
				Vector3 p = PathFollower.Drawer.<.ctor>g__GetCirclePosition|9_0(-1, ref CS$<>8__locals1);
				for (int i = 0; i < num; i++)
				{
					Vector3 vector = PathFollower.Drawer.<.ctor>g__GetCirclePosition|9_0(i, ref CS$<>8__locals1);
					this.wireMeshBuilder.AddLine(p, Color.yellow, vector, Color.yellow);
					p = vector;
				}
				this.passDistanceDrawer.hasMeshOwnership = true;
				this.passDistanceDrawer.mesh = this.wireMeshBuilder.GenerateMesh();
				this.wireMeshBuilder.Clear();
				this.pathDrawer.mesh = new Mesh();
				RoR2Application.onFixedUpdate += this.FixedUpdate;
				this.target = target;
			}

			// Token: 0x06003748 RID: 14152 RVA: 0x000E9524 File Offset: 0x000E7724
			private void SetWaypoints(List<Path.Waypoint> newWaypoints, int newCurrentWaypoint)
			{
				if (ListUtils.SequenceEquals<Path.Waypoint>(this.waypointCache, newWaypoints) && this.cachedCurrentWaypoint.Equals(newCurrentWaypoint))
				{
					return;
				}
				this.waypointCache.Clear();
				this.waypointCache.AddRange(newWaypoints);
				this.cachedCurrentWaypoint = newCurrentWaypoint;
				this.RebuildPathMesh();
			}

			// Token: 0x06003749 RID: 14153 RVA: 0x000E9574 File Offset: 0x000E7774
			private void RebuildPathMesh()
			{
				List<Path.Waypoint> waypoints = this.target.waypoints;
				if (waypoints.Count > 1)
				{
					PathFollower.Drawer.<>c__DisplayClass11_0 CS$<>8__locals1;
					CS$<>8__locals1.nodeColor = new Color(1f, 0.5f, 0.5f, 0.25f);
					CS$<>8__locals1.traversedPathColor = new Color(0.5f, 0.5f, 0.5f, 0.25f);
					CS$<>8__locals1.currentPathColor = new Color(1f, 0.5f, 0.5f, 1f);
					CS$<>8__locals1.untraversedPathColor = new Color(0.5f, 0f, 0f, 0.25f);
					Vector3 vector;
					this.target.nodeGraph.GetNodePosition(waypoints[0].nodeIndex, out vector);
					CS$<>8__locals1.drawPathColor = CS$<>8__locals1.untraversedPathColor;
					this.<RebuildPathMesh>g__DrawNode|11_1(vector, ref CS$<>8__locals1);
					for (int i = 1; i < waypoints.Count; i++)
					{
						this.<RebuildPathMesh>g__UpdateDrawPathColor|11_0(i, ref CS$<>8__locals1);
						Vector3 vector2;
						this.target.nodeGraph.GetNodePosition(waypoints[i].nodeIndex, out vector2);
						this.wireMeshBuilder.AddLine(vector, CS$<>8__locals1.drawPathColor, vector2, CS$<>8__locals1.drawPathColor);
						this.<RebuildPathMesh>g__DrawNode|11_1(vector2, ref CS$<>8__locals1);
						vector = vector2;
					}
				}
				this.wireMeshBuilder.GenerateMesh(this.pathDrawer.mesh);
				this.wireMeshBuilder.Clear();
			}

			// Token: 0x0600374A RID: 14154 RVA: 0x000E96D0 File Offset: 0x000E78D0
			private void FixedUpdate()
			{
				if (this.target == null)
				{
					return;
				}
				this.passDistanceDrawer.enabled = (this.target.currentWaypoint < this.target.waypoints.Count);
				if (this.passDistanceDrawer.enabled)
				{
					Vector3 position;
					this.target.nodeGraph.GetNodePosition(this.target.waypoints[this.target.currentWaypoint].nodeIndex, out position);
					this.passDistanceDrawer.transform.position = position;
					this.passDistanceDrawer.transform.localScale = Vector3.one * this.target.debugPassDistance;
				}
				this.SetWaypoints(this.target.waypoints, this.target.currentWaypoint);
			}

			// Token: 0x0600374B RID: 14155 RVA: 0x000E97A0 File Offset: 0x000E79A0
			public void Dispose()
			{
				this.target = null;
				RoR2Application.onFixedUpdate -= this.FixedUpdate;
				this.pathDrawer.Dispose();
				this.passDistanceDrawer.Dispose();
				this.wireMeshBuilder.Dispose();
			}

			// Token: 0x0600374C RID: 14156 RVA: 0x000E97DB File Offset: 0x000E79DB
			[CompilerGenerated]
			internal static Vector3 <.ctor>g__GetCirclePosition|9_0(int i, ref PathFollower.Drawer.<>c__DisplayClass9_0 A_1)
			{
				return Quaternion.AngleAxis((float)i * A_1.sliceSize, Vector3.up) * A_1.halfForward;
			}

			// Token: 0x0600374D RID: 14157 RVA: 0x000E97FB File Offset: 0x000E79FB
			[CompilerGenerated]
			private void <RebuildPathMesh>g__UpdateDrawPathColor|11_0(int i, ref PathFollower.Drawer.<>c__DisplayClass11_0 A_2)
			{
				if (this.cachedCurrentWaypoint == i)
				{
					A_2.drawPathColor = A_2.currentPathColor;
					return;
				}
				if (this.cachedCurrentWaypoint < i)
				{
					A_2.drawPathColor = A_2.untraversedPathColor;
					return;
				}
				A_2.drawPathColor = A_2.traversedPathColor;
			}

			// Token: 0x0600374E RID: 14158 RVA: 0x000E9835 File Offset: 0x000E7A35
			[CompilerGenerated]
			private void <RebuildPathMesh>g__DrawNode|11_1(Vector3 position, ref PathFollower.Drawer.<>c__DisplayClass11_0 A_2)
			{
				this.wireMeshBuilder.AddLine(position - Vector3.up, A_2.nodeColor, position + Vector3.up, A_2.nodeColor);
			}

			// Token: 0x040037A0 RID: 14240
			private PathFollower _target;

			// Token: 0x040037A1 RID: 14241
			private WireMeshBuilder wireMeshBuilder;

			// Token: 0x040037A2 RID: 14242
			private DebugOverlay.MeshDrawer passDistanceDrawer;

			// Token: 0x040037A3 RID: 14243
			private DebugOverlay.MeshDrawer pathDrawer;

			// Token: 0x040037A4 RID: 14244
			private List<Path.Waypoint> waypointCache;

			// Token: 0x040037A5 RID: 14245
			private int cachedCurrentWaypoint;
		}
	}
}
