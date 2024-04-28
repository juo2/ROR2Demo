using System;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Sniper.Scope
{
	// Token: 0x020001C7 RID: 455
	public class ScopeSniper : BaseState
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0002297C File Offset: 0x00020B7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.charge = 0f;
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Slow50);
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.FirstPerson);
				base.cameraTargetParams.fovOverride = 20f;
			}
			if (ScopeSniper.crosshairPrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, ScopeSniper.crosshairPrefab, CrosshairUtils.OverridePriority.Skill);
			}
			this.laserPointerObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/LaserPointerBeamEnd"));
			this.laserPointerObject.GetComponent<LaserPointerController>().source = base.inputBank;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00022A3C File Offset: 0x00020C3C
		public override void OnExit()
		{
			EntityState.Destroy(this.laserPointerObject);
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.Slow50);
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = -1f;
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00022AC0 File Offset: 0x00020CC0
		public override void FixedUpdate()
		{
			this.charge = Mathf.Min(this.charge + this.attackSpeedStat / ScopeSniper.baseChargeDuration * Time.fixedDeltaTime, 1f);
			if (base.isAuthority && (!base.inputBank || !base.inputBank.skill2.down))
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400099E RID: 2462
		public static float baseChargeDuration = 4f;

		// Token: 0x0400099F RID: 2463
		public static GameObject crosshairPrefab;

		// Token: 0x040009A0 RID: 2464
		public float charge;

		// Token: 0x040009A1 RID: 2465
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x040009A2 RID: 2466
		private GameObject laserPointerObject;

		// Token: 0x040009A3 RID: 2467
		private CameraTargetParams.AimRequest aimRequest;
	}
}
