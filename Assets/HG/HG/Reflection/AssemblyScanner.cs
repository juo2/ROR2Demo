using System;
using System.Collections.Generic;
using System.Reflection;

namespace HG.Reflection
{
	// Token: 0x0200001B RID: 27
	public class AssemblyScanner
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004A54 File Offset: 0x00002C54
		public AssemblyScanner()
		{
			this.assemblyNameComparer = new AssemblyNameComparer(true);
			this.orderedAssemblies = new List<Assembly>();
			this.assemblyInfoByAssemblyName = new Dictionary<AssemblyName, AssemblyScanner.AssemblyInfo>(this.assemblyNameComparer);
			this.assemblyInfoByAssembly = new Dictionary<Assembly, AssemblyScanner.AssemblyInfo>();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004A90 File Offset: 0x00002C90
		public void AddAssemblies(IEnumerable<Assembly> newAssemblies)
		{
			foreach (Assembly assembly in newAssemblies)
			{
				this.AddAssemblyInfo(new AssemblyScanner.AssemblyInfo(assembly));
			}
			foreach (Assembly assembly2 in newAssemblies)
			{
				foreach (AssemblyName key in this.assemblyInfoByAssembly[assembly2].referencedAssemblyNames)
				{
					AssemblyScanner.AssemblyInfo assemblyInfo;
					if (this.assemblyInfoByAssemblyName.TryGetValue(key, out assemblyInfo))
					{
						assemblyInfo.referencingAssemblies.Add(assembly2);
					}
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004B58 File Offset: 0x00002D58
		private void AddAssemblyInfo(AssemblyScanner.AssemblyInfo assemblyInfo)
		{
			this.orderedAssemblies.Add(assemblyInfo.assembly);
			this.assemblyInfoByAssembly[assemblyInfo.assembly] = assemblyInfo;
			this.assemblyInfoByAssemblyName[assemblyInfo.assemblyName] = assemblyInfo;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B90 File Offset: 0x00002D90
		private AssemblyScanner.AssemblyInfo GetAssemblyInfo(Assembly assembly)
		{
			AssemblyScanner.AssemblyInfo result;
			if (this.assemblyInfoByAssembly.TryGetValue(assembly, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004BB0 File Offset: 0x00002DB0
		private AssemblyScanner.AssemblyInfo GetAssemblyInfo(AssemblyName assemblyName)
		{
			AssemblyScanner.AssemblyInfo result;
			if (this.assemblyInfoByAssemblyName.TryGetValue(assemblyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public AssemblyName[] GetAssemblyReferences(Assembly assembly)
		{
			AssemblyScanner.AssemblyInfo assemblyInfo = this.GetAssemblyInfo(assembly);
			if (assemblyInfo == null)
			{
				return null;
			}
			return assemblyInfo.referencedAssemblyNames;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public Type[] GetAssemblyTypes(Assembly assembly)
		{
			AssemblyScanner.AssemblyInfo assemblyInfo = this.GetAssemblyInfo(assembly);
			if (assemblyInfo == null)
			{
				return null;
			}
			return assemblyInfo.GetTypes();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public List<Assembly> GetReferencingAssemblies(Assembly assembly)
		{
			AssemblyScanner.AssemblyInfo assemblyInfo = this.GetAssemblyInfo(assembly);
			if (assemblyInfo == null)
			{
				return null;
			}
			return assemblyInfo.referencingAssemblies;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004C0C File Offset: 0x00002E0C
		public Assembly GetAssemblyByName(AssemblyName assemblyName)
		{
			AssemblyScanner.AssemblyInfo assemblyInfo = this.GetAssemblyInfo(assemblyName);
			if (assemblyInfo == null)
			{
				return null;
			}
			return assemblyInfo.assembly;
		}

		// Token: 0x04000030 RID: 48
		private readonly List<Assembly> orderedAssemblies;

		// Token: 0x04000031 RID: 49
		private readonly AssemblyNameComparer assemblyNameComparer;

		// Token: 0x04000032 RID: 50
		private readonly Dictionary<AssemblyName, AssemblyScanner.AssemblyInfo> assemblyInfoByAssemblyName;

		// Token: 0x04000033 RID: 51
		private readonly Dictionary<Assembly, AssemblyScanner.AssemblyInfo> assemblyInfoByAssembly;

		// Token: 0x0200002D RID: 45
		private class AssemblyInfo
		{
			// Token: 0x06000151 RID: 337 RVA: 0x00005970 File Offset: 0x00003B70
			public AssemblyInfo(Assembly assembly)
			{
				this.assembly = assembly;
				this.assemblyName = assembly.GetName();
				this.referencedAssemblyNames = assembly.GetReferencedAssemblies();
				this.referencingAssemblies = new List<Assembly>();
			}

			// Token: 0x06000152 RID: 338 RVA: 0x000059A2 File Offset: 0x00003BA2
			public Type[] GetTypes()
			{
				if (this.assemblyTypeCache == null)
				{
					this.assemblyTypeCache = this.assembly.GetTypes();
				}
				return this.assemblyTypeCache;
			}

			// Token: 0x04000062 RID: 98
			public readonly Assembly assembly;

			// Token: 0x04000063 RID: 99
			public readonly AssemblyName assemblyName;

			// Token: 0x04000064 RID: 100
			public readonly AssemblyName[] referencedAssemblyNames;

			// Token: 0x04000065 RID: 101
			public readonly List<Assembly> referencingAssemblies;

			// Token: 0x04000066 RID: 102
			private Type[] assemblyTypeCache;
		}
	}
}
