using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009F4 RID: 2548
	public class ProjectIssueChecker
	{
		// Token: 0x06003AF9 RID: 15097 RVA: 0x000F430D File Offset: 0x000F250D
		private static IEnumerable<Assembly> GetAssemblies()
		{
			List<string> list = new List<string>();
			Stack<Assembly> stack = new Stack<Assembly>();
			stack.Push(Assembly.GetEntryAssembly());
			do
			{
				Assembly asm = stack.Pop();
				yield return asm;
				foreach (AssemblyName assemblyName in asm.GetReferencedAssemblies())
				{
					if (!list.Contains(assemblyName.FullName))
					{
						stack.Push(Assembly.Load(assemblyName));
						list.Add(assemblyName.FullName);
					}
				}
				asm = null;
			}
			while (stack.Count > 0);
			yield break;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000F4318 File Offset: 0x000F2518
		private ProjectIssueChecker()
		{
			this.assetCheckMethods = new Dictionary<Type, List<MethodInfo>>();
			this.allChecks = new List<MethodInfo>();
			this.enabledChecks = new Dictionary<MethodInfo, bool>();
			Assembly[] source = new Assembly[]
			{
				typeof(GameObject).Assembly,
				typeof(Canvas).Assembly,
				typeof(RoR2Application).Assembly,
				typeof(TMP_Text).Assembly
			};
			ProjectIssueChecker.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.types = source.SelectMany((Assembly a) => a.GetTypes()).ToArray<Type>();
			Type[] types = CS$<>8__locals1.types;
			for (int i = 0; i < types.Length; i++)
			{
				foreach (MethodInfo methodInfo in types[i].GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					foreach (AssetCheckAttribute assetCheckAttribute in methodInfo.GetCustomAttributes<AssetCheckAttribute>())
					{
						Type assetType = assetCheckAttribute.assetType;
						this.<.ctor>g__AddMethodForTypeDescending|6_1(assetType, methodInfo, ref CS$<>8__locals1);
						if (!this.allChecks.Contains(methodInfo))
						{
							this.allChecks.Add(methodInfo);
							this.enabledChecks.Add(methodInfo, true);
						}
					}
				}
			}
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000F44C8 File Offset: 0x000F26C8
		private string GetCurrentAssetFullPath()
		{
			GameObject gameObject = null;
			string arg = "";
			if (this.currentAsset is GameObject)
			{
				gameObject = (GameObject)this.currentAsset;
			}
			else if (this.currentAsset is Component)
			{
				gameObject = ((Component)this.currentAsset).gameObject;
			}
			string arg2 = this.currentAsset ? this.currentAsset.name : "NULL ASSET";
			if (gameObject)
			{
				arg2 = Util.GetGameObjectHierarchyName(gameObject);
			}
			string arg3 = this.currentAsset ? this.currentAsset.GetType().Name : "VOID";
			return string.Format("{0}:{1}({2})", arg, arg2, arg3);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000F4578 File Offset: 0x000F2778
		public void Log(string message, UnityEngine.Object context = null)
		{
			this.log.Add(new ProjectIssueChecker.LogMessage
			{
				error = false,
				message = message,
				assetPath = this.GetCurrentAssetFullPath(),
				context = context
			});
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x000F45C0 File Offset: 0x000F27C0
		public void LogError(string message, UnityEngine.Object context = null)
		{
			this.log.Add(new ProjectIssueChecker.LogMessage
			{
				error = true,
				message = message,
				assetPath = this.GetCurrentAssetFullPath(),
				context = context
			});
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000F4608 File Offset: 0x000F2808
		public void LogFormat(UnityEngine.Object context, string format, params object[] args)
		{
			this.log.Add(new ProjectIssueChecker.LogMessage
			{
				error = false,
				message = string.Format(format, args),
				assetPath = this.GetCurrentAssetFullPath(),
				context = context
			});
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000F4654 File Offset: 0x000F2854
		public void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
		{
			this.log.Add(new ProjectIssueChecker.LogMessage
			{
				error = true,
				message = string.Format(format, args),
				assetPath = this.GetCurrentAssetFullPath(),
				context = context
			});
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000F46A0 File Offset: 0x000F28A0
		private void FlushLog()
		{
			bool flag = false;
			for (int i = 0; i < this.log.Count; i++)
			{
				if (this.log[i].error)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				foreach (ProjectIssueChecker.LogMessage logMessage in this.log)
				{
					if (logMessage.error)
					{
						Debug.LogErrorFormat(logMessage.context, "[\"{0}\"] {1}", new object[]
						{
							logMessage.assetPath,
							logMessage.message
						});
					}
					else
					{
						Debug.LogFormat(logMessage.context, "[\"{0}\"] {1}", new object[]
						{
							logMessage.assetPath,
							logMessage.message
						});
					}
				}
			}
			this.log.Clear();
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000F4788 File Offset: 0x000F2988
		[CompilerGenerated]
		private void <.ctor>g__AddMethodForType|6_0(Type t, MethodInfo methodInfo)
		{
			List<MethodInfo> list = null;
			this.assetCheckMethods.TryGetValue(t, out list);
			if (list == null)
			{
				list = new List<MethodInfo>();
				this.assetCheckMethods[t] = list;
			}
			list.Add(methodInfo);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000F47C4 File Offset: 0x000F29C4
		[CompilerGenerated]
		private void <.ctor>g__AddMethodForTypeDescending|6_1(Type t, MethodInfo methodInfo, ref ProjectIssueChecker.<>c__DisplayClass6_0 A_3)
		{
			foreach (Type t2 in A_3.types.Where(new Func<Type, bool>(t.IsAssignableFrom)))
			{
				this.<.ctor>g__AddMethodForType|6_0(t2, methodInfo);
			}
		}

		// Token: 0x040039B7 RID: 14775
		private Dictionary<Type, List<MethodInfo>> assetCheckMethods;

		// Token: 0x040039B8 RID: 14776
		private List<MethodInfo> allChecks;

		// Token: 0x040039B9 RID: 14777
		private Dictionary<MethodInfo, bool> enabledChecks;

		// Token: 0x040039BA RID: 14778
		private bool checkScenes = true;

		// Token: 0x040039BB RID: 14779
		private List<string> scenesToCheck = new List<string>();

		// Token: 0x040039BC RID: 14780
		private string currentAssetPath = "";

		// Token: 0x040039BD RID: 14781
		private readonly Stack<UnityEngine.Object> assetStack = new Stack<UnityEngine.Object>();

		// Token: 0x040039BE RID: 14782
		private UnityEngine.Object currentAsset;

		// Token: 0x040039BF RID: 14783
		private List<ProjectIssueChecker.LogMessage> log = new List<ProjectIssueChecker.LogMessage>();

		// Token: 0x040039C0 RID: 14784
		private string currentSceneName = "";

		// Token: 0x020009F5 RID: 2549
		private struct LogMessage
		{
			// Token: 0x040039C1 RID: 14785
			public bool error;

			// Token: 0x040039C2 RID: 14786
			public string message;

			// Token: 0x040039C3 RID: 14787
			public UnityEngine.Object context;

			// Token: 0x040039C4 RID: 14788
			public string assetPath;
		}
	}
}
