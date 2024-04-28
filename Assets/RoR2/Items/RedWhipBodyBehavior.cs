using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE3 RID: 3043
	internal class RedWhipBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x0600450C RID: 17676 RVA: 0x0011F861 File Offset: 0x0011DA61
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.SprintOutOfCombat;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x0011F868 File Offset: 0x0011DA68
		private void SetProvidingBuff(bool shouldProvideBuff)
		{
			if (shouldProvideBuff == this.providingBuff)
			{
				return;
			}
			this.providingBuff = shouldProvideBuff;
			if (this.providingBuff)
			{
				base.body.AddBuff(RoR2Content.Buffs.WhipBoost);
				EffectData effectData = new EffectData();
				effectData.origin = base.body.corePosition;
				CharacterDirection characterDirection = base.body.characterDirection;
				bool flag = false;
				if (characterDirection && characterDirection.moveVector != Vector3.zero)
				{
					effectData.rotation = Util.QuaternionSafeLookRotation(characterDirection.moveVector);
					flag = true;
				}
				if (!flag)
				{
					effectData.rotation = base.body.transform.rotation;
				}
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/SprintActivate"), effectData, true);
				return;
			}
			base.body.RemoveBuff(RoR2Content.Buffs.WhipBoost);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x0011F92E File Offset: 0x0011DB2E
		private void OnDisable()
		{
			this.SetProvidingBuff(false);
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x0011F937 File Offset: 0x0011DB37
		private void FixedUpdate()
		{
			this.SetProvidingBuff(base.body.outOfCombat);
		}

		// Token: 0x04004372 RID: 17266
		private bool providingBuff;
	}
}
