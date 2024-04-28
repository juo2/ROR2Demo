using System;
using System.Collections.Generic;
using HG;
using Rewired;
using RoR2.Networking;
using RoR2.UI;
using UnityEngine;

namespace RoR2.CameraModes
{
	// Token: 0x02000E44 RID: 3652
	public abstract class CameraModeBase
	{
		// Token: 0x060053D0 RID: 21456 RVA: 0x00159C60 File Offset: 0x00157E60
		public void OnInstall(CameraRigController cameraRigController)
		{
			object obj = this.CreateInstanceData(cameraRigController);
			this.camToRawInstanceData.Add(cameraRigController, obj);
			try
			{
				this.OnInstallInternal(obj, cameraRigController);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			if (cameraRigController.target != null)
			{
				this.OnTargetChanged(cameraRigController, new CameraModeBase.OnTargetChangedArgs
				{
					oldTarget = null,
					newTarget = cameraRigController.target
				});
			}
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnInstallInternal(object rawInstancedata, CameraRigController cameraRigController)
		{
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00159CD8 File Offset: 0x00157ED8
		public void OnUninstall(CameraRigController cameraRigController)
		{
			if (cameraRigController.target != null)
			{
				this.OnTargetChanged(cameraRigController, new CameraModeBase.OnTargetChangedArgs
				{
					oldTarget = cameraRigController.target,
					newTarget = null
				});
			}
			try
			{
				this.OnUninstallInternal(this.camToRawInstanceData[cameraRigController], cameraRigController);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			this.camToRawInstanceData.Remove(cameraRigController);
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnUninstallInternal(object rawInstanceData, CameraRigController cameraRigController)
		{
		}

		// Token: 0x060053D4 RID: 21460
		protected abstract object CreateInstanceData(CameraRigController cameraRigController);

		// Token: 0x060053D5 RID: 21461
		public abstract bool IsSpectating(CameraRigController cameraRigController);

		// Token: 0x060053D6 RID: 21462 RVA: 0x00159D58 File Offset: 0x00157F58
		public void Update(in CameraModeBase.CameraModeContext context, out CameraModeBase.UpdateResult result)
		{
			object rawInstanceData = this.camToRawInstanceData[context.cameraInfo.cameraRigController];
			this.UpdateInternal(rawInstanceData, context, out result);
		}

		// Token: 0x060053D7 RID: 21463
		protected abstract void UpdateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.UpdateResult result);

		// Token: 0x060053D8 RID: 21464 RVA: 0x00159D8C File Offset: 0x00157F8C
		public void CollectLookInput(in CameraModeBase.CameraModeContext context, out CameraModeBase.CollectLookInputResult result)
		{
			object rawInstanceData = this.camToRawInstanceData[context.cameraInfo.cameraRigController];
			this.CollectLookInputInternal(rawInstanceData, context, out result);
		}

		// Token: 0x060053D9 RID: 21465
		protected abstract void CollectLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.CollectLookInputResult result);

		// Token: 0x060053DA RID: 21466 RVA: 0x00159DC0 File Offset: 0x00157FC0
		public void ApplyLookInput(in CameraModeBase.CameraModeContext context, in CameraModeBase.ApplyLookInputArgs args)
		{
			object rawInstanceData = this.camToRawInstanceData[context.cameraInfo.cameraRigController];
			this.ApplyLookInputInternal(rawInstanceData, context, args);
		}

		// Token: 0x060053DB RID: 21467
		protected abstract void ApplyLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraModeBase.ApplyLookInputArgs input);

		// Token: 0x060053DC RID: 21468 RVA: 0x00159DF4 File Offset: 0x00157FF4
		public void OnTargetChanged(CameraRigController cameraRigController, CameraModeBase.OnTargetChangedArgs args)
		{
			object rawInstanceData = this.camToRawInstanceData[cameraRigController];
			this.OnTargetChangedInternal(rawInstanceData, cameraRigController, args);
		}

		// Token: 0x060053DD RID: 21469
		protected abstract void OnTargetChangedInternal(object rawInstanceData, CameraRigController cameraRigController, in CameraModeBase.OnTargetChangedArgs args);

		// Token: 0x060053DE RID: 21470 RVA: 0x00159E20 File Offset: 0x00158020
		public void MatchState(in CameraModeBase.CameraModeContext context, in CameraState cameraStateToMatch)
		{
			object rawInstanceData = this.camToRawInstanceData[context.cameraInfo.cameraRigController];
			this.MatchStateInternal(rawInstanceData, context, cameraStateToMatch);
		}

		// Token: 0x060053DF RID: 21471
		protected abstract void MatchStateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraState cameraStateToMatch);

		// Token: 0x060053E0 RID: 21472 RVA: 0x00159E54 File Offset: 0x00158054
		public object DebugGetInstanceData(CameraRigController cameraRigController)
		{
			object result;
			this.camToRawInstanceData.TryGetValue(cameraRigController, out result);
			return result;
		}

		// Token: 0x04004FB0 RID: 20400
		private Dictionary<UnityObjectWrapperKey<CameraRigController>, object> camToRawInstanceData = new Dictionary<UnityObjectWrapperKey<CameraRigController>, object>();

		// Token: 0x02000E45 RID: 3653
		public struct CameraInfo
		{
			// Token: 0x04004FB1 RID: 20401
			public CameraRigController cameraRigController;

			// Token: 0x04004FB2 RID: 20402
			public Camera sceneCam;

			// Token: 0x04004FB3 RID: 20403
			public ICameraStateProvider overrideCam;

			// Token: 0x04004FB4 RID: 20404
			public CameraState previousCameraState;

			// Token: 0x04004FB5 RID: 20405
			public float baseFov;
		}

		// Token: 0x02000E46 RID: 3654
		public struct TargetInfo
		{
			// Token: 0x04004FB6 RID: 20406
			public GameObject target;

			// Token: 0x04004FB7 RID: 20407
			public CharacterBody body;

			// Token: 0x04004FB8 RID: 20408
			public InputBankTest inputBank;

			// Token: 0x04004FB9 RID: 20409
			public CameraTargetParams targetParams;

			// Token: 0x04004FBA RID: 20410
			public TeamIndex teamIndex;

			// Token: 0x04004FBB RID: 20411
			public bool isSprinting;

			// Token: 0x04004FBC RID: 20412
			public bool isViewerControlled;

			// Token: 0x04004FBD RID: 20413
			public CharacterMaster master;

			// Token: 0x04004FBE RID: 20414
			public NetworkUser networkUser;

			// Token: 0x04004FBF RID: 20415
			public NetworkedViewAngles networkedViewAngles;
		}

		// Token: 0x02000E47 RID: 3655
		public struct ViewerInfo
		{
			// Token: 0x04004FC0 RID: 20416
			public LocalUser localUser;

			// Token: 0x04004FC1 RID: 20417
			public UserProfile userProfile;

			// Token: 0x04004FC2 RID: 20418
			public Player inputPlayer;

			// Token: 0x04004FC3 RID: 20419
			public MPEventSystem eventSystem;

			// Token: 0x04004FC4 RID: 20420
			public bool hasCursor;

			// Token: 0x04004FC5 RID: 20421
			public bool isUIFocused;
		}

		// Token: 0x02000E48 RID: 3656
		public struct CameraModeContext
		{
			// Token: 0x04004FC6 RID: 20422
			public CameraModeBase.CameraInfo cameraInfo;

			// Token: 0x04004FC7 RID: 20423
			public CameraModeBase.TargetInfo targetInfo;

			// Token: 0x04004FC8 RID: 20424
			public CameraModeBase.ViewerInfo viewerInfo;
		}

		// Token: 0x02000E49 RID: 3657
		public struct UpdateArgs
		{
			// Token: 0x04004FC9 RID: 20425
			public CameraModeBase.CameraInfo cameraInfo;

			// Token: 0x04004FCA RID: 20426
			public CameraModeBase.TargetInfo targetInfo;
		}

		// Token: 0x02000E4A RID: 3658
		public struct UpdateResult
		{
			// Token: 0x04004FCB RID: 20427
			public GameObject firstPersonTarget;

			// Token: 0x04004FCC RID: 20428
			public CameraState cameraState;

			// Token: 0x04004FCD RID: 20429
			public bool showSprintParticles;

			// Token: 0x04004FCE RID: 20430
			public Vector3 crosshairWorldPosition;
		}

		// Token: 0x02000E4B RID: 3659
		public struct CollectLookInputResult
		{
			// Token: 0x04004FCF RID: 20431
			public Vector2 lookInput;
		}

		// Token: 0x02000E4C RID: 3660
		public struct ApplyLookInputArgs
		{
			// Token: 0x04004FD0 RID: 20432
			public Vector2 lookInput;
		}

		// Token: 0x02000E4D RID: 3661
		public struct OnTargetChangedArgs
		{
			// Token: 0x04004FD1 RID: 20433
			public GameObject oldTarget;

			// Token: 0x04004FD2 RID: 20434
			public GameObject newTarget;
		}
	}
}
