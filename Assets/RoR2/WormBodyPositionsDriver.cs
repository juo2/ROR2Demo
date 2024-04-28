using System;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000911 RID: 2321
	[RequireComponent(typeof(WormBodyPositions2))]
	public class WormBodyPositionsDriver : MonoBehaviour
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000DD866 File Offset: 0x000DBA66
		// (set) Token: 0x06003473 RID: 13427 RVA: 0x000DD86E File Offset: 0x000DBA6E
		public Vector3 chaserVelocity { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000DD877 File Offset: 0x000DBA77
		// (set) Token: 0x06003475 RID: 13429 RVA: 0x000DD87F File Offset: 0x000DBA7F
		public Vector3 chaserPosition { get; private set; }

		// Token: 0x06003476 RID: 13430 RVA: 0x000DD888 File Offset: 0x000DBA88
		private void Awake()
		{
			this.wormBodyPositions = base.GetComponent<WormBodyPositions2>();
			this.characterDirection = base.GetComponent<CharacterDirection>();
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000DD8A2 File Offset: 0x000DBAA2
		private void OnEnable()
		{
			this.wormBodyPositions.onPredictedBreachDiscovered += this.OnPredictedBreachDiscovered;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000DD8BB File Offset: 0x000DBABB
		private void OnDisable()
		{
			this.wormBodyPositions.onPredictedBreachDiscovered -= this.OnPredictedBreachDiscovered;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000DD8D4 File Offset: 0x000DBAD4
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.chaserPosition = base.transform.position;
				this.chaserVelocity = this.characterDirection.forward;
			}
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000DD8FF File Offset: 0x000DBAFF
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000DD910 File Offset: 0x000DBB10
		public void OnTeleport(Vector3 oldPosition, Vector3 newPosition)
		{
			Vector3 b = newPosition - oldPosition;
			this.chaserPosition += b;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000DD938 File Offset: 0x000DBB38
		private void OnPredictedBreachDiscovered(float expectedTime, Vector3 hitPosition, Vector3 hitNormal)
		{
			float magnitude = this.chaserVelocity.magnitude;
			if (magnitude > this.maxBreachSpeed)
			{
				this.chaserVelocity /= magnitude / this.maxBreachSpeed;
			}
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000DD978 File Offset: 0x000DBB78
		private void FixedUpdateServer()
		{
			Vector3 position = this.referenceTransform.position;
			float speedMultiplier = this.wormBodyPositions.speedMultiplier;
			Vector3 vector = position - this.chaserPosition;
			Vector3 normalized = vector.normalized;
			float num = (this.chaserIsUnderground ? this.maxTurnSpeed : (this.maxTurnSpeed * this.turnRateCoefficientAboveGround)) * 0.017453292f;
			Vector3 vector2 = new Vector3(this.chaserVelocity.x, 0f, this.chaserVelocity.z);
			Vector3 a = new Vector3(normalized.x, 0f, normalized.z);
			vector2 = Vector3.RotateTowards(vector2, a * speedMultiplier, num * Time.fixedDeltaTime, float.PositiveInfinity);
			vector2 = vector2.normalized * speedMultiplier;
			float num2 = position.y - this.chaserPosition.y;
			float num3 = -this.chaserVelocity.y * this.yDamperConstant;
			float num4 = num2 * this.ySpringConstant;
			if (this.allowShoving && Mathf.Abs(this.chaserVelocity.y) < this.yShoveVelocityThreshold && num2 > this.yShovePositionThreshold)
			{
				vector = this.chaserVelocity;
				this.chaserVelocity = vector.XAZ(this.chaserVelocity.y + this.yShoveForce * Time.fixedDeltaTime);
			}
			if (!this.chaserIsUnderground)
			{
				num4 *= this.wormForceCoefficientAboveGround;
				num3 *= this.wormForceCoefficientAboveGround;
			}
			vector = this.chaserVelocity;
			this.chaserVelocity = vector.XAZ(this.chaserVelocity.y + (num4 + num3) * Time.fixedDeltaTime);
			this.chaserVelocity += Physics.gravity * Time.fixedDeltaTime;
			this.chaserVelocity = new Vector3(vector2.x, this.chaserVelocity.y, vector2.z);
			this.chaserPosition += this.chaserVelocity * Time.fixedDeltaTime;
			this.chasePositionVisualizer.position = this.chaserPosition;
			this.chaserIsUnderground = (-num2 < this.wormBodyPositions.undergroundTestYOffset);
			this.keyFrameGenerationTimer -= Time.deltaTime;
			if (this.keyFrameGenerationTimer <= 0f)
			{
				this.keyFrameGenerationTimer = this.keyFrameGenerationInterval;
				this.wormBodyPositions.AttemptToGenerateKeyFrame(this.wormBodyPositions.GetSynchronizedTimeStamp() + this.wormBodyPositions.followDelay, this.chaserPosition, this.chaserVelocity);
			}
		}

		// Token: 0x04003576 RID: 13686
		public Transform referenceTransform;

		// Token: 0x04003577 RID: 13687
		public Transform chasePositionVisualizer;

		// Token: 0x04003578 RID: 13688
		public float maxTurnSpeed = 180f;

		// Token: 0x04003579 RID: 13689
		public float verticalTurnSquashFactor = 2f;

		// Token: 0x0400357A RID: 13690
		public float ySpringConstant = 100f;

		// Token: 0x0400357B RID: 13691
		public float yDamperConstant = 1f;

		// Token: 0x0400357C RID: 13692
		public bool allowShoving;

		// Token: 0x0400357D RID: 13693
		public float yShoveVelocityThreshold;

		// Token: 0x0400357E RID: 13694
		public float yShovePositionThreshold;

		// Token: 0x0400357F RID: 13695
		public float yShoveForce;

		// Token: 0x04003580 RID: 13696
		public float turnRateCoefficientAboveGround;

		// Token: 0x04003581 RID: 13697
		public float wormForceCoefficientAboveGround;

		// Token: 0x04003582 RID: 13698
		public float keyFrameGenerationInterval = 0.25f;

		// Token: 0x04003583 RID: 13699
		public float maxBreachSpeed = 40f;

		// Token: 0x04003584 RID: 13700
		private WormBodyPositions2 wormBodyPositions;

		// Token: 0x04003585 RID: 13701
		private CharacterDirection characterDirection;

		// Token: 0x04003588 RID: 13704
		private Vector3 chaserPreviousVelocity;

		// Token: 0x04003589 RID: 13705
		private bool chaserIsUnderground;

		// Token: 0x0400358A RID: 13706
		private float keyFrameGenerationTimer;
	}
}
