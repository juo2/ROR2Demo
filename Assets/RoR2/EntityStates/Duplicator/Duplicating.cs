using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Duplicator
{
	// Token: 0x020003BD RID: 957
	public class Duplicating : BaseState
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x0004B0D0 File Offset: 0x000492D0
		public override void OnEnter()
		{
			base.OnEnter();
			ChildLocator component = base.GetModelTransform().GetComponent<ChildLocator>();
			if (component)
			{
				this.muzzleTransform = component.FindChild(Duplicating.muzzleString);
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004B108 File Offset: 0x00049308
		private void BeginCooking()
		{
			if (this.hasStartedCooking)
			{
				return;
			}
			this.hasStartedCooking = true;
			this.PlayAnimation("Body", "Cook");
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
			if (this.muzzleTransform)
			{
				this.bakeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(Duplicating.bakeEffectPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004B18C File Offset: 0x0004938C
		private void DropDroplet()
		{
			if (this.hasDroppedDroplet)
			{
				return;
			}
			this.hasDroppedDroplet = true;
			base.GetComponent<ShopTerminalBehavior>().DropPickup();
			if (this.muzzleTransform)
			{
				if (this.bakeEffectInstance)
				{
					EntityState.Destroy(this.bakeEffectInstance);
				}
				if (Duplicating.releaseEffectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(Duplicating.releaseEffectPrefab, base.gameObject, Duplicating.muzzleString, false);
				}
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0004B1FB File Offset: 0x000493FB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Duplicating.initialDelayDuration)
			{
				this.BeginCooking();
			}
			if (base.fixedAge >= Duplicating.initialDelayDuration + Duplicating.timeBetweenStartAndDropDroplet)
			{
				this.DropDroplet();
			}
		}

		// Token: 0x0400158E RID: 5518
		public static float initialDelayDuration = 1f;

		// Token: 0x0400158F RID: 5519
		public static float timeBetweenStartAndDropDroplet = 3f;

		// Token: 0x04001590 RID: 5520
		public static string muzzleString;

		// Token: 0x04001591 RID: 5521
		public static GameObject bakeEffectPrefab;

		// Token: 0x04001592 RID: 5522
		public static GameObject releaseEffectPrefab;

		// Token: 0x04001593 RID: 5523
		private GameObject bakeEffectInstance;

		// Token: 0x04001594 RID: 5524
		private bool hasStartedCooking;

		// Token: 0x04001595 RID: 5525
		private bool hasDroppedDroplet;

		// Token: 0x04001596 RID: 5526
		private Transform muzzleTransform;
	}
}
