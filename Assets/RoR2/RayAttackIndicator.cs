using System;
using System.Collections.Generic;
using HG;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000840 RID: 2112
	public class RayAttackIndicator : MonoBehaviour
	{
		// Token: 0x06002E16 RID: 11798 RVA: 0x000C4518 File Offset: 0x000C2718
		private void Awake()
		{
			this.layerMask = LayerIndex.CommonMasks.bullet;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000C4525 File Offset: 0x000C2725
		private void OnEnable()
		{
			InstanceTracker.Add<RayAttackIndicator>(this);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000C452D File Offset: 0x000C272D
		private void OnDisable()
		{
			this.AllocateHitIndicators(0);
			InstanceTracker.Remove<RayAttackIndicator>(this);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000C453C File Offset: 0x000C273C
		private void AllocateHitIndicators(int newHitIndicatorCount)
		{
			while (newHitIndicatorCount < this.hitIndicators.Count)
			{
				UnityEngine.Object.Destroy(ListUtils.TakeLast<RayAttackIndicator.HitIndicatorInfo>(this.hitIndicators).gameObject);
			}
			while (newHitIndicatorCount > this.hitIndicators.Count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitIndicatorPrefab);
				gameObject.SetActive(true);
				this.hitIndicators.Add(new RayAttackIndicator.HitIndicatorInfo
				{
					gameObject = gameObject,
					transform = gameObject.transform
				});
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000C45BC File Offset: 0x000C27BC
		private void SetHits(List<RayAttackIndicator.HitPointIndicatorData> hits)
		{
			if (!base.enabled)
			{
				return;
			}
			this.AllocateHitIndicators(hits.Count);
			for (int i = 0; i < hits.Count; i++)
			{
				RayAttackIndicator.HitIndicatorInfo hitIndicatorInfo = this.hitIndicators[i];
				RayAttackIndicator.HitPointIndicatorData hitPointIndicatorData = hits[i];
				hitIndicatorInfo.transform.SetPositionAndRotation(hitPointIndicatorData.position, hitPointIndicatorData.rotation);
				hitIndicatorInfo.transform.localScale = new Vector3(hitPointIndicatorData.scale, hitPointIndicatorData.scale, hitPointIndicatorData.scale);
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000C463B File Offset: 0x000C283B
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			RoR2Application.onUpdate += RayAttackIndicator.StaticUpdate;
			RoR2Application.onLateUpdate += RayAttackIndicator.StaticLateUpdate;
			RoR2Application.onFixedUpdate += RayAttackIndicator.StaticFixedUpdate;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000C4670 File Offset: 0x000C2870
		private static void StaticUpdate()
		{
			if (!RayAttackIndicator.raysDirty)
			{
				return;
			}
			RayAttackIndicator.raysDirty = false;
			RayAttackIndicator.updater.ScheduleUpdate();
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000C468A File Offset: 0x000C288A
		private static void StaticLateUpdate()
		{
			RayAttackIndicator.updater.CompleteUpdate();
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000C4696 File Offset: 0x000C2896
		private static void StaticFixedUpdate()
		{
			RayAttackIndicator.raysDirty = true;
		}

		// Token: 0x04003008 RID: 12296
		[Tooltip("The prefab that will be instantiated and moved to wherever the ray hits.")]
		public GameObject hitIndicatorPrefab;

		// Token: 0x04003009 RID: 12297
		[Tooltip("Transform which will be moved to the start of the ray.")]
		public Transform originRecipient;

		// Token: 0x0400300A RID: 12298
		[Tooltip("Transform which will be moved to the nearest hit.")]
		public Transform nearestHitRecipient;

		// Token: 0x0400300B RID: 12299
		[Tooltip("Transform which will be moved to the furthest hit.")]
		public Transform furthestHitRecipient;

		// Token: 0x0400300C RID: 12300
		[Range(1f, 1f)]
		[Tooltip("How many hits this raycast is allowed to make. This is currently limited to 1.")]
		public int maxHits = 1;

		// Token: 0x0400300D RID: 12301
		private static readonly Ray defaultRay = new Ray(Vector3.zero, Vector3.up);

		// Token: 0x0400300E RID: 12302
		[NonSerialized]
		public Ray attackRay = RayAttackIndicator.defaultRay;

		// Token: 0x0400300F RID: 12303
		[NonSerialized]
		public float attackRange = float.PositiveInfinity;

		// Token: 0x04003010 RID: 12304
		[NonSerialized]
		public float attackRadius = 1f;

		// Token: 0x04003011 RID: 12305
		[NonSerialized]
		public LayerMask layerMask;

		// Token: 0x04003012 RID: 12306
		private List<RayAttackIndicator.HitIndicatorInfo> hitIndicators = new List<RayAttackIndicator.HitIndicatorInfo>();

		// Token: 0x04003013 RID: 12307
		private static bool raysDirty = false;

		// Token: 0x04003014 RID: 12308
		private static List<RayAttackIndicator> raycastRequestersList;

		// Token: 0x04003015 RID: 12309
		private static NativeArray<RaycastCommand> raycastCommandBuffer;

		// Token: 0x04003016 RID: 12310
		private static NativeArray<RaycastHit> resultsBuffer;

		// Token: 0x04003017 RID: 12311
		private static NativeArray<RayAttackIndicator.RaycastInfo> requestsBuffer;

		// Token: 0x04003018 RID: 12312
		private static JobHandle raycastJobHandle;

		// Token: 0x04003019 RID: 12313
		private static RayAttackIndicator.Updater updater;

		// Token: 0x02000841 RID: 2113
		private struct HitIndicatorInfo
		{
			// Token: 0x0400301A RID: 12314
			public GameObject gameObject;

			// Token: 0x0400301B RID: 12315
			public Transform transform;
		}

		// Token: 0x02000842 RID: 2114
		private struct RaycastInfo
		{
			// Token: 0x0400301C RID: 12316
			public Ray ray;

			// Token: 0x0400301D RID: 12317
			public float maxDistance;

			// Token: 0x0400301E RID: 12318
			public int hitsStart;

			// Token: 0x0400301F RID: 12319
			public int maxHits;
		}

		// Token: 0x02000843 RID: 2115
		private struct HitPointIndicatorData
		{
			// Token: 0x04003020 RID: 12320
			public Vector3 position;

			// Token: 0x04003021 RID: 12321
			public Quaternion rotation;

			// Token: 0x04003022 RID: 12322
			public float scale;
		}

		// Token: 0x02000844 RID: 2116
		private struct Updater
		{
			// Token: 0x06002E21 RID: 11809 RVA: 0x000C46F8 File Offset: 0x000C28F8
			public void ScheduleUpdate()
			{
				if (this.running)
				{
					return;
				}
				List<RayAttackIndicator> instancesList = InstanceTracker.GetInstancesList<RayAttackIndicator>();
				if (instancesList.Count == 0)
				{
					return;
				}
				RayAttackIndicator.requestsBuffer = new NativeArray<RayAttackIndicator.RaycastInfo>(instancesList.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				RayAttackIndicator.raycastCommandBuffer = new NativeArray<RaycastCommand>(RayAttackIndicator.requestsBuffer.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				RayAttackIndicator.raycastRequestersList = CollectionPool<RayAttackIndicator, List<RayAttackIndicator>>.RentCollection();
				int num = 1;
				RayAttackIndicator.resultsBuffer = new NativeArray<RaycastHit>(RayAttackIndicator.requestsBuffer.Length * num, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < instancesList.Count; i++)
				{
					RayAttackIndicator rayAttackIndicator = instancesList[i];
					RayAttackIndicator.raycastRequestersList.Add(rayAttackIndicator);
					RayAttackIndicator.requestsBuffer[i] = new RayAttackIndicator.RaycastInfo
					{
						ray = rayAttackIndicator.attackRay,
						maxDistance = rayAttackIndicator.attackRange,
						hitsStart = i * num,
						maxHits = 1
					};
					RayAttackIndicator.raycastCommandBuffer[i] = new RaycastCommand(rayAttackIndicator.attackRay.origin, rayAttackIndicator.attackRay.direction, rayAttackIndicator.attackRange, rayAttackIndicator.layerMask, 1);
				}
				RayAttackIndicator.raycastJobHandle = RaycastCommand.ScheduleBatch(RayAttackIndicator.raycastCommandBuffer, RayAttackIndicator.resultsBuffer, 4, default(JobHandle));
				this.running = true;
			}

			// Token: 0x06002E22 RID: 11810 RVA: 0x000C4834 File Offset: 0x000C2A34
			public void CompleteUpdate()
			{
				if (!this.running)
				{
					return;
				}
				RayAttackIndicator.raycastJobHandle.Complete();
				for (int i = 0; i < RayAttackIndicator.requestsBuffer.Length; i++)
				{
					RayAttackIndicator.RaycastInfo raycastInfo = RayAttackIndicator.requestsBuffer[i];
					RayAttackIndicator rayAttackIndicator = RayAttackIndicator.raycastRequestersList[i];
					if (rayAttackIndicator)
					{
						List<RayAttackIndicator.HitPointIndicatorData> list = CollectionPool<RayAttackIndicator.HitPointIndicatorData, List<RayAttackIndicator.HitPointIndicatorData>>.RentCollection();
						try
						{
							Vector3 up = rayAttackIndicator.transform.up;
							Ray ray = raycastInfo.ray;
							Vector3 origin = ray.origin;
							Vector3 direction = ray.direction;
							if (rayAttackIndicator.originRecipient)
							{
								rayAttackIndicator.originRecipient.SetPositionAndRotation(origin, Quaternion.LookRotation(direction, rayAttackIndicator.originRecipient.up));
							}
							float num = raycastInfo.maxDistance;
							float num2 = 0f;
							for (int j = 0; j < raycastInfo.maxHits; j++)
							{
								RaycastHit raycastHit = RayAttackIndicator.resultsBuffer[raycastInfo.hitsStart + j];
								bool flag = raycastHit.collider != null;
								float num3 = flag ? raycastHit.distance : raycastInfo.maxDistance;
								num = ((num3 < num) ? num3 : num);
								num2 = ((num3 > num2) ? num3 : num2);
								Vector3 position = origin + direction * num3;
								if (flag)
								{
									list.Add(new RayAttackIndicator.HitPointIndicatorData
									{
										position = position,
										rotation = Quaternion.LookRotation(-direction, up),
										scale = rayAttackIndicator.attackRadius
									});
								}
							}
							rayAttackIndicator.SetHits(list);
							if (rayAttackIndicator.nearestHitRecipient)
							{
								rayAttackIndicator.nearestHitRecipient.SetPositionAndRotation(origin + direction * num, Quaternion.LookRotation(-direction, rayAttackIndicator.nearestHitRecipient.up));
							}
							if (rayAttackIndicator.furthestHitRecipient)
							{
								rayAttackIndicator.furthestHitRecipient.SetPositionAndRotation(origin + direction * num2, Quaternion.LookRotation(-direction, rayAttackIndicator.furthestHitRecipient.up));
							}
						}
						catch (Exception exception)
						{
							Debug.LogException(exception);
							rayAttackIndicator.enabled = false;
						}
						finally
						{
							list = CollectionPool<RayAttackIndicator.HitPointIndicatorData, List<RayAttackIndicator.HitPointIndicatorData>>.ReturnCollection(list);
						}
					}
				}
				RayAttackIndicator.resultsBuffer.Dispose();
				RayAttackIndicator.requestsBuffer.Dispose();
				RayAttackIndicator.raycastCommandBuffer.Dispose();
				RayAttackIndicator.raycastRequestersList = CollectionPool<RayAttackIndicator, List<RayAttackIndicator>>.ReturnCollection(RayAttackIndicator.raycastRequestersList);
				this.running = false;
			}

			// Token: 0x04003023 RID: 12323
			private bool running;
		}
	}
}
