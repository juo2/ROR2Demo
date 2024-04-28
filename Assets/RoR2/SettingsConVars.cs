using System;
using System.Globalization;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2
{
	// Token: 0x02000A3D RID: 2621
	public static class SettingsConVars
	{
		// Token: 0x04003BCD RID: 15309
		public static readonly BoolConVar cvExpAndMoneyEffects = new BoolConVar("exp_and_money_effects", ConVarFlags.Archive, "1", "Whether or not to create effects for experience and money from defeating monsters.");

		// Token: 0x04003BCE RID: 15310
		public static BoolConVar enableDamageNumbers = new BoolConVar("enable_damage_numbers", ConVarFlags.Archive, "1", "Whether or not damage and healing numbers spawn.");

		// Token: 0x02000A3E RID: 2622
		private class VSyncCountConVar : BaseConVar
		{
			// Token: 0x06003C77 RID: 15479 RVA: 0x00009F73 File Offset: 0x00008173
			private VSyncCountConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C78 RID: 15480 RVA: 0x000FA514 File Offset: 0x000F8714
			public override void SetString(string newValue)
			{
				int vSyncCount;
				if (TextSerialization.TryParseInvariant(newValue, out vSyncCount))
				{
					QualitySettings.vSyncCount = vSyncCount;
				}
			}

			// Token: 0x06003C79 RID: 15481 RVA: 0x000FA531 File Offset: 0x000F8731
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.vSyncCount);
			}

			// Token: 0x04003BCF RID: 15311
			private static SettingsConVars.VSyncCountConVar instance = new SettingsConVars.VSyncCountConVar("vsync_count", ConVarFlags.Archive | ConVarFlags.Engine, null, "Vsync count.");
		}

		// Token: 0x02000A3F RID: 2623
		private class WindowModeConVar : BaseConVar
		{
			// Token: 0x06003C7B RID: 15483 RVA: 0x00009F73 File Offset: 0x00008173
			private WindowModeConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C7C RID: 15484 RVA: 0x000FA558 File Offset: 0x000F8758
			public override void SetString(string newValue)
			{
				try
				{
					FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
					switch ((SettingsConVars.WindowModeConVar.WindowMode)Enum.Parse(typeof(SettingsConVars.WindowModeConVar.WindowMode), newValue, true))
					{
					case SettingsConVars.WindowModeConVar.WindowMode.Fullscreen:
						fullScreenMode = FullScreenMode.FullScreenWindow;
						break;
					case SettingsConVars.WindowModeConVar.WindowMode.Window:
						fullScreenMode = FullScreenMode.Windowed;
						break;
					case SettingsConVars.WindowModeConVar.WindowMode.FullscreenExclusive:
						fullScreenMode = FullScreenMode.ExclusiveFullScreen;
						break;
					}
					Screen.fullScreenMode = fullScreenMode;
				}
				catch (ArgumentException)
				{
					Console.ShowHelpText(this.name);
				}
			}

			// Token: 0x06003C7D RID: 15485 RVA: 0x000FA5C4 File Offset: 0x000F87C4
			public override string GetString()
			{
				SettingsConVars.WindowModeConVar.WindowMode windowMode;
				switch (Screen.fullScreenMode)
				{
				case FullScreenMode.ExclusiveFullScreen:
					windowMode = SettingsConVars.WindowModeConVar.WindowMode.FullscreenExclusive;
					break;
				case FullScreenMode.FullScreenWindow:
					windowMode = SettingsConVars.WindowModeConVar.WindowMode.Fullscreen;
					break;
				case FullScreenMode.MaximizedWindow:
					windowMode = SettingsConVars.WindowModeConVar.WindowMode.Window;
					break;
				case FullScreenMode.Windowed:
					windowMode = SettingsConVars.WindowModeConVar.WindowMode.Window;
					break;
				default:
					windowMode = SettingsConVars.WindowModeConVar.WindowMode.Fullscreen;
					break;
				}
				return windowMode.ToString();
			}

			// Token: 0x04003BD0 RID: 15312
			private static SettingsConVars.WindowModeConVar instance = new SettingsConVars.WindowModeConVar("window_mode", ConVarFlags.Archive | ConVarFlags.Engine, null, "The window mode. Choices are Fullscreen and Window.");

			// Token: 0x02000A40 RID: 2624
			private enum WindowMode
			{
				// Token: 0x04003BD2 RID: 15314
				Fullscreen,
				// Token: 0x04003BD3 RID: 15315
				Window,
				// Token: 0x04003BD4 RID: 15316
				FullscreenExclusive
			}
		}

		// Token: 0x02000A41 RID: 2625
		private class ResolutionConVar : BaseConVar
		{
			// Token: 0x06003C7F RID: 15487 RVA: 0x00009F73 File Offset: 0x00008173
			private ResolutionConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C80 RID: 15488 RVA: 0x000FA628 File Offset: 0x000F8828
			public override void SetString(string newValue)
			{
				string[] array = newValue.Split(new char[]
				{
					'x'
				});
				int width;
				if (array.Length < 1 || !TextSerialization.TryParseInvariant(array[0], out width))
				{
					throw new ConCommandException("Invalid resolution format. No width integer. Example: \"1920x1080x60\".");
				}
				int height;
				if (array.Length < 2 || !TextSerialization.TryParseInvariant(array[1], out height))
				{
					throw new ConCommandException("Invalid resolution format. No height integer. Example: \"1920x1080x60\".");
				}
				int preferredRefreshRate;
				if (array.Length < 3 || !TextSerialization.TryParseInvariant(array[2], out preferredRefreshRate))
				{
					throw new ConCommandException("Invalid resolution format. No refresh rate integer. Example: \"1920x1080x60\".");
				}
				Screen.SetResolution(width, height, Screen.fullScreenMode, preferredRefreshRate);
			}

			// Token: 0x06003C81 RID: 15489 RVA: 0x000FA6AC File Offset: 0x000F88AC
			public override string GetString()
			{
				Resolution currentResolution = Screen.currentResolution;
				return string.Format(CultureInfo.InvariantCulture, "{0}x{1}x{2}", Screen.width, Screen.height, currentResolution.refreshRate);
			}

			// Token: 0x06003C82 RID: 15490 RVA: 0x000FA6F0 File Offset: 0x000F88F0
			[ConCommand(commandName = "resolution_list", flags = ConVarFlags.None, helpText = "Prints a list of all possible resolutions for the current display.")]
			private static void CCResolutionList(ConCommandArgs args)
			{
				Resolution[] resolutions = Screen.resolutions;
				string[] array = new string[resolutions.Length];
				for (int i = 0; i < resolutions.Length; i++)
				{
					Resolution resolution = resolutions[i];
					array[i] = string.Format("{0}x{1}x{2}", resolution.width, resolution.height, resolution.refreshRate);
				}
				Debug.Log(string.Join("\n", array));
			}

			// Token: 0x04003BD5 RID: 15317
			private static SettingsConVars.ResolutionConVar instance = new SettingsConVars.ResolutionConVar("resolution", ConVarFlags.Archive | ConVarFlags.Engine, null, "The resolution of the game window. Format example: 1920x1080x60");
		}

		// Token: 0x02000A42 RID: 2626
		private class FpsMaxConVar : BaseConVar
		{
			// Token: 0x06003C84 RID: 15492 RVA: 0x00009F73 File Offset: 0x00008173
			private FpsMaxConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C85 RID: 15493 RVA: 0x000FA77C File Offset: 0x000F897C
			public override void SetString(string newValue)
			{
				int targetFrameRate;
				if (TextSerialization.TryParseInvariant(newValue, out targetFrameRate))
				{
					Application.targetFrameRate = targetFrameRate;
				}
			}

			// Token: 0x06003C86 RID: 15494 RVA: 0x000FA799 File Offset: 0x000F8999
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(Application.targetFrameRate);
			}

			// Token: 0x04003BD6 RID: 15318
			private static SettingsConVars.FpsMaxConVar instance = new SettingsConVars.FpsMaxConVar("fps_max", ConVarFlags.Archive, "60", "Maximum FPS. -1 is unlimited.");
		}

		// Token: 0x02000A43 RID: 2627
		private class ShadowsConVar : BaseConVar
		{
			// Token: 0x06003C88 RID: 15496 RVA: 0x00009F73 File Offset: 0x00008173
			private ShadowsConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C89 RID: 15497 RVA: 0x000FA7C4 File Offset: 0x000F89C4
			public override void SetString(string newValue)
			{
				try
				{
					QualitySettings.shadows = (ShadowQuality)Enum.Parse(typeof(ShadowQuality), newValue, true);
				}
				catch (ArgumentException)
				{
					Console.ShowHelpText(this.name);
				}
			}

			// Token: 0x06003C8A RID: 15498 RVA: 0x000FA80C File Offset: 0x000F8A0C
			public override string GetString()
			{
				return QualitySettings.shadows.ToString();
			}

			// Token: 0x04003BD7 RID: 15319
			private static SettingsConVars.ShadowsConVar instance = new SettingsConVars.ShadowsConVar("r_shadows", ConVarFlags.Archive | ConVarFlags.Engine, null, "Shadow quality. Can be \"All\" \"HardOnly\" or \"Disable\"");
		}

		// Token: 0x02000A44 RID: 2628
		private class SoftParticlesConVar : BaseConVar
		{
			// Token: 0x06003C8C RID: 15500 RVA: 0x00009F73 File Offset: 0x00008173
			private SoftParticlesConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C8D RID: 15501 RVA: 0x000FA848 File Offset: 0x000F8A48
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					QualitySettings.softParticles = (num != 0);
				}
			}

			// Token: 0x06003C8E RID: 15502 RVA: 0x000FA868 File Offset: 0x000F8A68
			public override string GetString()
			{
				if (!QualitySettings.softParticles)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x04003BD8 RID: 15320
			private static SettingsConVars.SoftParticlesConVar instance = new SettingsConVars.SoftParticlesConVar("r_softparticles", ConVarFlags.Archive | ConVarFlags.Engine, null, "Whether or not soft particles are enabled.");
		}

		// Token: 0x02000A45 RID: 2629
		private class FoliageWindConVar : BaseConVar
		{
			// Token: 0x06003C90 RID: 15504 RVA: 0x00009F73 File Offset: 0x00008173
			private FoliageWindConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C91 RID: 15505 RVA: 0x000FA898 File Offset: 0x000F8A98
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					if (num >= 1)
					{
						Shader.EnableKeyword("ENABLE_WIND");
						return;
					}
					Shader.DisableKeyword("ENABLE_WIND");
				}
			}

			// Token: 0x06003C92 RID: 15506 RVA: 0x000FA8C8 File Offset: 0x000F8AC8
			public override string GetString()
			{
				if (!Shader.IsKeywordEnabled("ENABLE_WIND"))
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x04003BD9 RID: 15321
			private static SettingsConVars.FoliageWindConVar instance = new SettingsConVars.FoliageWindConVar("r_foliagewind", ConVarFlags.Archive, "1", "Whether or not foliage has wind.");
		}

		// Token: 0x02000A46 RID: 2630
		private class LodBiasConVar : BaseConVar
		{
			// Token: 0x06003C94 RID: 15508 RVA: 0x00009F73 File Offset: 0x00008173
			private LodBiasConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C95 RID: 15509 RVA: 0x000FA900 File Offset: 0x000F8B00
			public override void SetString(string newValue)
			{
				float lodBias;
				if (TextSerialization.TryParseInvariant(newValue, out lodBias))
				{
					QualitySettings.lodBias = lodBias;
				}
			}

			// Token: 0x06003C96 RID: 15510 RVA: 0x000FA91D File Offset: 0x000F8B1D
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.lodBias);
			}

			// Token: 0x04003BDA RID: 15322
			private static SettingsConVars.LodBiasConVar instance = new SettingsConVars.LodBiasConVar("r_lod_bias", ConVarFlags.Archive | ConVarFlags.Engine, null, "LOD bias.");
		}

		// Token: 0x02000A47 RID: 2631
		private class MaximumLodConVar : BaseConVar
		{
			// Token: 0x06003C98 RID: 15512 RVA: 0x00009F73 File Offset: 0x00008173
			private MaximumLodConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C99 RID: 15513 RVA: 0x000FA944 File Offset: 0x000F8B44
			public override void SetString(string newValue)
			{
				int maximumLODLevel;
				if (TextSerialization.TryParseInvariant(newValue, out maximumLODLevel))
				{
					QualitySettings.maximumLODLevel = maximumLODLevel;
				}
			}

			// Token: 0x06003C9A RID: 15514 RVA: 0x000FA961 File Offset: 0x000F8B61
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.maximumLODLevel);
			}

			// Token: 0x04003BDB RID: 15323
			private static SettingsConVars.MaximumLodConVar instance = new SettingsConVars.MaximumLodConVar("r_lod_max", ConVarFlags.Archive | ConVarFlags.Engine, null, "The maximum allowed LOD level.");
		}

		// Token: 0x02000A48 RID: 2632
		private class MasterTextureLimitConVar : BaseConVar
		{
			// Token: 0x06003C9C RID: 15516 RVA: 0x00009F73 File Offset: 0x00008173
			private MasterTextureLimitConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003C9D RID: 15517 RVA: 0x000FA988 File Offset: 0x000F8B88
			public override void SetString(string newValue)
			{
				int masterTextureLimit;
				if (TextSerialization.TryParseInvariant(newValue, out masterTextureLimit))
				{
					QualitySettings.masterTextureLimit = masterTextureLimit;
				}
			}

			// Token: 0x06003C9E RID: 15518 RVA: 0x000FA9A5 File Offset: 0x000F8BA5
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.masterTextureLimit);
			}

			// Token: 0x04003BDC RID: 15324
			private static SettingsConVars.MasterTextureLimitConVar instance = new SettingsConVars.MasterTextureLimitConVar("master_texture_limit", ConVarFlags.Archive | ConVarFlags.Engine, null, "Reduction in texture quality. 0 is highest quality textures, 1 is half, 2 is quarter, etc.");
		}

		// Token: 0x02000A49 RID: 2633
		private class AnisotropicFilteringConVar : BaseConVar
		{
			// Token: 0x06003CA0 RID: 15520 RVA: 0x00009F73 File Offset: 0x00008173
			private AnisotropicFilteringConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003CA1 RID: 15521 RVA: 0x000FA9CC File Offset: 0x000F8BCC
			public override void SetString(string newValue)
			{
				try
				{
					QualitySettings.anisotropicFiltering = (AnisotropicFiltering)Enum.Parse(typeof(AnisotropicFiltering), newValue, true);
				}
				catch (ArgumentException)
				{
					Console.ShowHelpText(this.name);
				}
			}

			// Token: 0x06003CA2 RID: 15522 RVA: 0x000FAA14 File Offset: 0x000F8C14
			public override string GetString()
			{
				return QualitySettings.anisotropicFiltering.ToString();
			}

			// Token: 0x04003BDD RID: 15325
			private static SettingsConVars.AnisotropicFilteringConVar instance = new SettingsConVars.AnisotropicFilteringConVar("anisotropic_filtering", ConVarFlags.Archive, "Disable", "The anisotropic filtering mode. Can be \"Disable\", \"Enable\" or \"ForceEnable\".");
		}

		// Token: 0x02000A4A RID: 2634
		private class ShadowResolutionConVar : BaseConVar
		{
			// Token: 0x06003CA4 RID: 15524 RVA: 0x00009F73 File Offset: 0x00008173
			private ShadowResolutionConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003CA5 RID: 15525 RVA: 0x000FAA50 File Offset: 0x000F8C50
			public override void SetString(string newValue)
			{
				try
				{
					QualitySettings.shadowResolution = (ShadowResolution)Enum.Parse(typeof(ShadowResolution), newValue, true);
				}
				catch (ArgumentException)
				{
					Console.ShowHelpText(this.name);
				}
			}

			// Token: 0x06003CA6 RID: 15526 RVA: 0x000FAA98 File Offset: 0x000F8C98
			public override string GetString()
			{
				return QualitySettings.shadowResolution.ToString();
			}

			// Token: 0x04003BDE RID: 15326
			private static SettingsConVars.ShadowResolutionConVar instance = new SettingsConVars.ShadowResolutionConVar("shadow_resolution", ConVarFlags.Archive | ConVarFlags.Engine, "Medium", "Default shadow resolution. Can be \"Low\", \"Medium\", \"High\" or \"VeryHigh\".");
		}

		// Token: 0x02000A4B RID: 2635
		private class ShadowCascadesConVar : BaseConVar
		{
			// Token: 0x06003CA8 RID: 15528 RVA: 0x00009F73 File Offset: 0x00008173
			private ShadowCascadesConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003CA9 RID: 15529 RVA: 0x000FAAD8 File Offset: 0x000F8CD8
			public override void SetString(string newValue)
			{
				int shadowCascades;
				if (TextSerialization.TryParseInvariant(newValue, out shadowCascades))
				{
					QualitySettings.shadowCascades = shadowCascades;
				}
			}

			// Token: 0x06003CAA RID: 15530 RVA: 0x000FAAF5 File Offset: 0x000F8CF5
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.shadowCascades);
			}

			// Token: 0x04003BDF RID: 15327
			private static SettingsConVars.ShadowCascadesConVar instance = new SettingsConVars.ShadowCascadesConVar("shadow_cascades", ConVarFlags.Archive | ConVarFlags.Engine, null, "The number of cascades to use for directional light shadows. low=0 high=4");
		}

		// Token: 0x02000A4C RID: 2636
		private class ShadowDistanceConvar : BaseConVar
		{
			// Token: 0x06003CAC RID: 15532 RVA: 0x00009F73 File Offset: 0x00008173
			private ShadowDistanceConvar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003CAD RID: 15533 RVA: 0x000FAB1C File Offset: 0x000F8D1C
			public override void SetString(string newValue)
			{
				float shadowDistance;
				if (TextSerialization.TryParseInvariant(newValue, out shadowDistance))
				{
					QualitySettings.shadowDistance = shadowDistance;
				}
			}

			// Token: 0x06003CAE RID: 15534 RVA: 0x000FAB39 File Offset: 0x000F8D39
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(QualitySettings.shadowDistance);
			}

			// Token: 0x04003BE0 RID: 15328
			private static SettingsConVars.ShadowDistanceConvar instance = new SettingsConVars.ShadowDistanceConvar("shadow_distance", ConVarFlags.Archive, "200", "The distance in meters to draw shadows.");
		}

		// Token: 0x02000A4D RID: 2637
		private class MSAAConvar : BaseConVar
		{
			// Token: 0x06003CB0 RID: 15536 RVA: 0x00009F73 File Offset: 0x00008173
			private MSAAConvar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003CB1 RID: 15537 RVA: 0x000FAB64 File Offset: 0x000F8D64
			public override void SetString(string newValue)
			{
				int antiAliasing;
				if (TextSerialization.TryParseInvariant(newValue, out antiAliasing))
				{
					QualitySettings.antiAliasing = antiAliasing;
				}
			}

			// Token: 0x06003CB2 RID: 15538 RVA: 0x000FAB84 File Offset: 0x000F8D84
			public override string GetString()
			{
				return QualitySettings.antiAliasing.ToString();
			}

			// Token: 0x04003BE1 RID: 15329
			private static SettingsConVars.MSAAConvar instance = new SettingsConVars.MSAAConvar("r_ui_msaa", ConVarFlags.Archive, "0", "Whether or not MSAA for the UI is enabled.");
		}

		// Token: 0x02000A4E RID: 2638
		private class PpMotionBlurConVar : BaseConVar
		{
			// Token: 0x06003CB4 RID: 15540 RVA: 0x000FABBA File Offset: 0x000F8DBA
			private PpMotionBlurConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<MotionBlur>(out SettingsConVars.PpMotionBlurConVar.settings);
			}

			// Token: 0x06003CB5 RID: 15541 RVA: 0x000FABE4 File Offset: 0x000F8DE4
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && SettingsConVars.PpMotionBlurConVar.settings)
				{
					SettingsConVars.PpMotionBlurConVar.settings.active = (num == 0);
				}
			}

			// Token: 0x06003CB6 RID: 15542 RVA: 0x000FAC15 File Offset: 0x000F8E15
			public override string GetString()
			{
				if (!SettingsConVars.PpMotionBlurConVar.settings)
				{
					return "1";
				}
				if (!SettingsConVars.PpMotionBlurConVar.settings.active)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04003BE2 RID: 15330
			private static MotionBlur settings;

			// Token: 0x04003BE3 RID: 15331
			private static SettingsConVars.PpMotionBlurConVar instance = new SettingsConVars.PpMotionBlurConVar("pp_motionblur", ConVarFlags.Archive, "0", "Motion blur. 0 = disabled 1 = enabled");
		}

		// Token: 0x02000A4F RID: 2639
		private class PpSobelOutlineConVar : BaseConVar
		{
			// Token: 0x06003CB8 RID: 15544 RVA: 0x000FAC5C File Offset: 0x000F8E5C
			private PpSobelOutlineConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<SobelOutline>(out SettingsConVars.PpSobelOutlineConVar.sobelOutlineSettings);
			}

			// Token: 0x06003CB9 RID: 15545 RVA: 0x000FAC84 File Offset: 0x000F8E84
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && SettingsConVars.PpSobelOutlineConVar.sobelOutlineSettings)
				{
					SettingsConVars.PpSobelOutlineConVar.sobelOutlineSettings.active = (num == 0);
				}
			}

			// Token: 0x06003CBA RID: 15546 RVA: 0x000FACB5 File Offset: 0x000F8EB5
			public override string GetString()
			{
				if (!SettingsConVars.PpSobelOutlineConVar.sobelOutlineSettings)
				{
					return "1";
				}
				if (!SettingsConVars.PpSobelOutlineConVar.sobelOutlineSettings.active)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04003BE4 RID: 15332
			private static SobelOutline sobelOutlineSettings;

			// Token: 0x04003BE5 RID: 15333
			private static SettingsConVars.PpSobelOutlineConVar instance = new SettingsConVars.PpSobelOutlineConVar("pp_sobel_outline", ConVarFlags.Archive, "1", "Whether or not to use the sobel rim light effect.");
		}

		// Token: 0x02000A50 RID: 2640
		private class PpBloomConVar : BaseConVar
		{
			// Token: 0x06003CBC RID: 15548 RVA: 0x000FACFC File Offset: 0x000F8EFC
			private PpBloomConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<Bloom>(out SettingsConVars.PpBloomConVar.settings);
			}

			// Token: 0x06003CBD RID: 15549 RVA: 0x000FAD24 File Offset: 0x000F8F24
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && SettingsConVars.PpBloomConVar.settings)
				{
					SettingsConVars.PpBloomConVar.settings.active = (num == 0);
				}
			}

			// Token: 0x06003CBE RID: 15550 RVA: 0x000FAD55 File Offset: 0x000F8F55
			public override string GetString()
			{
				if (!SettingsConVars.PpBloomConVar.settings)
				{
					return "1";
				}
				if (!SettingsConVars.PpBloomConVar.settings.active)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04003BE6 RID: 15334
			private static Bloom settings;

			// Token: 0x04003BE7 RID: 15335
			private static SettingsConVars.PpBloomConVar instance = new SettingsConVars.PpBloomConVar("pp_bloom", ConVarFlags.Archive | ConVarFlags.Engine, null, "Bloom postprocessing. 0 = disabled 1 = enabled");
		}

		// Token: 0x02000A51 RID: 2641
		private class PpAOConVar : BaseConVar
		{
			// Token: 0x06003CC0 RID: 15552 RVA: 0x000FAD99 File Offset: 0x000F8F99
			private PpAOConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<AmbientOcclusion>(out SettingsConVars.PpAOConVar.settings);
			}

			// Token: 0x06003CC1 RID: 15553 RVA: 0x000FADC0 File Offset: 0x000F8FC0
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && SettingsConVars.PpAOConVar.settings)
				{
					SettingsConVars.PpAOConVar.settings.active = (num == 0);
				}
			}

			// Token: 0x06003CC2 RID: 15554 RVA: 0x000FADF1 File Offset: 0x000F8FF1
			public override string GetString()
			{
				if (!SettingsConVars.PpAOConVar.settings)
				{
					return "1";
				}
				if (!SettingsConVars.PpAOConVar.settings.active)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04003BE8 RID: 15336
			private static AmbientOcclusion settings;

			// Token: 0x04003BE9 RID: 15337
			private static SettingsConVars.PpAOConVar instance = new SettingsConVars.PpAOConVar("pp_ao", ConVarFlags.Archive | ConVarFlags.Engine, null, "SSAO postprocessing. 0 = disabled 1 = enabled");
		}

		// Token: 0x02000A52 RID: 2642
		private class PpScreenDistortionConVar : BaseConVar
		{
			// Token: 0x06003CC4 RID: 15556 RVA: 0x000FAE35 File Offset: 0x000F9035
			private PpScreenDistortionConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<LensDistortion>(out SettingsConVars.PpScreenDistortionConVar.settings);
			}

			// Token: 0x06003CC5 RID: 15557 RVA: 0x000FAE5C File Offset: 0x000F905C
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && SettingsConVars.PpScreenDistortionConVar.settings)
				{
					SettingsConVars.PpScreenDistortionConVar.settings.active = (num == 0);
				}
			}

			// Token: 0x06003CC6 RID: 15558 RVA: 0x000FAE8D File Offset: 0x000F908D
			public override string GetString()
			{
				if (!SettingsConVars.PpScreenDistortionConVar.settings)
				{
					return "1";
				}
				if (!SettingsConVars.PpScreenDistortionConVar.settings.active)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04003BEA RID: 15338
			private static LensDistortion settings;

			// Token: 0x04003BEB RID: 15339
			private static SettingsConVars.PpScreenDistortionConVar instance = new SettingsConVars.PpScreenDistortionConVar("pp_screendistortion", ConVarFlags.Archive | ConVarFlags.Engine, null, "Screen distortion, like from Spinel Tonic. 0 = disabled 1 = enabled");
		}

		// Token: 0x02000A53 RID: 2643
		private class PpGammaConVar : BaseConVar
		{
			// Token: 0x06003CC8 RID: 15560 RVA: 0x000FAED1 File Offset: 0x000F90D1
			private PpGammaConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				RoR2Application.instance.postProcessSettingsController.sharedProfile.TryGetSettings<ColorGrading>(out SettingsConVars.PpGammaConVar.colorGradingSettings);
			}

			// Token: 0x06003CC9 RID: 15561 RVA: 0x000FAEF8 File Offset: 0x000F90F8
			public override void SetString(string newValue)
			{
				float w;
				if (TextSerialization.TryParseInvariant(newValue, out w) && SettingsConVars.PpGammaConVar.colorGradingSettings)
				{
					SettingsConVars.PpGammaConVar.colorGradingSettings.gamma.value.w = w;
				}
			}

			// Token: 0x06003CCA RID: 15562 RVA: 0x000FAF30 File Offset: 0x000F9130
			public override string GetString()
			{
				if (!SettingsConVars.PpGammaConVar.colorGradingSettings)
				{
					return "0";
				}
				return TextSerialization.ToStringInvariant(SettingsConVars.PpGammaConVar.colorGradingSettings.gamma.value.w);
			}

			// Token: 0x04003BEC RID: 15340
			private static ColorGrading colorGradingSettings;

			// Token: 0x04003BED RID: 15341
			private static SettingsConVars.PpGammaConVar instance = new SettingsConVars.PpGammaConVar("gamma", ConVarFlags.Archive, "0", "Gamma boost, from -inf to inf.");
		}
	}
}
