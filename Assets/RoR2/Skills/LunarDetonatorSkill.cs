using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Skills
{
	// Token: 0x02000C06 RID: 3078
	public class LunarDetonatorSkill : SkillDef
	{
		// Token: 0x060045B7 RID: 17847 RVA: 0x00121AEF File Offset: 0x0011FCEF
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			LunarDetonatorSkill.lunarDetonatorPassiveAttachmentPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/LunarDetonatorPassiveAttachment");
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00121B00 File Offset: 0x0011FD00
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new LunarDetonatorSkill.InstanceData
			{
				skillSlot = skillSlot
			};
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x00121B0E File Offset: 0x0011FD0E
		public override void OnUnassigned(GenericSkill skillSlot)
		{
			((LunarDetonatorSkill.InstanceData)skillSlot.skillInstanceData).skillSlot = null;
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00121B21 File Offset: 0x0011FD21
		public override float GetRechargeInterval(GenericSkill skillSlot)
		{
			return (float)skillSlot.characterBody.inventory.GetItemCount(RoR2Content.Items.LunarSpecialReplacement) * this.baseRechargeInterval;
		}

		// Token: 0x040043D2 RID: 17362
		private static GameObject lunarDetonatorPassiveAttachmentPrefab;

		// Token: 0x02000C07 RID: 3079
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x060045BC RID: 17852 RVA: 0x00121B40 File Offset: 0x0011FD40
			// (set) Token: 0x060045BD RID: 17853 RVA: 0x00121B48 File Offset: 0x0011FD48
			public GenericSkill skillSlot
			{
				get
				{
					return this._skillSlot;
				}
				set
				{
					if (this._skillSlot == value)
					{
						return;
					}
					if (this._skillSlot != null)
					{
						this._skillSlot.characterBody.onInventoryChanged -= this.OnInventoryChanged;
					}
					if (this.lunarDetonatorPassiveAttachment)
					{
						UnityEngine.Object.Destroy(this.lunarDetonatorPassiveAttachment.gameObject);
					}
					this.lunarDetonatorPassiveAttachment = null;
					this._skillSlot = value;
					if (this._skillSlot != null)
					{
						this._skillSlot.characterBody.onInventoryChanged += this.OnInventoryChanged;
						if (NetworkServer.active && this._skillSlot.characterBody)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LunarDetonatorSkill.lunarDetonatorPassiveAttachmentPrefab);
							this.lunarDetonatorPassiveAttachment = gameObject.GetComponent<LunarDetonatorPassiveAttachment>();
							this.lunarDetonatorPassiveAttachment.monitoredSkill = this.skillSlot;
							gameObject.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(this._skillSlot.characterBody.gameObject, null);
						}
					}
				}
			}

			// Token: 0x060045BE RID: 17854 RVA: 0x00121C2F File Offset: 0x0011FE2F
			public void OnInventoryChanged()
			{
				this.skillSlot.RecalculateValues();
			}

			// Token: 0x040043D3 RID: 17363
			private GenericSkill _skillSlot;

			// Token: 0x040043D4 RID: 17364
			private LunarDetonatorPassiveAttachment lunarDetonatorPassiveAttachment;
		}
	}
}
