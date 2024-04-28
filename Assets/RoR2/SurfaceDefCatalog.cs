using System;
using HG;
using RoR2.ContentManagement;

namespace RoR2
{
	// Token: 0x02000A79 RID: 2681
	public static class SurfaceDefCatalog
	{
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000FEBD7 File Offset: 0x000FCDD7
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			SurfaceDefCatalog.SetSurfaceDefs(ContentManager.surfaceDefs);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000FEBE4 File Offset: 0x000FCDE4
		private static void SetSurfaceDefs(SurfaceDef[] newSurfaceDefs)
		{
			SurfaceDef[] array = SurfaceDefCatalog.surfaceDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].surfaceDefIndex = SurfaceDefIndex.Invalid;
			}
			ArrayUtils.CloneTo<SurfaceDef>(newSurfaceDefs, ref SurfaceDefCatalog.surfaceDefs);
			Array.Sort<SurfaceDef>(SurfaceDefCatalog.surfaceDefs, (SurfaceDef a, SurfaceDef b) => string.CompareOrdinal(a.name, b.name));
			for (int j = 0; j < SurfaceDefCatalog.surfaceDefs.Length; j++)
			{
				SurfaceDefCatalog.surfaceDefs[j].surfaceDefIndex = (SurfaceDefIndex)j;
			}
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000FEC61 File Offset: 0x000FCE61
		public static SurfaceDef GetSurfaceDef(SurfaceDefIndex surfaceDefIndex)
		{
			return ArrayUtils.GetSafe<SurfaceDef>(SurfaceDefCatalog.surfaceDefs, (int)surfaceDefIndex);
		}

		// Token: 0x04003C80 RID: 15488
		private static SurfaceDef[] surfaceDefs = Array.Empty<SurfaceDef>();
	}
}
