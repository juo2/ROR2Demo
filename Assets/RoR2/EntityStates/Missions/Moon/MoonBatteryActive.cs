using System;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000242 RID: 578
	public class MoonBatteryActive : MoonBatteryBaseState
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002A87C File Offset: 0x00028A7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.holdoutZoneController = base.GetComponent<HoldoutZoneController>();
			this.holdoutZoneController.enabled = true;
			if (NetworkServer.active)
			{
				base.GetComponent<CombatDirector>().enabled = true;
			}
			Animator[] animators = this.animators;
			for (int i = 0; i < animators.Length; i++)
			{
				animators[i].SetTrigger(MoonBatteryActive.activeTriggerName);
			}
			Util.PlaySound(MoonBatteryActive.soundEntryEvent, base.gameObject);
			Util.PlaySound(MoonBatteryActive.soundLoopStartEvent, base.gameObject);
			base.FindModelChild("ChargingFX").gameObject.SetActive(true);
			Transform transform = base.FindModelChild("PositionIndicatorPosition").transform;
			PositionIndicator component = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PositionIndicators/PillarChargingPositionIndicator"), base.transform.position, Quaternion.identity).GetComponent<PositionIndicator>();
			component.targetTransform = transform;
			this.chargeIndicatorController = component.GetComponent<ChargeIndicatorController>();
			this.chargeIndicatorController.holdoutZoneController = this.holdoutZoneController;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002A970 File Offset: 0x00028B70
		public override void Update()
		{
			base.Update();
			if (this.holdoutZoneController)
			{
				Animator[] animators = this.animators;
				for (int i = 0; i < animators.Length; i++)
				{
					animators[i].SetFloat("Active.cycleOffset", this.holdoutZoneController.charge * 0.99f);
				}
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002A9C3 File Offset: 0x00028BC3
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.holdoutZoneController.charge >= 1f)
			{
				this.outer.SetNextState(new MoonBatteryComplete());
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002A9F8 File Offset: 0x00028BF8
		public override void OnExit()
		{
			base.FindModelChild("ChargingFX").gameObject.SetActive(false);
			if (this.holdoutZoneController)
			{
				this.holdoutZoneController.enabled = false;
			}
			Animator[] animators = this.animators;
			for (int i = 0; i < animators.Length; i++)
			{
				animators[i].SetTrigger(MoonBatteryActive.completeTriggerName);
			}
			Util.PlaySound(MoonBatteryActive.soundLoopEndEvent, base.gameObject);
			Util.PlaySound(MoonBatteryActive.soundExitEvent, base.gameObject);
			if (this.chargeIndicatorController)
			{
				EntityState.Destroy(this.chargeIndicatorController.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000BFE RID: 3070
		public static string soundEntryEvent;

		// Token: 0x04000BFF RID: 3071
		public static string soundLoopStartEvent;

		// Token: 0x04000C00 RID: 3072
		public static string soundLoopEndEvent;

		// Token: 0x04000C01 RID: 3073
		public static string soundExitEvent;

		// Token: 0x04000C02 RID: 3074
		public static string activeTriggerName;

		// Token: 0x04000C03 RID: 3075
		public static string completeTriggerName;

		// Token: 0x04000C04 RID: 3076
		private HoldoutZoneController holdoutZoneController;

		// Token: 0x04000C05 RID: 3077
		private ChargeIndicatorController chargeIndicatorController;
	}
}
