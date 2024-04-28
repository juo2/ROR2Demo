using System;
using EntityStates.VagrantMonster;
using RoR2;
using UnityEngine;

namespace EntityStates.SiphonItem
{
	// Token: 0x020001CB RID: 459
	public class ChargeState : BaseSiphonItemState
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00022CF0 File Offset: 0x00020EF0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeState.baseDuration / (base.attachedBody ? base.attachedBody.attackSpeed : 1f);
			base.TurnOffHealingFX();
			if (base.attachedBody)
			{
				Vector3 position = base.transform.position;
				Quaternion rotation = base.transform.rotation;
				this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaNova.chargingEffectPrefab, position, rotation);
				this.chargeVfxInstance.transform.localScale = Vector3.one * 0.25f;
				Util.PlaySound(ChargeState.chargeSound, base.gameObject);
				this.areaIndicatorVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaNova.areaIndicatorPrefab, position, rotation);
				ObjectScaleCurve component = this.areaIndicatorVfxInstance.GetComponent<ObjectScaleCurve>();
				component.timeMax = this.duration;
				component.baseScale = Vector3.one * DetonateState.baseSiphonRange * 2f;
				this.areaIndicatorVfxInstance.GetComponent<AnimateShaderAlpha>().timeMax = this.duration;
			}
			RoR2Application.onLateUpdate += this.OnLateUpdate;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00022E0C File Offset: 0x0002100C
		public override void OnExit()
		{
			RoR2Application.onLateUpdate -= this.OnLateUpdate;
			if (this.chargeVfxInstance != null)
			{
				EntityState.Destroy(this.chargeVfxInstance);
				this.chargeVfxInstance = null;
			}
			if (this.areaIndicatorVfxInstance != null)
			{
				EntityState.Destroy(this.areaIndicatorVfxInstance);
				this.areaIndicatorVfxInstance = null;
			}
			base.OnExit();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00022E64 File Offset: 0x00021064
		private void OnLateUpdate()
		{
			if (this.chargeVfxInstance)
			{
				this.chargeVfxInstance.transform.position = base.transform.position;
			}
			if (this.areaIndicatorVfxInstance)
			{
				this.areaIndicatorVfxInstance.transform.position = base.transform.position;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00022EC1 File Offset: 0x000210C1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new DetonateState());
			}
		}

		// Token: 0x040009AA RID: 2474
		public static float baseDuration = 3f;

		// Token: 0x040009AB RID: 2475
		public static string chargeSound;

		// Token: 0x040009AC RID: 2476
		private float duration;

		// Token: 0x040009AD RID: 2477
		private GameObject chargeVfxInstance;

		// Token: 0x040009AE RID: 2478
		private GameObject areaIndicatorVfxInstance;
	}
}
