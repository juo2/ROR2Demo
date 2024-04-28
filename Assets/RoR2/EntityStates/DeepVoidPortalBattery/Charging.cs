using System;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.DeepVoidPortalBattery
{
	// Token: 0x020003D6 RID: 982
	public class Charging : BaseDeepVoidPortalBatteryState
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x0004D594 File Offset: 0x0004B794
		public override void OnEnter()
		{
			base.OnEnter();
			this.holdoutZoneController = base.GetComponent<HoldoutZoneController>();
			if (this.holdoutZoneController)
			{
				this.holdoutZoneController.enabled = true;
			}
			Transform transform = base.FindModelChild("PositionIndicatorPosition").transform;
			PositionIndicator component = UnityEngine.Object.Instantiate<GameObject>(this.chargingPositionIndicator, base.transform.position, Quaternion.identity).GetComponent<PositionIndicator>();
			component.targetTransform = transform;
			this.chargeIndicatorController = component.GetComponent<ChargeIndicatorController>();
			this.chargeIndicatorController.holdoutZoneController = this.holdoutZoneController;
			if (NetworkServer.active)
			{
				this.combatDirector = base.GetComponent<CombatDirector>();
				if (this.combatDirector)
				{
					this.combatDirector.enabled = true;
					this.combatDirector.SetNextSpawnAsBoss();
				}
				if (this.holdoutZoneController && VoidStageMissionController.instance)
				{
					this.fogRequest = VoidStageMissionController.instance.RequestFog(this.holdoutZoneController);
				}
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004D688 File Offset: 0x0004B888
		public override void OnExit()
		{
			if (this.chargeIndicatorController)
			{
				EntityState.Destroy(this.chargeIndicatorController.gameObject);
			}
			if (this.holdoutZoneController)
			{
				this.holdoutZoneController.enabled = false;
			}
			if (NetworkServer.active)
			{
				if (this.combatDirector)
				{
					this.combatDirector.enabled = false;
				}
				VoidStageMissionController.FogRequest fogRequest = this.fogRequest;
				if (fogRequest != null)
				{
					fogRequest.Dispose();
				}
				this.fogRequest = null;
			}
			base.OnExit();
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004D709 File Offset: 0x0004B909
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.holdoutZoneController.charge >= 1f)
			{
				this.outer.SetNextState(new Charged());
			}
		}

		// Token: 0x04001642 RID: 5698
		[SerializeField]
		public GameObject chargingPositionIndicator;

		// Token: 0x04001643 RID: 5699
		private CombatDirector combatDirector;

		// Token: 0x04001644 RID: 5700
		private HoldoutZoneController holdoutZoneController;

		// Token: 0x04001645 RID: 5701
		private VoidStageMissionController.FogRequest fogRequest;

		// Token: 0x04001646 RID: 5702
		private ChargeIndicatorController chargeIndicatorController;
	}
}
