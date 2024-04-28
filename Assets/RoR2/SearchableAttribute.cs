using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x02000A34 RID: 2612
	[MeansImplicitUse]
	public abstract class SearchableAttribute : Attribute
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000F9901 File Offset: 0x000F7B01
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000F9909 File Offset: 0x000F7B09
		public object target { get; private set; }

		// Token: 0x06003C5F RID: 15455 RVA: 0x000F9914 File Offset: 0x000F7B14
		public static List<SearchableAttribute> GetInstances<T>() where T : SearchableAttribute
		{
			List<SearchableAttribute> result;
			if (SearchableAttribute.instancesListsByType.TryGetValue(typeof(T), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000F993C File Offset: 0x000F7B3C
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

		// Token: 0x06003C61 RID: 15457 RVA: 0x000F999C File Offset: 0x000F7B9C
		private static IEnumerable<Assembly> GetScannableAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
			where !SearchableAttribute.assemblyBlacklist.Contains(a.GetName().Name)
			select a;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000F99CC File Offset: 0x000F7BCC
		private static void Scan()
		{
			IEnumerable<Type> enumerable = SearchableAttribute.GetScannableAssemblies().SelectMany((Assembly a) => a.GetTypes());
			SearchableAttribute.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.allInstancesList = new List<SearchableAttribute>();
			CS$<>8__locals1.memoizedInput = null;
			CS$<>8__locals1.memoizedOutput = null;
			foreach (Type type in enumerable)
			{
				foreach (SearchableAttribute attribute in type.GetCustomAttributes(false))
				{
					SearchableAttribute.<Scan>g__Register|8_2(attribute, type, ref CS$<>8__locals1);
				}
				foreach (MemberInfo memberInfo in type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					foreach (SearchableAttribute attribute2 in memberInfo.GetCustomAttributes(false))
					{
						SearchableAttribute.<Scan>g__Register|8_2(attribute2, memberInfo, ref CS$<>8__locals1);
					}
				}
			}
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000F9AF8 File Offset: 0x000F7CF8
		static SearchableAttribute()
		{
			try
			{
				SearchableAttribute.Scan();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x000FA184 File Offset: 0x000F8384
		[CompilerGenerated]
		internal static List<SearchableAttribute> <Scan>g__GetInstancesListForType|8_1(Type attributeType, ref SearchableAttribute.<>c__DisplayClass8_0 A_1)
		{
			if (attributeType.Equals(A_1.memoizedInput))
			{
				return A_1.memoizedOutput;
			}
			List<SearchableAttribute> list;
			if (!SearchableAttribute.instancesListsByType.TryGetValue(attributeType, out list))
			{
				list = new List<SearchableAttribute>();
				SearchableAttribute.instancesListsByType.Add(attributeType, list);
			}
			A_1.memoizedInput = attributeType;
			A_1.memoizedOutput = list;
			return list;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000FA1D6 File Offset: 0x000F83D6
		[CompilerGenerated]
		internal static void <Scan>g__Register|8_2(SearchableAttribute attribute, object target, ref SearchableAttribute.<>c__DisplayClass8_0 A_2)
		{
			attribute.target = target;
			A_2.allInstancesList.Add(attribute);
			SearchableAttribute.<Scan>g__GetInstancesListForType|8_1(attribute.GetType(), ref A_2).Add(attribute);
		}

		// Token: 0x04003BB6 RID: 15286
		private static readonly Dictionary<Type, List<SearchableAttribute>> instancesListsByType = new Dictionary<Type, List<SearchableAttribute>>();

		// Token: 0x04003BB7 RID: 15287
		private static HashSet<string> assemblyBlacklist = new HashSet<string>
		{
			"mscorlib",
			"UnityEngine",
			"UnityEngine.AIModule",
			"UnityEngine.ARModule",
			"UnityEngine.AccessibilityModule",
			"UnityEngine.AnimationModule",
			"UnityEngine.AssetBundleModule",
			"UnityEngine.AudioModule",
			"UnityEngine.BaselibModule",
			"UnityEngine.ClothModule",
			"UnityEngine.ClusterInputModule",
			"UnityEngine.ClusterRendererModule",
			"UnityEngine.CoreModule",
			"UnityEngine.CrashReportingModule",
			"UnityEngine.DirectorModule",
			"UnityEngine.FileSystemHttpModule",
			"UnityEngine.GameCenterModule",
			"UnityEngine.GridModule",
			"UnityEngine.HotReloadModule",
			"UnityEngine.IMGUIModule",
			"UnityEngine.ImageConversionModule",
			"UnityEngine.InputModule",
			"UnityEngine.JSONSerializeModule",
			"UnityEngine.LocalizationModule",
			"UnityEngine.ParticleSystemModule",
			"UnityEngine.PerformanceReportingModule",
			"UnityEngine.PhysicsModule",
			"UnityEngine.Physics2DModule",
			"UnityEngine.ProfilerModule",
			"UnityEngine.ScreenCaptureModule",
			"UnityEngine.SharedInternalsModule",
			"UnityEngine.SpatialTrackingModule",
			"UnityEngine.SpriteMaskModule",
			"UnityEngine.SpriteShapeModule",
			"UnityEngine.StreamingModule",
			"UnityEngine.StyleSheetsModule",
			"UnityEngine.SubstanceModule",
			"UnityEngine.TLSModule",
			"UnityEngine.TerrainModule",
			"UnityEngine.TerrainPhysicsModule",
			"UnityEngine.TextCoreModule",
			"UnityEngine.TextRenderingModule",
			"UnityEngine.TilemapModule",
			"UnityEngine.TimelineModule",
			"UnityEngine.UIModule",
			"UnityEngine.UIElementsModule",
			"UnityEngine.UNETModule",
			"UnityEngine.UmbraModule",
			"UnityEngine.UnityAnalyticsModule",
			"UnityEngine.UnityConnectModule",
			"UnityEngine.UnityTestProtocolModule",
			"UnityEngine.UnityWebRequestModule",
			"UnityEngine.UnityWebRequestAssetBundleModule",
			"UnityEngine.UnityWebRequestAudioModule",
			"UnityEngine.UnityWebRequestTextureModule",
			"UnityEngine.UnityWebRequestWWWModule",
			"UnityEngine.VFXModule",
			"UnityEngine.VRModule",
			"UnityEngine.VehiclesModule",
			"UnityEngine.VideoModule",
			"UnityEngine.WindModule",
			"UnityEngine.XRModule",
			"UnityEditor",
			"Unity.Locator",
			"System.Core",
			"System",
			"Mono.Security",
			"System.Configuration",
			"System.Xml",
			"Unity.DataContract",
			"Unity.PackageManager",
			"UnityEngine.UI",
			"UnityEditor.UI",
			"UnityEditor.TestRunner",
			"UnityEngine.TestRunner",
			"nunit.framework",
			"UnityEngine.Timeline",
			"UnityEditor.Timeline",
			"UnityEngine.Networking",
			"UnityEditor.Networking",
			"UnityEditor.GoogleAudioSpatializer",
			"UnityEngine.GoogleAudioSpatializer",
			"UnityEditor.SpatialTracking",
			"UnityEngine.SpatialTracking",
			"UnityEditor.VR",
			"UnityEditor.Graphs",
			"UnityEditor.WindowsStandalone.Extensions",
			"SyntaxTree.VisualStudio.Unity.Bridge",
			"Rewired_ControlMapper_CSharp_Editor",
			"Rewired_CSharp_Editor",
			"Unity.ProBuilder.AddOns.Editor",
			"Wwise-Editor",
			"Unity.RenderPipelines.Core.Editor",
			"Unity.RenderPipelines.Core.Runtime",
			"Unity.TextMeshPro.Editor",
			"Unity.PackageManagerUI.Editor",
			"Rewired_NintendoSwitch_CSharp",
			"Unity.Postprocessing.Editor",
			"Rewired_CSharp",
			"Unity.Postprocessing.Runtime",
			"Rewired_NintendoSwitch_CSharp_Editor",
			"Wwise",
			"Unity.RenderPipelines.Core.ShaderLibrary",
			"Unity.TextMeshPro",
			"Rewired_UnityUI_CSharp_Editor",
			"Facepunch.Steamworks",
			"Rewired_Editor",
			"Rewired_Core",
			"Rewired_Windows_Lib",
			"Rewired_NintendoSwitch_Editor",
			"Rewired_NintendoSwitch_EditorRuntime",
			"Zio",
			"AssetIdRemapUtility",
			"ProBuilderCore",
			"ProBuilderMeshOps",
			"KdTreeLib",
			"pb_Stl",
			"Poly2Tri",
			"ProBuilderEditor",
			"netstandard",
			"System.Xml.Linq",
			"Unity.Cecil",
			"Unity.SerializationLogic",
			"Unity.Legacy.NRefactory",
			"ExCSS.Unity",
			"Unity.IvyParser",
			"UnityEditor.iOS.Extensions.Xcode",
			"SyntaxTree.VisualStudio.Unity.Messaging",
			"Microsoft.GeneratedCode",
			"Anonymously",
			"Hosted",
			"DynamicMethods",
			"Assembly",
			"UnityEditor.Switch.Extensions"
		};
	}
}
