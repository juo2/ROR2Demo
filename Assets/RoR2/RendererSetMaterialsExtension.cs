using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000087 RID: 135
public static class RendererSetMaterialsExtension
{
	// Token: 0x06000236 RID: 566 RVA: 0x0000A258 File Offset: 0x00008458
	private static void InitSharedMaterialsArrays(int maxMaterials)
	{
		RendererSetMaterialsExtension.sharedMaterialArrays = new Material[maxMaterials + 1][];
		if (maxMaterials > 0)
		{
			RendererSetMaterialsExtension.sharedMaterialArrays[0] = Array.Empty<Material>();
			for (int i = 1; i < RendererSetMaterialsExtension.sharedMaterialArrays.Length; i++)
			{
				RendererSetMaterialsExtension.sharedMaterialArrays[i] = new Material[i];
			}
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000A2A1 File Offset: 0x000084A1
	static RendererSetMaterialsExtension()
	{
		RendererSetMaterialsExtension.InitSharedMaterialsArrays(16);
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000A2AC File Offset: 0x000084AC
	public static void SetSharedMaterials(this Renderer renderer, Material[] sharedMaterials, int count)
	{
		if (RendererSetMaterialsExtension.sharedMaterialArrays.Length < count)
		{
			RendererSetMaterialsExtension.InitSharedMaterialsArrays(count);
		}
		Material[] array = RendererSetMaterialsExtension.sharedMaterialArrays[count];
		Array.Copy(sharedMaterials, array, count);
		renderer.sharedMaterials = array;
		Array.Clear(array, 0, count);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000A2E8 File Offset: 0x000084E8
	public static void SetSharedMaterials(this Renderer renderer, List<Material> sharedMaterials)
	{
		int count = sharedMaterials.Count;
		if (RendererSetMaterialsExtension.sharedMaterialArrays.Length < count)
		{
			RendererSetMaterialsExtension.InitSharedMaterialsArrays(count);
		}
		Material[] array = RendererSetMaterialsExtension.sharedMaterialArrays[count];
		sharedMaterials.CopyTo(array, 0);
		renderer.sharedMaterials = array;
		Array.Clear(array, 0, count);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000A32C File Offset: 0x0000852C
	public static void SetMaterials(this Renderer renderer, Material[] materials, int count)
	{
		if (RendererSetMaterialsExtension.sharedMaterialArrays.Length < count)
		{
			RendererSetMaterialsExtension.InitSharedMaterialsArrays(count);
		}
		Material[] array = RendererSetMaterialsExtension.sharedMaterialArrays[count];
		Array.Copy(materials, array, count);
		renderer.materials = array;
		Array.Clear(array, 0, count);
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000A368 File Offset: 0x00008568
	public static void SetMaterials(this Renderer renderer, List<Material> materials)
	{
		int count = materials.Count;
		if (RendererSetMaterialsExtension.sharedMaterialArrays.Length < count)
		{
			RendererSetMaterialsExtension.InitSharedMaterialsArrays(count);
		}
		Material[] array = RendererSetMaterialsExtension.sharedMaterialArrays[count];
		materials.CopyTo(array, 0);
		renderer.materials = array;
		Array.Clear(array, 0, count);
	}

	// Token: 0x04000236 RID: 566
	private static Material[][] sharedMaterialArrays;
}
