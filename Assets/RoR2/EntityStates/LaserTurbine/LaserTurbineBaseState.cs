using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002D9 RID: 729
	public class LaserTurbineBaseState : EntityState
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00036996 File Offset: 0x00034B96
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x0003699E File Offset: 0x00034B9E
		private protected LaserTurbineController laserTurbineController { protected get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000369A7 File Offset: 0x00034BA7
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x000369AF File Offset: 0x00034BAF
		private protected SimpleRotateToDirection simpleRotateToDirection { protected get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000369B8 File Offset: 0x00034BB8
		protected CharacterBody ownerBody
		{
			get
			{
				GenericOwnership genericOwnership = this.genericOwnership;
				return this.bodyGetComponent.Get((genericOwnership != null) ? genericOwnership.ownerObject : null);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000369D7 File Offset: 0x00034BD7
		public override void OnEnter()
		{
			base.OnEnter();
			this.genericOwnership = base.GetComponent<GenericOwnership>();
			this.simpleLeash = base.GetComponent<SimpleLeash>();
			this.simpleRotateToDirection = base.GetComponent<SimpleRotateToDirection>();
			this.laserTurbineController = base.GetComponent<LaserTurbineController>();
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00036A0F File Offset: 0x00034C0F
		protected InputBankTest GetInputBank()
		{
			CharacterBody ownerBody = this.ownerBody;
			if (ownerBody == null)
			{
				return null;
			}
			return ownerBody.inputBank;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00036A22 File Offset: 0x00034C22
		protected Ray GetAimRay()
		{
			return new Ray(base.transform.position, base.transform.forward);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00036A3F File Offset: 0x00034C3F
		protected Transform GetMuzzleTransform()
		{
			return base.transform;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool shouldFollow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00036A48 File Offset: 0x00034C48
		public override void Update()
		{
			base.Update();
			if (this.ownerBody && this.shouldFollow)
			{
				this.simpleLeash.leashOrigin = this.ownerBody.corePosition;
				this.simpleRotateToDirection.targetRotation = Quaternion.LookRotation(this.ownerBody.inputBank.aimDirection);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00036AA8 File Offset: 0x00034CA8
		protected float GetDamage()
		{
			float num = 1f;
			if (this.ownerBody)
			{
				num = this.ownerBody.damage;
				if (this.ownerBody.inventory)
				{
					num *= (float)this.ownerBody.inventory.GetItemCount(RoR2Content.Items.LaserTurbine);
				}
			}
			return num;
		}

		// Token: 0x04000FDE RID: 4062
		private GenericOwnership genericOwnership;

		// Token: 0x04000FDF RID: 4063
		private SimpleLeash simpleLeash;

		// Token: 0x04000FE1 RID: 4065
		private MemoizedGetComponent<CharacterBody> bodyGetComponent;
	}
}
