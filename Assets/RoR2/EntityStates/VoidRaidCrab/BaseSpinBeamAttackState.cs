using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000120 RID: 288
	public class BaseSpinBeamAttackState : BaseState
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00015D02 File Offset: 0x00013F02
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00015D0A File Offset: 0x00013F0A
		private protected float duration { protected get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00015D13 File Offset: 0x00013F13
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00015D1B File Offset: 0x00013F1B
		private protected Animator modelAnimator { protected get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00015D24 File Offset: 0x00013F24
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00015D2C File Offset: 0x00013F2C
		private protected GameObject beamVfxInstance { protected get; private set; }

		// Token: 0x06000516 RID: 1302 RVA: 0x00015D38 File Offset: 0x00013F38
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.headTransform = base.FindModelChild(BaseSpinBeamAttackState.headTransformNameInChildLocator);
			this.muzzleTransform = base.FindModelChild(BaseSpinBeamAttackState.muzzleTransformNameInChildLocator);
			this.duration = this.baseDuration;
			if (!string.IsNullOrEmpty(this.animLayerName) && !string.IsNullOrEmpty(this.animStateName))
			{
				if (!string.IsNullOrEmpty(this.animPlaybackRateParamName))
				{
					base.PlayAnimation(this.animLayerName, this.animStateName, this.animPlaybackRateParamName, this.duration);
				}
				else
				{
					this.PlayAnimation(this.animLayerName, this.animStateName);
				}
			}
			if (this.modelAnimator)
			{
				this.modelAnimator.GetComponent<AimAnimator>().enabled = true;
			}
			this.hasHeadSpinOwnership = true;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00015E04 File Offset: 0x00014004
		public override void ModifyNextState(EntityState nextState)
		{
			base.ModifyNextState(nextState);
			BaseSpinBeamAttackState baseSpinBeamAttackState;
			if ((baseSpinBeamAttackState = (nextState as BaseSpinBeamAttackState)) != null)
			{
				baseSpinBeamAttackState.hasHeadSpinOwnership |= this.hasHeadSpinOwnership;
				this.hasHeadSpinOwnership = false;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015E3C File Offset: 0x0001403C
		public override void OnExit()
		{
			if (this.hasHeadSpinOwnership)
			{
				this.SetHeadYawRevolutions(0f);
				this.hasHeadSpinOwnership = true;
			}
			this.DestroyBeamVFXInstance();
			base.OnExit();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015E64 File Offset: 0x00014064
		protected void SetHeadYawRevolutions(float newRevolutions)
		{
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(BaseSpinBeamAttackState.headYawParamName, (0.5f + newRevolutions) % 1f);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00015E90 File Offset: 0x00014090
		protected Ray GetBeamRay()
		{
			Vector3 forward = this.muzzleTransform.forward;
			Vector3 forward2 = this.headTransform.forward;
			forward2.y = this.headForwardYCurve.Evaluate(base.fixedAge / this.duration);
			forward2.Normalize();
			return new Ray(this.muzzleTransform.position, forward2);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00015EEC File Offset: 0x000140EC
		protected float normalizedFixedAge
		{
			get
			{
				return Mathf.Clamp01(base.fixedAge / this.duration);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00015F00 File Offset: 0x00014100
		protected void CreateBeamVFXInstance(GameObject beamVfxPrefab)
		{
			if (this.beamVfxInstance != null)
			{
				throw new InvalidOperationException();
			}
			this.beamVfxInstance = UnityEngine.Object.Instantiate<GameObject>(beamVfxPrefab);
			this.beamVfxInstance.transform.SetParent(this.headTransform, true);
			this.UpdateBeamTransforms();
			RoR2Application.onLateUpdate += this.UpdateBeamTransformsInLateUpdate;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00015F55 File Offset: 0x00014155
		protected void DestroyBeamVFXInstance()
		{
			if (this.beamVfxInstance != null)
			{
				RoR2Application.onLateUpdate -= this.UpdateBeamTransformsInLateUpdate;
				VfxKillBehavior.KillVfxObject(this.beamVfxInstance);
				this.beamVfxInstance = null;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015F84 File Offset: 0x00014184
		private void UpdateBeamTransformsInLateUpdate()
		{
			try
			{
				this.UpdateBeamTransforms();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00015FB0 File Offset: 0x000141B0
		private void UpdateBeamTransforms()
		{
			Ray beamRay = this.GetBeamRay();
			this.beamVfxInstance.transform.SetPositionAndRotation(beamRay.origin, Quaternion.LookRotation(beamRay.direction));
		}

		// Token: 0x040005F4 RID: 1524
		[SerializeField]
		public string animLayerName;

		// Token: 0x040005F5 RID: 1525
		[SerializeField]
		public string animStateName;

		// Token: 0x040005F6 RID: 1526
		[SerializeField]
		public string animPlaybackRateParamName;

		// Token: 0x040005F7 RID: 1527
		[SerializeField]
		public float baseDuration;

		// Token: 0x040005F9 RID: 1529
		public static string headTransformNameInChildLocator;

		// Token: 0x040005FA RID: 1530
		public static string muzzleTransformNameInChildLocator;

		// Token: 0x040005FB RID: 1531
		public static string headYawParamName;

		// Token: 0x040005FC RID: 1532
		[SerializeField]
		public AnimationCurve headForwardYCurve;

		// Token: 0x040005FD RID: 1533
		private Transform headTransform;

		// Token: 0x040005FE RID: 1534
		private Transform muzzleTransform;

		// Token: 0x04000600 RID: 1536
		private bool hasHeadSpinOwnership;
	}
}
