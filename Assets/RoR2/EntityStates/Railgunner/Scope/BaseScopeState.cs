using System;
using RoR2;
using RoR2.HudOverlay;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000201 RID: 513
	public class BaseScopeState : BaseSkillState
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00025A4C File Offset: 0x00023C4C
		public override void OnEnter()
		{
			base.OnEnter();
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, new OverlayCreationParams
			{
				prefab = this.scopeOverlayPrefab,
				childLocatorEntry = "ScopeContainer"
			});
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.SetBool(BaseScopeState.mecanimBoolName, true);
			}
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00025AD4 File Offset: 0x00023CD4
		public override void OnExit()
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.SetBool(BaseScopeState.mecanimBoolName, false);
			}
			this.RemoveOverlay(0f);
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00025B1E File Offset: 0x00023D1E
		protected void SetScopeAlpha(float alpha)
		{
			if (this.overlayController != null)
			{
				this.overlayController.alpha = alpha;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00025B34 File Offset: 0x00023D34
		protected void RemoveOverlay(float transitionDuration)
		{
			if (this.overlayController != null)
			{
				HudOverlayManager.RemoveOverlay(this.overlayController);
				this.overlayController = null;
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00025B50 File Offset: 0x00023D50
		protected void StartScopeParamsOverride(float transitionDuration)
		{
			if (this.cameraParamsOverrideHandle.isValid)
			{
				return;
			}
			this.cameraParamsOverrideHandle = base.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
			{
				cameraParamsData = this.cameraParams.data,
				priority = this.cameraOverridePriority
			}, transitionDuration);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00025BA5 File Offset: 0x00023DA5
		protected void EndScopeParamsOverride(float transitionDuration)
		{
			if (this.cameraParamsOverrideHandle.isValid)
			{
				base.cameraTargetParams.RemoveParamsOverride(this.cameraParamsOverrideHandle, transitionDuration);
				this.cameraParamsOverrideHandle = default(CameraTargetParams.CameraParamsOverrideHandle);
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00025BD3 File Offset: 0x00023DD3
		protected virtual CharacterCameraParams GetCameraParams()
		{
			return this.cameraParams;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float GetScopeEntryDuration()
		{
			return 0f;
		}

		// Token: 0x04000A98 RID: 2712
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000A99 RID: 2713
		[SerializeField]
		public GameObject scopeOverlayPrefab;

		// Token: 0x04000A9A RID: 2714
		[SerializeField]
		public CharacterCameraParams cameraParams;

		// Token: 0x04000A9B RID: 2715
		[SerializeField]
		public float cameraOverridePriority;

		// Token: 0x04000A9C RID: 2716
		public static string mecanimBoolName;

		// Token: 0x04000A9D RID: 2717
		private OverlayController overlayController;

		// Token: 0x04000A9E RID: 2718
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000A9F RID: 2719
		private CameraTargetParams.CameraParamsOverrideHandle cameraParamsOverrideHandle;
	}
}
