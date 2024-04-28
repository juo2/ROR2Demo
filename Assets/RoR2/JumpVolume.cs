using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x02000790 RID: 1936
	public class JumpVolume : MonoBehaviour
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x000B1710 File Offset: 0x000AF910
		public void OnTriggerStay(Collider other)
		{
			CharacterMotor component = other.GetComponent<CharacterMotor>();
			if (component && component.hasEffectiveAuthority)
			{
				this.onJump.Invoke();
				if (!component.disableAirControlUntilCollision)
				{
					Util.PlaySound(this.jumpSoundString, base.gameObject);
				}
				component.velocity = this.jumpVelocity;
				component.disableAirControlUntilCollision = true;
				component.Motor.ForceUnground();
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000B1778 File Offset: 0x000AF978
		private void OnDrawGizmos()
		{
			int num = 20;
			float d = this.time / (float)num;
			Vector3 vector = base.transform.position;
			Vector3 position = base.transform.position;
			Vector3 a = this.jumpVelocity;
			Gizmos.color = Color.yellow;
			for (int i = 0; i <= num; i++)
			{
				Vector3 vector2 = vector + a * d;
				a += Physics.gravity * d;
				Gizmos.DrawLine(vector2, vector);
				vector = vector2;
			}
		}

		// Token: 0x04002C60 RID: 11360
		public Transform targetElevationTransform;

		// Token: 0x04002C61 RID: 11361
		public Vector3 jumpVelocity;

		// Token: 0x04002C62 RID: 11362
		public float time;

		// Token: 0x04002C63 RID: 11363
		public string jumpSoundString;

		// Token: 0x04002C64 RID: 11364
		public UnityEvent onJump;
	}
}
