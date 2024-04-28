using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace HG.Reflection
{
	// Token: 0x0200001C RID: 28
	[MeansImplicitUse]
	public abstract class SearchableAttribute : Attribute
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004C20 File Offset: 0x00002E20
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00004C28 File Offset: 0x00002E28
		public object target { get; private set; }

		// Token: 0x0600010A RID: 266 RVA: 0x00004C34 File Offset: 0x00002E34
		public static List<SearchableAttribute> GetInstances<T>() where T : SearchableAttribute
		{
			List<SearchableAttribute> result;
			if (SearchableAttribute.instancesListsByType.TryGetValue(typeof(T), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004C5C File Offset: 0x00002E5C
		public static void GetInstances<T>(List<T> dest) where T : SearchableAttribute
		{
			List<SearchableAttribute> instances = SearchableAttribute.GetInstances<T>();
			if (instances == null)
			{
				return;
			}
			foreach (SearchableAttribute searchableAttribute in instances)
			{
				dest.Add((T)((object)searchableAttribute));
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004CBC File Offset: 0x00002EBC
		public static void ScanAllAssemblies()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					SearchableAttribute.ScanAssembly(assembly);
				}
				catch (Exception arg)
				{
					Console.WriteLine(string.Format("ScanAllAssemblies failed for '{0}':  {1}", assembly.FullName, arg));
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004D18 File Offset: 0x00002F18
		public static void ScanAssembly(Assembly assembly)
		{
			if (SearchableAttribute.assemblyBlacklist.Contains(assembly.FullName))
			{
				return;
			}
			SearchableAttribute.assemblyBlacklist.Add(assembly.FullName);
			if (assembly.GetCustomAttribute<SearchableAttribute.OptInAttribute>() == null)
			{
				return;
			}
			Type[] types;
			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				Debug.LogError(string.Format("ScanAssembly:  {0}", ex));
				types = ex.Types;
				if (types == null)
				{
					return;
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("ScanAssembly:  {0}", arg));
				return;
			}
			foreach (Type type in types)
			{
				foreach (SearchableAttribute attribute in type.GetCustomAttributes(false))
				{
					SearchableAttribute.RegisterAttribute(attribute, type);
				}
				foreach (MemberInfo memberInfo in type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					foreach (SearchableAttribute attribute2 in memberInfo.GetCustomAttributes(false))
					{
						SearchableAttribute.RegisterAttribute(attribute2, memberInfo);
					}
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004E7C File Offset: 0x0000307C
		private static void RegisterAttribute(SearchableAttribute attribute, object target)
		{
			attribute.target = target;
			Type type = attribute.GetType();
			while (type != null && typeof(SearchableAttribute).IsAssignableFrom(type))
			{
				SearchableAttribute.GetInstancesListForType(type).Add(attribute);
				type = type.BaseType;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004EC8 File Offset: 0x000030C8
		private static List<SearchableAttribute> GetInstancesListForType(Type attributeType)
		{
			List<SearchableAttribute> list;
			if (!SearchableAttribute.instancesListsByType.TryGetValue(attributeType, out list))
			{
				list = new List<SearchableAttribute>();
				SearchableAttribute.instancesListsByType.Add(attributeType, list);
			}
			return list;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004EF8 File Offset: 0x000030F8
		static SearchableAttribute()
		{
			try
			{
				SearchableAttribute.ScanAllAssemblies();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x04000035 RID: 53
		private static readonly Dictionary<Type, List<SearchableAttribute>> instancesListsByType = new Dictionary<Type, List<SearchableAttribute>>();

		// Token: 0x04000036 RID: 54
		private static HashSet<string> assemblyBlacklist = new HashSet<string>();

		// Token: 0x0200002E RID: 46
		[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
		public sealed class OptInAttribute : Attribute
		{
		}
	}
}
