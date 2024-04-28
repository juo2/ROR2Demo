using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HG.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A81 RID: 2689
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[MeansImplicitUse]
	public class SystemInitializerAttribute : SearchableAttribute
	{
		// Token: 0x06003DC9 RID: 15817 RVA: 0x000FF17A File Offset: 0x000FD37A
		public SystemInitializerAttribute(params Type[] dependencies)
		{
			if (dependencies != null)
			{
				this.dependencies = dependencies;
			}
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000FF198 File Offset: 0x000FD398
		public static void Execute()
		{
			Queue<SystemInitializerAttribute> queue = new Queue<SystemInitializerAttribute>();
			foreach (SearchableAttribute searchableAttribute in SearchableAttribute.GetInstances<SystemInitializerAttribute>())
			{
				SystemInitializerAttribute systemInitializerAttribute = (SystemInitializerAttribute)searchableAttribute;
				MethodInfo methodInfo = systemInitializerAttribute.target as MethodInfo;
				if (methodInfo != null && methodInfo.IsStatic)
				{
					queue.Enqueue(systemInitializerAttribute);
					systemInitializerAttribute.methodInfo = methodInfo;
					systemInitializerAttribute.associatedType = methodInfo.DeclaringType;
				}
			}
			SystemInitializerAttribute.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.initializedTypes = new HashSet<Type>();
			SystemInitializerAttribute.SystemInitializationLogHandler systemInitializationLogHandler = new SystemInitializerAttribute.SystemInitializationLogHandler();
			ILogHandler logHandler = Debug.unityLogger.logHandler;
			systemInitializationLogHandler.underlyingLogHandler = logHandler;
			int num = 0;
			while (queue.Count > 0)
			{
				SystemInitializerAttribute systemInitializerAttribute2 = queue.Dequeue();
				if (!SystemInitializerAttribute.<Execute>g__InitializerDependenciesMet|6_0(systemInitializerAttribute2, ref CS$<>8__locals1))
				{
					queue.Enqueue(systemInitializerAttribute2);
					num++;
					if (num >= queue.Count)
					{
						Debug.LogFormat("SystemInitializerAttribute infinite loop detected. currentMethod={0}", new object[]
						{
							systemInitializerAttribute2.associatedType.FullName + systemInitializerAttribute2.methodInfo.Name
						});
						Debug.LogFormat("initializer dependencies = " + string.Join<Type>(",\n", systemInitializerAttribute2.dependencies), Array.Empty<object>());
						Debug.LogFormat("initialized types = " + string.Join<Type>(",\n", CS$<>8__locals1.initializedTypes), Array.Empty<object>());
						break;
					}
				}
				else
				{
					try
					{
						Debug.unityLogger.logHandler = systemInitializationLogHandler;
						systemInitializationLogHandler.currentInitializer = systemInitializerAttribute2;
						systemInitializerAttribute2.methodInfo.Invoke(null, Array.Empty<object>());
						CS$<>8__locals1.initializedTypes.Add(systemInitializerAttribute2.associatedType);
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					finally
					{
						Debug.unityLogger.logHandler = logHandler;
					}
					num = 0;
				}
			}
			SystemInitializerAttribute.hasExecuted = true;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000FF384 File Offset: 0x000FD584
		[CompilerGenerated]
		internal static bool <Execute>g__InitializerDependenciesMet|6_0(SystemInitializerAttribute initializerAttribute, ref SystemInitializerAttribute.<>c__DisplayClass6_0 A_1)
		{
			foreach (Type item in initializerAttribute.dependencies)
			{
				if (!A_1.initializedTypes.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003C93 RID: 15507
		public static bool hasExecuted;

		// Token: 0x04003C94 RID: 15508
		public Type[] dependencies = Array.Empty<Type>();

		// Token: 0x04003C95 RID: 15509
		private MethodInfo methodInfo;

		// Token: 0x04003C96 RID: 15510
		private Type associatedType;

		// Token: 0x02000A82 RID: 2690
		private class SystemInitializationLogHandler : ILogHandler
		{
			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000FF3BB File Offset: 0x000FD5BB
			// (set) Token: 0x06003DCE RID: 15822 RVA: 0x000FF3C3 File Offset: 0x000FD5C3
			public SystemInitializerAttribute currentInitializer
			{
				get
				{
					return this._currentInitializer;
				}
				set
				{
					this._currentInitializer = value;
					this.logPrefix = "[" + this.currentInitializer.associatedType.FullName + "] ";
				}
			}

			// Token: 0x06003DCF RID: 15823 RVA: 0x000FF3F1 File Offset: 0x000FD5F1
			public void LogException(Exception exception, UnityEngine.Object context)
			{
				this.LogFormat(LogType.Exception, context, exception.Message, null);
			}

			// Token: 0x06003DD0 RID: 15824 RVA: 0x000FF402 File Offset: 0x000FD602
			public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
			{
				this.underlyingLogHandler.LogFormat(logType, context, this.logPrefix + format, args);
			}

			// Token: 0x04003C97 RID: 15511
			public ILogHandler underlyingLogHandler;

			// Token: 0x04003C98 RID: 15512
			private SystemInitializerAttribute _currentInitializer;

			// Token: 0x04003C99 RID: 15513
			private string logPrefix = string.Empty;
		}
	}
}
