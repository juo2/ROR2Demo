using System;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000425 RID: 1061
	public class SetupSupplyDrop : BaseState
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00054E49 File Offset: 0x00053049
		private float exitDuration
		{
			get
			{
				return SetupSupplyDrop.baseExitDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00054E58 File Offset: 0x00053058
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.PlayAnimation("Gesture, Override", "PrepSupplyDrop");
			this.PlayAnimation("Gesture, Additive", "PrepSupplyDrop");
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("PrepSupplyDrop", true);
			}
			Transform transform = base.FindModelChild(SetupSupplyDrop.effectMuzzleString);
			if (transform)
			{
				this.effectMuzzleInstance = UnityEngine.Object.Instantiate<GameObject>(SetupSupplyDrop.effectMuzzlePrefab, transform);
			}
			if (SetupSupplyDrop.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, SetupSupplyDrop.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			Util.PlaySound(SetupSupplyDrop.enterSoundString, base.gameObject);
			this.blueprints = UnityEngine.Object.Instantiate<GameObject>(SetupSupplyDrop.blueprintPrefab, this.currentPlacementInfo.position, this.currentPlacementInfo.rotation).GetComponent<BlueprintController>();
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			this.originalPrimarySkill = base.skillLocator.primary;
			this.originalSecondarySkill = base.skillLocator.secondary;
			base.skillLocator.primary = base.skillLocator.FindSkill("SupplyDrop1");
			base.skillLocator.secondary = base.skillLocator.FindSkill("SupplyDrop2");
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00054FB0 File Offset: 0x000531B0
		public override void Update()
		{
			base.Update();
			this.currentPlacementInfo = SetupSupplyDrop.GetPlacementInfo(base.GetAimRay(), base.gameObject);
			if (this.blueprints)
			{
				this.blueprints.PushState(this.currentPlacementInfo.position, this.currentPlacementInfo.rotation, this.currentPlacementInfo.ok);
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00055014 File Offset: 0x00053214
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (base.isAuthority && this.beginExit)
			{
				this.timerSinceComplete += Time.fixedDeltaTime;
				if (this.timerSinceComplete > this.exitDuration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00055088 File Offset: 0x00053288
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				Util.PlaySound(SetupSupplyDrop.exitSoundString, base.gameObject);
			}
			if (this.effectMuzzleInstance)
			{
				EntityState.Destroy(this.effectMuzzleInstance);
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.skillLocator.primary = this.originalPrimarySkill;
			base.skillLocator.secondary = this.originalSecondarySkill;
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("PrepSupplyDrop", false);
			}
			if (this.blueprints)
			{
				EntityState.Destroy(this.blueprints.gameObject);
				this.blueprints = null;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00055158 File Offset: 0x00053358
		public static SetupSupplyDrop.PlacementInfo GetPlacementInfo(Ray aimRay, GameObject gameObject)
		{
			float num = 0f;
			CameraRigController.ModifyAimRayIfApplicable(aimRay, gameObject, out num);
			Vector3 vector = -aimRay.direction;
			Vector3 vector2 = Vector3.up;
			Vector3 lhs = Vector3.Cross(vector2, vector);
			SetupSupplyDrop.PlacementInfo result = default(SetupSupplyDrop.PlacementInfo);
			result.ok = false;
			RaycastHit raycastHit;
			if (Physics.Raycast(aimRay, out raycastHit, SetupSupplyDrop.maxPlacementDistance, LayerIndex.world.mask) && raycastHit.normal.y > SetupSupplyDrop.normalYThreshold)
			{
				vector2 = raycastHit.normal;
				vector = Vector3.Cross(lhs, vector2);
				result.ok = true;
			}
			result.rotation = Util.QuaternionSafeLookRotation(vector, vector2);
			Vector3 point = raycastHit.point;
			result.position = point;
			return result;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001871 RID: 6257
		public static GameObject crosshairOverridePrefab;

		// Token: 0x04001872 RID: 6258
		public static string enterSoundString;

		// Token: 0x04001873 RID: 6259
		public static string exitSoundString;

		// Token: 0x04001874 RID: 6260
		public static GameObject effectMuzzlePrefab;

		// Token: 0x04001875 RID: 6261
		public static string effectMuzzleString;

		// Token: 0x04001876 RID: 6262
		public static float baseExitDuration;

		// Token: 0x04001877 RID: 6263
		public static float maxPlacementDistance;

		// Token: 0x04001878 RID: 6264
		public static GameObject blueprintPrefab;

		// Token: 0x04001879 RID: 6265
		public static float normalYThreshold;

		// Token: 0x0400187A RID: 6266
		private SetupSupplyDrop.PlacementInfo currentPlacementInfo;

		// Token: 0x0400187B RID: 6267
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x0400187C RID: 6268
		private GenericSkill primarySkillSlot;

		// Token: 0x0400187D RID: 6269
		private AimAnimator modelAimAnimator;

		// Token: 0x0400187E RID: 6270
		private GameObject effectMuzzleInstance;

		// Token: 0x0400187F RID: 6271
		private Animator modelAnimator;

		// Token: 0x04001880 RID: 6272
		private float timerSinceComplete;

		// Token: 0x04001881 RID: 6273
		private bool beginExit;

		// Token: 0x04001882 RID: 6274
		private GenericSkill originalPrimarySkill;

		// Token: 0x04001883 RID: 6275
		private GenericSkill originalSecondarySkill;

		// Token: 0x04001884 RID: 6276
		private BlueprintController blueprints;

		// Token: 0x04001885 RID: 6277
		private CameraTargetParams.AimRequest aimRequest;

		// Token: 0x02000426 RID: 1062
		public struct PlacementInfo
		{
			// Token: 0x0600131C RID: 4892 RVA: 0x0005520F File Offset: 0x0005340F
			public void Serialize(NetworkWriter writer)
			{
				writer.Write(this.ok);
				writer.Write(this.position);
				writer.Write(this.rotation);
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x00055235 File Offset: 0x00053435
			public void Deserialize(NetworkReader reader)
			{
				this.ok = reader.ReadBoolean();
				this.position = reader.ReadVector3();
				this.rotation = reader.ReadQuaternion();
			}

			// Token: 0x04001886 RID: 6278
			public bool ok;

			// Token: 0x04001887 RID: 6279
			public Vector3 position;

			// Token: 0x04001888 RID: 6280
			public Quaternion rotation;
		}
	}
}
