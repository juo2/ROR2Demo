using System;
using EntityStates.VagrantMonster;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantNovaItem
{
	// Token: 0x0200016A RID: 362
	public class ChargeState : BaseVagrantNovaItemState
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0001B27C File Offset: 0x0001947C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeState.baseDuration / (base.attachedBody ? base.attachedBody.attackSpeed : 1f);
			base.SetChargeSparkEmissionRateMultiplier(1f);
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
				component.baseScale = Vector3.one * DetonateState.blastRadius * 2f;
				this.areaIndicatorVfxInstance.GetComponent<AnimateShaderAlpha>().timeMax = this.duration;
			}
			RoR2Application.onLateUpdate += this.OnLateUpdate;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001B3A0 File Offset: 0x000195A0
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

		// Token: 0x06000654 RID: 1620 RVA: 0x0001B3F8 File Offset: 0x000195F8
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

		// Token: 0x06000655 RID: 1621 RVA: 0x0001B455 File Offset: 0x00019655
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new DetonateState());
			}
		}

		// Token: 0x040007B0 RID: 1968
		public static float baseDuration = 3f;

		// Token: 0x040007B1 RID: 1969
		public static string chargeSound;

		// Token: 0x040007B2 RID: 1970
		private float duration;

		// Token: 0x040007B3 RID: 1971
		private GameObject chargeVfxInstance;

		// Token: 0x040007B4 RID: 1972
		private GameObject areaIndicatorVfxInstance;
	}
}
