using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200091C RID: 2332
	public static class GetComponentsCache<T>
	{
		// Token: 0x060034BE RID: 13502 RVA: 0x000DEBCF File Offset: 0x000DCDCF
		public static void ReturnBuffer(List<T> buffer)
		{
			buffer.Clear();
			GetComponentsCache<T>.buffers.Push(buffer);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000DEBE2 File Offset: 0x000DCDE2
		private static List<T> RequestBuffer()
		{
			if (GetComponentsCache<T>.buffers.Count == 0)
			{
				return new List<T>();
			}
			return GetComponentsCache<T>.buffers.Pop();
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000DEC00 File Offset: 0x000DCE00
		public static List<T> GetGameObjectComponents(GameObject gameObject)
		{
			List<T> list = GetComponentsCache<T>.RequestBuffer();
			gameObject.GetComponents<T>(list);
			return list;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000DEC1C File Offset: 0x000DCE1C
		public static List<T> GetGameObjectComponentsInChildren(GameObject gameObject, bool includeInactive = false)
		{
			List<T> list = GetComponentsCache<T>.RequestBuffer();
			gameObject.GetComponentsInChildren<T>(includeInactive, list);
			return list;
		}

		// Token: 0x040035B2 RID: 13746
		private static readonly Stack<List<T>> buffers = new Stack<List<T>>();
	}
}
