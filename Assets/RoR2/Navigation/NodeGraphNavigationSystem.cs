using System;
using HG;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B5A RID: 2906
	public class NodeGraphNavigationSystem : BroadNavigationSystem
	{
		// Token: 0x06004200 RID: 16896 RVA: 0x00111E06 File Offset: 0x00110006
		public new NodeGraphNavigationSystem.Agent GetAgent(BroadNavigationSystem.AgentHandle agentHandle)
		{
			return new NodeGraphNavigationSystem.Agent(base.GetAgent(agentHandle));
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x00111E14 File Offset: 0x00110014
		protected override void CreateAgent(in BroadNavigationSystem.AgentHandle index)
		{
			ArrayUtils.EnsureCapacity<NodeGraphNavigationSystem.AgentData>(ref this.allAgentData, base.agentCount);
			this.allAgentData[index].Initialize();
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x00111E39 File Offset: 0x00110039
		protected override void DestroyAgent(in BroadNavigationSystem.AgentHandle index)
		{
			this.allAgentData[index].Dispose();
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x00111E50 File Offset: 0x00110050
		protected override void UpdateAgents(float deltaTime)
		{
			for (BroadNavigationSystem.AgentHandle agentHandle = (BroadNavigationSystem.AgentHandle)0; agentHandle < (BroadNavigationSystem.AgentHandle)this.allAgentData.Length; agentHandle++)
			{
				this.UpdateAgent(agentHandle);
			}
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x00111E78 File Offset: 0x00110078
		protected override void ConfigureAgentFromBody(in BroadNavigationSystem.AgentHandle index, CharacterBody body)
		{
			ref NodeGraphNavigationSystem.AgentData ptr = ref this.allAgentData[index];
			if (body)
			{
				ptr.hullClassification = body.hullClassification;
				ptr.nodeGraph = (SceneInfo.instance ? SceneInfo.instance.GetNodeGraph(body.isFlying ? MapNodeGroup.GraphType.Air : MapNodeGroup.GraphType.Ground) : null);
				return;
			}
			ptr.hullClassification = HullClassification.Human;
			ptr.nodeGraph = null;
			ptr.InvalidatePath();
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x00111EE7 File Offset: 0x001100E7
		protected override void InvalidateAgentPath(in BroadNavigationSystem.AgentHandle index)
		{
			this.allAgentData[index].InvalidatePath();
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x00111EFC File Offset: 0x001100FC
		private void UpdateAgent(in BroadNavigationSystem.AgentHandle agentHandle)
		{
			ref readonly BroadNavigationSystem.BaseAgentData agentData = ref base.GetAgentData(agentHandle);
			if (!agentData.enabled)
			{
				return;
			}
			ref NodeGraphNavigationSystem.AgentData ptr = ref this.allAgentData[(int)agentHandle];
			if (ptr.pathTask == null && ptr.nextUpdate <= this.localTime)
			{
				ptr.nextUpdate = this.localTime + ptr.updateInterval;
				NodeGraph.NodeIndex? lastKnownNode = ptr.lastKnownNode;
				NodeGraph.NodeIndex nextNode = ptr.pathFollower.nextNode;
				if (lastKnownNode == null || (lastKnownNode != null && lastKnownNode.GetValueOrDefault() != nextNode))
				{
					ptr.lastKnownNode = new NodeGraph.NodeIndex?(ptr.pathFollower.nextNode);
					ptr.timeLastKnownNodeEncountered = this.localTime;
					Vector3? nextPosition = ptr.pathFollower.GetNextPosition();
					if (nextPosition != null && agentData.currentPosition != null && agentData.maxWalkSpeed > 0f)
					{
						ptr.estimatedArrivalTimeAtLastKnownNode = this.localTime + (nextPosition.Value - agentData.currentPosition.Value).magnitude / agentData.maxWalkSpeed;
					}
					else
					{
						ptr.estimatedArrivalTimeAtLastKnownNode = float.PositiveInfinity;
					}
				}
				if (agentData.currentPosition != null && agentData.goalPosition != null && ptr.nodeGraph && ptr.lastKnownNode != null)
				{
					ptr.pathRequest.startPos = agentData.currentPosition.Value;
					if (this.localTime <= ptr.estimatedArrivalTimeAtLastKnownNode + 1f)
					{
						Vector3 a;
						ptr.nodeGraph.GetNodePosition(ptr.lastKnownNode.Value, out a);
						float num = (agentData.maxWalkSpeed + ptr.updateInterval) * 1.5f;
						float num2 = num * num;
						RaycastHit raycastHit;
						if ((a - agentData.currentPosition.Value).sqrMagnitude < num2 && !Physics.Linecast(agentData.currentPosition.Value, a + new Vector3(0f, 0.2f, 0f), out raycastHit, LayerIndex.world.intVal, QueryTriggerInteraction.Ignore))
						{
							ptr.pathRequest.startPos = ptr.lastKnownNode.Value;
						}
					}
					ptr.pathRequest.endPos = agentData.goalPosition.Value;
					ptr.pathRequest.hullClassification = ptr.hullClassification;
					ptr.pathRequest.maxJumpHeight = agentData.maxJumpHeight;
					ptr.pathRequest.maxSlope = agentData.maxSlopeAngle;
					ptr.pathRequest.maxSpeed = agentData.maxWalkSpeed;
					ptr.pathRequest.path = new Path(ptr.nodeGraph);
					ptr.pathTask = ptr.nodeGraph.ComputePath(ptr.pathRequest);
				}
			}
			if (ptr.pathTask != null && ptr.pathTask.status == PathTask.TaskStatus.Complete)
			{
				ptr.pathFollower.SetPath(ptr.pathTask.path);
				ptr.lastTargetReachableUpdate = this.localTime;
				ptr.targetReachable = ptr.pathTask.wasReachable;
				ptr.pathTask = null;
			}
			if (agentData.currentPosition != null)
			{
				ptr.pathFollower.UpdatePosition(agentData.currentPosition.Value);
			}
			Vector3? nextPosition2 = ptr.pathFollower.GetNextPosition();
			if (ptr.targetReachable && nextPosition2 == null)
			{
				nextPosition2 = agentData.goalPosition;
			}
			BroadNavigationSystem.AgentOutput agentOutput = new BroadNavigationSystem.AgentOutput
			{
				nextPosition = nextPosition2,
				desiredJumpVelocity = (ptr.pathFollower.nextWaypointNeedsJump ? ptr.pathFollower.CalculateJumpVelocityNeededToReachNextWaypoint(agentData.maxWalkSpeed) : 0f),
				targetReachable = ptr.targetReachable,
				lastPathUpdate = ptr.lastTargetReachableUpdate
			};
			base.SetAgentOutput(agentHandle, agentOutput);
		}

		// Token: 0x04004034 RID: 16436
		private NodeGraphNavigationSystem.AgentData[] allAgentData = Array.Empty<NodeGraphNavigationSystem.AgentData>();

		// Token: 0x02000B5B RID: 2907
		public struct AgentData
		{
			// Token: 0x06004208 RID: 16904 RVA: 0x001122D8 File Offset: 0x001104D8
			public void Initialize()
			{
				this.nextUpdate = float.NegativeInfinity;
				this.nodeGraph = null;
				this.updateInterval = 2f;
				this.hullClassification = HullClassification.Human;
				this.pathFollower = (this.pathFollower ?? new PathFollower());
				this.pathRequest = (this.pathRequest ?? new NodeGraph.PathRequest());
				this.targetReachable = false;
				this.lastTargetReachableUpdate = float.NegativeInfinity;
				this.lastKnownNode = null;
				this.timeLastKnownNodeEncountered = float.NegativeInfinity;
				this.estimatedArrivalTimeAtLastKnownNode = float.NegativeInfinity;
				this.drawPath = false;
			}

			// Token: 0x06004209 RID: 16905 RVA: 0x0011236E File Offset: 0x0011056E
			public void Dispose()
			{
				this.drawPath = false;
				this.nodeGraph = null;
				this.nextUpdate = float.NegativeInfinity;
				this.pathFollower.Reset();
				this.pathRequest.Reset();
				this.pathTask = null;
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x0600420A RID: 16906 RVA: 0x001123A6 File Offset: 0x001105A6
			// (set) Token: 0x0600420B RID: 16907 RVA: 0x001123B1 File Offset: 0x001105B1
			public bool drawPath
			{
				get
				{
					return this.pathDrawer != null;
				}
				set
				{
					if (this.drawPath == value)
					{
						return;
					}
					if (!value)
					{
						this.pathDrawer.Dispose();
						this.pathDrawer = null;
						return;
					}
					this.pathDrawer = new PathFollower.Drawer(this.pathFollower);
				}
			}

			// Token: 0x0600420C RID: 16908 RVA: 0x001123E4 File Offset: 0x001105E4
			public void InvalidatePath()
			{
				this.nextUpdate = float.NegativeInfinity;
				this.pathTask = null;
			}

			// Token: 0x04004035 RID: 16437
			public NodeGraph nodeGraph;

			// Token: 0x04004036 RID: 16438
			public PathFollower pathFollower;

			// Token: 0x04004037 RID: 16439
			public NodeGraph.PathRequest pathRequest;

			// Token: 0x04004038 RID: 16440
			public HullClassification hullClassification;

			// Token: 0x04004039 RID: 16441
			public PathTask pathTask;

			// Token: 0x0400403A RID: 16442
			public float updateInterval;

			// Token: 0x0400403B RID: 16443
			public float nextUpdate;

			// Token: 0x0400403C RID: 16444
			public bool targetReachable;

			// Token: 0x0400403D RID: 16445
			public float lastTargetReachableUpdate;

			// Token: 0x0400403E RID: 16446
			public NodeGraph.NodeIndex? lastKnownNode;

			// Token: 0x0400403F RID: 16447
			public float timeLastKnownNodeEncountered;

			// Token: 0x04004040 RID: 16448
			public float estimatedArrivalTimeAtLastKnownNode;

			// Token: 0x04004041 RID: 16449
			private PathFollower.Drawer pathDrawer;
		}

		// Token: 0x02000B5C RID: 2908
		public new struct Agent
		{
			// Token: 0x0600420D RID: 16909 RVA: 0x001123F8 File Offset: 0x001105F8
			public Agent(BroadNavigationSystem.Agent inner)
			{
				this.inner = inner;
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x0600420E RID: 16910 RVA: 0x00112401 File Offset: 0x00110601
			private NodeGraphNavigationSystem system
			{
				get
				{
					return (NodeGraphNavigationSystem)this.inner.system;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x0600420F RID: 16911 RVA: 0x00112413 File Offset: 0x00110613
			private ref NodeGraphNavigationSystem.AgentData agentData
			{
				get
				{
					return ref this.system.allAgentData[(int)this.inner.handle];
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06004210 RID: 16912 RVA: 0x00112430 File Offset: 0x00110630
			// (set) Token: 0x06004211 RID: 16913 RVA: 0x0011243D File Offset: 0x0011063D
			public NodeGraph nodeGraph
			{
				get
				{
					return this.agentData.nodeGraph;
				}
				set
				{
					this.agentData.nodeGraph = value;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06004212 RID: 16914 RVA: 0x0011244B File Offset: 0x0011064B
			// (set) Token: 0x06004213 RID: 16915 RVA: 0x00112458 File Offset: 0x00110658
			public HullClassification hullClassification
			{
				get
				{
					return this.agentData.hullClassification;
				}
				set
				{
					this.agentData.hullClassification = value;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06004214 RID: 16916 RVA: 0x00112466 File Offset: 0x00110666
			// (set) Token: 0x06004215 RID: 16917 RVA: 0x00112473 File Offset: 0x00110673
			public bool drawPath
			{
				get
				{
					return this.agentData.drawPath;
				}
				set
				{
					this.agentData.drawPath = value;
				}
			}

			// Token: 0x06004216 RID: 16918 RVA: 0x00112481 File Offset: 0x00110681
			public void InvalidatePath()
			{
				this.agentData.InvalidatePath();
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06004217 RID: 16919 RVA: 0x0011248E File Offset: 0x0011068E
			// (set) Token: 0x06004218 RID: 16920 RVA: 0x0011249B File Offset: 0x0011069B
			public PathFollower pathFollower
			{
				get
				{
					return this.agentData.pathFollower;
				}
				set
				{
					this.agentData.pathFollower = value;
				}
			}

			// Token: 0x06004219 RID: 16921 RVA: 0x001124A9 File Offset: 0x001106A9
			public void DebugDrawPath(Color color, float duration)
			{
				this.agentData.pathFollower.DebugDrawPath(color, duration);
			}

			// Token: 0x0600421A RID: 16922 RVA: 0x001124BD File Offset: 0x001106BD
			public static explicit operator NodeGraphNavigationSystem.Agent(BroadNavigationSystem.Agent other)
			{
				return new NodeGraphNavigationSystem.Agent(other);
			}

			// Token: 0x04004042 RID: 16450
			private readonly BroadNavigationSystem.Agent inner;
		}
	}
}
