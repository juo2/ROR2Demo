using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x020005AF RID: 1455
	public static class EquipmentCatalog
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000716BF File Offset: 0x0006F8BF
		public static int equipmentCount
		{
			get
			{
				return EquipmentCatalog.equipmentDefs.Length;
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000716C8 File Offset: 0x0006F8C8
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			EquipmentCatalog.SetEquipmentDefs(ContentManager.equipmentDefs);
			EquipmentCatalog.availability.MakeAvailable();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000716E0 File Offset: 0x0006F8E0
		private static void SetEquipmentDefs(EquipmentDef[] newEquipmentDefs)
		{
			EquipmentDef[] array = EquipmentCatalog.equipmentDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].equipmentIndex = EquipmentIndex.None;
			}
			EquipmentCatalog.equipmentNameToIndex.Clear();
			EquipmentCatalog.equipmentList.Clear();
			EquipmentCatalog.enigmaEquipmentList.Clear();
			EquipmentCatalog.randomTriggerEquipmentList.Clear();
			ArrayUtils.CloneTo<EquipmentDef>(newEquipmentDefs, ref EquipmentCatalog.equipmentDefs);
			Array.Sort<EquipmentDef>(EquipmentCatalog.equipmentDefs, (EquipmentDef a, EquipmentDef b) => string.CompareOrdinal(a.name, b.name));
			for (EquipmentIndex equipmentIndex = (EquipmentIndex)0; equipmentIndex < (EquipmentIndex)EquipmentCatalog.equipmentDefs.Length; equipmentIndex++)
			{
				EquipmentCatalog.RegisterEquipment(equipmentIndex, EquipmentCatalog.equipmentDefs[(int)equipmentIndex]);
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00071788 File Offset: 0x0006F988
		private static void RegisterEquipment(EquipmentIndex equipmentIndex, EquipmentDef equipmentDef)
		{
			equipmentDef.equipmentIndex = equipmentIndex;
			if (equipmentDef.canDrop)
			{
				EquipmentCatalog.equipmentList.Add(equipmentIndex);
				if (equipmentDef.enigmaCompatible)
				{
					EquipmentCatalog.enigmaEquipmentList.Add(equipmentIndex);
				}
				if (equipmentDef.canBeRandomlyTriggered)
				{
					EquipmentCatalog.randomTriggerEquipmentList.Add(equipmentIndex);
				}
			}
			string name = equipmentDef.name;
			EquipmentCatalog.equipmentNameToIndex[name] = equipmentIndex;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000717E8 File Offset: 0x0006F9E8
		public static EquipmentDef GetEquipmentDef(EquipmentIndex equipmentIndex)
		{
			return ArrayUtils.GetSafe<EquipmentDef>(EquipmentCatalog.equipmentDefs, (int)equipmentIndex);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000717F8 File Offset: 0x0006F9F8
		public static EquipmentIndex FindEquipmentIndex(string equipmentName)
		{
			EquipmentIndex result;
			if (EquipmentCatalog.equipmentNameToIndex.TryGetValue(equipmentName, out result))
			{
				return result;
			}
			return EquipmentIndex.None;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00071817 File Offset: 0x0006FA17
		public static T[] GetPerEquipmentBuffer<T>()
		{
			return new T[EquipmentCatalog.equipmentCount];
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00071823 File Offset: 0x0006FA23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsIndexValid(in EquipmentIndex equipmentIndex)
		{
			return equipmentIndex < (EquipmentIndex)EquipmentCatalog.equipmentCount;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00071830 File Offset: 0x0006FA30
		[ConCommand(commandName = "equipment_list", flags = ConVarFlags.None, helpText = "Lists internal names of all equipment registered to the equipment catalog.")]
		private static void CCEquipmentList(ConCommandArgs args)
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			foreach (EquipmentDef equipmentDef in EquipmentCatalog.equipmentDefs)
			{
				stringBuilder.AppendLine(equipmentDef.name + "  (" + Language.GetString(equipmentDef.nameToken) + ")");
			}
			args.Log(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001A63 RID: 6755 RVA: 0x00071896 File Offset: 0x0006FA96
		// (remove) Token: 0x06001A64 RID: 6756 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<EquipmentDef>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<EquipmentDef>("RoR2.EquipmentCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.equipmentDefs);
			}
			remove
			{
			}
		}

		// Token: 0x0400207C RID: 8316
		private static EquipmentDef[] equipmentDefs = Array.Empty<EquipmentDef>();

		// Token: 0x0400207D RID: 8317
		public static List<EquipmentIndex> equipmentList = new List<EquipmentIndex>();

		// Token: 0x0400207E RID: 8318
		public static List<EquipmentIndex> enigmaEquipmentList = new List<EquipmentIndex>();

		// Token: 0x0400207F RID: 8319
		public static List<EquipmentIndex> randomTriggerEquipmentList = new List<EquipmentIndex>();

		// Token: 0x04002080 RID: 8320
		private static readonly Dictionary<string, EquipmentIndex> equipmentNameToIndex = new Dictionary<string, EquipmentIndex>();

		// Token: 0x04002081 RID: 8321
		public static ResourceAvailability availability = default(ResourceAvailability);

		// Token: 0x04002082 RID: 8322
		public static readonly GenericStaticEnumerable<EquipmentIndex, EquipmentCatalog.AllEquipmentEnumerator> allEquipment;

		// Token: 0x020005B0 RID: 1456
		public struct AllEquipmentEnumerator : IEnumerator<EquipmentIndex>, IEnumerator, IDisposable
		{
			// Token: 0x06001A66 RID: 6758 RVA: 0x000718F6 File Offset: 0x0006FAF6
			public bool MoveNext()
			{
				this.position++;
				return this.position < (EquipmentIndex)EquipmentCatalog.equipmentCount;
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x00071913 File Offset: 0x0006FB13
			public void Reset()
			{
				this.position = EquipmentIndex.None;
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0007191C File Offset: 0x0006FB1C
			public EquipmentIndex Current
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00071924 File Offset: 0x0006FB24
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x000026ED File Offset: 0x000008ED
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04002083 RID: 8323
			private EquipmentIndex position;
		}
	}
}
