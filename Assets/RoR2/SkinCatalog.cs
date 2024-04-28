using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A59 RID: 2649
	public static class SkinCatalog
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x000FB3B1 File Offset: 0x000F95B1
		public static int skinCount
		{
			get
			{
				return SkinCatalog.allSkinDefs.Length;
			}
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000FB3BC File Offset: 0x000F95BC
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			List<SkinDef> list = new List<SkinDef>();
			SkinCatalog.skinsByBody = new SkinDef[BodyCatalog.bodyCount][];
			for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)BodyCatalog.bodyCount; bodyIndex++)
			{
				SkinDef[] array = ArrayUtils.Clone<SkinDef>(SkinCatalog.FindSkinsForBody(bodyIndex));
				SkinCatalog.skinsByBody[(int)bodyIndex] = array;
				list.AddRange(array);
			}
			SkinCatalog.allSkinDefs = list.ToArray();
			for (int i = 0; i < SkinCatalog.allSkinDefs.Length; i++)
			{
				SkinCatalog.allSkinDefs[i].skinIndex = (SkinIndex)i;
			}
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000FB434 File Offset: 0x000F9634
		[CanBeNull]
		public static SkinDef FindCurrentSkinDefForBodyInstance(GameObject bodyObject)
		{
			ModelLocator component = bodyObject.GetComponent<ModelLocator>();
			if (!component || !component.modelTransform)
			{
				return null;
			}
			ModelSkinController component2 = component.modelTransform.GetComponent<ModelSkinController>();
			if (!component2)
			{
				return null;
			}
			return ArrayUtils.GetSafe<SkinDef>(component2.skins, component2.currentSkinIndex);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000FB488 File Offset: 0x000F9688
		[NotNull]
		private static SkinDef[] FindSkinsForBody(BodyIndex bodyIndex)
		{
			ModelLocator component = BodyCatalog.GetBodyPrefab(bodyIndex).GetComponent<ModelLocator>();
			if (!component || !component.modelTransform)
			{
				return Array.Empty<SkinDef>();
			}
			ModelSkinController component2 = component.modelTransform.GetComponent<ModelSkinController>();
			if (!component2)
			{
				return Array.Empty<SkinDef>();
			}
			return component2.skins;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000FB4DC File Offset: 0x000F96DC
		[CanBeNull]
		public static SkinDef GetSkinDef(SkinIndex skinIndex)
		{
			return ArrayUtils.GetSafe<SkinDef>(SkinCatalog.allSkinDefs, (int)skinIndex);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000FB4EC File Offset: 0x000F96EC
		[NotNull]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static SkinDef[] GetBodySkinDefs(BodyIndex bodyIndex)
		{
			SkinDef[][] array = SkinCatalog.skinsByBody;
			SkinDef[] array2 = Array.Empty<SkinDef>();
			return ArrayUtils.GetSafe<SkinDef[]>(array, (int)bodyIndex, array2);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000FB50C File Offset: 0x000F970C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetBodySkinCount(BodyIndex bodyIndex)
		{
			return SkinCatalog.GetBodySkinDefs(bodyIndex).Length;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000FB516 File Offset: 0x000F9716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SkinDef GetBodySkinDef(BodyIndex bodyIndex, int skinIndex)
		{
			return ArrayUtils.GetSafe<SkinDef>(SkinCatalog.GetBodySkinDefs(bodyIndex), skinIndex);
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000FB524 File Offset: 0x000F9724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FindLocalSkinIndexForBody(BodyIndex bodyIndex, [CanBeNull] SkinDef skinDef)
		{
			return Array.IndexOf<SkinDef>(SkinCatalog.GetBodySkinDefs(bodyIndex), skinDef);
		}

		// Token: 0x04003BFE RID: 15358
		private static SkinDef[] allSkinDefs = Array.Empty<SkinDef>();

		// Token: 0x04003BFF RID: 15359
		private static SkinDef[][] skinsByBody = Array.Empty<SkinDef[]>();
	}
}
