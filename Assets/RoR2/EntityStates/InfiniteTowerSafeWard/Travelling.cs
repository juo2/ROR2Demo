using System;
using System.Collections.Generic;
using HG;
using RoR2;
using RoR2.Audio;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002FD RID: 765
	public class Travelling : BaseSafeWardState
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x000394AA File Offset: 0x000376AA
		public Travelling()
		{
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000394BD File Offset: 0x000376BD
		public Travelling(Xoroshiro128Plus rng)
		{
			this.rng = rng;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000394D8 File Offset: 0x000376D8
		public override void OnEnter()
		{
			base.OnEnter();
			this.didFail = false;
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			this.ResetVoTimer();
			if (this.loopSoundDef)
			{
				this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
			}
			if (this.zone)
			{
				this.zone.Networkradius = this.radius;
			}
			if (NetworkServer.active)
			{
				this.groundNodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
				this.potentialEndNodes = this.groundNodeGraph.FindNodesInRangeWithFlagConditions(base.transform.position, 0f, this.minDistanceToNewLocation, HullMask.Human, NodeFlags.TeleporterOK, NodeFlags.None, false);
				Util.ShuffleList<NodeGraph.NodeIndex>(this.potentialEndNodes, this.rng);
				List<NodeGraph.NodeIndex> list = this.groundNodeGraph.FindNodesInRangeWithFlagConditions(base.transform.position, this.minDistanceToNewLocation, this.maxDistanceToNewLocation, HullMask.Human, NodeFlags.TeleporterOK, NodeFlags.None, false);
				Util.ShuffleList<NodeGraph.NodeIndex>(list, this.rng);
				this.potentialEndNodes.AddRange(list);
				this.groundPath = new Path(this.groundNodeGraph);
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00039605 File Offset: 0x00037805
		public override void OnExit()
		{
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			base.OnExit();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00039618 File Offset: 0x00037818
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.voTimer -= Time.fixedDeltaTime;
			if (this.voTimer <= 0f)
			{
				this.ResetVoTimer();
				Util.PlaySound(this.voSoundString, base.gameObject);
			}
			if (NetworkServer.active)
			{
				if (!this.didFail)
				{
					if (this.currentCurve == null)
					{
						if (this.potentialEndNodes.Count > 0)
						{
							this.EvaluateNextEndpoint();
						}
						else
						{
							this.didFail = true;
							Debug.LogError("SafeWard failed to find endpoint!");
						}
					}
					else
					{
						this.tCurve = this.currentCurve.AdvanceTByDistance(this.tCurve, this.travelSpeed * Time.fixedDeltaTime);
						this.tCurve = Mathf.Min(1f, this.tCurve);
						base.transform.position = this.currentCurve.Evaluate(this.tCurve);
						Vector3 target = this.currentCurve.EvaluateDerivative(this.tCurve);
						target.y = 0f;
						target = target.normalized;
						Vector3 forward = Vector3.SmoothDamp(base.transform.forward, target, ref this.rotationVelocity, 0.5f);
						base.transform.forward = forward;
						while (this.tCurve >= 1f && this.GetRemainingCurveSegmentCount() > 0)
						{
							this.tCurve -= 1f;
							this.catmullRomIndex++;
							this.UpdateCurveSegmentPoints();
						}
					}
				}
				if (this.didFail || (this.GetRemainingCurveSegmentCount() <= 0 && this.tCurve >= 1f))
				{
					this.outer.SetNextState(new Burrow());
				}
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000397C0 File Offset: 0x000379C0
		private void EvaluateNextEndpoint()
		{
			int index = this.potentialEndNodes.Count - 1;
			NodeGraph.NodeIndex nodeIndex = this.potentialEndNodes[index];
			this.potentialEndNodes.RemoveAt(index);
			NodeGraph.PathRequest pathRequest = new NodeGraph.PathRequest
			{
				startPos = base.transform.position,
				endPos = nodeIndex,
				hullClassification = HullClassification.Human,
				maxJumpHeight = this.pathMaxJumpHeight,
				maxSlope = this.pathMaxSlope,
				maxSpeed = this.pathMaxSpeed,
				path = this.groundPath
			};
			PathTask pathTask = this.groundNodeGraph.ComputePath(pathRequest);
			Vector3 vector;
			if (pathTask != null && pathTask.status == PathTask.TaskStatus.Complete && pathTask.wasReachable && pathTask.path.waypointsCount > 1 && this.groundNodeGraph.GetNodePosition(nodeIndex, out vector))
			{
				float num = 1f / (float)(pathTask.path.waypointsCount - 1);
				for (int i = 0; i < pathTask.path.waypointsCount; i++)
				{
					if (i % this.pathNodeInclusionPeriod == 0 || i == pathTask.path.waypointsCount - 1)
					{
						Path.Waypoint waypoint = pathTask.path[i];
						Vector3 item;
						if (this.groundNodeGraph.GetNodePosition(waypoint.nodeIndex, out item))
						{
							if (i > 0 && i < pathTask.path.waypointsCount - 1)
							{
								item.y += this.travelHeight;
							}
							this.catmullRomPoints.Add(item);
						}
					}
				}
				if (this.catmullRomPoints.Count > 1)
				{
					Debug.Log(string.Format("SafeWard will travel to {0}", this.catmullRomPoints[this.catmullRomPoints.Count - 1]));
					Vector3 item2 = 2f * this.catmullRomPoints[0] - this.catmullRomPoints[1];
					this.catmullRomPoints.Insert(0, item2);
					Vector3 item3 = 2f * this.catmullRomPoints[this.catmullRomPoints.Count - 1] - this.catmullRomPoints[this.catmullRomPoints.Count - 2];
					this.catmullRomPoints.Add(item3);
					DirectorCore.instance.AddOccupiedNode(this.groundNodeGraph, nodeIndex);
					this.catmullRomIndex = 0;
					this.currentCurve = new CatmullRom3();
					this.tCurve = 0f;
					this.UpdateCurveSegmentPoints();
					return;
				}
				this.catmullRomPoints.Clear();
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00039A4C File Offset: 0x00037C4C
		private void UpdateCurveSegmentPoints()
		{
			this.currentCurve.SetPoints(this.catmullRomPoints[this.catmullRomIndex], this.catmullRomPoints[this.catmullRomIndex + 1], this.catmullRomPoints[this.catmullRomIndex + 2], this.catmullRomPoints[this.catmullRomIndex + 3]);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00039AAE File Offset: 0x00037CAE
		private int GetRemainingCurveSegmentCount()
		{
			return this.catmullRomPoints.Count - 4 - this.catmullRomIndex;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00039AC4 File Offset: 0x00037CC4
		private void ResetVoTimer()
		{
			this.voTimer = UnityEngine.Random.Range(this.minimumVoDelay, this.maximumVoDelay);
		}

		// Token: 0x040010AB RID: 4267
		[SerializeField]
		public float radius;

		// Token: 0x040010AC RID: 4268
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040010AD RID: 4269
		[SerializeField]
		public string animationStateName;

		// Token: 0x040010AE RID: 4270
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040010AF RID: 4271
		[SerializeField]
		public float minDistanceToNewLocation;

		// Token: 0x040010B0 RID: 4272
		[SerializeField]
		public float maxDistanceToNewLocation;

		// Token: 0x040010B1 RID: 4273
		[SerializeField]
		public float travelSpeed;

		// Token: 0x040010B2 RID: 4274
		[SerializeField]
		public float travelHeight;

		// Token: 0x040010B3 RID: 4275
		[SerializeField]
		public float pathMaxSlope;

		// Token: 0x040010B4 RID: 4276
		[SerializeField]
		public float pathMaxJumpHeight;

		// Token: 0x040010B5 RID: 4277
		[SerializeField]
		public float pathMaxSpeed;

		// Token: 0x040010B6 RID: 4278
		[SerializeField]
		public int pathNodeInclusionPeriod;

		// Token: 0x040010B7 RID: 4279
		[SerializeField]
		public string voSoundString;

		// Token: 0x040010B8 RID: 4280
		[SerializeField]
		public float minimumVoDelay;

		// Token: 0x040010B9 RID: 4281
		[SerializeField]
		public float maximumVoDelay;

		// Token: 0x040010BA RID: 4282
		[SerializeField]
		public LoopSoundDef loopSoundDef;

		// Token: 0x040010BB RID: 4283
		private LoopSoundManager.SoundLoopPtr loopPtr;

		// Token: 0x040010BC RID: 4284
		private const HullMask wardHullMask = HullMask.Human;

		// Token: 0x040010BD RID: 4285
		private const HullClassification pathHullClassification = HullClassification.Human;

		// Token: 0x040010BE RID: 4286
		private bool didFail;

		// Token: 0x040010BF RID: 4287
		private NodeGraph groundNodeGraph;

		// Token: 0x040010C0 RID: 4288
		private List<NodeGraph.NodeIndex> potentialEndNodes;

		// Token: 0x040010C1 RID: 4289
		private List<Vector3> catmullRomPoints = new List<Vector3>();

		// Token: 0x040010C2 RID: 4290
		private Vector3 rotationVelocity;

		// Token: 0x040010C3 RID: 4291
		private Path groundPath;

		// Token: 0x040010C4 RID: 4292
		private int catmullRomIndex;

		// Token: 0x040010C5 RID: 4293
		private CatmullRom3 currentCurve;

		// Token: 0x040010C6 RID: 4294
		private float tCurve;

		// Token: 0x040010C7 RID: 4295
		private float voTimer;

		// Token: 0x040010C8 RID: 4296
		private Xoroshiro128Plus rng;
	}
}
