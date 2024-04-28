using System;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000354 RID: 852
	public class PortalFist : BaseState
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00041FE4 File Offset: 0x000401E4
		public override void OnEnter()
		{
			base.OnEnter();
			this.fistMeshGameObject = base.FindModelChild(PortalFist.fistMeshChildLocatorString).gameObject;
			this.fistBoneGameObject = base.FindModelChild(PortalFist.fistBoneChildLocatorString).gameObject;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.characterModel = modelTransform.GetComponent<CharacterModel>();
			}
			this.duration = PortalFist.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "PortalFist", "PortalFist.playbackRate", this.duration, 0.3f);
			Transform transform = base.FindModelChild("PortalFistTargetRig");
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.viewer = base.characterBody;
			bullseyeSearch.searchOrigin = base.characterBody.corePosition;
			bullseyeSearch.searchDirection = base.characterBody.corePosition;
			bullseyeSearch.maxDistanceFilter = PortalFist.targetSearchMaxDistance;
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
			bullseyeSearch.teamMaskFilter.RemoveTeam(TeamIndex.Neutral);
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
			bullseyeSearch.RefreshCandidates();
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				transform.position = hurtBox.transform.position;
				this.fistTargetMuzzleString = string.Format("PortalFistTargetPosition{0}", UnityEngine.Random.Range(1, 5).ToString());
				this.fistTargetGameObject = base.FindModelChild(this.fistTargetMuzzleString).gameObject;
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0004214C File Offset: 0x0004034C
		public override void Update()
		{
			base.Update();
			bool flag = this.modelAnimator.GetFloat(PortalFist.mecanimFistVisibilityString) > 0.5f;
			if (flag != this.fistWasOutOfPortal)
			{
				this.fistWasOutOfPortal = flag;
				EffectManager.SimpleMuzzleFlash(flag ? PortalFist.portalOutEffectPrefab : PortalFist.portalInEffectPrefab, base.gameObject, PortalFist.portalMuzzleString, false);
				EffectManager.SimpleMuzzleFlash(flag ? PortalFist.portalOutEffectPrefab : PortalFist.portalInEffectPrefab, base.gameObject, this.fistTargetMuzzleString, false);
				if (this.characterModel && PortalFist.fistOverlayMaterial)
				{
					TemporaryOverlay temporaryOverlay = this.characterModel.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = PortalFist.fistOverlayDuration;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = PortalFist.fistOverlayMaterial;
					temporaryOverlay.inspectorCharacterModel = this.characterModel;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
				}
			}
			if (this.fistTargetGameObject && !flag)
			{
				this.fistBoneGameObject.transform.SetPositionAndRotation(this.fistTargetGameObject.transform.position, this.fistTargetGameObject.transform.rotation);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00042282 File Offset: 0x00040482
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04001329 RID: 4905
		public static float baseDuration;

		// Token: 0x0400132A RID: 4906
		public static GameObject portalInEffectPrefab;

		// Token: 0x0400132B RID: 4907
		public static GameObject portalOutEffectPrefab;

		// Token: 0x0400132C RID: 4908
		public static string portalMuzzleString;

		// Token: 0x0400132D RID: 4909
		public static string fistMeshChildLocatorString;

		// Token: 0x0400132E RID: 4910
		public static string fistBoneChildLocatorString;

		// Token: 0x0400132F RID: 4911
		public static string mecanimFistVisibilityString;

		// Token: 0x04001330 RID: 4912
		public static float fistOverlayDuration;

		// Token: 0x04001331 RID: 4913
		public static Material fistOverlayMaterial;

		// Token: 0x04001332 RID: 4914
		public static float targetSearchMaxDistance;

		// Token: 0x04001333 RID: 4915
		private GameObject fistMeshGameObject;

		// Token: 0x04001334 RID: 4916
		private GameObject fistBoneGameObject;

		// Token: 0x04001335 RID: 4917
		private GameObject fistTargetGameObject;

		// Token: 0x04001336 RID: 4918
		private string fistTargetMuzzleString;

		// Token: 0x04001337 RID: 4919
		private Animator modelAnimator;

		// Token: 0x04001338 RID: 4920
		private CharacterModel characterModel;

		// Token: 0x04001339 RID: 4921
		private float duration;

		// Token: 0x0400133A RID: 4922
		private bool fistWasOutOfPortal = true;
	}
}
