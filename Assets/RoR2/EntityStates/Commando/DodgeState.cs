using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Commando
{
	// Token: 0x020003E3 RID: 995
	public class DodgeState : BaseState
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x0004E764 File Offset: 0x0004C964
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(DodgeState.dodgeSoundString, base.gameObject);
			this.animator = base.GetModelAnimator();
			ChildLocator component = this.animator.GetComponent<ChildLocator>();
			if (base.isAuthority)
			{
				if (base.inputBank && base.characterDirection)
				{
					this.forwardDirection = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
				}
				if (base.skillLocator.primary)
				{
					base.skillLocator.primary.Reset();
					base.skillLocator.primary.stock = DodgeState.primaryReloadStockCount;
				}
			}
			Vector3 rhs = base.characterDirection ? base.characterDirection.forward : this.forwardDirection;
			Vector3 rhs2 = Vector3.Cross(Vector3.up, rhs);
			float num = Vector3.Dot(this.forwardDirection, rhs);
			float num2 = Vector3.Dot(this.forwardDirection, rhs2);
			this.animator.SetFloat("forwardSpeed", num, 0.1f, Time.fixedDeltaTime);
			this.animator.SetFloat("rightSpeed", num2, 0.1f, Time.fixedDeltaTime);
			if (Mathf.Abs(num) > Mathf.Abs(num2))
			{
				base.PlayAnimation("Body", (num > 0f) ? "DodgeForward" : "DodgeBackward", "Dodge.playbackRate", this.duration);
			}
			else
			{
				base.PlayAnimation("Body", (num2 > 0f) ? "DodgeRight" : "DodgeLeft", "Dodge.playbackRate", this.duration);
			}
			if (DodgeState.jetEffect)
			{
				Transform transform = component.FindChild("LeftJet");
				Transform transform2 = component.FindChild("RightJet");
				if (transform)
				{
					UnityEngine.Object.Instantiate<GameObject>(DodgeState.jetEffect, transform);
				}
				if (transform2)
				{
					UnityEngine.Object.Instantiate<GameObject>(DodgeState.jetEffect, transform2);
				}
			}
			this.RecalculateRollSpeed();
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity.y = 0f;
				base.characterMotor.velocity = this.forwardDirection * this.rollSpeed;
			}
			Vector3 b = base.characterMotor ? base.characterMotor.velocity : Vector3.zero;
			this.previousPosition = base.transform.position - b;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004E9FB File Offset: 0x0004CBFB
		private void RecalculateRollSpeed()
		{
			this.rollSpeed = this.moveSpeedStat * Mathf.Lerp(this.initialSpeedCoefficient, this.finalSpeedCoefficient, base.fixedAge / this.duration);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004EA28 File Offset: 0x0004CC28
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.RecalculateRollSpeed();
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = Mathf.Lerp(DodgeState.dodgeFOV, 60f, base.fixedAge / this.duration);
			}
			Vector3 normalized = (base.transform.position - this.previousPosition).normalized;
			if (base.characterMotor && base.characterDirection && normalized != Vector3.zero)
			{
				Vector3 vector = normalized * this.rollSpeed;
				float y = vector.y;
				vector.y = 0f;
				float d = Mathf.Max(Vector3.Dot(vector, this.forwardDirection), 0f);
				vector = this.forwardDirection * d;
				vector.y += Mathf.Max(y, 0f);
				base.characterMotor.velocity = vector;
			}
			this.previousPosition = base.transform.position;
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004EB59 File Offset: 0x0004CD59
		public override void OnExit()
		{
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = -1f;
			}
			base.OnExit();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004EB7E File Offset: 0x0004CD7E
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.forwardDirection);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004EB93 File Offset: 0x0004CD93
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.forwardDirection = reader.ReadVector3();
		}

		// Token: 0x04001697 RID: 5783
		[SerializeField]
		public float duration = 0.9f;

		// Token: 0x04001698 RID: 5784
		[SerializeField]
		public float initialSpeedCoefficient;

		// Token: 0x04001699 RID: 5785
		[SerializeField]
		public float finalSpeedCoefficient;

		// Token: 0x0400169A RID: 5786
		public static string dodgeSoundString;

		// Token: 0x0400169B RID: 5787
		public static GameObject jetEffect;

		// Token: 0x0400169C RID: 5788
		public static float dodgeFOV;

		// Token: 0x0400169D RID: 5789
		public static int primaryReloadStockCount;

		// Token: 0x0400169E RID: 5790
		private float rollSpeed;

		// Token: 0x0400169F RID: 5791
		private Vector3 forwardDirection;

		// Token: 0x040016A0 RID: 5792
		private Animator animator;

		// Token: 0x040016A1 RID: 5793
		private Vector3 previousPosition;
	}
}
