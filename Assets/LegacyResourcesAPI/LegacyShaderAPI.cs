using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RoR2
{
	// Token: 0x02000003 RID: 3
	public static class LegacyShaderAPI
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00015A9C File Offset: 0x00013C9C
		public static Shader Find(string shaderName)
		{
			Shader result;
			if (LegacyShaderAPI.shaderNameToShaderCache.TryGetValue(shaderName, out result))
			{
				return result;
			}
			string text;
			if (!LegacyShaderAPI.nameToGuid.TryGetValue(shaderName, out text))
			{
				throw new Exception("No GUID record is available for shader name. shaderName=\"" + shaderName + "\"");
			}
			Shader shader = null;
			try
			{
				AsyncOperationHandle<Shader> asyncOperationHandle = default(AsyncOperationHandle<Shader>);
				LegacyShaderAPI.<>c__DisplayClass3_0 CS$<>8__locals1 = new LegacyShaderAPI.<>c__DisplayClass3_0();
				CS$<>8__locals1.oldResourceManagerExceptionHandler = ResourceManager.ExceptionHandler;
				ResourceManager.ExceptionHandler = new Action<AsyncOperationHandle, Exception>(CS$<>8__locals1.<Find>g__ResourceManagerExceptionHandler|0);
				try
				{
					asyncOperationHandle = Addressables.LoadAssetAsync<Shader>(text);
				}
				finally
				{
					ResourceManager.ExceptionHandler = CS$<>8__locals1.oldResourceManagerExceptionHandler;
				}
				if (asyncOperationHandle.IsValid())
				{
					LegacyShaderAPI.timeoutStopwatch.Restart();
					while (!asyncOperationHandle.IsDone)
					{
						if (asyncOperationHandle.PercentComplete >= 0.5f)
						{
							asyncOperationHandle.WaitForCompletion();
						}
					}
					LegacyShaderAPI.timeoutStopwatch.Stop();
					if (asyncOperationHandle.Result)
					{
						shader = asyncOperationHandle.Result;
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			if (!shader)
			{
				throw new Exception("Shader could not be loaded. shaderName=\"" + shaderName + "\" shaderGuid=" + text);
			}
			LegacyShaderAPI.shaderNameToShaderCache.Add(shaderName, shader);
			return shader;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00015BCC File Offset: 0x00013DCC
		// Note: this type is marked as 'beforefieldinit'.
		static LegacyShaderAPI()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["Hidden/UnderWaterFog"] = "472da7a0e387cf14abf12a99c96998fd";
			dictionary["CalmWater/Calm Water [Double Sided]"] = "64cc94e88500a904084a18cf5fcb2991";
			dictionary["CalmWater/Calm Water [DX11] [Double Sided]"] = "c960b2fa5c6943a47bf5f9dbab77ea35";
			dictionary["CalmWater/Calm Water [DX11]"] = "2e5d60e33aa6da447bb69ec37053c5ba";
			dictionary["CalmWater/Calm Water [Single Sided]"] = "5113337feed5cb641a5590972b07b9c1";
			dictionary["Projector/Caustics"] = "28260a8d98706fe40a3c99f2f781b3cb";
			dictionary["Hidden/Decalicious Game Object ID"] = "af51ca0e06c840e4b9efd34efed7cc85";
			dictionary["Decalicious/Deferred Decal"] = "45d0c9045cf6b7f44b2550a667173c5f";
			dictionary["Decalicious/Unlit Decal"] = "3f698aa8b620b764d962905c27b9f1ae";
			dictionary["Hidden/EfficientBlur"] = "12e87e7c7fde8e74db9f75172456c5c3";
			dictionary["Hidden/FillCrop"] = "d996ab4381100014d99e25e68e9aac84";
			dictionary["UI/TranslucentImage"] = "b1115addd36579a429d5e6b4ffae668d";
			dictionary["Hidden/NGSS_ContactShadows"] = "ab1a11124b683ad44bcb86515a434172";
			dictionary["Hidden/NGSS_Directional"] = "d42da1e69d53bc044ad4a55ce9a410c1";
			dictionary["Custom/Fill-Linear"] = "4aa2a800cd44e904fbd41952f82c6370";
			dictionary["Hidden/PostProcessing/HopooSSR"] = "c2c5464695b95d34c833a4c0642e7532";
			dictionary["Hidden/PostProcess/RampFog"] = "c14b6abb309b8c246add9e94a6920d7f";
			dictionary["Hidden/PostProcess/SobelOutline"] = "7d6541fe01591cd418caf48d3c099e52";
			dictionary["Hidden/PostProcess/SobelRain"] = "b7d96c4da51704442b8cfedb1872b45f";
			dictionary["Hopoo Games/Deferred/Snow Topped"] = "ec2c273472427df41846b25c110155c2";
			dictionary["Hopoo Games/Deferred/Standard"] = "48dca5b99d113b8d11006bab44295342";
			dictionary["Hopoo Games/Deferred/Triplanar Terrain Blend"] = "cd44d5076b47fbc4d8872b2a500b78f8";
			dictionary["Hopoo Games/Deferred/Wavy Cloth"] = "69d9da0a01c9f774e8e80f16ecd381b0";
			dictionary["Hidden/Internal-DeferredReflections"] = "441e313ad6852fb47825253de68f351f";
			dictionary["Hidden/Internal-DeferredReflections"] = "40eea4bc126f35642be7798d96f9f7c1";
			dictionary["Hidden/Internal-DeferredShading"] = "5b1b52b10f4dd5047b8400977ff4c0d7";
			dictionary["Hopoo Games/Environment/Distant Water"] = "d48a4aa52cd665f45a89801d053c38de";
			dictionary["Hopoo Games/Environment/Waving Grass"] = "cb13e29f56673694cbaeb73c22d3cd1c";
			dictionary["Hopoo Games/Environment/Waterfall"] = "38f45f1c98f056f4ca78cb3f37bcc47d";
			dictionary["Hopoo Games/FX/Cloud Remap"] = "bbffe49749c91724d819563daf91445d";
			dictionary["Hopoo Games/FX/Damage Number"] = "7d8dad6ac5790494cafac7c5a3fcb748";
			dictionary["Hopoo Games/FX/Distortion"] = "f6bd449dcf2a4496da3d2ad0c3881450";
			dictionary["Hopoo Games/FX/Forward Planet"] = "94b2ede73cf555f4f8549dc24b957446";
			dictionary["Hopoo Games/FX/Cloud Intersection Remap"] = "43a6c7a9084ef9743ab45ee8d5f3c4e9";
			dictionary["Hopoo Games/FX/Opaque Cloud Remap"] = "a035a371a79a19c468ec4e6dc40911c5";
			dictionary["Hopoo Games/FX/Solid Parallax"] = "302e1057ea9d0e74dab5a0de5cbf611c";
			dictionary["Hopoo Games/FX/Vertex Colors Only"] = "3b2fa336b2f421746a875f53f075d95f";
			dictionary["Hidden/FastBlur"] = "f9d5fa183cd6b45eeb1491f74863cd91";
			dictionary["Hopoo Games/Internal/Outline Highlight"] = "41e90cbc5ad198a438128b019f8d8553";
			dictionary["Hopoo Games/Post Process/Scope Distortion"] = "c59dc7ff2931ab5409d6ab6a95e504fb";
			dictionary["Hopoo Games/Post Process/Screen Damage"] = "62decc840d5afaa49886d8b13165939e";
			dictionary["Hopoo Games/Internal/SobelBuffer"] = "780f79b2a62a0df439dedf59c533eee6";
			dictionary["Nature/SpeedTree Billboard"] = "72ed34a768bf9604bbfe1e9fe1edbbb2";
			dictionary["Nature/SpeedTree"] = "85eebb34728e99141abefe9e3f234e55";
			dictionary["Nature/Tree Soft Occlusion Bark"] = "cea4b848c6b05db458c71e5ad1a005b3";
			dictionary["Nature/Tree Soft Occlusion Leaves"] = "4420bd398c9be744082b5d2458829746";
			dictionary["Hopoo Games/UI/Animate Alpha"] = "de7d3aae599afd94491a236cc750f320";
			dictionary["Hopoo Games/UI/UI Bar Remap"] = "fc58d915158fa30429e09867bf1a1929";
			dictionary["Hopoo Games/UI/Masked UI Blur"] = "84464b76c258972459d5ed8d5a875d0b";
			dictionary["Hopoo Games/UI/Custom Blend"] = "6ecc105be1511d240b11e32eb05f80e8";
			dictionary["Hopoo Games/UI/Debug Ignore Z"] = "36643fea3e3d2a844ac2e6e777f5b249";
			dictionary["Hopoo Games/UI/Default Overbrighten"] = "cf1ebead4b1ec6c4591b2066773bffd7";
			dictionary["Projector/Light"] = "c0ace1ca4bc0718448acf798c93d52d9";
			dictionary["Projector/Multiply"] = "01a668cc78047034a8a0c5ca2d24c6f7";
			dictionary["TextMeshPro/Bitmap Custom Atlas"] = "48bb5f55d8670e349b6e614913f9d910";
			dictionary["TextMeshPro/Mobile/Bitmap"] = "1e3b057af24249748ff873be7fafee47";
			dictionary["TextMeshPro/Bitmap"] = "128e987d567d4e2c824d754223b3f3b0";
			dictionary["TextMeshPro/Distance Field Overlay"] = "dd89cf5b9246416f84610a006f916af7";
			dictionary["TextMeshPro/Mobile/Distance Field - Masking"] = "bc1ede39bf3643ee8e493720e4259791";
			dictionary["TextMeshPro/Mobile/Distance Field Overlay"] = "a02a7d8c237544f1962732b55a9aebf1";
			dictionary["TextMeshPro/Mobile/Distance Field"] = "fe393ace9b354375a9cb14cdbbc28be4";
			dictionary["TextMeshPro/Mobile/Distance Field (Surface)"] = "85187c2149c549c5b33f0cdb02836b17";
			dictionary["TextMeshPro/Distance Field (Surface)"] = "f7ada0af4f174f0694ca6a487b8f543d";
			dictionary["TextMeshPro/Distance Field"] = "68e6db2ebdc24f95958faec2be5558d6";
			dictionary["TextMeshPro/Sprite"] = "cf81c85f95fe47e1a27f6ae460cf182c";
			dictionary["TextMeshPro/Distance Field SSD"] = "14eb328de4b8eb245bb7cea29e4ac00b";
			dictionary["TextMeshPro/Mobile/Distance Field SSD"] = "c8d12adcee749c344b8117cf7c7eb912";
			dictionary["Hidden/VertexPainterPro_Preview"] = "54ade7f6a9261fe4aadcf809dcc7d2c7";
			dictionary["Hidden/UnlitTransparentColored"] = "2df96b66b5510f94b98df2ac6f926ef4";
			dictionary["Hidden/LookDev/Compositor"] = "4386e57b23a56004c93e1d57d2bbcb4f";
			dictionary["Hidden/LookDev/CubeToLatlong"] = "a16365e8c873daa4c94919438490b905";
			dictionary["Hidden/PostProcessing/Bloom"] = "c1e1d3119c6fd4646aea0b4b74cacc1a";
			dictionary["Hidden/PostProcessing/Copy"] = "cdbdb71de5f9c454b980f6d0e87f0afb";
			dictionary["Hidden/PostProcessing/CopyStd"] = "4bf4cff0d0bac3d43894e2e8839feb40";
			dictionary["Hidden/PostProcessing/CopyStdFromDoubleWide"] = "e8ce9961912f3214586fe8709b9012c1";
			dictionary["Hidden/PostProcessing/CopyStdFromTexArray"] = "02d2da9bc88d25c4d878c1ed4e0b3854";
			dictionary["Hidden/PostProcessing/DeferredFog"] = "4117fce9491711c4094d33a048e36e73";
			dictionary["Hidden/PostProcessing/DepthOfField"] = "0ef78d24e85a44f4da9d5b5eaa00e50b";
			dictionary["Hidden/PostProcessing/DiscardAlpha"] = "5ab0816423f0dfe45841cab3b05ec9ef";
			dictionary["Hidden/PostProcessing/FinalPass"] = "f75014305794b3948a3c6d5ccd550e05";
			dictionary["Hidden/PostProcessing/GrainBaker"] = "0d8afcb51cc9f0349a6d190da929b838";
			dictionary["Hidden/PostProcessing/Lut2DBaker"] = "7ad194cbe7d006f4bace915156972026";
			dictionary["Hidden/PostProcessing/MotionBlur"] = "2c459b89a7c8b1a4fbefe0d81341651c";
			dictionary["Hidden/PostProcessing/MultiScaleVO"] = "67f9497810829eb4791ec19e95781e51";
			dictionary["Hidden/PostProcessing/ScalableAO"] = "d7640629310e79646af0f46eb55ae466";
			dictionary["Hidden/PostProcessing/ScreenSpaceReflections"] = "f997a3dc9254c44459323cced085150c";
			dictionary["Hidden/PostProcessing/SubpixelMorphologicalAntialiasing"] = "81af42a93ade3dd46a9b583d4eec76d6";
			dictionary["Hidden/PostProcessing/TemporalAntialiasing"] = "51bcf79c50dc92e47ba87821b61100c3";
			dictionary["Hidden/PostProcessing/Texture2DLerp"] = "34a819c9e33402547a81619693adc8d5";
			dictionary["Hidden/PostProcessing/Uber"] = "382151503e2a43a4ebb7366d1632731d";
			dictionary["Hidden/PostProcessing/Debug/Histogram"] = "f7ea35cfb33fcad4ab8f2429ec103bef";
			dictionary["Hidden/PostProcessing/Debug/LightMeter"] = "b34a29e523cb9d545881e193a079f2df";
			dictionary["Hidden/PostProcessing/Debug/Overlays"] = "b958ad1c92bd3d64c9e61318b8681dab";
			dictionary["Hidden/PostProcessing/Debug/Vectorscope"] = "a71093f2a4fe26a40805c22739e10e4a";
			dictionary["Hidden/PostProcessing/Debug/Waveform"] = "3020ac7ece79a7f4eb789a236f8bd6c5";
			dictionary["Hidden/PostProcessing/Editor/ConvertToLog"] = "fe144c7e314047f4bb779d555c5405ac";
			dictionary["Hidden/PostProcessing/Editor/CurveGrid"] = "5bb6ef6f3e1b20348b4fdb01e4c404e2";
			dictionary["Hidden/PostProcessing/Editor/Trackball"] = "de7f3ac52268a194383c7d62c2a343c1";
			dictionary["ProBuilder/Diffuse Texture Blend"] = "33cb4e8ff0c1d43ac8ec40692656c7ed";
			dictionary["ProBuilder/Diffuse Vertex Color"] = "911130a939bf84843bcc4211c327f579";
			dictionary["Hidden/ProBuilder/EdgePicker"] = "86212504c7e9f468db2300dc5932dc17";
			dictionary["Hidden/ProBuilder/EdgePicker"] = "f1f8c9218a014514ab6f04436ad4c25a";
			dictionary["Hidden/ProBuilder/FaceHighlight"] = "6f37d9e45fbae41b386f76d3bfefec4a";
			dictionary["Hidden/ProBuilder/FacePicker"] = "c84d57d423a2042eb92da86f9fa670a3";
			dictionary["Hidden/ProBuilder/FacePickerHDRP"] = "d632b247f24ee384c9fbd156e34ea930";
			dictionary["Hidden/ProBuilder/HideVertices"] = "cfa7edf01005f4e338d973cea2f6eab3";
			dictionary["Hidden/ProBuilder/LineBillboard"] = "4293210a3280c4283b9872316017f1f9";
			dictionary["Hidden/ProBuilder/LineBillboardMetal"] = "4300ae919783d4fa38001d745fbdecc1";
			dictionary["Hidden/ProBuilder/NormalPreview"] = "a07688e921e974338945464731e069ce";
			dictionary["Hidden/ProBuilder/PointBillboard"] = "8ca8b34aa013842f3b399b6961e7dc3b";
			dictionary["ProBuilder/Reference Unlit"] = "209f4c3496c1b46a3a2f9f0f27be788f";
			dictionary["Hidden/ProBuilder/ScrollHighlight"] = "04b5dda5f278e4f56a9dbda64d90bb95";
			dictionary["Hidden/ProBuilder/SelectionPicker"] = "1fd883d610b2f42a98fc7abaf6809430";
			dictionary["Hidden/ProBuilder/SmoothingPreview"] = "e8c3a1662a88140ae84d69a3a4cfdbaa";
			dictionary["ProBuilder/Standard Vertex Color"] = "1ccecd9b89b8a4f14bfb64f29ddfcc81";
			dictionary["Hidden/ProBuilder/TransparentOverlay"] = "25b8a80cb03994c769145599929a5e97";
			dictionary["ProBuilder/Unlit Solid Color"] = "24f9f312f97bd48dba1a728d58c89842";
			dictionary["ProBuilder/UnlitVertexColor"] = "35e778fba8b2b49a198d8d7cfced67eb";
			dictionary["Hidden/ProBuilder/VertexPicker"] = "c26db15b9a2ed4b2ebefefd26ce85ae1";
			dictionary["Hidden/ProBuilder/VertexPickerHDRP"] = "00e56516c5ee19442bc12a25d21ca18d";
			dictionary["Hidden/ProBuilder/VertexShader"] = "d6e5744ac426949ff8c0f02936689d4c";
			dictionary["Hidden/TMP/Internal/Editor/Distance Field SSD"] = "9c442dc870b456e48b615cd8add0e9ef";
			dictionary["Hopoo Games/Internal/VisionLimit"] = "5e246b703e2090148a761d52af19744b";
			LegacyShaderAPI.nameToGuid = dictionary;
		}

		// Token: 0x04000002 RID: 2
		private static readonly Dictionary<string, Shader> shaderNameToShaderCache = new Dictionary<string, Shader>();

		// Token: 0x04000003 RID: 3
		private static readonly Stopwatch timeoutStopwatch = new Stopwatch();

		// Token: 0x04000004 RID: 4
		private static readonly float timeoutDuration = 3f;

		// Token: 0x04000005 RID: 5
		private static Dictionary<string, string> nameToGuid;
	}
}
