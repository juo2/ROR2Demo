using System;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B65 RID: 2917
	public struct VibrationContext
	{
		// Token: 0x0600424E RID: 16974 RVA: 0x00112A38 File Offset: 0x00110C38
		public float CalcCamDisplacementMagnitude()
		{
			if (!this.cameraRigController)
			{
				return 0f;
			}
			return this.cameraRigController.rawScreenShakeDisplacement.magnitude;
		}

		// Token: 0x04004058 RID: 16472
		public CameraRigController cameraRigController;

		// Token: 0x04004059 RID: 16473
		public LocalUser localUser;

		// Token: 0x0400405A RID: 16474
		public float userVibrationScale;
	}
}
