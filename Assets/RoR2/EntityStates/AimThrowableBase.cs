using System;
using RoR2;
using RoR2.Projectile;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000AC RID: 172
	public abstract class AimThrowableBase : BaseSkillState
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000B124 File Offset: 0x00009324
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.arcVisualizerPrefab)
			{
				this.arcVisualizerLineRenderer = UnityEngine.Object.Instantiate<GameObject>(this.arcVisualizerPrefab, base.transform.position, Quaternion.identity).GetComponent<LineRenderer>();
				this.calculateArcPointsJob = default(AimThrowableBase.CalculateArcPointsJob);
				this.completeArcPointsVisualizerJobMethod = new Action(this.CompleteArcVisualizerJob);
				RoR2Application.onLateUpdate += this.completeArcPointsVisualizerJobMethod;
			}
			if (this.endpointVisualizerPrefab)
			{
				this.endpointVisualizerTransform = UnityEngine.Object.Instantiate<GameObject>(this.endpointVisualizerPrefab, base.transform.position, Quaternion.identity).transform;
			}
			if (base.characterBody)
			{
				base.characterBody.hideCrosshair = true;
			}
			ProjectileSimple component = this.projectilePrefab.GetComponent<ProjectileSimple>();
			if (component)
			{
				this.projectileBaseSpeed = component.velocity;
			}
			Rigidbody component2 = this.projectilePrefab.GetComponent<Rigidbody>();
			if (component2)
			{
				this.useGravity = component2.useGravity;
			}
			this.minimumDuration = this.baseMinimumDuration / this.attackSpeedStat;
			ProjectileImpactExplosion component3 = this.projectilePrefab.GetComponent<ProjectileImpactExplosion>();
			if (component3)
			{
				this.detonationRadius = component3.blastRadius;
				if (this.endpointVisualizerTransform)
				{
					this.endpointVisualizerTransform.localScale = new Vector3(this.detonationRadius, this.detonationRadius, this.detonationRadius);
				}
			}
			this.UpdateVisualizers(this.currentTrajectoryInfo);
			SceneCamera.onSceneCameraPreRender += this.OnPreRenderSceneCam;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public override void OnExit()
		{
			SceneCamera.onSceneCameraPreRender -= this.OnPreRenderSceneCam;
			if (!this.outer.destroying)
			{
				if (base.isAuthority)
				{
					this.FireProjectile();
				}
				this.OnProjectileFiredLocal();
			}
			if (base.characterBody)
			{
				base.characterBody.hideCrosshair = false;
			}
			this.calculateArcPointsJobHandle.Complete();
			if (this.arcVisualizerLineRenderer)
			{
				EntityState.Destroy(this.arcVisualizerLineRenderer.gameObject);
				this.arcVisualizerLineRenderer = null;
			}
			if (this.completeArcPointsVisualizerJobMethod != null)
			{
				RoR2Application.onLateUpdate -= this.completeArcPointsVisualizerJobMethod;
				this.completeArcPointsVisualizerJobMethod = null;
			}
			this.calculateArcPointsJob.Dispose();
			this.pointsBuffer = Array.Empty<Vector3>();
			if (this.endpointVisualizerTransform)
			{
				EntityState.Destroy(this.endpointVisualizerTransform.gameObject);
				this.endpointVisualizerTransform = null;
			}
			base.OnExit();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B381 File Offset: 0x00009581
		protected virtual bool KeyIsDown()
		{
			return base.IsKeyDownAuthority();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnProjectileFiredLocal()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B38C File Offset: 0x0000958C
		protected virtual void FireProjectile()
		{
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				crit = base.RollCrit(),
				owner = base.gameObject,
				position = this.currentTrajectoryInfo.finalRay.origin,
				projectilePrefab = this.projectilePrefab,
				rotation = Util.QuaternionSafeLookRotation(this.currentTrajectoryInfo.finalRay.direction, Vector3.up),
				speedOverride = this.currentTrajectoryInfo.speedOverride,
				damage = this.damageCoefficient * this.damageStat
			};
			if (this.setFuse)
			{
				fireProjectileInfo.fuseOverride = this.currentTrajectoryInfo.travelTime;
			}
			this.ModifyProjectile(ref fireProjectileInfo);
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B458 File Offset: 0x00009658
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && !this.KeyIsDown() && base.fixedAge >= this.minimumDuration)
			{
				this.UpdateTrajectoryInfo(out this.currentTrajectoryInfo);
				EntityState entityState = this.PickNextState();
				if (entityState != null)
				{
					this.outer.SetNextState(entityState);
					return;
				}
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual EntityState PickNextState()
		{
			return null;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B4BA File Offset: 0x000096BA
		public override void Update()
		{
			base.Update();
			if (CameraRigController.IsObjectSpectatedByAnyCamera(base.gameObject))
			{
				this.UpdateTrajectoryInfo(out this.currentTrajectoryInfo);
				this.UpdateVisualizers(this.currentTrajectoryInfo);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B4E8 File Offset: 0x000096E8
		protected virtual void UpdateTrajectoryInfo(out AimThrowableBase.TrajectoryInfo dest)
		{
			dest = default(AimThrowableBase.TrajectoryInfo);
			Ray aimRay = base.GetAimRay();
			RaycastHit raycastHit = default(RaycastHit);
			bool flag = false;
			if (this.rayRadius > 0f && Util.CharacterSpherecast(base.gameObject, aimRay, this.rayRadius, out raycastHit, this.maxDistance, LayerIndex.CommonMasks.bullet, QueryTriggerInteraction.UseGlobal) && raycastHit.collider.GetComponent<HurtBox>())
			{
				flag = true;
			}
			if (!flag)
			{
				flag = Util.CharacterRaycast(base.gameObject, aimRay, out raycastHit, this.maxDistance, LayerIndex.CommonMasks.bullet, QueryTriggerInteraction.UseGlobal);
			}
			if (flag)
			{
				dest.hitPoint = raycastHit.point;
				dest.hitNormal = raycastHit.normal;
			}
			else
			{
				dest.hitPoint = aimRay.GetPoint(this.maxDistance);
				dest.hitNormal = -aimRay.direction;
			}
			Vector3 vector = dest.hitPoint - aimRay.origin;
			if (this.useGravity)
			{
				float num = this.projectileBaseSpeed;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				float magnitude = vector2.magnitude;
				float y = Trajectory.CalculateInitialYSpeed(magnitude / num, vector.y);
				Vector3 a = new Vector3(vector2.x / magnitude * num, y, vector2.y / magnitude * num);
				dest.speedOverride = a.magnitude;
				dest.finalRay = new Ray(aimRay.origin, a / dest.speedOverride);
				dest.travelTime = Trajectory.CalculateGroundTravelTime(num, magnitude);
				return;
			}
			dest.speedOverride = this.projectileBaseSpeed;
			dest.finalRay = aimRay;
			dest.travelTime = this.projectileBaseSpeed / vector.magnitude;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B690 File Offset: 0x00009890
		private void CompleteArcVisualizerJob()
		{
			this.calculateArcPointsJobHandle.Complete();
			if (this.arcVisualizerLineRenderer)
			{
				Array.Resize<Vector3>(ref this.pointsBuffer, this.calculateArcPointsJob.outputPositions.Length);
				this.calculateArcPointsJob.outputPositions.CopyTo(this.pointsBuffer);
				this.arcVisualizerLineRenderer.SetPositions(this.pointsBuffer);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private void UpdateVisualizers(AimThrowableBase.TrajectoryInfo trajectoryInfo)
		{
			if (this.arcVisualizerLineRenderer && this.calculateArcPointsJobHandle.IsCompleted)
			{
				this.calculateArcPointsJob.SetParameters(trajectoryInfo.finalRay.origin, trajectoryInfo.finalRay.direction * trajectoryInfo.speedOverride, trajectoryInfo.travelTime, this.arcVisualizerLineRenderer.positionCount, this.useGravity ? Physics.gravity.y : 0f);
				this.calculateArcPointsJobHandle = this.calculateArcPointsJob.Schedule(this.calculateArcPointsJob.outputPositions.Length, 32, default(JobHandle));
			}
			if (this.endpointVisualizerTransform)
			{
				this.endpointVisualizerTransform.SetPositionAndRotation(trajectoryInfo.hitPoint, Util.QuaternionSafeLookRotation(trajectoryInfo.hitNormal));
				if (!this.endpointVisualizerRadiusScale.Equals(0f))
				{
					this.endpointVisualizerTransform.localScale = new Vector3(this.endpointVisualizerRadiusScale, this.endpointVisualizerRadiusScale, this.endpointVisualizerRadiusScale);
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B808 File Offset: 0x00009A08
		private void OnPreRenderSceneCam(SceneCamera sceneCam)
		{
			if (this.arcVisualizerLineRenderer)
			{
				this.arcVisualizerLineRenderer.renderingLayerMask = ((sceneCam.cameraRigController.target == base.gameObject) ? 1U : 0U);
			}
			if (this.endpointVisualizerTransform)
			{
				this.endpointVisualizerTransform.gameObject.layer = ((sceneCam.cameraRigController.target == base.gameObject) ? LayerIndex.defaultLayer.intVal : LayerIndex.noDraw.intVal);
			}
		}

		// Token: 0x040002ED RID: 749
		[SerializeField]
		public float maxDistance;

		// Token: 0x040002EE RID: 750
		[SerializeField]
		public float rayRadius;

		// Token: 0x040002EF RID: 751
		[SerializeField]
		public GameObject arcVisualizerPrefab;

		// Token: 0x040002F0 RID: 752
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040002F1 RID: 753
		[SerializeField]
		public GameObject endpointVisualizerPrefab;

		// Token: 0x040002F2 RID: 754
		[SerializeField]
		public float endpointVisualizerRadiusScale;

		// Token: 0x040002F3 RID: 755
		[SerializeField]
		public bool setFuse;

		// Token: 0x040002F4 RID: 756
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x040002F5 RID: 757
		[SerializeField]
		public float baseMinimumDuration;

		// Token: 0x040002F6 RID: 758
		protected LineRenderer arcVisualizerLineRenderer;

		// Token: 0x040002F7 RID: 759
		protected Transform endpointVisualizerTransform;

		// Token: 0x040002F8 RID: 760
		protected float projectileBaseSpeed;

		// Token: 0x040002F9 RID: 761
		protected float detonationRadius;

		// Token: 0x040002FA RID: 762
		protected float minimumDuration;

		// Token: 0x040002FB RID: 763
		protected bool useGravity;

		// Token: 0x040002FC RID: 764
		private AimThrowableBase.CalculateArcPointsJob calculateArcPointsJob;

		// Token: 0x040002FD RID: 765
		private JobHandle calculateArcPointsJobHandle;

		// Token: 0x040002FE RID: 766
		private Vector3[] pointsBuffer = Array.Empty<Vector3>();

		// Token: 0x040002FF RID: 767
		private Action completeArcPointsVisualizerJobMethod;

		// Token: 0x04000300 RID: 768
		protected AimThrowableBase.TrajectoryInfo currentTrajectoryInfo;

		// Token: 0x020000AD RID: 173
		private struct CalculateArcPointsJob : IJobParallelFor, IDisposable
		{
			// Token: 0x060002CA RID: 714 RVA: 0x0000B8A8 File Offset: 0x00009AA8
			public void SetParameters(Vector3 origin, Vector3 velocity, float totalTravelTime, int positionCount, float gravity)
			{
				this.origin = origin;
				this.velocity = velocity;
				if (this.outputPositions.Length != positionCount)
				{
					if (this.outputPositions.IsCreated)
					{
						this.outputPositions.Dispose();
					}
					this.outputPositions = new NativeArray<Vector3>(positionCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				this.indexMultiplier = totalTravelTime / (float)(positionCount - 1);
				this.gravity = gravity;
			}

			// Token: 0x060002CB RID: 715 RVA: 0x0000B90E File Offset: 0x00009B0E
			public void Dispose()
			{
				if (this.outputPositions.IsCreated)
				{
					this.outputPositions.Dispose();
				}
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0000B928 File Offset: 0x00009B28
			public void Execute(int index)
			{
				float t = (float)index * this.indexMultiplier;
				this.outputPositions[index] = Trajectory.CalculatePositionAtTime(this.origin, this.velocity, t, this.gravity);
			}

			// Token: 0x04000301 RID: 769
			[global::ReadOnly]
			private Vector3 origin;

			// Token: 0x04000302 RID: 770
			[global::ReadOnly]
			private Vector3 velocity;

			// Token: 0x04000303 RID: 771
			[global::ReadOnly]
			private float indexMultiplier;

			// Token: 0x04000304 RID: 772
			[global::ReadOnly]
			private float gravity;

			// Token: 0x04000305 RID: 773
			[WriteOnly]
			public NativeArray<Vector3> outputPositions;
		}

		// Token: 0x020000AE RID: 174
		protected struct TrajectoryInfo
		{
			// Token: 0x04000306 RID: 774
			public Ray finalRay;

			// Token: 0x04000307 RID: 775
			public Vector3 hitPoint;

			// Token: 0x04000308 RID: 776
			public Vector3 hitNormal;

			// Token: 0x04000309 RID: 777
			public float travelTime;

			// Token: 0x0400030A RID: 778
			public float speedOverride;
		}
	}
}
