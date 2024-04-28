using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000852 RID: 2130
	public class RoachController : MonoBehaviour
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x000C70B4 File Offset: 0x000C52B4
		private void Awake()
		{
			this.roachTransforms = new Transform[this.roaches.Length];
			for (int i = 0; i < this.roachTransforms.Length; i++)
			{
				this.roachTransforms[i] = UnityEngine.Object.Instantiate<GameObject>(this.roachParams.roachPrefab, this.roaches[i].keyFrames[0].position, this.roaches[i].keyFrames[0].rotation).transform;
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000C713C File Offset: 0x000C533C
		private void OnDestroy()
		{
			for (int i = 0; i < this.roachTransforms.Length; i++)
			{
				if (this.roachTransforms[i])
				{
					UnityEngine.Object.Destroy(this.roachTransforms[i].gameObject);
				}
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000C7180 File Offset: 0x000C5380
		public void BakeRoaches2()
		{
			List<RoachController.Roach> list = new List<RoachController.Roach>();
			for (int i = 0; i < this.roachCount; i++)
			{
				Ray ray = new Ray(base.transform.position, Util.ApplySpread(base.transform.forward, this.placementSpreadMin, this.placementSpreadMax, 1f, 1f, 0f, 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, this.placementMaxDistance, LayerIndex.world.mask))
				{
					RoachController.SimulatedRoach simulatedRoach = new RoachController.SimulatedRoach(raycastHit.point + raycastHit.normal * 0.01f, raycastHit.normal, ray.direction, this.roachParams);
					float keyframeInterval = this.roachParams.keyframeInterval;
					List<RoachController.KeyFrame> list2 = new List<RoachController.KeyFrame>();
					while (!simulatedRoach.finished)
					{
						simulatedRoach.Simulate(keyframeInterval);
						list2.Add(new RoachController.KeyFrame
						{
							position = simulatedRoach.transform.position,
							rotation = simulatedRoach.transform.rotation,
							time = simulatedRoach.age
						});
					}
					RoachController.KeyFrame keyFrame = list2[list2.Count - 1];
					keyFrame.position += keyFrame.rotation * (Vector3.down * 0.25f);
					list2[list2.Count - 1] = keyFrame;
					simulatedRoach.Dispose();
					list.Add(new RoachController.Roach
					{
						keyFrames = list2.ToArray()
					});
				}
			}
			this.roaches = list.ToArray();
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000C733E File Offset: 0x000C553E
		public void BakeRoaches()
		{
			this.BakeRoaches2();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000C7348 File Offset: 0x000C5548
		private void ClearRoachPathEditors()
		{
			for (int i = base.transform.childCount - 1; i > 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(base.transform.GetChild(i).gameObject);
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000C7384 File Offset: 0x000C5584
		public void DebakeRoaches()
		{
			this.ClearRoachPathEditors();
			for (int i = 0; i < this.roaches.Length; i++)
			{
				RoachController.Roach roach = this.roaches[i];
				RoachController.RoachPathEditorComponent roachPathEditorComponent = this.AddPathEditorObject();
				for (int j = 0; j < roach.keyFrames.Length; j++)
				{
					RoachController.KeyFrame keyFrame = roach.keyFrames[j];
					RoachController.RoachNodeEditorComponent roachNodeEditorComponent = roachPathEditorComponent.AddNode();
					roachNodeEditorComponent.transform.position = keyFrame.position;
					roachNodeEditorComponent.transform.rotation = keyFrame.rotation;
				}
			}
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000C7408 File Offset: 0x000C5608
		public RoachController.RoachPathEditorComponent AddPathEditorObject()
		{
			GameObject gameObject = new GameObject("Roach Path (Temporary)");
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.transform.SetParent(base.transform, false);
			RoachController.RoachPathEditorComponent roachPathEditorComponent = gameObject.AddComponent<RoachController.RoachPathEditorComponent>();
			roachPathEditorComponent.roachController = this;
			return roachPathEditorComponent;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000C743C File Offset: 0x000C563C
		private void UpdateRoach(int i)
		{
			RoachController.KeyFrame[] keyFrames = this.roaches[i].keyFrames;
			float num = Mathf.Min(this.scatterStartTime.timeSince, keyFrames[keyFrames.Length - 1].time);
			for (int j = 1; j < keyFrames.Length; j++)
			{
				if (num <= keyFrames[j].time)
				{
					RoachController.KeyFrame keyFrame = keyFrames[j - 1];
					RoachController.KeyFrame keyFrame2 = keyFrames[j];
					float t = Mathf.InverseLerp(keyFrame.time, keyFrame2.time, num);
					this.SetRoachPosition(i, Vector3.Lerp(keyFrame.position, keyFrame2.position, t), Quaternion.Slerp(keyFrame.rotation, keyFrame2.rotation, t));
					return;
				}
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000C74F1 File Offset: 0x000C56F1
		private void SetRoachPosition(int i, Vector3 position, Quaternion rotation)
		{
			this.roachTransforms[i].SetPositionAndRotation(position, rotation);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000C7504 File Offset: 0x000C5704
		private void Update()
		{
			for (int i = 0; i < this.roaches.Length; i++)
			{
				this.UpdateRoach(i);
			}
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000C752B File Offset: 0x000C572B
		private void Scatter()
		{
			if (this.scattered)
			{
				return;
			}
			Util.PlaySound("Play_env_roach_scatter", base.gameObject);
			this.scattered = true;
			this.scatterStartTime = Run.TimeStamp.now;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000C7559 File Offset: 0x000C5759
		private void OnTriggerEnter(Collider other)
		{
			CharacterBody component = other.GetComponent<CharacterBody>();
			if (component != null && component.isPlayerControlled)
			{
				this.Scatter();
			}
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000C7578 File Offset: 0x000C5778
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one);
			Gizmos.DrawFrustum(Vector3.zero, this.placementSpreadMax * 0.5f, this.placementMaxDistance, 0f, 1f);
		}

		// Token: 0x040030AD RID: 12461
		public RoachParams roachParams;

		// Token: 0x040030AE RID: 12462
		public int roachCount;

		// Token: 0x040030AF RID: 12463
		public float placementSpreadMin = 1f;

		// Token: 0x040030B0 RID: 12464
		public float placementSpreadMax = 25f;

		// Token: 0x040030B1 RID: 12465
		public float placementMaxDistance = 10f;

		// Token: 0x040030B2 RID: 12466
		public RoachController.Roach[] roaches;

		// Token: 0x040030B3 RID: 12467
		private Transform[] roachTransforms;

		// Token: 0x040030B4 RID: 12468
		private bool scattered;

		// Token: 0x040030B5 RID: 12469
		private Run.TimeStamp scatterStartTime = Run.TimeStamp.positiveInfinity;

		// Token: 0x040030B6 RID: 12470
		private const string roachScatterSoundString = "Play_env_roach_scatter";

		// Token: 0x02000853 RID: 2131
		[Serializable]
		public struct KeyFrame
		{
			// Token: 0x040030B7 RID: 12471
			public float time;

			// Token: 0x040030B8 RID: 12472
			public Vector3 position;

			// Token: 0x040030B9 RID: 12473
			public Quaternion rotation;
		}

		// Token: 0x02000854 RID: 2132
		[Serializable]
		public struct Roach
		{
			// Token: 0x040030BA RID: 12474
			public RoachController.KeyFrame[] keyFrames;
		}

		// Token: 0x02000855 RID: 2133
		private class SimulatedRoach : IDisposable
		{
			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000C760E File Offset: 0x000C580E
			// (set) Token: 0x06002EB1 RID: 11953 RVA: 0x000C7616 File Offset: 0x000C5816
			public Transform transform { get; private set; }

			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000C761F File Offset: 0x000C581F
			// (set) Token: 0x06002EB3 RID: 11955 RVA: 0x000C7627 File Offset: 0x000C5827
			public float age { get; private set; }

			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000C7630 File Offset: 0x000C5830
			// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x000C7638 File Offset: 0x000C5838
			public bool finished { get; private set; }

			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000C7641 File Offset: 0x000C5841
			private bool onGround
			{
				get
				{
					return this.groundNormal != Vector3.zero;
				}
			}

			// Token: 0x06002EB7 RID: 11959 RVA: 0x000C7654 File Offset: 0x000C5854
			public SimulatedRoach(Vector3 position, Vector3 groundNormal, Vector3 initialFleeNormal, RoachParams roachParams)
			{
				this.roachParams = roachParams;
				GameObject gameObject = new GameObject("SimulatedRoach");
				this.transform = gameObject.transform;
				this.transform.position = position;
				this.transform.up = groundNormal;
				this.transform.Rotate(this.transform.up, UnityEngine.Random.Range(0f, 360f));
				this.transform.forward = UnityEngine.Random.onUnitSphere;
				this.groundNormal = groundNormal;
				this.initialFleeNormal = initialFleeNormal;
				this.desiredMovement = UnityEngine.Random.onUnitSphere;
				this.age = UnityEngine.Random.Range(roachParams.minReactionTime, roachParams.maxReactionTime);
				this.simulationDuration = this.age + UnityEngine.Random.Range(roachParams.minSimulationDuration, roachParams.maxSimulationDuration);
			}

			// Token: 0x06002EB8 RID: 11960 RVA: 0x000C7730 File Offset: 0x000C5930
			private void SetUpVector(Vector3 desiredUp)
			{
				Vector3 right = this.transform.right;
				Vector3 up = this.transform.up;
				this.transform.Rotate(right, Vector3.SignedAngle(up, desiredUp, right), Space.World);
			}

			// Token: 0x06002EB9 RID: 11961 RVA: 0x000C776C File Offset: 0x000C596C
			public void Simulate(float deltaTime)
			{
				this.age += deltaTime;
				if (this.onGround)
				{
					this.SetUpVector(this.groundNormal);
					this.turnVelocity += UnityEngine.Random.Range(-this.roachParams.wiggle, this.roachParams.wiggle) * deltaTime;
					this.TurnDesiredMovement(this.turnVelocity * deltaTime);
					Vector3 up = this.transform.up;
					Vector3 normalized = Vector3.ProjectOnPlane(this.desiredMovement, up).normalized;
					float value = Vector3.SignedAngle(this.transform.forward, normalized, up);
					this.TurnBody(Mathf.Clamp(value, -this.turnVelocity * deltaTime, this.turnVelocity * deltaTime));
					this.currentSpeed = Mathf.MoveTowards(this.currentSpeed, this.roachParams.maxSpeed, deltaTime * this.roachParams.acceleration);
					this.StepGround(this.currentSpeed * deltaTime);
				}
				else
				{
					this.velocity += Physics.gravity * deltaTime;
					this.StepAir(this.velocity);
				}
				this.reorientTimer -= deltaTime;
				if (this.reorientTimer <= 0f)
				{
					this.desiredMovement = this.initialFleeNormal;
					this.reorientTimer = UnityEngine.Random.Range(this.roachParams.reorientTimerMin, this.roachParams.reorientTimerMax);
				}
				if (this.age >= this.simulationDuration)
				{
					this.finished = true;
				}
			}

			// Token: 0x06002EBA RID: 11962 RVA: 0x000C78E8 File Offset: 0x000C5AE8
			private void OnBump()
			{
				this.TurnDesiredMovement(UnityEngine.Random.Range(-90f, 90f));
				this.currentSpeed *= -0.5f;
				if (this.roachParams.chanceToFinishOnBump < UnityEngine.Random.value)
				{
					this.finished = true;
				}
			}

			// Token: 0x06002EBB RID: 11963 RVA: 0x000C7938 File Offset: 0x000C5B38
			private void TurnDesiredMovement(float degrees)
			{
				Quaternion rotation = Quaternion.AngleAxis(degrees, this.transform.up);
				this.desiredMovement = rotation * this.desiredMovement;
			}

			// Token: 0x06002EBC RID: 11964 RVA: 0x000C7969 File Offset: 0x000C5B69
			private void TurnBody(float degrees)
			{
				this.transform.Rotate(Vector3.up, degrees, Space.Self);
			}

			// Token: 0x06002EBD RID: 11965 RVA: 0x000C7980 File Offset: 0x000C5B80
			private void StepAir(Vector3 movement)
			{
				RoachController.SimulatedRoach.RaycastResult raycastResult = RoachController.SimulatedRoach.SimpleRaycast(new Ray(this.transform.position, movement), movement.magnitude);
				Debug.DrawLine(this.transform.position, raycastResult.point, Color.magenta, 10f, false);
				if (raycastResult.didHit)
				{
					this.groundNormal = raycastResult.normal;
					this.velocity = Vector3.zero;
				}
				this.transform.position = raycastResult.point;
			}

			// Token: 0x06002EBE RID: 11966 RVA: 0x000C79FC File Offset: 0x000C5BFC
			private void StepGround(float distance)
			{
				this.groundNormal = Vector3.zero;
				Vector3 up = this.transform.up;
				Vector3 forward = this.transform.forward;
				float stepSize = this.roachParams.stepSize;
				Vector3 vector = up * stepSize;
				Vector3 vector2 = this.transform.position;
				vector2 += vector;
				Debug.DrawLine(this.transform.position, vector2, Color.red, 10f, false);
				RoachController.SimulatedRoach.RaycastResult raycastResult = RoachController.SimulatedRoach.SimpleRaycast(new Ray(vector2, forward), distance);
				Debug.DrawLine(vector2, raycastResult.point, Color.green, 10f, false);
				vector2 = raycastResult.point;
				if (raycastResult.didHit)
				{
					if (Vector3.Dot(raycastResult.normal, forward) < -0.5f)
					{
						this.OnBump();
					}
					this.groundNormal = raycastResult.normal;
				}
				else
				{
					RoachController.SimulatedRoach.RaycastResult raycastResult2 = RoachController.SimulatedRoach.SimpleRaycast(new Ray(vector2, -vector), stepSize * 2f);
					if (raycastResult2.didHit)
					{
						Debug.DrawLine(vector2, raycastResult2.point, Color.blue, 10f, false);
						vector2 = raycastResult2.point;
						this.groundNormal = raycastResult2.normal;
					}
					else
					{
						Debug.DrawLine(vector2, vector2 - vector, Color.white, 10f);
						vector2 -= vector;
					}
				}
				if (this.groundNormal == Vector3.zero)
				{
					this.currentSpeed = 0f;
				}
				this.transform.position = vector2;
			}

			// Token: 0x06002EBF RID: 11967 RVA: 0x000C7B6C File Offset: 0x000C5D6C
			private static RoachController.SimulatedRoach.RaycastResult SimpleRaycast(Ray ray, float maxDistance)
			{
				RaycastHit raycastHit;
				bool flag = Physics.Raycast(ray, out raycastHit, maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
				return new RoachController.SimulatedRoach.RaycastResult
				{
					didHit = flag,
					point = (flag ? raycastHit.point : ray.GetPoint(maxDistance)),
					normal = (flag ? raycastHit.normal : Vector3.zero),
					distance = (flag ? raycastHit.distance : maxDistance)
				};
			}

			// Token: 0x06002EC0 RID: 11968 RVA: 0x000C7BEE File Offset: 0x000C5DEE
			public void Dispose()
			{
				UnityEngine.Object.DestroyImmediate(this.transform.gameObject);
				this.transform = null;
			}

			// Token: 0x040030BB RID: 12475
			private Vector3 initialFleeNormal;

			// Token: 0x040030BC RID: 12476
			private Vector3 desiredMovement;

			// Token: 0x040030BD RID: 12477
			private RoachParams roachParams;

			// Token: 0x040030C1 RID: 12481
			private float reorientTimer;

			// Token: 0x040030C2 RID: 12482
			private float backupTimer;

			// Token: 0x040030C3 RID: 12483
			private Vector3 velocity = Vector3.zero;

			// Token: 0x040030C4 RID: 12484
			private float currentSpeed;

			// Token: 0x040030C5 RID: 12485
			private float turnVelocity;

			// Token: 0x040030C6 RID: 12486
			private Vector3 groundNormal;

			// Token: 0x040030C7 RID: 12487
			private float simulationDuration;

			// Token: 0x02000856 RID: 2134
			private struct RaycastResult
			{
				// Token: 0x040030C8 RID: 12488
				public bool didHit;

				// Token: 0x040030C9 RID: 12489
				public Vector3 point;

				// Token: 0x040030CA RID: 12490
				public Vector3 normal;

				// Token: 0x040030CB RID: 12491
				public float distance;
			}
		}

		// Token: 0x02000857 RID: 2135
		public class RoachPathEditorComponent : MonoBehaviour
		{
			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000C7C07 File Offset: 0x000C5E07
			public int nodeCount
			{
				get
				{
					return base.transform.childCount;
				}
			}

			// Token: 0x06002EC2 RID: 11970 RVA: 0x000C7C14 File Offset: 0x000C5E14
			public RoachController.RoachNodeEditorComponent AddNode()
			{
				GameObject gameObject = new GameObject("Roach Path Node (Temporary)");
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(base.transform);
				RoachController.RoachNodeEditorComponent roachNodeEditorComponent = gameObject.AddComponent<RoachController.RoachNodeEditorComponent>();
				roachNodeEditorComponent.path = this;
				return roachNodeEditorComponent;
			}

			// Token: 0x06002EC3 RID: 11971 RVA: 0x000C7C48 File Offset: 0x000C5E48
			private void OnDrawGizmosSelected()
			{
				Gizmos.color = Color.white;
				int num = 0;
				while (num + 1 < this.nodeCount)
				{
					Vector3 position = base.transform.GetChild(num).transform.position;
					Vector3 position2 = base.transform.GetChild(num + 1).transform.position;
					Gizmos.DrawLine(position, position2);
					num++;
				}
			}

			// Token: 0x040030CC RID: 12492
			public RoachController roachController;
		}

		// Token: 0x02000858 RID: 2136
		public class RoachNodeEditorComponent : MonoBehaviour
		{
			// Token: 0x06002EC5 RID: 11973 RVA: 0x000C7CA8 File Offset: 0x000C5EA8
			public void FacePosition(Vector3 position)
			{
				Vector3 position2 = base.transform.position;
				Vector3 up = base.transform.up;
				Quaternion rotation = Quaternion.LookRotation(position - position2, up);
				base.transform.rotation = rotation;
				base.transform.up = up;
			}

			// Token: 0x040030CD RID: 12493
			public RoachController.RoachPathEditorComponent path;
		}
	}
}
