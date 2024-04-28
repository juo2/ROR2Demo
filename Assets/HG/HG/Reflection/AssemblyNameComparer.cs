using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HG.Reflection
{
	// Token: 0x0200001A RID: 26
	public class AssemblyNameComparer : IEqualityComparer<AssemblyName>
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000049B3 File Offset: 0x00002BB3
		public AssemblyNameComparer(bool useCache = true)
		{
			this.useCache = useCache;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000049C2 File Offset: 0x00002BC2
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000049CA File Offset: 0x00002BCA
		public bool useCache
		{
			get
			{
				return this._useCache;
			}
			set
			{
				if (this._useCache == value)
				{
					return;
				}
				this._useCache = value;
				if (this._useCache)
				{
					this.nameCache = (this.nameCache ?? new ConditionalWeakTable<AssemblyName, string>());
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000049FC File Offset: 0x00002BFC
		private string GetAssemblyNameFullNameString(AssemblyName assemblyName)
		{
			if (this.useCache)
			{
				string fullName;
				if (!this.nameCache.TryGetValue(assemblyName, out fullName))
				{
					fullName = assemblyName.FullName;
					this.nameCache.Add(assemblyName, fullName);
				}
				return fullName;
			}
			return assemblyName.FullName;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004A3D File Offset: 0x00002C3D
		public bool Equals(AssemblyName x, AssemblyName y)
		{
			return AssemblyName.ReferenceMatchesDefinition(x, y);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004A46 File Offset: 0x00002C46
		public int GetHashCode(AssemblyName assemblyName)
		{
			return this.GetAssemblyNameFullNameString(assemblyName).GetHashCode();
		}

		// Token: 0x0400002E RID: 46
		private ConditionalWeakTable<AssemblyName, string> nameCache;

		// Token: 0x0400002F RID: 47
		private bool _useCache;
	}
}
