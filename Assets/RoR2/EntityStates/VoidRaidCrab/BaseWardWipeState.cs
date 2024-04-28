using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000112 RID: 274
	public class BaseWardWipeState : BaseState
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00014A04 File Offset: 0x00012C04
		public override void ModifyNextState(EntityState nextState)
		{
			base.ModifyNextState(nextState);
			BaseWardWipeState baseWardWipeState = nextState as BaseWardWipeState;
			if (baseWardWipeState != null)
			{
				baseWardWipeState.fogDamageController = this.fogDamageController;
				baseWardWipeState.safeWards = this.safeWards;
				return;
			}
			if (this.fogDamageController)
			{
				this.fogDamageController.enabled = false;
			}
			if (this.safeWards != null && NetworkServer.active)
			{
				foreach (GameObject gameObject in this.safeWards)
				{
					if (this.fogDamageController)
					{
						IZone component = gameObject.GetComponent<IZone>();
						this.fogDamageController.RemoveSafeZone(component);
					}
					if (this.safeWardDisappearEffectPrefab)
					{
						EffectData effectData = new EffectData();
						effectData.origin = gameObject.transform.position;
						EffectManager.SpawnEffect(this.safeWardDisappearEffectPrefab, effectData, true);
					}
					EntityState.Destroy(gameObject);
				}
			}
		}

		// Token: 0x04000579 RID: 1401
		[SerializeField]
		public GameObject safeWardDisappearEffectPrefab;

		// Token: 0x0400057A RID: 1402
		protected List<GameObject> safeWards = new List<GameObject>();

		// Token: 0x0400057B RID: 1403
		protected FogDamageController fogDamageController;
	}
}
