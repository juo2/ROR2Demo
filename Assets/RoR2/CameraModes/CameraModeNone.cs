using System;
using UnityEngine;

namespace RoR2.CameraModes
{
	// Token: 0x02000E4E RID: 3662
	public class CameraModeNone : CameraModeBase
	{
		// Token: 0x060053E2 RID: 21474 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool IsSpectating(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void ApplyLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraModeBase.ApplyLookInputArgs input)
		{
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x00159E89 File Offset: 0x00158089
		protected override void CollectLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.CollectLookInputResult result)
		{
			result.lookInput = Vector2.zero;
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override object CreateInstanceData(CameraRigController cameraRigController)
		{
			return null;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void MatchStateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraState cameraStateToMatch)
		{
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void OnTargetChangedInternal(object rawInstanceData, CameraRigController cameraRigController, in CameraModeBase.OnTargetChangedArgs args)
		{
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x00159E96 File Offset: 0x00158096
		protected override void UpdateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.UpdateResult result)
		{
			result.cameraState = context.cameraInfo.previousCameraState;
			result.firstPersonTarget = null;
			result.showSprintParticles = false;
			result.crosshairWorldPosition = result.cameraState.position;
		}

		// Token: 0x04004FD3 RID: 20435
		public static readonly CameraModeNone instance;
	}
}
