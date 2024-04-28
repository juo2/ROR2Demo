using System;
using UnityEngine;

namespace RoR2.CameraModes
{
	// Token: 0x02000E4F RID: 3663
	public class CameraModePlayer2D : CameraModeBase
	{
		// Token: 0x060053EA RID: 21482 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool IsSpectating(CameraRigController cameraRigController)
		{
			return false;
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void ApplyLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraModeBase.ApplyLookInputArgs input)
		{
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x00159E89 File Offset: 0x00158089
		protected override void CollectLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.CollectLookInputResult result)
		{
			result.lookInput = Vector2.zero;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00159ED0 File Offset: 0x001580D0
		protected override object CreateInstanceData(CameraRigController cameraRigController)
		{
			return new CameraModePlayer2D.InstanceData();
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void MatchStateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraState cameraStateToMatch)
		{
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void OnTargetChangedInternal(object rawInstanceData, CameraRigController cameraRigController, in CameraModeBase.OnTargetChangedArgs args)
		{
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00159ED8 File Offset: 0x001580D8
		protected override void UpdateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.UpdateResult result)
		{
			CameraModePlayer2D.InstanceData instanceData = (CameraModePlayer2D.InstanceData)rawInstanceData;
			result.cameraState = context.cameraInfo.previousCameraState;
			result.firstPersonTarget = null;
			result.showSprintParticles = false;
			result.crosshairWorldPosition = result.cameraState.position;
			if (context.targetInfo.target)
			{
				Quaternion identity = Quaternion.identity;
				Vector3 vector = identity * Quaternion.Euler(0f, 90f, 0f) * Vector3.forward;
				Quaternion lhs;
				if (context.targetInfo.body && context.targetInfo.body.characterDirection)
				{
					lhs = Quaternion.Euler(0f, context.targetInfo.body.characterDirection.yaw, 0f);
				}
				else
				{
					lhs = context.targetInfo.target.transform.rotation;
				}
				Vector3 a = context.targetInfo.target.transform.position;
				if (context.targetInfo.inputBank)
				{
					float num = Vector3.Dot(vector, context.targetInfo.inputBank.moveVector);
					if (num != 0f)
					{
						instanceData.facing = num;
					}
					a = context.targetInfo.inputBank.aimOrigin;
				}
				lhs * Quaternion.Euler(0f, 90f, 0f);
				result.cameraState.rotation = identity;
				result.cameraState.position = context.targetInfo.target.transform.position + new Vector3(0f, 10f, 0f) + identity * Vector3.forward * -30f;
				result.cameraState.fov = 60f;
				result.crosshairWorldPosition = a + vector * instanceData.facing * 30f;
			}
		}

		// Token: 0x04004FD4 RID: 20436
		public static readonly CameraModePlayer2D instance = new CameraModePlayer2D();

		// Token: 0x02000E50 RID: 3664
		private class InstanceData
		{
			// Token: 0x04004FD5 RID: 20437
			public float facing = 1f;
		}
	}
}
