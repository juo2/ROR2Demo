using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LockedMage
{
	// Token: 0x020002BD RID: 701
	public class UnlockingMage : BaseState
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000C6B RID: 3179 RVA: 0x00034504 File Offset: 0x00032704
		// (remove) Token: 0x06000C6C RID: 3180 RVA: 0x00034538 File Offset: 0x00032738
		public static event Action<Interactor> onOpened;

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003456C File Offset: 0x0003276C
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleEffect(UnlockingMage.unlockingMageChargeEffectPrefab, base.transform.position, Util.QuaternionSafeLookRotation(Vector3.up), false);
			Util.PlayAttackSpeedSound(UnlockingMage.unlockingChargeSFXString, base.gameObject, UnlockingMage.unlockingChargeSFXStringPitch);
			base.GetModelTransform().GetComponent<ChildLocator>().FindChild("Suspension").gameObject.SetActive(false);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000345D8 File Offset: 0x000327D8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= UnlockingMage.unlockingDuration && !this.unlocked)
			{
				base.gameObject.SetActive(false);
				EffectManager.SimpleEffect(UnlockingMage.unlockingMageExplosionEffectPrefab, base.transform.position, Util.QuaternionSafeLookRotation(Vector3.up), false);
				Util.PlayAttackSpeedSound(UnlockingMage.unlockingExplosionSFXString, base.gameObject, UnlockingMage.unlockingExplosionSFXStringPitch);
				this.unlocked = true;
				if (NetworkServer.active)
				{
					Action<Interactor> action = UnlockingMage.onOpened;
					if (action == null)
					{
						return;
					}
					action(base.GetComponent<PurchaseInteraction>().lastActivator);
				}
			}
		}

		// Token: 0x04000F28 RID: 3880
		public static GameObject unlockingMageChargeEffectPrefab;

		// Token: 0x04000F29 RID: 3881
		public static GameObject unlockingMageExplosionEffectPrefab;

		// Token: 0x04000F2A RID: 3882
		public static float unlockingDuration;

		// Token: 0x04000F2B RID: 3883
		public static string unlockingChargeSFXString;

		// Token: 0x04000F2C RID: 3884
		public static float unlockingChargeSFXStringPitch;

		// Token: 0x04000F2D RID: 3885
		public static string unlockingExplosionSFXString;

		// Token: 0x04000F2E RID: 3886
		public static float unlockingExplosionSFXStringPitch;

		// Token: 0x04000F30 RID: 3888
		private bool unlocked;
	}
}
