using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200094F RID: 2383
	public class Loadout
	{
		// Token: 0x060035EC RID: 13804 RVA: 0x000E3E37 File Offset: 0x000E2037
		public void Serialize(NetworkWriter writer)
		{
			this.bodyLoadoutManager.Serialize(writer);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000E3E45 File Offset: 0x000E2045
		public void Deserialize(NetworkReader reader)
		{
			this.bodyLoadoutManager.Deserialize(reader);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000E3E53 File Offset: 0x000E2053
		public void Copy(Loadout dest)
		{
			this.bodyLoadoutManager.Copy(dest.bodyLoadoutManager);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000E3E66 File Offset: 0x000E2066
		public void Clear()
		{
			this.bodyLoadoutManager.Clear();
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000E3E73 File Offset: 0x000E2073
		public bool ValueEquals(Loadout other)
		{
			return this == other || (other != null && this.bodyLoadoutManager.ValueEquals(other.bodyLoadoutManager));
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000E3E91 File Offset: 0x000E2091
		public XElement ToXml(string elementName)
		{
			XElement xelement = new XElement(elementName);
			xelement.Add(this.bodyLoadoutManager.ToXml("BodyLoadouts"));
			return xelement;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000E3EB4 File Offset: 0x000E20B4
		public bool FromXml(XElement element)
		{
			bool flag = true;
			XElement xelement = element.Element("BodyLoadouts");
			flag = (xelement != null && (flag & this.bodyLoadoutManager.FromXml(xelement)));
			this.EnforceValidity();
			return flag;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000E3EF1 File Offset: 0x000E20F1
		public void EnforceValidity()
		{
			this.bodyLoadoutManager.EnforceValidity();
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000E3EFE File Offset: 0x000E20FE
		public void EnforceUnlockables(UserProfile userProfile)
		{
			this.bodyLoadoutManager.EnforceUnlockables(userProfile);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000E3F0C File Offset: 0x000E210C
		private static void GenerateViewables()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ViewablesCatalog.Node node = new ViewablesCatalog.Node("Loadout", true, null);
			ViewablesCatalog.Node parent = new ViewablesCatalog.Node("Bodies", true, node);
			for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)BodyCatalog.bodyCount; bodyIndex++)
			{
				if (SurvivorCatalog.GetSurvivorIndexFromBodyIndex(bodyIndex) != SurvivorIndex.None)
				{
					string bodyName = BodyCatalog.GetBodyName(bodyIndex);
					GenericSkill[] bodyPrefabSkillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndex);
					ViewablesCatalog.Node parent2 = new ViewablesCatalog.Node(bodyName, true, parent);
					for (int i = 0; i < bodyPrefabSkillSlots.Length; i++)
					{
						SkillFamily skillFamily = bodyPrefabSkillSlots[i].skillFamily;
						if (skillFamily.variants.Length > 1)
						{
							string skillFamilyName = SkillCatalog.GetSkillFamilyName(skillFamily.catalogIndex);
							uint num = 0U;
							while ((ulong)num < (ulong)((long)skillFamily.variants.Length))
							{
								ref SkillFamily.Variant ptr = ref skillFamily.variants[(int)num];
								UnlockableDef unlockableDef = ptr.unlockableDef;
								string skillName = SkillCatalog.GetSkillName(ptr.skillDef.skillIndex);
								stringBuilder.Append(skillFamilyName).Append(".").Append(skillName);
								string name = stringBuilder.ToString();
								stringBuilder.Clear();
								ViewablesCatalog.Node variantNode = new ViewablesCatalog.Node(name, false, parent2);
								ptr.viewableNode = variantNode;
								variantNode.shouldShowUnviewed = ((UserProfile userProfile) => !userProfile.HasViewedViewable(variantNode.fullName) && userProfile.HasUnlockable(unlockableDef));
								num += 1U;
							}
						}
					}
					SkinDef[] bodySkins = BodyCatalog.GetBodySkins(bodyIndex);
					if (bodySkins.Length > 1)
					{
						ViewablesCatalog.Node parent3 = new ViewablesCatalog.Node("Skins", true, parent2);
						for (int j = 0; j < bodySkins.Length; j++)
						{
							UnlockableDef unlockableDef = bodySkins[j].unlockableDef;
							if (unlockableDef)
							{
								ViewablesCatalog.Node skinNode = new ViewablesCatalog.Node(bodySkins[j].name, false, parent3);
								skinNode.shouldShowUnviewed = ((UserProfile userProfile) => !userProfile.HasViewedViewable(skinNode.fullName) && userProfile.HasUnlockable(unlockableDef));
							}
						}
					}
				}
			}
			ViewablesCatalog.AddNodeToRoot(node);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000E4113 File Offset: 0x000E2313
		public static Loadout RequestInstance()
		{
			if (Loadout.instancePool.Count == 0)
			{
				return new Loadout();
			}
			return Loadout.instancePool.Pop();
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000E4131 File Offset: 0x000E2331
		public static Loadout ReturnInstance(Loadout instance)
		{
			instance.Clear();
			Loadout.instancePool.Push(instance);
			return null;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000026ED File Offset: 0x000008ED
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog),
			typeof(SkillCatalog),
			typeof(SkinCatalog)
		})]
		private static void Init()
		{
		}

		// Token: 0x040036A4 RID: 13988
		public readonly Loadout.BodyLoadoutManager bodyLoadoutManager = new Loadout.BodyLoadoutManager();

		// Token: 0x040036A5 RID: 13989
		private static readonly Stack<Loadout> instancePool = new Stack<Loadout>();

		// Token: 0x02000950 RID: 2384
		public class BodyLoadoutManager
		{
			// Token: 0x060035FB RID: 13819 RVA: 0x000E4164 File Offset: 0x000E2364
			private int FindModifiedBodyLoadoutIndexByBodyIndex(BodyIndex bodyIndex)
			{
				for (int i = 0; i < this.modifiedBodyLoadouts.Length; i++)
				{
					if (this.modifiedBodyLoadouts[i].bodyIndex == bodyIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060035FC RID: 13820 RVA: 0x000E4198 File Offset: 0x000E2398
			private Loadout.BodyLoadoutManager.BodyLoadout GetReadOnlyBodyLoadout(BodyIndex bodyIndex)
			{
				int num = this.FindModifiedBodyLoadoutIndexByBodyIndex(bodyIndex);
				if (num == -1)
				{
					return Loadout.BodyLoadoutManager.defaultBodyLoadouts[(int)bodyIndex];
				}
				return this.modifiedBodyLoadouts[num];
			}

			// Token: 0x060035FD RID: 13821 RVA: 0x000E41C4 File Offset: 0x000E23C4
			private Loadout.BodyLoadoutManager.BodyLoadout GetOrCreateModifiedBodyLoadout(BodyIndex bodyIndex)
			{
				int num = this.FindModifiedBodyLoadoutIndexByBodyIndex(bodyIndex);
				if (num != -1)
				{
					return this.modifiedBodyLoadouts[num];
				}
				Loadout.BodyLoadoutManager.BodyLoadout result = Loadout.BodyLoadoutManager.GetDefaultLoadoutForBody(bodyIndex).Clone();
				ArrayUtils.ArrayAppend<Loadout.BodyLoadoutManager.BodyLoadout>(ref this.modifiedBodyLoadouts, result);
				return result;
			}

			// Token: 0x060035FE RID: 13822 RVA: 0x000E4200 File Offset: 0x000E2400
			public uint GetSkillVariant(BodyIndex bodyIndex, int skillSlot)
			{
				return this.GetReadOnlyBodyLoadout(bodyIndex).skillPreferences[skillSlot];
			}

			// Token: 0x060035FF RID: 13823 RVA: 0x000E4210 File Offset: 0x000E2410
			public void SetSkillVariant(BodyIndex bodyIndex, int skillSlot, uint skillVariant)
			{
				if ((ulong)bodyIndex >= (ulong)((long)BodyCatalog.bodyCount))
				{
					throw new ArgumentOutOfRangeException("bodyIndex", (int)bodyIndex, string.Format("Value provided for 'bodyIndex' is outside range [0, {0}]", (int)bodyIndex));
				}
				if (this.GetSkillVariant(bodyIndex, skillSlot) == skillVariant)
				{
					return;
				}
				int skillSlotCountForBody = Loadout.BodyLoadoutManager.GetSkillSlotCountForBody(bodyIndex);
				if ((ulong)skillSlot >= (ulong)((long)skillSlotCountForBody))
				{
					throw new ArgumentOutOfRangeException("skillSlot", skillSlot, string.Format("Value provided for 'skillSlot' is outside range [0, {0}) for body=[{1}]", skillSlotCountForBody, (int)bodyIndex));
				}
				int num = Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].prefabSkillSlots[skillSlot].skillFamily.variants.Length;
				if ((ulong)skillVariant >= (ulong)((long)num))
				{
					throw new ArgumentOutOfRangeException("skillVariant", skillVariant, string.Format("Value provided for 'skillVariant' is outside range [0, {0}) for body=[{1}] skillSlot=[{2}]", num, (int)bodyIndex, skillSlot));
				}
				Loadout.BodyLoadoutManager.BodyLoadout orCreateModifiedBodyLoadout = this.GetOrCreateModifiedBodyLoadout(bodyIndex);
				orCreateModifiedBodyLoadout.SetSkillVariant(skillSlot, skillVariant);
				this.RemoveBodyLoadoutIfDefault(orCreateModifiedBodyLoadout);
			}

			// Token: 0x06003600 RID: 13824 RVA: 0x000E42F2 File Offset: 0x000E24F2
			public uint GetSkinIndex(BodyIndex bodyIndex)
			{
				return this.GetReadOnlyBodyLoadout(bodyIndex).skinPreference;
			}

			// Token: 0x06003601 RID: 13825 RVA: 0x000E4300 File Offset: 0x000E2500
			public void SetSkinIndex(BodyIndex bodyIndex, uint skinIndex)
			{
				Loadout.BodyLoadoutManager.BodyLoadout orCreateModifiedBodyLoadout = this.GetOrCreateModifiedBodyLoadout(bodyIndex);
				orCreateModifiedBodyLoadout.skinPreference = skinIndex;
				this.RemoveBodyLoadoutIfDefault(orCreateModifiedBodyLoadout);
			}

			// Token: 0x06003602 RID: 13826 RVA: 0x000E4323 File Offset: 0x000E2523
			private void RemoveBodyLoadoutIfDefault(Loadout.BodyLoadoutManager.BodyLoadout bodyLoadout)
			{
				this.RemoveBodyLoadoutIfDefault(this.FindModifiedBodyLoadoutIndexByBodyIndex(bodyLoadout.bodyIndex));
			}

			// Token: 0x06003603 RID: 13827 RVA: 0x000E4337 File Offset: 0x000E2537
			private void RemoveBodyLoadoutIfDefault(int modifiedBodyLoadoutIndex)
			{
				Loadout.BodyLoadoutManager.BodyLoadout bodyLoadout = this.modifiedBodyLoadouts[modifiedBodyLoadoutIndex];
				if (bodyLoadout.ValueEquals(Loadout.BodyLoadoutManager.GetDefaultLoadoutForBody(bodyLoadout.bodyIndex)))
				{
					this.RemoveBodyLoadoutAt(modifiedBodyLoadoutIndex);
				}
			}

			// Token: 0x06003604 RID: 13828 RVA: 0x000E435A File Offset: 0x000E255A
			private void RemoveBodyLoadoutAt(int i)
			{
				ArrayUtils.ArrayRemoveAtAndResize<Loadout.BodyLoadoutManager.BodyLoadout>(ref this.modifiedBodyLoadouts, i, 1);
			}

			// Token: 0x06003605 RID: 13829 RVA: 0x000E4369 File Offset: 0x000E2569
			private static Loadout.BodyLoadoutManager.BodyLoadout GetDefaultLoadoutForBody(BodyIndex bodyIndex)
			{
				return Loadout.BodyLoadoutManager.defaultBodyLoadouts[(int)bodyIndex];
			}

			// Token: 0x06003606 RID: 13830 RVA: 0x000E4372 File Offset: 0x000E2572
			private static int GetSkillSlotCountForBody(BodyIndex bodyIndex)
			{
				return Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].skillSlotCount;
			}

			// Token: 0x06003607 RID: 13831 RVA: 0x000E4384 File Offset: 0x000E2584
			[SystemInitializer(new Type[]
			{
				typeof(SkillCatalog),
				typeof(BodyCatalog)
			})]
			private static void Init()
			{
				Loadout.BodyLoadoutManager.defaultBodyLoadouts = new Loadout.BodyLoadoutManager.BodyLoadout[BodyCatalog.bodyCount];
				Loadout.BodyLoadoutManager.allBodyInfos = new Loadout.BodyLoadoutManager.BodyInfo[Loadout.BodyLoadoutManager.defaultBodyLoadouts.Length];
				for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)Loadout.BodyLoadoutManager.defaultBodyLoadouts.Length; bodyIndex++)
				{
					Loadout.BodyLoadoutManager.BodyInfo bodyInfo = default(Loadout.BodyLoadoutManager.BodyInfo);
					bodyInfo.prefabSkillSlots = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndex).GetComponents<GenericSkill>();
					bodyInfo.skillFamilyIndices = new int[bodyInfo.skillSlotCount];
					for (int i = 0; i < bodyInfo.prefabSkillSlots.Length; i++)
					{
						int[] skillFamilyIndices = bodyInfo.skillFamilyIndices;
						int num = i;
						SkillFamily skillFamily = bodyInfo.prefabSkillSlots[i].skillFamily;
						skillFamilyIndices[num] = ((skillFamily != null) ? skillFamily.catalogIndex : -1);
					}
					Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex] = bodyInfo;
					uint[] array = new uint[bodyInfo.skillSlotCount];
					for (int j = 0; j < bodyInfo.prefabSkillSlots.Length; j++)
					{
						array[j] = bodyInfo.prefabSkillSlots[j].skillFamily.defaultVariantIndex;
					}
					Loadout.BodyLoadoutManager.defaultBodyLoadouts[(int)bodyIndex] = new Loadout.BodyLoadoutManager.BodyLoadout
					{
						bodyIndex = bodyIndex,
						skinPreference = 0U,
						skillPreferences = array
					};
				}
				Loadout.GenerateViewables();
			}

			// Token: 0x06003608 RID: 13832 RVA: 0x000E4498 File Offset: 0x000E2698
			public void Copy(Loadout.BodyLoadoutManager dest)
			{
				Array.Resize<Loadout.BodyLoadoutManager.BodyLoadout>(ref dest.modifiedBodyLoadouts, this.modifiedBodyLoadouts.Length);
				for (int i = 0; i < this.modifiedBodyLoadouts.Length; i++)
				{
					dest.modifiedBodyLoadouts[i] = this.modifiedBodyLoadouts[i].Clone();
				}
			}

			// Token: 0x06003609 RID: 13833 RVA: 0x000E44E0 File Offset: 0x000E26E0
			public void Clear()
			{
				this.modifiedBodyLoadouts = Array.Empty<Loadout.BodyLoadoutManager.BodyLoadout>();
			}

			// Token: 0x0600360A RID: 13834 RVA: 0x000E44F0 File Offset: 0x000E26F0
			public void Serialize(NetworkWriter writer)
			{
				writer.WritePackedUInt32((uint)this.modifiedBodyLoadouts.Length);
				for (int i = 0; i < this.modifiedBodyLoadouts.Length; i++)
				{
					this.modifiedBodyLoadouts[i].Serialize(writer);
				}
			}

			// Token: 0x0600360B RID: 13835 RVA: 0x000E452C File Offset: 0x000E272C
			public void Deserialize(NetworkReader reader)
			{
				try
				{
					int num = (int)reader.ReadPackedUInt32();
					if (num > BodyCatalog.bodyCount)
					{
						num = BodyCatalog.bodyCount;
					}
					Array.Resize<Loadout.BodyLoadoutManager.BodyLoadout>(ref this.modifiedBodyLoadouts, num);
					for (int i = 0; i < num; i++)
					{
						Loadout.BodyLoadoutManager.BodyLoadout bodyLoadout = new Loadout.BodyLoadoutManager.BodyLoadout();
						bodyLoadout.Deserialize(reader);
						this.modifiedBodyLoadouts[i] = bodyLoadout;
					}
				}
				catch (Exception)
				{
					this.modifiedBodyLoadouts = Array.Empty<Loadout.BodyLoadoutManager.BodyLoadout>();
					throw;
				}
			}

			// Token: 0x0600360C RID: 13836 RVA: 0x000E459C File Offset: 0x000E279C
			public bool ValueEquals(Loadout.BodyLoadoutManager other)
			{
				if (this == other)
				{
					return true;
				}
				if (other == null)
				{
					return false;
				}
				if (this.modifiedBodyLoadouts.Length != other.modifiedBodyLoadouts.Length)
				{
					return false;
				}
				for (int i = 0; i < this.modifiedBodyLoadouts.Length; i++)
				{
					if (!this.modifiedBodyLoadouts[i].ValueEquals(other.modifiedBodyLoadouts[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600360D RID: 13837 RVA: 0x000E45F8 File Offset: 0x000E27F8
			public XElement ToXml(string elementName)
			{
				XElement xelement = new XElement(elementName);
				for (int i = 0; i < this.modifiedBodyLoadouts.Length; i++)
				{
					xelement.Add(this.modifiedBodyLoadouts[i].ToXml("BodyLoadout"));
				}
				return xelement;
			}

			// Token: 0x0600360E RID: 13838 RVA: 0x000E4640 File Offset: 0x000E2840
			public bool FromXml(XElement element)
			{
				Loadout.BodyLoadoutManager.<>c__DisplayClass24_0 CS$<>8__locals1;
				CS$<>8__locals1.bodyLoadouts = new List<Loadout.BodyLoadoutManager.BodyLoadout>();
				foreach (XElement element2 in element.Elements("BodyLoadout"))
				{
					Loadout.BodyLoadoutManager.BodyLoadout bodyLoadout = new Loadout.BodyLoadoutManager.BodyLoadout();
					if (bodyLoadout.FromXml(element2) && !Loadout.BodyLoadoutManager.<FromXml>g__BodyLoadoutAlreadyDefined|24_0(bodyLoadout.bodyIndex, ref CS$<>8__locals1) && !bodyLoadout.ValueEquals(Loadout.BodyLoadoutManager.GetDefaultLoadoutForBody(bodyLoadout.bodyIndex)))
					{
						CS$<>8__locals1.bodyLoadouts.Add(bodyLoadout);
					}
				}
				this.modifiedBodyLoadouts = CS$<>8__locals1.bodyLoadouts.ToArray();
				return true;
			}

			// Token: 0x0600360F RID: 13839 RVA: 0x000E46EC File Offset: 0x000E28EC
			public void EnforceUnlockables(UserProfile userProfile)
			{
				for (int i = this.modifiedBodyLoadouts.Length - 1; i >= 0; i--)
				{
					this.modifiedBodyLoadouts[i].EnforceUnlockables(userProfile);
					this.RemoveBodyLoadoutIfDefault(i);
				}
			}

			// Token: 0x06003610 RID: 13840 RVA: 0x000E4724 File Offset: 0x000E2924
			public void EnforceValidity()
			{
				for (int i = this.modifiedBodyLoadouts.Length - 1; i >= 0; i--)
				{
					this.modifiedBodyLoadouts[i].EnforceValidity();
					this.RemoveBodyLoadoutIfDefault(i);
				}
			}

			// Token: 0x06003612 RID: 13842 RVA: 0x000E4770 File Offset: 0x000E2970
			[CompilerGenerated]
			internal static bool <FromXml>g__BodyLoadoutAlreadyDefined|24_0(BodyIndex bodyIndex, ref Loadout.BodyLoadoutManager.<>c__DisplayClass24_0 A_1)
			{
				for (int i = 0; i < A_1.bodyLoadouts.Count; i++)
				{
					if (A_1.bodyLoadouts[i].bodyIndex == bodyIndex)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x040036A6 RID: 13990
			private Loadout.BodyLoadoutManager.BodyLoadout[] modifiedBodyLoadouts = Array.Empty<Loadout.BodyLoadoutManager.BodyLoadout>();

			// Token: 0x040036A7 RID: 13991
			private static Loadout.BodyLoadoutManager.BodyLoadout[] defaultBodyLoadouts;

			// Token: 0x040036A8 RID: 13992
			private static Loadout.BodyLoadoutManager.BodyInfo[] allBodyInfos;

			// Token: 0x02000951 RID: 2385
			private sealed class BodyLoadout
			{
				// Token: 0x06003613 RID: 13843 RVA: 0x000E47AA File Offset: 0x000E29AA
				[NotNull]
				public Loadout.BodyLoadoutManager.BodyLoadout Clone()
				{
					return new Loadout.BodyLoadoutManager.BodyLoadout
					{
						bodyIndex = this.bodyIndex,
						skinPreference = this.skinPreference,
						skillPreferences = (uint[])this.skillPreferences.Clone()
					};
				}

				// Token: 0x06003614 RID: 13844 RVA: 0x000E47E0 File Offset: 0x000E29E0
				public bool ValueEquals(Loadout.BodyLoadoutManager.BodyLoadout other)
				{
					return this == other || (other != null && this.bodyIndex.Equals(other.bodyIndex) && this.skinPreference.Equals(other.skinPreference) && ((IStructuralEquatable)this.skillPreferences).Equals(other.skillPreferences, EqualityComparer<uint>.Default));
				}

				// Token: 0x06003615 RID: 13845 RVA: 0x000E4843 File Offset: 0x000E2A43
				public uint GetSkillVariant(int skillSlotIndex)
				{
					if ((ulong)skillSlotIndex < (ulong)((long)this.skillPreferences.Length))
					{
						return this.skillPreferences[skillSlotIndex];
					}
					return 0U;
				}

				// Token: 0x06003616 RID: 13846 RVA: 0x000E485C File Offset: 0x000E2A5C
				public bool SetSkillVariant(int skillSlotIndex, uint skillVariant)
				{
					if ((ulong)skillSlotIndex < (ulong)((long)this.skillPreferences.Length))
					{
						this.skillPreferences[skillSlotIndex] = HGMath.Clamp(skillVariant, 0U, (uint)this.LookUpMaxSkillVariants(skillSlotIndex));
						return true;
					}
					return false;
				}

				// Token: 0x06003617 RID: 13847 RVA: 0x000E4884 File Offset: 0x000E2A84
				private bool IsSkillVariantValid(int skillSlotIndex)
				{
					SkillFamily skillFamily = this.GetSkillFamily(skillSlotIndex);
					return skillFamily && (ulong)this.GetSkillVariant(skillSlotIndex) < (ulong)((long)skillFamily.variants.Length);
				}

				// Token: 0x06003618 RID: 13848 RVA: 0x000E48BC File Offset: 0x000E2ABC
				private bool IsSkillVariantLocked(int skillSlotIndex, UserProfile userProfile)
				{
					SkillFamily skillFamily = this.GetSkillFamily(skillSlotIndex);
					if (!skillFamily)
					{
						return false;
					}
					uint skillVariant = this.GetSkillVariant(skillSlotIndex);
					return !userProfile.HasUnlockable(skillFamily.variants[(int)skillVariant].unlockableDef);
				}

				// Token: 0x06003619 RID: 13849 RVA: 0x000E48FD File Offset: 0x000E2AFD
				private void ResetSkillVariant(int skillSlotIndex)
				{
					if ((ulong)skillSlotIndex < (ulong)((long)this.skillPreferences.Length))
					{
						uint[] array = this.skillPreferences;
						SkillFamily skillFamily = this.GetSkillFamily(skillSlotIndex);
						array[skillSlotIndex] = ((skillFamily != null) ? skillFamily.defaultVariantIndex : 0U);
					}
				}

				// Token: 0x0600361A RID: 13850 RVA: 0x000E4928 File Offset: 0x000E2B28
				private bool IsSkinValid()
				{
					SkinDef[] bodySkins = BodyCatalog.GetBodySkins(this.bodyIndex);
					return (ulong)this.skinPreference < (ulong)((long)bodySkins.Length);
				}

				// Token: 0x0600361B RID: 13851 RVA: 0x000E4950 File Offset: 0x000E2B50
				private bool IsSkinLocked(UserProfile userProfile)
				{
					SkinDef safe = ArrayUtils.GetSafe<SkinDef>(BodyCatalog.GetBodySkins(this.bodyIndex), (int)this.skinPreference);
					return !safe || !userProfile.HasUnlockable(safe.unlockableDef);
				}

				// Token: 0x0600361C RID: 13852 RVA: 0x000E498D File Offset: 0x000E2B8D
				private void ResetSkin()
				{
					this.skinPreference = 0U;
				}

				// Token: 0x0600361D RID: 13853 RVA: 0x000E4998 File Offset: 0x000E2B98
				public void EnforceValidity()
				{
					for (int i = 0; i < this.skillPreferences.Length; i++)
					{
						if (!this.IsSkillVariantValid(i))
						{
							this.ResetSkillVariant(i);
						}
					}
					if (!this.IsSkinValid())
					{
						this.ResetSkin();
					}
				}

				// Token: 0x0600361E RID: 13854 RVA: 0x000E49D8 File Offset: 0x000E2BD8
				public void EnforceUnlockables(UserProfile userProfile)
				{
					for (int i = 0; i < this.skillPreferences.Length; i++)
					{
						if (this.IsSkillVariantLocked(i, userProfile))
						{
							this.ResetSkillVariant(i);
						}
					}
					if (this.IsSkinLocked(userProfile))
					{
						this.ResetSkin();
					}
				}

				// Token: 0x0600361F RID: 13855 RVA: 0x000E4A18 File Offset: 0x000E2C18
				[CanBeNull]
				private SkillFamily GetSkillFamily(int skillSlotIndex)
				{
					return BodyCatalog.GetBodyPrefabSkillSlots(this.bodyIndex)[skillSlotIndex].skillFamily;
				}

				// Token: 0x06003620 RID: 13856 RVA: 0x000E4A2C File Offset: 0x000E2C2C
				public int LookUpMaxSkillVariants(int skillSlotIndex)
				{
					if ((ulong)skillSlotIndex >= (ulong)((long)this.skillPreferences.Length))
					{
						return 0;
					}
					SkillFamily skillFamily = this.GetSkillFamily(skillSlotIndex);
					if (skillFamily == null)
					{
						return 0;
					}
					return skillFamily.variants.Length;
				}

				// Token: 0x06003621 RID: 13857 RVA: 0x000E4A54 File Offset: 0x000E2C54
				public void Serialize(NetworkWriter writer)
				{
					writer.WriteBodyIndex(this.bodyIndex);
					writer.WritePackedUInt32(this.skinPreference);
					for (int i = 0; i < this.skillPreferences.Length; i++)
					{
						writer.WritePackedUInt32(this.skillPreferences[i]);
					}
				}

				// Token: 0x06003622 RID: 13858 RVA: 0x000E4A9C File Offset: 0x000E2C9C
				public void Deserialize(NetworkReader reader)
				{
					this.bodyIndex = reader.ReadBodyIndex();
					if (this.bodyIndex < (BodyIndex)0)
					{
						this.bodyIndex = (BodyIndex)0;
					}
					if (this.bodyIndex >= (BodyIndex)BodyCatalog.bodyCount)
					{
						this.bodyIndex = (BodyIndex)(BodyCatalog.bodyCount - 1);
					}
					this.skinPreference = reader.ReadPackedUInt32();
					Array.Resize<uint>(ref this.skillPreferences, Loadout.BodyLoadoutManager.GetSkillSlotCountForBody(this.bodyIndex));
					for (int i = 0; i < this.skillPreferences.Length; i++)
					{
						this.SetSkillVariant(i, reader.ReadPackedUInt32());
					}
				}

				// Token: 0x06003623 RID: 13859 RVA: 0x000E4B24 File Offset: 0x000E2D24
				public XElement ToXml(string elementName)
				{
					XElement xelement = new XElement(elementName);
					xelement.SetAttributeValue("bodyName", BodyCatalog.GetBodyName(this.bodyIndex));
					xelement.Add(new XElement("Skin", this.skinPreference.ToString()));
					ref Loadout.BodyLoadoutManager.BodyInfo ptr = ref Loadout.BodyLoadoutManager.allBodyInfos[(int)this.bodyIndex];
					for (int i = 0; i < ptr.prefabSkillSlots.Length; i++)
					{
						int skillFamilyIndex = ptr.skillFamilyIndices[i];
						SkillFamily skillFamily = SkillCatalog.GetSkillFamily(skillFamilyIndex);
						string skillFamilyName = SkillCatalog.GetSkillFamilyName(skillFamilyIndex);
						string variantName = skillFamily.GetVariantName((int)this.skillPreferences[i]);
						if (variantName != null)
						{
							XElement xelement2 = new XElement("SkillPreference", variantName);
							xelement2.SetAttributeValue("skillFamily", skillFamilyName);
							xelement.Add(xelement2);
						}
					}
					return xelement;
				}

				// Token: 0x06003624 RID: 13860 RVA: 0x000E4BF8 File Offset: 0x000E2DF8
				public bool FromXml(XElement element)
				{
					XElement xelement = element.Element("Skin");
					uint.TryParse(((xelement != null) ? xelement.Value : null) ?? string.Empty, out this.skinPreference);
					XAttribute xattribute = element.Attribute("bodyName");
					string text = (xattribute != null) ? xattribute.Value : null;
					if (text == null)
					{
						Debug.Log("bodyName=null");
						return false;
					}
					this.bodyIndex = BodyCatalog.FindBodyIndex(text);
					if (this.bodyIndex == BodyIndex.None)
					{
						Debug.LogFormat("Could not find body index for bodyName={0}", new object[]
						{
							text
						});
						return false;
					}
					ref Loadout.BodyLoadoutManager.BodyInfo ptr = ref Loadout.BodyLoadoutManager.allBodyInfos[(int)this.bodyIndex];
					Loadout.BodyLoadoutManager.BodyLoadout.<>c__DisplayClass20_0 CS$<>8__locals1;
					CS$<>8__locals1.prefabSkillSlots = ptr.prefabSkillSlots;
					this.skillPreferences = new uint[CS$<>8__locals1.prefabSkillSlots.Length];
					foreach (XElement xelement2 in element.Elements("SkillPreference"))
					{
						XAttribute xattribute2 = xelement2.Attribute("skillFamily");
						string text2 = (xattribute2 != null) ? xattribute2.Value : null;
						string value = xelement2.Value;
						if (text2 != null)
						{
							int num = Loadout.BodyLoadoutManager.BodyLoadout.<FromXml>g__FindSkillSlotIndex|20_0(text2, ref CS$<>8__locals1);
							if (num != -1)
							{
								int variantIndex = CS$<>8__locals1.prefabSkillSlots[num].skillFamily.GetVariantIndex(value);
								if (variantIndex != -1)
								{
									this.skillPreferences[num] = (uint)variantIndex;
								}
								else
								{
									Debug.LogFormat("Could not find variant index for elementSkillFamilyName={0} elementSkillName={1}", new object[]
									{
										text2,
										value
									});
								}
							}
							else
							{
								Debug.LogFormat("Could not find skill slot index for elementSkillFamilyName={0} elementSkillName={1}", new object[]
								{
									text2,
									value
								});
							}
						}
					}
					return true;
				}

				// Token: 0x06003626 RID: 13862 RVA: 0x000E4DA0 File Offset: 0x000E2FA0
				[CompilerGenerated]
				internal static int <FromXml>g__FindSkillSlotIndex|20_0(string requestedSkillFamilyName, ref Loadout.BodyLoadoutManager.BodyLoadout.<>c__DisplayClass20_0 A_1)
				{
					for (int i = 0; i < A_1.prefabSkillSlots.Length; i++)
					{
						if (SkillCatalog.GetSkillFamilyName(A_1.prefabSkillSlots[i].skillFamily.catalogIndex).Equals(requestedSkillFamilyName, StringComparison.Ordinal))
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x040036A9 RID: 13993
				public BodyIndex bodyIndex;

				// Token: 0x040036AA RID: 13994
				public uint skinPreference;

				// Token: 0x040036AB RID: 13995
				public uint[] skillPreferences;
			}

			// Token: 0x02000953 RID: 2387
			private struct BodyInfo
			{
				// Token: 0x170004FE RID: 1278
				// (get) Token: 0x06003627 RID: 13863 RVA: 0x000E4DE3 File Offset: 0x000E2FE3
				public int skillSlotCount
				{
					get
					{
						return this.prefabSkillSlots.Length;
					}
				}

				// Token: 0x040036AD RID: 13997
				public int[] skillFamilyIndices;

				// Token: 0x040036AE RID: 13998
				public GenericSkill[] prefabSkillSlots;
			}
		}
	}
}
