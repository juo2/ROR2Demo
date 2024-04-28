using System;

namespace RoR2
{
	// Token: 0x02000609 RID: 1545
	public interface ICameraStateProvider
	{
		// Token: 0x06001C54 RID: 7252
		void GetCameraState(CameraRigController cameraRigController, ref CameraState cameraState);

		// Token: 0x06001C55 RID: 7253
		bool IsUserLookAllowed(CameraRigController cameraRigController);

		// Token: 0x06001C56 RID: 7254
		bool IsUserControlAllowed(CameraRigController cameraRigController);

		// Token: 0x06001C57 RID: 7255
		bool IsHudAllowed(CameraRigController cameraRigController);
	}
}
