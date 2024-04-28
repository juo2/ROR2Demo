using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2.Items
{
	// Token: 0x02000BCB RID: 3019
	public class ImmuneToDebuffBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044A5 RID: 17573 RVA: 0x0011DB7C File Offset: 0x0011BD7C
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return DLC1Content.Items.ImmuneToDebuff;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x0011DB84 File Offset: 0x0011BD84
		public static bool OverrideDebuff(BuffIndex buffIndex, CharacterBody body)
		{
			BuffDef buffDef = BuffCatalog.GetBuffDef(buffIndex);
			return buffDef && ImmuneToDebuffBehavior.OverrideDebuff(buffDef, body);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x0011DBA9 File Offset: 0x0011BDA9
		public static bool OverrideDebuff(BuffDef buffDef, CharacterBody body)
		{
			return buffDef.buffIndex != BuffIndex.None && buffDef.isDebuff && ImmuneToDebuffBehavior.TryApplyOverride(body);
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		public static bool OverrideDot(InflictDotInfo inflictDotInfo)
		{
			GameObject victimObject = inflictDotInfo.victimObject;
			CharacterBody characterBody = (victimObject != null) ? victimObject.GetComponent<CharacterBody>() : null;
			return characterBody && ImmuneToDebuffBehavior.TryApplyOverride(characterBody);
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x0011DBF4 File Offset: 0x0011BDF4
		private static bool TryApplyOverride(CharacterBody body)
		{
			ImmuneToDebuffBehavior component = body.GetComponent<ImmuneToDebuffBehavior>();
			if (component)
			{
				if (component.isProtected)
				{
					return true;
				}
				if (body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffReady) && component.healthComponent)
				{
					component.healthComponent.AddBarrier(0.1f * component.healthComponent.fullCombinedHealth);
					body.RemoveBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
					EffectManager.SimpleImpactEffect(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ImmuneToDebuff/ImmuneToDebuffEffect.prefab").WaitForCompletion(), body.corePosition, Vector3.up, true);
					if (!body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffReady))
					{
						body.AddTimedBuff(DLC1Content.Buffs.ImmuneToDebuffCooldown, 5f);
					}
					component.isProtected = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x0011DCA8 File Offset: 0x0011BEA8
		private void OnEnable()
		{
			this.healthComponent = base.GetComponent<HealthComponent>();
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0011DCB8 File Offset: 0x0011BEB8
		private void OnDisable()
		{
			this.healthComponent = null;
			if (base.body)
			{
				while (base.body.GetBuffCount(DLC1Content.Buffs.ImmuneToDebuffReady) > 0)
				{
					base.body.RemoveBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
				}
				if (base.body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffCooldown))
				{
					base.body.RemoveBuff(DLC1Content.Buffs.ImmuneToDebuffCooldown);
				}
			}
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x0011DD20 File Offset: 0x0011BF20
		private void FixedUpdate()
		{
			this.isProtected = false;
			bool flag = base.body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffCooldown);
			bool flag2 = base.body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
			if (!flag && !flag2)
			{
				for (int i = 0; i < this.stack; i++)
				{
					base.body.AddBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
				}
			}
			if (flag2 && flag)
			{
				base.body.RemoveBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
			}
		}

		// Token: 0x0400432A RID: 17194
		public const float barrierFraction = 0.1f;

		// Token: 0x0400432B RID: 17195
		public const float cooldownSeconds = 5f;

		// Token: 0x0400432C RID: 17196
		private HealthComponent healthComponent;

		// Token: 0x0400432D RID: 17197
		private bool isProtected;
	}
}
