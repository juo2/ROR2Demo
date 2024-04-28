using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.Skills
{
	// Token: 0x02000C19 RID: 3097
	[CreateAssetMenu(menuName = "RoR2/SkillFamily")]
	public class SkillFamily : ScriptableObject
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x00003BE8 File Offset: 0x00001DE8
		[Obsolete("Accessing UnityEngine.Object.Name causes allocations on read. Look up the name from the catalog instead. If absolutely necessary to perform direct access, cast to ScriptableObject first.")]
		public new string name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x001226E3 File Offset: 0x001208E3
		// (set) Token: 0x06004615 RID: 17941 RVA: 0x001226EB File Offset: 0x001208EB
		public int catalogIndex { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x001226F4 File Offset: 0x001208F4
		public SkillDef defaultSkillDef
		{
			get
			{
				return this.variants[(int)this.defaultVariantIndex].skillDef;
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x0012270C File Offset: 0x0012090C
		public void OnValidate()
		{
			if (this.variants == null)
			{
				string name = base.name;
				Debug.LogError("Skill Family \"" + name + "\" has a null variants array");
				return;
			}
			if ((ulong)this.defaultVariantIndex >= (ulong)((long)this.variants.Length))
			{
				string name2 = base.name;
				Debug.LogError(string.Format("Skill Family \"{0}\" defaultVariantIndex ({1}) is outside the bounds of the variants array ({2}).", name2, this.defaultVariantIndex, this.variants.Length));
			}
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x0012277F File Offset: 0x0012097F
		public string GetVariantName(int variantIndex)
		{
			return SkillCatalog.GetSkillName(this.variants[variantIndex].skillDef.skillIndex);
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x0012279C File Offset: 0x0012099C
		public int GetVariantIndex(string variantName)
		{
			for (int i = 0; i < this.variants.Length; i++)
			{
				if (this.GetVariantName(i).Equals(variantName, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x001227D0 File Offset: 0x001209D0
		[ContextMenu("Upgrade unlockableName to unlockableDef")]
		public void UpgradeUnlockableNameToUnlockableDef()
		{
			for (int i = 0; i < this.variants.Length; i++)
			{
				ref SkillFamily.Variant ptr = ref this.variants[i];
				if (!string.IsNullOrEmpty(ptr.unlockableName) && !ptr.unlockableDef)
				{
					UnlockableDef unlockableDef = LegacyResourcesAPI.Load<UnlockableDef>("UnlockableDefs/" + ptr.unlockableName);
					if (unlockableDef)
					{
						Debug.Log(unlockableDef);
						ptr.unlockableDef = unlockableDef;
						ptr.unlockableName = null;
					}
				}
			}
			EditorUtil.SetDirty(this);
		}

		// Token: 0x04004409 RID: 17417
		[FormerlySerializedAs("Entries")]
		public SkillFamily.Variant[] variants = Array.Empty<SkillFamily.Variant>();

		// Token: 0x0400440A RID: 17418
		[FormerlySerializedAs("defaultEntryIndex")]
		public uint defaultVariantIndex;

		// Token: 0x02000C1A RID: 3098
		[Serializable]
		public struct Variant
		{
			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x0600461C RID: 17948 RVA: 0x00122862 File Offset: 0x00120A62
			// (set) Token: 0x0600461D RID: 17949 RVA: 0x0012286A File Offset: 0x00120A6A
			public ViewablesCatalog.Node viewableNode { get; set; }

			// Token: 0x0400440B RID: 17419
			public SkillDef skillDef;

			// Token: 0x0400440C RID: 17420
			[Obsolete("Use 'unlockableDef' instead.")]
			public string unlockableName;

			// Token: 0x0400440D RID: 17421
			public UnlockableDef unlockableDef;
		}
	}
}
