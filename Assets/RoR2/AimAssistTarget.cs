using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005C2 RID: 1474
	public class AimAssistTarget : MonoBehaviour
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x00072946 File Offset: 0x00070B46
		private void OnEnable()
		{
			AimAssistTarget.instancesList.Add(this);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00072953 File Offset: 0x00070B53
		private void OnDisable()
		{
			AimAssistTarget.instancesList.Remove(this);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00072961 File Offset: 0x00070B61
		private void FixedUpdate()
		{
			if (this.healthComponent && !this.healthComponent.alive)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00072988 File Offset: 0x00070B88
		private void OnDrawGizmos()
		{
			if (this.point0)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(this.point0.position, 1f * this.assistScale * CameraRigController.aimStickAssistMinSize.value * AimAssistTarget.debugAimAssistVisualCoefficient.value);
				Gizmos.color = Color.white;
				Gizmos.DrawWireSphere(this.point0.position, 1f * this.assistScale * CameraRigController.aimStickAssistMaxSize.value * CameraRigController.aimStickAssistMinSize.value * AimAssistTarget.debugAimAssistVisualCoefficient.value);
			}
			if (this.point1)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(this.point1.position, 1f * this.assistScale * CameraRigController.aimStickAssistMinSize.value * AimAssistTarget.debugAimAssistVisualCoefficient.value);
				Gizmos.color = Color.white;
				Gizmos.DrawWireSphere(this.point1.position, 1f * this.assistScale * CameraRigController.aimStickAssistMaxSize.value * CameraRigController.aimStickAssistMinSize.value * AimAssistTarget.debugAimAssistVisualCoefficient.value);
			}
			if (this.point0 && this.point1)
			{
				Gizmos.DrawLine(this.point0.position, this.point1.position);
			}
		}

		// Token: 0x040020CD RID: 8397
		public Transform point0;

		// Token: 0x040020CE RID: 8398
		public Transform point1;

		// Token: 0x040020CF RID: 8399
		public float assistScale = 1f;

		// Token: 0x040020D0 RID: 8400
		public HealthComponent healthComponent;

		// Token: 0x040020D1 RID: 8401
		public TeamComponent teamComponent;

		// Token: 0x040020D2 RID: 8402
		public static List<AimAssistTarget> instancesList = new List<AimAssistTarget>();

		// Token: 0x040020D3 RID: 8403
		public static FloatConVar debugAimAssistVisualCoefficient = new FloatConVar("debug_aim_assist_visual_coefficient", ConVarFlags.None, "2", "Magic for debug visuals. Don't touch.");
	}
}
