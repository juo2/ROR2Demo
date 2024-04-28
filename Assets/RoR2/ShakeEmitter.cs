using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200088C RID: 2188
	public class ShakeEmitter : MonoBehaviour
	{
		// Token: 0x0600300E RID: 12302 RVA: 0x000CC640 File Offset: 0x000CA840
		public void StartShake()
		{
			this.stopwatch = 0f;
			this.halfPeriodVector = UnityEngine.Random.onUnitSphere;
			this.halfPeriodTimer = this.wave.period * 0.5f;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000CC66F File Offset: 0x000CA86F
		private void Start()
		{
			if (this.scaleShakeRadiusWithLocalScale)
			{
				this.radius *= base.transform.localScale.x;
			}
			if (this.shakeOnStart)
			{
				this.StartShake();
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000CC6A4 File Offset: 0x000CA8A4
		private void OnEnable()
		{
			ShakeEmitter.instances.Add(this);
			if (this.shakeOnEnable)
			{
				this.StartShake();
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000CC6BF File Offset: 0x000CA8BF
		private void OnDisable()
		{
			ShakeEmitter.instances.Remove(this);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000CC6CD File Offset: 0x000CA8CD
		private void OnValidate()
		{
			if (this.wave.frequency == 0f)
			{
				this.wave.frequency = 1f;
				Debug.Log("ShakeEmitter with wave frequency 0.0 is not allowed!");
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000CC6FB File Offset: 0x000CA8FB
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Application.onUpdate += ShakeEmitter.UpdateAll;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000CC710 File Offset: 0x000CA910
		public static void UpdateAll()
		{
			float deltaTime = Time.deltaTime;
			if (deltaTime == 0f)
			{
				return;
			}
			for (int i = 0; i < ShakeEmitter.instances.Count; i++)
			{
				ShakeEmitter.instances[i].ManualUpdate(deltaTime);
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000CC752 File Offset: 0x000CA952
		public float CurrentShakeFade()
		{
			if (!this.amplitudeTimeDecay)
			{
				return 1f;
			}
			return 1f - this.stopwatch / this.duration;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000CC778 File Offset: 0x000CA978
		public void ManualUpdate(float deltaTime)
		{
			this.stopwatch += deltaTime;
			if (this.stopwatch < this.duration)
			{
				float d = this.CurrentShakeFade();
				this.halfPeriodTimer -= deltaTime;
				if (this.halfPeriodTimer < 0f)
				{
					this.halfPeriodVector = Vector3.Slerp(UnityEngine.Random.onUnitSphere, -this.halfPeriodVector, 0.5f);
					this.halfPeriodTimer += this.wave.period * 0.5f;
				}
				this.currentOffset = this.halfPeriodVector * this.wave.Evaluate(this.halfPeriodTimer) * d;
				return;
			}
			this.currentOffset = Vector3.zero;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000CC838 File Offset: 0x000CAA38
		public static void ApplySpacialRumble(LocalUser localUser, Transform cameraTransform)
		{
			Vector3 right = cameraTransform.right;
			Vector3 position = cameraTransform.position;
			float num = 0f;
			float num2 = 0f;
			int i = 0;
			int count = ShakeEmitter.instances.Count;
			while (i < count)
			{
				ShakeEmitter shakeEmitter = ShakeEmitter.instances[i];
				Vector3 position2 = shakeEmitter.transform.position;
				float value = Vector3.Dot(position2 - position, right);
				float sqrMagnitude = (position - position2).sqrMagnitude;
				float num3 = shakeEmitter.radius;
				float num4 = 0f;
				if (sqrMagnitude < num3 * num3)
				{
					float num5 = 1f - Mathf.Sqrt(sqrMagnitude) / num3;
					num4 = shakeEmitter.CurrentShakeFade() * shakeEmitter.wave.amplitude * num5;
				}
				float num6 = Mathf.Clamp01(Util.Remap(value, -1f, 1f, 0f, 1f));
				float num7 = num4;
				num += num7 * (1f - num6);
				num2 += num7 * num6;
				i++;
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000CC940 File Offset: 0x000CAB40
		public static Vector3 ComputeTotalShakeAtPoint(Vector3 position)
		{
			Vector3 vector = Vector3.zero;
			int i = 0;
			int count = ShakeEmitter.instances.Count;
			while (i < count)
			{
				ShakeEmitter shakeEmitter = ShakeEmitter.instances[i];
				float sqrMagnitude = (position - shakeEmitter.transform.position).sqrMagnitude;
				float num = shakeEmitter.radius;
				if (sqrMagnitude < num * num)
				{
					float d = 1f - Mathf.Sqrt(sqrMagnitude) / num;
					vector += shakeEmitter.currentOffset * d;
				}
				i++;
			}
			return vector;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000CC9CC File Offset: 0x000CABCC
		public static ShakeEmitter CreateSimpleShakeEmitter(Vector3 position, Wave wave, float duration, float radius, bool amplitudeTimeDecay)
		{
			if (wave.frequency == 0f)
			{
				Debug.Log("ShakeEmitter with wave frequency 0.0 is not allowed!");
				wave.frequency = 1f;
			}
			GameObject gameObject = new GameObject("ShakeEmitter", new Type[]
			{
				typeof(ShakeEmitter),
				typeof(DestroyOnTimer)
			});
			ShakeEmitter component = gameObject.GetComponent<ShakeEmitter>();
			DestroyOnTimer component2 = gameObject.GetComponent<DestroyOnTimer>();
			gameObject.transform.position = position;
			component.wave = wave;
			component.duration = duration;
			component.radius = radius;
			component.amplitudeTimeDecay = amplitudeTimeDecay;
			component2.duration = duration;
			return component;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000CCA64 File Offset: 0x000CAC64
		private void OnDrawGizmosSelected()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Color color = Gizmos.color;
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(Vector3.zero, this.radius);
			Gizmos.color = color;
			Gizmos.matrix = matrix;
		}

		// Token: 0x040031BD RID: 12733
		private static readonly List<ShakeEmitter> instances = new List<ShakeEmitter>();

		// Token: 0x040031BE RID: 12734
		[Tooltip("Whether or not to begin shaking as soon as this instance becomes active.")]
		public bool shakeOnStart = true;

		// Token: 0x040031BF RID: 12735
		[Tooltip("Whether or not to begin shaking every time this instance is enabled.")]
		public bool shakeOnEnable;

		// Token: 0x040031C0 RID: 12736
		[Tooltip("The wave description of this motion.")]
		public Wave wave = new Wave
		{
			amplitude = 1f,
			frequency = 1f,
			cycleOffset = 0f
		};

		// Token: 0x040031C1 RID: 12737
		[Tooltip("How long the shake lasts, in seconds.")]
		public float duration = 1f;

		// Token: 0x040031C2 RID: 12738
		[Tooltip("How far the wave reaches.")]
		public float radius = 10f;

		// Token: 0x040031C3 RID: 12739
		[Tooltip("Whether or not the radius should be multiplied with local scale.")]
		public bool scaleShakeRadiusWithLocalScale;

		// Token: 0x040031C4 RID: 12740
		[Tooltip("Whether or not the ampitude should decay with time.")]
		public bool amplitudeTimeDecay = true;

		// Token: 0x040031C5 RID: 12741
		private float stopwatch = float.PositiveInfinity;

		// Token: 0x040031C6 RID: 12742
		private float halfPeriodTimer;

		// Token: 0x040031C7 RID: 12743
		private Vector3 halfPeriodVector;

		// Token: 0x040031C8 RID: 12744
		private Vector3 currentOffset;

		// Token: 0x040031C9 RID: 12745
		private const float deepRumbleFactor = 5f;

		// Token: 0x0200088D RID: 2189
		public struct MotorBias
		{
			// Token: 0x040031CA RID: 12746
			public float deepLeftBias;

			// Token: 0x040031CB RID: 12747
			public float quickRightBias;
		}
	}
}
