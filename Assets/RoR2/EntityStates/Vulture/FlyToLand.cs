using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Vulture
{
	// Token: 0x020000E2 RID: 226
	public class FlyToLand : BaseSkillState
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.characterGravityParameterProvider = base.gameObject.GetComponent<ICharacterGravityParameterProvider>();
			this.characterFlightParameterProvider = base.gameObject.GetComponent<ICharacterFlightParameterProvider>();
			Vector3 footPosition = this.GetFootPosition();
			if (base.isAuthority)
			{
				bool flag = false;
				NodeGraph groundNodes = SceneInfo.instance.groundNodes;
				if (groundNodes)
				{
					NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNodeWithFlagConditions(base.transform.position, base.characterBody.hullClassification, NodeFlags.None, NodeFlags.None, false);
					flag = (nodeIndex != NodeGraph.NodeIndex.invalid && groundNodes.GetNodePosition(nodeIndex, out this.targetPosition));
				}
				if (!flag)
				{
					this.outer.SetNextState(new Fly
					{
						activatorSkillSlot = base.activatorSkillSlot
					});
					this.duration = 0f;
					this.targetPosition = footPosition;
					return;
				}
			}
			Vector3 vector = this.targetPosition - footPosition;
			float num = this.moveSpeedStat * FlyToLand.speedMultiplier;
			this.duration = vector.magnitude / num;
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount++;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount++;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		private Vector3 GetFootPosition()
		{
			if (base.characterBody)
			{
				return base.characterBody.footPosition;
			}
			return base.transform.position;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			Vector3 footPosition = this.GetFootPosition();
			base.characterMotor.moveDirection = (this.targetPosition - footPosition).normalized * FlyToLand.speedMultiplier;
			if (base.isAuthority && (base.characterMotor.isGrounded || this.duration <= base.fixedAge))
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010E50 File Offset: 0x0000F050
		public override void OnExit()
		{
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount--;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount--;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.SetFloat("Flying", 0f);
			}
			base.OnExit();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00010EDA File Offset: 0x0000F0DA
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.targetPosition);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010EEF File Offset: 0x0000F0EF
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.targetPosition = reader.ReadVector3();
		}

		// Token: 0x04000413 RID: 1043
		private float duration;

		// Token: 0x04000414 RID: 1044
		private Vector3 targetPosition;

		// Token: 0x04000415 RID: 1045
		public static float speedMultiplier;

		// Token: 0x04000416 RID: 1046
		private ICharacterGravityParameterProvider characterGravityParameterProvider;

		// Token: 0x04000417 RID: 1047
		private ICharacterFlightParameterProvider characterFlightParameterProvider;
	}
}
