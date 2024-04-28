using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003B RID: 59
[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone : MonoBehaviour
{
	// Token: 0x06000109 RID: 265 RVA: 0x000057EE File Offset: 0x000039EE
	private void Start()
	{
		this.SetupParticles();
	}

	// Token: 0x0600010A RID: 266 RVA: 0x000057F6 File Offset: 0x000039F6
	private void FixedUpdate()
	{
		if (this.m_UpdateMode == DynamicBone.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00005807 File Offset: 0x00003A07
	private void Update()
	{
		if (this.m_UpdateMode != DynamicBone.UpdateMode.AnimatePhysics)
		{
			this.PreUpdate();
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00005818 File Offset: 0x00003A18
	private void LateUpdate()
	{
		if (this.m_DistantDisable)
		{
			this.CheckDistance();
		}
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			float deltaTime = Time.deltaTime;
			this.UpdateDynamicBones(deltaTime);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000585D File Offset: 0x00003A5D
	private void PreUpdate()
	{
		if (this.m_Weight > 0f && (!this.m_DistantDisable || !this.m_DistantDisabled))
		{
			this.InitTransforms();
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00005884 File Offset: 0x00003A84
	private void CheckDistance()
	{
		Transform transform = this.m_ReferenceObject;
		if (transform == null && Camera.main)
		{
			transform = Camera.main.transform;
		}
		if (transform)
		{
			bool flag = (transform.position - base.transform.position).sqrMagnitude > this.m_DistanceToObject * this.m_DistanceToObject;
			if (flag != this.m_DistantDisabled)
			{
				if (!flag)
				{
					this.ResetParticlesPosition();
				}
				this.m_DistantDisabled = flag;
			}
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00005907 File Offset: 0x00003B07
	private void OnEnable()
	{
		this.ResetParticlesPosition();
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000590F File Offset: 0x00003B0F
	private void OnDisable()
	{
		this.InitTransforms();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00005918 File Offset: 0x00003B18
	private void OnValidate()
	{
		this.m_UpdateRate = Mathf.Max(this.m_UpdateRate, 0f);
		this.m_Damping = Mathf.Clamp01(this.m_Damping);
		this.m_Elasticity = Mathf.Clamp01(this.m_Elasticity);
		this.m_Stiffness = Mathf.Clamp01(this.m_Stiffness);
		this.m_Inert = Mathf.Clamp01(this.m_Inert);
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		if (Application.isEditor && Application.isPlaying)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000059B0 File Offset: 0x00003BB0
	private void OnDrawGizmosSelected()
	{
		if (!base.enabled || this.m_Root == null)
		{
			return;
		}
		if (Application.isEditor && !Application.isPlaying && base.transform.hasChanged)
		{
			this.InitTransforms();
			this.SetupParticles();
		}
		Gizmos.color = Color.white;
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
			}
			if (particle.m_Radius > 0f)
			{
				Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * this.m_ObjectScale);
			}
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00005A79 File Offset: 0x00003C79
	public void SetWeight(float w)
	{
		if (this.m_Weight != w)
		{
			if (w == 0f)
			{
				this.InitTransforms();
			}
			else if (this.m_Weight == 0f)
			{
				this.ResetParticlesPosition();
			}
			this.m_Weight = w;
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00005AAE File Offset: 0x00003CAE
	public float GetWeight()
	{
		return this.m_Weight;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00005AB8 File Offset: 0x00003CB8
	private void UpdateDynamicBones(float t)
	{
		if (this.m_Root == null)
		{
			return;
		}
		this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
		this.m_ObjectMove = base.transform.position - this.m_ObjectPrevPosition;
		this.m_ObjectPrevPosition = base.transform.position;
		int num = 1;
		if (this.m_UpdateRate > 0f)
		{
			float num2 = 1f / this.m_UpdateRate;
			this.m_Time += t;
			num = 0;
			while (this.m_Time >= num2)
			{
				this.m_Time -= num2;
				if (++num >= 3)
				{
					this.m_Time = 0f;
					break;
				}
			}
		}
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.UpdateParticles1();
				this.UpdateParticles2();
				this.m_ObjectMove = Vector3.zero;
			}
		}
		else
		{
			this.SkipUpdateParticles();
		}
		this.ApplyParticlesToTransforms();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00005BAC File Offset: 0x00003DAC
	private void SetupParticles()
	{
		this.m_Particles.Clear();
		if (this.m_Root == null)
		{
			return;
		}
		this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
		this.m_ObjectScale = Mathf.Abs(base.transform.lossyScale.x);
		this.m_ObjectPrevPosition = base.transform.position;
		this.m_ObjectMove = Vector3.zero;
		this.m_BoneTotalLength = 0f;
		this.AppendParticles(this.m_Root, -1, 0f);
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			particle.m_Damping = this.m_Damping;
			particle.m_Elasticity = this.m_Elasticity;
			particle.m_Stiffness = this.m_Stiffness;
			particle.m_Inert = this.m_Inert;
			particle.m_Radius = this.m_Radius;
			if (this.m_BoneTotalLength > 0f)
			{
				float time = particle.m_BoneLength / this.m_BoneTotalLength;
				if (this.m_DampingDistrib != null && this.m_DampingDistrib.keys.Length != 0)
				{
					particle.m_Damping *= this.m_DampingDistrib.Evaluate(time);
				}
				if (this.m_ElasticityDistrib != null && this.m_ElasticityDistrib.keys.Length != 0)
				{
					particle.m_Elasticity *= this.m_ElasticityDistrib.Evaluate(time);
				}
				if (this.m_StiffnessDistrib != null && this.m_StiffnessDistrib.keys.Length != 0)
				{
					particle.m_Stiffness *= this.m_StiffnessDistrib.Evaluate(time);
				}
				if (this.m_InertDistrib != null && this.m_InertDistrib.keys.Length != 0)
				{
					particle.m_Inert *= this.m_InertDistrib.Evaluate(time);
				}
				if (this.m_RadiusDistrib != null && this.m_RadiusDistrib.keys.Length != 0)
				{
					particle.m_Radius *= this.m_RadiusDistrib.Evaluate(time);
				}
			}
			particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
			particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
			particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
			particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
			particle.m_Radius = Mathf.Max(particle.m_Radius, 0f);
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00005E08 File Offset: 0x00004008
	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		DynamicBone.Particle particle = new DynamicBone.Particle();
		particle.m_Transform = b;
		particle.m_ParentIndex = parentIndex;
		if (b)
		{
			particle.m_Position = (particle.m_PrevPosition = b.position);
			particle.m_InitLocalPosition = b.localPosition;
			particle.m_InitLocalRotation = b.localRotation;
		}
		else
		{
			Transform transform = this.m_Particles[parentIndex].m_Transform;
			if (this.m_EndLength > 0f)
			{
				Transform parent = transform.parent;
				if (parent)
				{
					particle.m_EndOffset = transform.InverseTransformPoint(transform.position * 2f - parent.position) * this.m_EndLength;
				}
				else
				{
					particle.m_EndOffset = new Vector3(this.m_EndLength, 0f, 0f);
				}
			}
			else
			{
				particle.m_EndOffset = transform.InverseTransformPoint(base.transform.TransformDirection(this.m_EndOffset) + transform.position);
			}
			particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
		}
		if (parentIndex >= 0)
		{
			boneLength += (this.m_Particles[parentIndex].m_Transform.position - particle.m_Position).magnitude;
			particle.m_BoneLength = boneLength;
			this.m_BoneTotalLength = Mathf.Max(this.m_BoneTotalLength, boneLength);
		}
		int count = this.m_Particles.Count;
		this.m_Particles.Add(particle);
		if (b)
		{
			for (int i = 0; i < b.childCount; i++)
			{
				bool flag = false;
				if (this.m_Exclusions != null)
				{
					for (int j = 0; j < this.m_Exclusions.Count; j++)
					{
						if (this.m_Exclusions[j] == b.GetChild(i))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.AppendParticles(b.GetChild(i), count, boneLength);
				}
			}
			if (b.childCount == 0 && (this.m_EndLength > 0f || this.m_EndOffset != Vector3.zero))
			{
				this.AppendParticles(null, count, boneLength);
			}
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00006034 File Offset: 0x00004234
	private void InitTransforms()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_Transform)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00006094 File Offset: 0x00004294
	private void ResetParticlesPosition()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_Transform)
			{
				particle.m_Position = (particle.m_PrevPosition = particle.m_Transform.position);
			}
			else
			{
				Transform transform = this.m_Particles[particle.m_ParentIndex].m_Transform;
				particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
			}
		}
		this.m_ObjectPrevPosition = base.transform.position;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00006130 File Offset: 0x00004330
	private void UpdateParticles1()
	{
		Vector3 vector = this.m_Gravity;
		Vector3 normalized = this.m_Gravity.normalized;
		Vector3 lhs = this.m_Root.TransformDirection(this.m_LocalGravity);
		Vector3 b = normalized * Mathf.Max(Vector3.Dot(lhs, normalized), 0f);
		vector -= b;
		vector = (vector + this.m_Force) * this.m_ObjectScale;
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 a = particle.m_Position - particle.m_PrevPosition;
				Vector3 b2 = this.m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + b2;
				particle.m_Position += a * (1f - particle.m_Damping) + vector + b2;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00006268 File Offset: 0x00004468
	private void UpdateParticles2()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			if (particle2.m_Transform)
			{
				float magnitude;
				if (particle.m_Transform)
				{
					magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
				}
				else
				{
					magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
				}
				float num = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
				if (num > 0f || particle.m_Elasticity > 0f)
				{
					Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
					localToWorldMatrix.SetColumn(3, particle2.m_Position);
					Vector3 a;
					if (particle.m_Transform)
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
					}
					else
					{
						a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
					}
					Vector3 a2 = a - particle.m_Position;
					particle.m_Position += a2 * particle.m_Elasticity;
					if (num > 0f)
					{
						a2 = a - particle.m_Position;
						float magnitude2 = a2.magnitude;
						float num2 = magnitude * (1f - num) * 2f;
						if (magnitude2 > num2)
						{
							particle.m_Position += a2 * ((magnitude2 - num2) / magnitude2);
						}
					}
				}
				if (this.m_Colliders != null)
				{
					float particleRadius = particle.m_Radius * this.m_ObjectScale;
					for (int j = 0; j < this.m_Colliders.Count; j++)
					{
						DynamicBoneCollider dynamicBoneCollider = this.m_Colliders[j];
						if (dynamicBoneCollider && dynamicBoneCollider.enabled)
						{
							dynamicBoneCollider.Collide(ref particle.m_Position, particleRadius);
						}
					}
				}
				if (this.m_FreezeAxis != DynamicBone.FreezeAxis.None)
				{
					switch (this.m_FreezeAxis)
					{
					case DynamicBone.FreezeAxis.X:
						plane.SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
						break;
					case DynamicBone.FreezeAxis.Y:
						plane.SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
						break;
					case DynamicBone.FreezeAxis.Z:
						plane.SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
						break;
					}
					particle.m_Position -= plane.normal * plane.GetDistanceToPoint(particle.m_Position);
				}
				Vector3 a3 = particle2.m_Position - particle.m_Position;
				float magnitude3 = a3.magnitude;
				if (magnitude3 > 0f)
				{
					particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
				}
			}
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00006574 File Offset: 0x00004774
	private void SkipUpdateParticles()
	{
		for (int i = 0; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				particle.m_PrevPosition += this.m_ObjectMove;
				particle.m_Position += this.m_ObjectMove;
				DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
				if (particle2.m_Transform)
				{
					float magnitude;
					if (particle.m_Transform)
					{
						magnitude = (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
					}
					else
					{
						magnitude = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude;
					}
					float num = Mathf.Lerp(1f, particle.m_Stiffness, this.m_Weight);
					if (num > 0f)
					{
						Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
						localToWorldMatrix.SetColumn(3, particle2.m_Position);
						Vector3 a;
						if (particle.m_Transform)
						{
							a = localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
						}
						else
						{
							a = localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset);
						}
						Vector3 a2 = a - particle.m_Position;
						float magnitude2 = a2.magnitude;
						float num2 = magnitude * (1f - num) * 2f;
						if (magnitude2 > num2)
						{
							particle.m_Position += a2 * ((magnitude2 - num2) / magnitude2);
						}
					}
					Vector3 a3 = particle2.m_Position - particle.m_Position;
					float magnitude3 = a3.magnitude;
					if (magnitude3 > 0f)
					{
						particle.m_Position += a3 * ((magnitude3 - magnitude) / magnitude3);
					}
				}
			}
			else if (particle.m_Transform)
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00006794 File Offset: 0x00004994
	private static Vector3 MirrorVector(Vector3 v, Vector3 axis)
	{
		return v - axis * (Vector3.Dot(v, axis) * 2f);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000067B0 File Offset: 0x000049B0
	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < this.m_Particles.Count; i++)
		{
			DynamicBone.Particle particle = this.m_Particles[i];
			DynamicBone.Particle particle2 = this.m_Particles[particle.m_ParentIndex];
			if (particle2.m_Transform.childCount <= 1)
			{
				Vector3 direction;
				if (particle.m_Transform)
				{
					direction = particle.m_Transform.localPosition;
				}
				else
				{
					direction = particle.m_EndOffset;
				}
				Vector3 toDirection = particle.m_Position - particle2.m_Position;
				Quaternion lhs = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), toDirection);
				particle2.m_Transform.rotation = lhs * particle2.m_Transform.rotation;
			}
			if (particle.m_Transform)
			{
				particle.m_Transform.position = particle.m_Position;
			}
		}
	}

	// Token: 0x040000F3 RID: 243
	public Transform m_Root;

	// Token: 0x040000F4 RID: 244
	public float m_UpdateRate = 60f;

	// Token: 0x040000F5 RID: 245
	public DynamicBone.UpdateMode m_UpdateMode;

	// Token: 0x040000F6 RID: 246
	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	// Token: 0x040000F7 RID: 247
	public AnimationCurve m_DampingDistrib;

	// Token: 0x040000F8 RID: 248
	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	// Token: 0x040000F9 RID: 249
	public AnimationCurve m_ElasticityDistrib;

	// Token: 0x040000FA RID: 250
	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	// Token: 0x040000FB RID: 251
	public AnimationCurve m_StiffnessDistrib;

	// Token: 0x040000FC RID: 252
	[Range(0f, 1f)]
	public float m_Inert;

	// Token: 0x040000FD RID: 253
	public AnimationCurve m_InertDistrib;

	// Token: 0x040000FE RID: 254
	public float m_Radius;

	// Token: 0x040000FF RID: 255
	public AnimationCurve m_RadiusDistrib;

	// Token: 0x04000100 RID: 256
	public float m_EndLength;

	// Token: 0x04000101 RID: 257
	public Vector3 m_EndOffset = Vector3.zero;

	// Token: 0x04000102 RID: 258
	public Vector3 m_Gravity = Vector3.zero;

	// Token: 0x04000103 RID: 259
	public Vector3 m_Force = Vector3.zero;

	// Token: 0x04000104 RID: 260
	public List<DynamicBoneCollider> m_Colliders;

	// Token: 0x04000105 RID: 261
	public List<Transform> m_Exclusions;

	// Token: 0x04000106 RID: 262
	public DynamicBone.FreezeAxis m_FreezeAxis;

	// Token: 0x04000107 RID: 263
	public bool m_DistantDisable;

	// Token: 0x04000108 RID: 264
	public Transform m_ReferenceObject;

	// Token: 0x04000109 RID: 265
	public float m_DistanceToObject = 20f;

	// Token: 0x0400010A RID: 266
	private Vector3 m_LocalGravity = Vector3.zero;

	// Token: 0x0400010B RID: 267
	private Vector3 m_ObjectMove = Vector3.zero;

	// Token: 0x0400010C RID: 268
	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	// Token: 0x0400010D RID: 269
	private float m_BoneTotalLength;

	// Token: 0x0400010E RID: 270
	private float m_ObjectScale = 1f;

	// Token: 0x0400010F RID: 271
	private float m_Time;

	// Token: 0x04000110 RID: 272
	private float m_Weight = 1f;

	// Token: 0x04000111 RID: 273
	private bool m_DistantDisabled;

	// Token: 0x04000112 RID: 274
	private List<DynamicBone.Particle> m_Particles = new List<DynamicBone.Particle>();

	// Token: 0x0200003C RID: 60
	public enum UpdateMode
	{
		// Token: 0x04000114 RID: 276
		Normal,
		// Token: 0x04000115 RID: 277
		AnimatePhysics,
		// Token: 0x04000116 RID: 278
		UnscaledTime
	}

	// Token: 0x0200003D RID: 61
	public enum FreezeAxis
	{
		// Token: 0x04000118 RID: 280
		None,
		// Token: 0x04000119 RID: 281
		X,
		// Token: 0x0400011A RID: 282
		Y,
		// Token: 0x0400011B RID: 283
		Z
	}

	// Token: 0x0200003E RID: 62
	private class Particle
	{
		// Token: 0x0400011C RID: 284
		public Transform m_Transform;

		// Token: 0x0400011D RID: 285
		public int m_ParentIndex = -1;

		// Token: 0x0400011E RID: 286
		public float m_Damping;

		// Token: 0x0400011F RID: 287
		public float m_Elasticity;

		// Token: 0x04000120 RID: 288
		public float m_Stiffness;

		// Token: 0x04000121 RID: 289
		public float m_Inert;

		// Token: 0x04000122 RID: 290
		public float m_Radius;

		// Token: 0x04000123 RID: 291
		public float m_BoneLength;

		// Token: 0x04000124 RID: 292
		public Vector3 m_Position = Vector3.zero;

		// Token: 0x04000125 RID: 293
		public Vector3 m_PrevPosition = Vector3.zero;

		// Token: 0x04000126 RID: 294
		public Vector3 m_EndOffset = Vector3.zero;

		// Token: 0x04000127 RID: 295
		public Vector3 m_InitLocalPosition = Vector3.zero;

		// Token: 0x04000128 RID: 296
		public Quaternion m_InitLocalRotation = Quaternion.identity;
	}
}
