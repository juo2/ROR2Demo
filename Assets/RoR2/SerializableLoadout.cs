using System;
using System.Collections.Generic;
using RoR2.Skills;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A37 RID: 2615
	[Serializable]
	public class SerializableLoadout
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000FA223 File Offset: 0x000F8423
		public bool isEmpty
		{
			get
			{
				return this.bodyLoadouts.Length == 0;
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000FA22F File Offset: 0x000F842F
		public void Apply(Loadout loadout)
		{
			if (!SerializableLoadout.loadoutSystemReady)
			{
				throw new InvalidOperationException("Loadout system is not yet initialized.");
			}
			if (this.loadoutBuilder == null)
			{
				this.loadoutBuilder = new SerializableLoadout.LoadoutBuilder(this);
			}
			this.loadoutBuilder.Apply(loadout);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000FA263 File Offset: 0x000F8463
		[SystemInitializer(new Type[]
		{
			typeof(Loadout)
		})]
		private static void Init()
		{
			SerializableLoadout.loadoutSystemReady = true;
		}

		// Token: 0x04003BBE RID: 15294
		public SerializableLoadout.BodyLoadout[] bodyLoadouts = Array.Empty<SerializableLoadout.BodyLoadout>();

		// Token: 0x04003BBF RID: 15295
		private SerializableLoadout.LoadoutBuilder loadoutBuilder;

		// Token: 0x04003BC0 RID: 15296
		private static bool loadoutSystemReady;

		// Token: 0x02000A38 RID: 2616
		[Serializable]
		public struct BodyLoadout
		{
			// Token: 0x04003BC1 RID: 15297
			public CharacterBody body;

			// Token: 0x04003BC2 RID: 15298
			public SerializableLoadout.BodyLoadout.SkillChoice[] skillChoices;

			// Token: 0x04003BC3 RID: 15299
			public SkinDef skinChoice;

			// Token: 0x02000A39 RID: 2617
			[Serializable]
			public struct SkillChoice
			{
				// Token: 0x04003BC4 RID: 15300
				public SkillFamily skillFamily;

				// Token: 0x04003BC5 RID: 15301
				public SkillDef variant;
			}
		}

		// Token: 0x02000A3A RID: 2618
		private class LoadoutBuilder
		{
			// Token: 0x06003C70 RID: 15472 RVA: 0x000FA280 File Offset: 0x000F8480
			public LoadoutBuilder(SerializableLoadout serializedLoadout)
			{
				SerializableLoadout.BodyLoadout[] bodyLoadouts = serializedLoadout.bodyLoadouts;
				List<SerializableLoadout.LoadoutBuilder.SkillSetter> list = new List<SerializableLoadout.LoadoutBuilder.SkillSetter>(8);
				List<SerializableLoadout.LoadoutBuilder.SkinSetter> list2 = new List<SerializableLoadout.LoadoutBuilder.SkinSetter>(bodyLoadouts.Length);
				for (int i = 0; i < bodyLoadouts.Length; i++)
				{
					ref SerializableLoadout.BodyLoadout ptr = ref bodyLoadouts[i];
					CharacterBody body = ptr.body;
					if (body)
					{
						BodyIndex bodyIndex = body.bodyIndex;
						GenericSkill[] bodyPrefabSkillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndex);
						SerializableLoadout.BodyLoadout.SkillChoice[] skillChoices = ptr.skillChoices;
						for (int j = 0; j < skillChoices.Length; j++)
						{
							ref SerializableLoadout.BodyLoadout.SkillChoice ptr2 = ref skillChoices[j];
							int num = SerializableLoadout.LoadoutBuilder.FindSkillSlotIndex(bodyPrefabSkillSlots, ptr2.skillFamily);
							int num2 = SerializableLoadout.LoadoutBuilder.FindSkillVariantIndex(ptr2.skillFamily, ptr2.variant);
							if (num != -1 && num2 != -1)
							{
								list.Add(new SerializableLoadout.LoadoutBuilder.SkillSetter(bodyIndex, num, (uint)num2));
							}
						}
						int num3 = Array.IndexOf<SkinDef>(BodyCatalog.GetBodySkins(bodyIndex), ptr.skinChoice);
						if (num3 != -1)
						{
							list2.Add(new SerializableLoadout.LoadoutBuilder.SkinSetter(bodyIndex, (uint)num3));
						}
					}
				}
				this.skillSetters = list.ToArray();
				this.skinSetters = list2.ToArray();
			}

			// Token: 0x06003C71 RID: 15473 RVA: 0x000FA398 File Offset: 0x000F8598
			public void Apply(Loadout loadout)
			{
				for (int i = 0; i < this.skillSetters.Length; i++)
				{
					ref SerializableLoadout.LoadoutBuilder.SkillSetter ptr = ref this.skillSetters[i];
					loadout.bodyLoadoutManager.SetSkillVariant(ptr.bodyIndex, ptr.skillSlotIndex, ptr.skillVariantIndex);
				}
				for (int j = 0; j < this.skinSetters.Length; j++)
				{
					ref SerializableLoadout.LoadoutBuilder.SkinSetter ptr2 = ref this.skinSetters[j];
					loadout.bodyLoadoutManager.SetSkinIndex(ptr2.bodyIndex, ptr2.skinIndex);
				}
			}

			// Token: 0x06003C72 RID: 15474 RVA: 0x000FA41C File Offset: 0x000F861C
			private static int FindSkillSlotIndex(GenericSkill[] skillSlots, SkillFamily skillFamily)
			{
				for (int i = 0; i < skillSlots.Length; i++)
				{
					if (skillSlots[i].skillFamily == skillFamily)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06003C73 RID: 15475 RVA: 0x000FA448 File Offset: 0x000F8648
			private static int FindSkillVariantIndex(SkillFamily skillFamily, SkillDef skillDef)
			{
				if (skillFamily)
				{
					if (skillFamily.variants != null)
					{
						for (int i = 0; i < skillFamily.variants.Length; i++)
						{
							if (skillFamily.variants[i].skillDef == skillDef)
							{
								return i;
							}
						}
					}
					else
					{
						Debug.LogError("SkillFamily " + skillFamily.name + " has null skill variants");
					}
				}
				else
				{
					Debug.LogError("FindSkillVariantIndex: SkillFamily is null");
				}
				return -1;
			}

			// Token: 0x04003BC6 RID: 15302
			private readonly SerializableLoadout.LoadoutBuilder.SkillSetter[] skillSetters;

			// Token: 0x04003BC7 RID: 15303
			private readonly SerializableLoadout.LoadoutBuilder.SkinSetter[] skinSetters;

			// Token: 0x02000A3B RID: 2619
			private struct SkillSetter
			{
				// Token: 0x06003C74 RID: 15476 RVA: 0x000FA4B7 File Offset: 0x000F86B7
				public SkillSetter(BodyIndex bodyIndex, int skillSlotIndex, uint skillVariantIndex)
				{
					this.bodyIndex = bodyIndex;
					this.skillSlotIndex = skillSlotIndex;
					this.skillVariantIndex = skillVariantIndex;
				}

				// Token: 0x04003BC8 RID: 15304
				public readonly BodyIndex bodyIndex;

				// Token: 0x04003BC9 RID: 15305
				public readonly int skillSlotIndex;

				// Token: 0x04003BCA RID: 15306
				public readonly uint skillVariantIndex;
			}

			// Token: 0x02000A3C RID: 2620
			private struct SkinSetter
			{
				// Token: 0x06003C75 RID: 15477 RVA: 0x000FA4CE File Offset: 0x000F86CE
				public SkinSetter(BodyIndex bodyIndex, uint skinIndex)
				{
					this.bodyIndex = bodyIndex;
					this.skinIndex = skinIndex;
				}

				// Token: 0x04003BCB RID: 15307
				public readonly BodyIndex bodyIndex;

				// Token: 0x04003BCC RID: 15308
				public readonly uint skinIndex;
			}
		}
	}
}
