using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000899 RID: 2201
	[RequireComponent(typeof(CharacterModel))]
	[RequireComponent(typeof(Animator))]
	public class SkateSparks : MonoBehaviour
	{
		// Token: 0x0600309F RID: 12447 RVA: 0x000CE938 File Offset: 0x000CCB38
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.characterModel = base.GetComponent<CharacterModel>();
			this.leftFoot = (this.leftParticleSystem ? this.leftParticleSystem.transform : null);
			this.rightFoot = (this.rightParticleSystem ? this.rightParticleSystem.transform : null);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000CE9A0 File Offset: 0x000CCBA0
		private static void UpdateFoot(ref SkateSparks.FootState footState, ParticleSystem particleSystem, Transform transform, float walkSpeed, float overspeedStressCoefficient, float accelerationStressCoefficient, float perpendicularTravelStressCoefficient, bool isGrounded, float maxStress, float minStressForEmission, float maxEmissionRate, float deltaTime)
		{
			Vector3? position = transform ? new Vector3?(transform.position) : null;
			float? speed = null;
			Vector3 a = Vector3.zero;
			if (position != null && footState.position != null)
			{
				a = position.Value - footState.position.Value;
				a.y = 0f;
			}
			float magnitude = a.magnitude;
			float num = 0f;
			float num2 = 0f;
			if (deltaTime != 0f)
			{
				speed = new float?(magnitude / deltaTime);
				if (footState.speed != null)
				{
					num = speed.Value - footState.speed.Value;
				}
				float num3 = Mathf.Max(speed.Value - walkSpeed, 0f);
				if (transform && magnitude > 0f)
				{
					Vector3 rhs = a / magnitude;
					num2 = Mathf.Abs(Vector3.Dot(transform.right, rhs)) * magnitude;
				}
				footState.stressAccumulator += num3 * overspeedStressCoefficient * deltaTime + num * accelerationStressCoefficient + num2 * perpendicularTravelStressCoefficient;
				footState.debugAcceleration = num;
				footState.debugOverspeed = num3;
			}
			if (!isGrounded)
			{
				footState.stressAccumulator = 0f;
			}
			footState.stressAccumulator = Mathf.Clamp(footState.stressAccumulator, 0f, maxStress);
			footState.emissionTimer -= deltaTime * maxEmissionRate;
			if (footState.emissionTimer <= 0f)
			{
				footState.emissionTimer = 1f;
				footState.stressAccumulator -= 1f;
				if (footState.stressAccumulator >= minStressForEmission && particleSystem)
				{
					particleSystem.Emit(1);
				}
			}
			footState.position = position;
			footState.speed = speed;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000CEB64 File Offset: 0x000CCD64
		private void Update()
		{
			bool flag = false;
			float num = 0f;
			CharacterBody body = this.characterModel.body;
			if (body)
			{
				num = body.moveSpeed;
				if (body.isSprinting)
				{
					num /= body.sprintingSpeedMultiplier;
				}
				CharacterMotor characterMotor = body.characterMotor;
				if (characterMotor)
				{
					flag = characterMotor.isGrounded;
				}
			}
			if (flag != this.previousIsGrounded)
			{
				float num2 = (float)this.landingStress;
				this.leftFootState.stressAccumulator = this.leftFootState.stressAccumulator + num2;
				this.rightFootState.stressAccumulator = this.rightFootState.stressAccumulator + num2;
			}
			float deltaTime = Time.deltaTime;
			SkateSparks.UpdateFoot(ref this.leftFootState, this.leftParticleSystem, this.leftFoot, num, this.overspeedStressCoefficient, this.accelerationStressCoefficient, this.perpendicularTravelStressCoefficient, flag, this.maxStress, this.minStressForEmission, this.maxEmissionRate, deltaTime);
			SkateSparks.UpdateFoot(ref this.rightFootState, this.rightParticleSystem, this.rightFoot, num, this.overspeedStressCoefficient, this.accelerationStressCoefficient, this.perpendicularTravelStressCoefficient, flag, this.maxStress, this.minStressForEmission, this.maxEmissionRate, deltaTime);
			this.debugWalkSpeed = num;
			this.previousIsGrounded = flag;
		}

		// Token: 0x04003242 RID: 12866
		public float maxStress = 4f;

		// Token: 0x04003243 RID: 12867
		public float minStressForEmission = 1f;

		// Token: 0x04003244 RID: 12868
		public float overspeedStressCoefficient = 1f;

		// Token: 0x04003245 RID: 12869
		public float accelerationStressCoefficient = 1f;

		// Token: 0x04003246 RID: 12870
		public float perpendicularTravelStressCoefficient = 1f;

		// Token: 0x04003247 RID: 12871
		public float maxEmissionRate = 10f;

		// Token: 0x04003248 RID: 12872
		public int landingStress = 4;

		// Token: 0x04003249 RID: 12873
		public ParticleSystem leftParticleSystem;

		// Token: 0x0400324A RID: 12874
		public ParticleSystem rightParticleSystem;

		// Token: 0x0400324B RID: 12875
		private Animator animator;

		// Token: 0x0400324C RID: 12876
		private Transform leftFoot;

		// Token: 0x0400324D RID: 12877
		private Transform rightFoot;

		// Token: 0x0400324E RID: 12878
		private CharacterModel characterModel;

		// Token: 0x0400324F RID: 12879
		private static readonly int isGroundedParam = Animator.StringToHash("isGrounded");

		// Token: 0x04003250 RID: 12880
		private bool previousIsGrounded = true;

		// Token: 0x04003251 RID: 12881
		private SkateSparks.FootState leftFootState;

		// Token: 0x04003252 RID: 12882
		private SkateSparks.FootState rightFootState;

		// Token: 0x04003253 RID: 12883
		private float debugWalkSpeed;

		// Token: 0x0200089A RID: 2202
		[Serializable]
		private struct FootState
		{
			// Token: 0x04003254 RID: 12884
			public float? speed;

			// Token: 0x04003255 RID: 12885
			public Vector3? position;

			// Token: 0x04003256 RID: 12886
			public float stressAccumulator;

			// Token: 0x04003257 RID: 12887
			public float emissionTimer;

			// Token: 0x04003258 RID: 12888
			public float debugAcceleration;

			// Token: 0x04003259 RID: 12889
			public float debugOverspeed;
		}
	}
}
