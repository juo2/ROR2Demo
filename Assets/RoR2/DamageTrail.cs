using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200068B RID: 1675
	public class DamageTrail : MonoBehaviour
	{
		// Token: 0x060020BB RID: 8379 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
		private void Awake()
		{
			this.pointsList = new List<DamageTrail.TrailPoint>();
			this.transform = base.transform;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0008CAC9 File Offset: 0x0008ACC9
		private void Start()
		{
			this.localTime = 0f;
			this.AddPoint();
			this.AddPoint();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0008CAE4 File Offset: 0x0008ACE4
		private void FixedUpdate()
		{
			this.localTime += Time.fixedDeltaTime;
			if (this.localTime >= this.nextTrailPointUpdate)
			{
				this.nextTrailPointUpdate += this.pointUpdateInterval;
				this.UpdateTrail(this.active);
			}
			if (this.localTime >= this.nextTrailDamageUpdate)
			{
				this.nextTrailDamageUpdate += this.damageUpdateInterval;
				this.DoDamage();
			}
			if (this.pointsList.Count > 0)
			{
				DamageTrail.TrailPoint trailPoint = this.pointsList[this.pointsList.Count - 1];
				trailPoint.position = this.transform.position;
				trailPoint.localEndTime = this.localTime + this.pointLifetime;
				this.pointsList[this.pointsList.Count - 1] = trailPoint;
				if (trailPoint.segmentTransform)
				{
					trailPoint.segmentTransform.position = this.transform.position;
				}
				if (this.lineRenderer)
				{
					this.lineRenderer.SetPosition(this.pointsList.Count - 1, trailPoint.position);
				}
			}
			if (this.segmentPrefab)
			{
				Vector3 position = this.transform.position;
				for (int i = this.pointsList.Count - 1; i >= 0; i--)
				{
					Transform segmentTransform = this.pointsList[i].segmentTransform;
					segmentTransform.LookAt(position, Vector3.up);
					Vector3 a = this.pointsList[i].position - position;
					segmentTransform.position = position + a * 0.5f;
					float num = Mathf.Clamp01(Mathf.InverseLerp(this.pointsList[i].localStartTime, this.pointsList[i].localEndTime, this.localTime));
					Vector3 localScale = new Vector3(this.radius * (1f - num), this.radius * (1f - num), a.magnitude);
					segmentTransform.localScale = localScale;
					position = this.pointsList[i].position;
				}
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0008CD10 File Offset: 0x0008AF10
		private void UpdateTrail(bool addPoint)
		{
			while (this.pointsList.Count > 0 && this.pointsList[0].localEndTime <= this.localTime)
			{
				this.RemovePoint(0);
			}
			if (addPoint)
			{
				this.AddPoint();
			}
			if (this.lineRenderer)
			{
				this.UpdateLineRenderer(this.lineRenderer);
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0008CD70 File Offset: 0x0008AF70
		private void DoDamage()
		{
			if (!NetworkServer.active || this.pointsList.Count == 0)
			{
				return;
			}
			Vector3 b = this.pointsList[this.pointsList.Count - 1].position;
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			TeamIndex attackerTeamIndex = TeamIndex.Neutral;
			float damage = this.damagePerSecond * this.damageUpdateInterval;
			if (this.owner)
			{
				hashSet.Add(this.owner);
				attackerTeamIndex = TeamComponent.GetObjectTeam(this.owner);
			}
			DamageInfo damageInfo = new DamageInfo();
			damageInfo.attacker = this.owner;
			damageInfo.inflictor = base.gameObject;
			damageInfo.crit = false;
			damageInfo.damage = damage;
			damageInfo.damageColorIndex = DamageColorIndex.Item;
			damageInfo.damageType = DamageType.Generic;
			damageInfo.force = Vector3.zero;
			damageInfo.procCoefficient = 0f;
			for (int i = this.pointsList.Count - 2; i >= 0; i--)
			{
				Vector3 position = this.pointsList[i].position;
				Vector3 forward = position - b;
				Vector3 halfExtents = new Vector3(this.radius, this.height, forward.magnitude);
				Vector3 center = Vector3.Lerp(position, b, 0.5f);
				Quaternion orientation = Util.QuaternionSafeLookRotation(forward);
				Collider[] array = Physics.OverlapBox(center, halfExtents, orientation, LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal);
				for (int j = 0; j < array.Length; j++)
				{
					HurtBox component = array[j].GetComponent<HurtBox>();
					if (component)
					{
						HealthComponent healthComponent = component.healthComponent;
						if (healthComponent)
						{
							GameObject gameObject = healthComponent.gameObject;
							if (!hashSet.Contains(gameObject))
							{
								hashSet.Add(gameObject);
								if (FriendlyFireManager.ShouldSplashHitProceed(healthComponent, attackerTeamIndex))
								{
									damageInfo.position = array[j].transform.position;
									healthComponent.TakeDamage(damageInfo);
								}
							}
						}
					}
				}
				b = position;
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0008CF58 File Offset: 0x0008B158
		private void UpdateLineRenderer(LineRenderer lineRenderer)
		{
			lineRenderer.positionCount = this.pointsList.Count;
			for (int i = 0; i < this.pointsList.Count; i++)
			{
				lineRenderer.SetPosition(i, this.pointsList[i].position);
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0008CFA4 File Offset: 0x0008B1A4
		private void AddPoint()
		{
			DamageTrail.TrailPoint item = new DamageTrail.TrailPoint
			{
				position = this.transform.position,
				localStartTime = this.localTime,
				localEndTime = this.localTime + this.pointLifetime
			};
			if (this.segmentPrefab)
			{
				item.segmentTransform = UnityEngine.Object.Instantiate<GameObject>(this.segmentPrefab, this.transform).transform;
			}
			this.pointsList.Add(item);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0008D024 File Offset: 0x0008B224
		private void RemovePoint(int pointIndex)
		{
			if (this.destroyTrailSegments && this.pointsList[pointIndex].segmentTransform)
			{
				UnityEngine.Object.Destroy(this.pointsList[pointIndex].segmentTransform.gameObject);
			}
			this.pointsList.RemoveAt(pointIndex);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0008D078 File Offset: 0x0008B278
		private void OnDrawGizmos()
		{
			Vector3 b = this.pointsList[this.pointsList.Count - 1].position;
			for (int i = this.pointsList.Count - 2; i >= 0; i--)
			{
				Vector3 position = this.pointsList[i].position;
				Vector3 forward = position - b;
				Vector3 s = new Vector3(this.radius, 0.5f, forward.magnitude);
				Vector3 pos = Vector3.Lerp(position, b, 0.5f);
				Quaternion q = Util.QuaternionSafeLookRotation(forward);
				Gizmos.matrix = Matrix4x4.TRS(pos, q, s);
				Gizmos.color = Color.blue;
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
				Gizmos.matrix = Matrix4x4.identity;
				b = position;
			}
		}

		// Token: 0x040025FF RID: 9727
		[FormerlySerializedAs("updateInterval")]
		[Tooltip("How often to drop a new point onto the trail.")]
		public float pointUpdateInterval = 0.2f;

		// Token: 0x04002600 RID: 9728
		[Tooltip("How often the damage trail should deal damage.")]
		public float damageUpdateInterval = 0.2f;

		// Token: 0x04002601 RID: 9729
		[Tooltip("How large the radius, or width, of the damage detection should be.")]
		public float radius = 0.5f;

		// Token: 0x04002602 RID: 9730
		[Tooltip("How large the height of the damage detection should be.")]
		public float height = 0.5f;

		// Token: 0x04002603 RID: 9731
		[Tooltip("How long a point on the trail should last.")]
		public float pointLifetime = 3f;

		// Token: 0x04002604 RID: 9732
		[Tooltip("The line renderer to use for display.")]
		public LineRenderer lineRenderer;

		// Token: 0x04002605 RID: 9733
		public bool active = true;

		// Token: 0x04002606 RID: 9734
		[Tooltip("Prefab to use per segment.")]
		public GameObject segmentPrefab;

		// Token: 0x04002607 RID: 9735
		public bool destroyTrailSegments;

		// Token: 0x04002608 RID: 9736
		public float damagePerSecond;

		// Token: 0x04002609 RID: 9737
		public GameObject owner;

		// Token: 0x0400260A RID: 9738
		private new Transform transform;

		// Token: 0x0400260B RID: 9739
		private List<DamageTrail.TrailPoint> pointsList;

		// Token: 0x0400260C RID: 9740
		private float localTime;

		// Token: 0x0400260D RID: 9741
		private float nextTrailPointUpdate;

		// Token: 0x0400260E RID: 9742
		private float nextTrailDamageUpdate;

		// Token: 0x0200068C RID: 1676
		private struct TrailPoint
		{
			// Token: 0x0400260F RID: 9743
			public Vector3 position;

			// Token: 0x04002610 RID: 9744
			public float localStartTime;

			// Token: 0x04002611 RID: 9745
			public float localEndTime;

			// Token: 0x04002612 RID: 9746
			public Transform segmentTransform;
		}
	}
}
