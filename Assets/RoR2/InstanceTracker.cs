using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000936 RID: 2358
	public static class InstanceTracker
	{
		// Token: 0x0600354F RID: 13647 RVA: 0x000E1981 File Offset: 0x000DFB81
		public static void Add<T>([NotNull] T instance) where T : MonoBehaviour
		{
			InstanceTracker.TypeData<T>.Add(instance);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000E1989 File Offset: 0x000DFB89
		public static void Remove<T>([NotNull] T instance) where T : MonoBehaviour
		{
			InstanceTracker.TypeData<T>.Remove(instance);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000E1991 File Offset: 0x000DFB91
		[NotNull]
		public static List<T> GetInstancesList<T>() where T : MonoBehaviour
		{
			return InstanceTracker.TypeData<T>.instancesList;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000E1998 File Offset: 0x000DFB98
		public static T FirstOrNull<T>() where T : MonoBehaviour
		{
			if (InstanceTracker.TypeData<T>.instancesList.Count == 0)
			{
				return default(T);
			}
			return InstanceTracker.TypeData<T>.instancesList[0];
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000E19C6 File Offset: 0x000DFBC6
		public static bool Any<T>() where T : MonoBehaviour
		{
			return InstanceTracker.TypeData<T>.instancesList.Count != 0;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000E19D8 File Offset: 0x000DFBD8
		[NotNull]
		public static IEnumerable<MonoBehaviour> FindInstancesEnumerable([NotNull] Type t)
		{
			IEnumerable<MonoBehaviour> result;
			if (!InstanceTracker.instancesLists.TryGetValue(t, out result))
			{
				return Enumerable.Empty<MonoBehaviour>();
			}
			return result;
		}

		// Token: 0x0400361F RID: 13855
		private static readonly Dictionary<Type, IEnumerable<MonoBehaviour>> instancesLists = new Dictionary<Type, IEnumerable<MonoBehaviour>>();

		// Token: 0x02000937 RID: 2359
		private static class TypeData<T> where T : MonoBehaviour
		{
			// Token: 0x06003556 RID: 13654 RVA: 0x000E1A07 File Offset: 0x000DFC07
			static TypeData()
			{
				InstanceTracker.instancesLists[typeof(T)] = InstanceTracker.TypeData<T>.instancesList;
			}

			// Token: 0x06003557 RID: 13655 RVA: 0x000E1A2C File Offset: 0x000DFC2C
			public static void Add(T instance)
			{
				InstanceTracker.TypeData<T>.instancesList.Add(instance);
			}

			// Token: 0x06003558 RID: 13656 RVA: 0x000E1A39 File Offset: 0x000DFC39
			public static void Remove(T instance)
			{
				InstanceTracker.TypeData<T>.instancesList.Remove(instance);
			}

			// Token: 0x04003620 RID: 13856
			public static readonly List<T> instancesList = new List<T>();
		}
	}
}
