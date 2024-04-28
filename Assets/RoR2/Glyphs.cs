using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Rewired;
using RoR2.UI;

namespace RoR2
{
	// Token: 0x0200091D RID: 2333
	public static class Glyphs
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x000DEC44 File Offset: 0x000DCE44
		private static void AddGlyph(string controllerName, int elementIndex, string assetName, string glyphName)
		{
			Glyphs.glyphMap[new Glyphs.GlyphKey(controllerName, elementIndex)] = string.Format(CultureInfo.InvariantCulture, "<sprite=\"{0}\" name=\"{1}\">", assetName, glyphName);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000DEC68 File Offset: 0x000DCE68
		private static void RegisterXBoxController(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 0, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_4");
			Glyphs.AddGlyph(controllerName, 1, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_4");
			Glyphs.AddGlyph(controllerName, 2, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_8");
			Glyphs.AddGlyph(controllerName, 3, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_8");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_5");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_9");
			Glyphs.AddGlyph(controllerName, 10, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_2");
			Glyphs.AddGlyph(controllerName, 11, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_6");
			Glyphs.AddGlyph(controllerName, 6, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_0");
			Glyphs.AddGlyph(controllerName, 7, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_1");
			Glyphs.AddGlyph(controllerName, 8, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_7");
			Glyphs.AddGlyph(controllerName, 9, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_11");
			Glyphs.AddGlyph(controllerName, 12, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_10");
			Glyphs.AddGlyph(controllerName, 13, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_3");
			Glyphs.AddGlyph(controllerName, 14, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_4");
			Glyphs.AddGlyph(controllerName, 15, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_8");
			Glyphs.AddGlyph(controllerName, 19, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_12");
			Glyphs.AddGlyph(controllerName, 17, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_13");
			Glyphs.AddGlyph(controllerName, 16, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_14");
			Glyphs.AddGlyph(controllerName, 18, "tmpsprXboxOneGlyphs", "texXBoxOneGlyphs_15");
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000DEDD4 File Offset: 0x000DCFD4
		private static void RegisterDS4Controller(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 0, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_25");
			Glyphs.AddGlyph(controllerName, 1, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_25");
			Glyphs.AddGlyph(controllerName, 2, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_24");
			Glyphs.AddGlyph(controllerName, 3, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_24");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_6");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_7");
			Glyphs.AddGlyph(controllerName, 10, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_4");
			Glyphs.AddGlyph(controllerName, 11, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_5");
			Glyphs.AddGlyph(controllerName, 6, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_0");
			Glyphs.AddGlyph(controllerName, 7, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_2");
			Glyphs.AddGlyph(controllerName, 8, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_3");
			Glyphs.AddGlyph(controllerName, 9, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_1");
			Glyphs.AddGlyph(controllerName, 15, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_26");
			Glyphs.AddGlyph(controllerName, 16, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_15");
			Glyphs.AddGlyph(controllerName, 17, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_16");
			Glyphs.AddGlyph(controllerName, 18, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_21");
			Glyphs.AddGlyph(controllerName, 19, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_19");
			Glyphs.AddGlyph(controllerName, 20, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_22");
			Glyphs.AddGlyph(controllerName, 21, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_20");
			Glyphs.AddGlyph(controllerName, 13, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_8");
			Glyphs.AddGlyph(controllerName, 12, "tmpsprPS4GlyphsUnified", "texPS4GlyphsUnified_9");
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000DEF54 File Offset: 0x000DD154
		private static void RegisterSwitchController(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 1, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_62");
			Glyphs.AddGlyph(controllerName, 0, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_62");
			Glyphs.AddGlyph(controllerName, 3, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_63");
			Glyphs.AddGlyph(controllerName, 2, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_63");
			Glyphs.AddGlyph(controllerName, 10, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_6");
			Glyphs.AddGlyph(controllerName, 11, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_7");
			Glyphs.AddGlyph(controllerName, 8, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_4");
			Glyphs.AddGlyph(controllerName, 9, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_5");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_1");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_0");
			Glyphs.AddGlyph(controllerName, 6, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_3");
			Glyphs.AddGlyph(controllerName, 7, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_2");
			Glyphs.AddGlyph(controllerName, 12, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_19");
			Glyphs.AddGlyph(controllerName, 13, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_18");
			Glyphs.AddGlyph(controllerName, 16, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_74");
			Glyphs.AddGlyph(controllerName, 17, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_75");
			Glyphs.AddGlyph(controllerName, 18, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_12");
			Glyphs.AddGlyph(controllerName, 19, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_14");
			Glyphs.AddGlyph(controllerName, 20, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_13");
			Glyphs.AddGlyph(controllerName, 21, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_15");
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000DF0C0 File Offset: 0x000DD2C0
		private static void RegisterSwitchProController(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 1, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_62");
			Glyphs.AddGlyph(controllerName, 0, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_62");
			Glyphs.AddGlyph(controllerName, 3, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_63");
			Glyphs.AddGlyph(controllerName, 2, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_63");
			Glyphs.AddGlyph(controllerName, 10, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_6");
			Glyphs.AddGlyph(controllerName, 11, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_7");
			Glyphs.AddGlyph(controllerName, 8, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_4");
			Glyphs.AddGlyph(controllerName, 9, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_5");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_1");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_0");
			Glyphs.AddGlyph(controllerName, 6, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_3");
			Glyphs.AddGlyph(controllerName, 7, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_2");
			Glyphs.AddGlyph(controllerName, 12, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_19");
			Glyphs.AddGlyph(controllerName, 13, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_18");
			Glyphs.AddGlyph(controllerName, 16, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_74");
			Glyphs.AddGlyph(controllerName, 17, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_75");
			Glyphs.AddGlyph(controllerName, 18, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_50");
			Glyphs.AddGlyph(controllerName, 19, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_53");
			Glyphs.AddGlyph(controllerName, 20, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_51");
			Glyphs.AddGlyph(controllerName, 21, "tmpsprSwitchGlyphsUnified", "texSwitchGlyphsUnified_52");
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000DF22C File Offset: 0x000DD42C
		private static void RegisterStadiaController(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 0, "tmpsprStadiaGlyphs", "texStadiaGlyphs_28");
			Glyphs.AddGlyph(controllerName, 1, "tmpsprStadiaGlyphs", "texStadiaGlyphs_15");
			Glyphs.AddGlyph(controllerName, 2, "tmpsprStadiaGlyphs", "texStadiaGlyphs_10");
			Glyphs.AddGlyph(controllerName, 3, "tmpsprStadiaGlyphs", "texStadiaGlyphs_0");
			Glyphs.AddGlyph(controllerName, 9, "tmpsprStadiaGlyphs", "texStadiaGlyphs_16");
			Glyphs.AddGlyph(controllerName, 11, "tmpsprStadiaGlyphs", "texStadiaGlyphs_1");
			Glyphs.AddGlyph(controllerName, 8, "tmpsprStadiaGlyphs", "texStadiaGlyphs_21");
			Glyphs.AddGlyph(controllerName, 10, "tmpsprStadiaGlyphs", "texStadiaGlyphs_19");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprStadiaGlyphs", "texStadiaGlyphs_29");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprStadiaGlyphs", "texStadiaGlyphs_31");
			Glyphs.AddGlyph(controllerName, 6, "tmpsprStadiaGlyphs", "texStadiaGlyphs_3");
			Glyphs.AddGlyph(controllerName, 7, "tmpsprStadiaGlyphs", "texStadiaGlyphs_4");
			Glyphs.AddGlyph(controllerName, 13, "tmpsprStadiaGlyphs", "texStadiaGlyphs_17");
			Glyphs.AddGlyph(controllerName, 14, "tmpsprStadiaGlyphs", "texStadiaGlyphs_18");
			Glyphs.AddGlyph(controllerName, 15, "tmpsprStadiaGlyphs", "texStadiaGlyphs_30");
			Glyphs.AddGlyph(controllerName, 16, "tmpsprStadiaGlyphs", "texStadiaGlyphs_32");
			Glyphs.AddGlyph(controllerName, 17, "tmpsprStadiaGlyphs", "texStadiaGlyphs_2");
			Glyphs.AddGlyph(controllerName, 18, "tmpsprStadiaGlyphs", "texStadiaGlyphs_22");
			Glyphs.AddGlyph(controllerName, 19, "tmpsprStadiaGlyphs", "texStadiaGlyphs_20");
			Glyphs.AddGlyph(controllerName, 20, "tmpsprStadiaGlyphs", "texStadiaGlyphs_36");
			Glyphs.AddGlyph(controllerName, 21, "tmpsprStadiaGlyphs", "texStadiaGlyphs_35");
			Glyphs.AddGlyph(controllerName, 22, "tmpsprStadiaGlyphs", "texStadiaGlyphs_33");
			Glyphs.AddGlyph(controllerName, 23, "tmpsprStadiaGlyphs", "texStadiaGlyphs_34");
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000DF3CE File Offset: 0x000DD5CE
		private static void RegisterMouse(string controllerName)
		{
			Glyphs.AddGlyph(controllerName, 3, "tmpsprSteamGlyphs", "texSteamGlyphs_17");
			Glyphs.AddGlyph(controllerName, 4, "tmpsprSteamGlyphs", "texSteamGlyphs_18");
			Glyphs.AddGlyph(controllerName, 5, "tmpsprSteamGlyphs", "texSteamGlyphs_19");
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000026ED File Offset: 0x000008ED
		private static void RegisterKeyboard(string controllerName)
		{
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000DF404 File Offset: 0x000DD604
		static Glyphs()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["Left Mouse Button"] = "M1";
			dictionary["Right Mouse Button"] = "M2";
			dictionary["Mouse Button 3"] = "M3";
			dictionary["Mouse Button 4"] = "M4";
			dictionary["Mouse Button 5"] = "M5";
			dictionary["Mouse Button 6"] = "M6";
			dictionary["Mouse Button 7"] = "M7";
			dictionary["Mouse Wheel"] = "MW";
			dictionary["Mouse Wheel +"] = "MW+";
			dictionary["Mouse Wheel -"] = "MW-";
			Glyphs.mouseElementRenameMap = dictionary;
			Glyphs.resultsList = new List<ActionElementMap>();
			Glyphs.RegisterXBoxController("Xbox 360 Controller");
			for (int i = 0; i < 4; i++)
			{
				Glyphs.RegisterXBoxController("XInput Gamepad " + i);
			}
			Glyphs.RegisterXBoxController("Xbox One Controller");
			Glyphs.RegisterXBoxController("Gamepad");
			Glyphs.RegisterDS4Controller("Sony DualShock 4");
			Glyphs.RegisterDS4Controller("PlayStation Controller");
			Glyphs.RegisterDS4Controller("Sony DualSense");
			Glyphs.RegisterSwitchController("Nintendo Controller");
			Glyphs.RegisterSwitchProController("Nintendo Switch Pro Controller");
			Glyphs.RegisterSwitchProController("Pro Controller");
			Glyphs.RegisterStadiaController("Stadia Controller");
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000DF560 File Offset: 0x000DD760
		private static string GetKeyboardGlyphString(string actionName)
		{
			string text;
			if (!Glyphs.keyboardRawNameToGlyphName.TryGetValue(actionName, out text))
			{
				if (!(actionName == "Left Shift"))
				{
					if (actionName == "Left Control")
					{
						actionName = "Ctrl";
					}
				}
				else
				{
					actionName = "Shift";
				}
				text = actionName;
				Glyphs.keyboardRawNameToGlyphName[actionName] = text;
			}
			return text;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
		public static string GetGlyphString(MPEventSystemLocator eventSystemLocator, string actionName)
		{
			MPEventSystem eventSystem = eventSystemLocator.eventSystem;
			if (eventSystem)
			{
				return Glyphs.GetGlyphString(eventSystem, actionName, AxisRange.Full);
			}
			return "UNKNOWN";
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000DF5E2 File Offset: 0x000DD7E2
		public static string GetGlyphString(MPEventSystem eventSystem, string actionName, AxisRange axisRange = AxisRange.Full)
		{
			return Glyphs.GetGlyphString(eventSystem, actionName, axisRange, eventSystem.currentInputSource);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000DF5F4 File Offset: 0x000DD7F4
		public static string GetGlyphString(MPEventSystem eventSystem, string actionName, AxisRange axisRange, MPEventSystem.InputSource currentInputSource)
		{
			if (!eventSystem)
			{
				return "???";
			}
			Glyphs.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.inputPlayer = eventSystem.player;
			InputAction action = ReInput.mapping.GetAction(actionName);
			CS$<>8__locals1.inputActionId = action.id;
			CS$<>8__locals1.controllerName = "Xbox One Controller";
			CS$<>8__locals1.controllerType = (ControllerType)(-1);
			CS$<>8__locals1.axisContributionMatters = (axisRange > AxisRange.Full);
			CS$<>8__locals1.axisContribution = Pole.Positive;
			if (axisRange == AxisRange.Negative)
			{
				CS$<>8__locals1.axisContribution = Pole.Negative;
			}
			if (currentInputSource != MPEventSystem.InputSource.MouseAndKeyboard)
			{
				if (currentInputSource != MPEventSystem.InputSource.Gamepad)
				{
					throw new ArgumentOutOfRangeException();
				}
				CS$<>8__locals1.controllerType = ControllerType.Joystick;
				Glyphs.<GetGlyphString>g__SetController|27_0(CS$<>8__locals1.inputPlayer.controllers.GetLastActiveController(ControllerType.Joystick), ref CS$<>8__locals1);
				if (CS$<>8__locals1.actionElementMap == null)
				{
					foreach (Controller controller in CS$<>8__locals1.inputPlayer.controllers.Controllers)
					{
						if (controller.type == ControllerType.Joystick)
						{
							Glyphs.<GetGlyphString>g__SetController|27_0(controller, ref CS$<>8__locals1);
							if (CS$<>8__locals1.actionElementMap != null)
							{
								break;
							}
						}
					}
				}
				if (CS$<>8__locals1.actionElementMap == null && eventSystem.localUser != null)
				{
					using (IEnumerator<ActionElementMap> enumerator2 = eventSystem.localUser.userProfile.joystickMap.ElementMapsWithAction(CS$<>8__locals1.inputActionId).GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							ActionElementMap actionElementMap = enumerator2.Current;
							CS$<>8__locals1.actionElementMap = actionElementMap;
						}
					}
				}
				if (CS$<>8__locals1.actionElementMap == null)
				{
					return Language.GetString("INPUT_GAMEPAD_UNBOUND");
				}
			}
			else
			{
				Glyphs.<GetGlyphString>g__SetController|27_0(CS$<>8__locals1.inputPlayer.controllers.Keyboard, ref CS$<>8__locals1);
				if (CS$<>8__locals1.actionElementMap == null)
				{
					Glyphs.<GetGlyphString>g__SetController|27_0(CS$<>8__locals1.inputPlayer.controllers.Mouse, ref CS$<>8__locals1);
				}
				if (CS$<>8__locals1.actionElementMap == null)
				{
					return Language.GetString("INPUT_KEYBOARD_UNBOUND");
				}
			}
			int elementIdentifierId = CS$<>8__locals1.actionElementMap.elementIdentifierId;
			Glyphs.GlyphKey key = new Glyphs.GlyphKey(CS$<>8__locals1.controllerName, elementIdentifierId);
			string result;
			if (Glyphs.glyphMap.TryGetValue(key, out result))
			{
				return result;
			}
			if (CS$<>8__locals1.controllerType == ControllerType.Keyboard)
			{
				return Glyphs.GetKeyboardGlyphString(CS$<>8__locals1.actionElementMap.elementIdentifierName);
			}
			if (CS$<>8__locals1.controllerType == ControllerType.Mouse)
			{
				string text = CS$<>8__locals1.actionElementMap.elementIdentifierName;
				string text2;
				if (Glyphs.mouseElementRenameMap.TryGetValue(text, out text2))
				{
					text = text2;
				}
				return text;
			}
			return "???";
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000DF84C File Offset: 0x000DDA4C
		[CompilerGenerated]
		internal static void <GetGlyphString>g__SetController|27_0(Controller newController, ref Glyphs.<>c__DisplayClass27_0 A_1)
		{
			if (newController != null)
			{
				A_1.controllerName = newController.name;
				A_1.controllerType = newController.type;
			}
			A_1.actionElementMap = null;
			if (newController != null)
			{
				Glyphs.resultsList.Clear();
				A_1.inputPlayer.controllers.maps.GetElementMapsWithAction(newController.type, newController.id, A_1.inputActionId, false, Glyphs.resultsList);
				foreach (ActionElementMap actionElementMap in Glyphs.resultsList)
				{
					if (!A_1.axisContributionMatters || actionElementMap.axisContribution == A_1.axisContribution)
					{
						A_1.actionElementMap = actionElementMap;
						break;
					}
				}
			}
		}

		// Token: 0x040035B3 RID: 13747
		private static readonly Dictionary<Glyphs.GlyphKey, string> glyphMap = new Dictionary<Glyphs.GlyphKey, string>();

		// Token: 0x040035B4 RID: 13748
		private const string xbox360ControllerName = "Xbox 360 Controller";

		// Token: 0x040035B5 RID: 13749
		private const string xboxOneControllerName = "Xbox One Controller";

		// Token: 0x040035B6 RID: 13750
		private const string dualshock4ControllerName = "Sony DualShock 4";

		// Token: 0x040035B7 RID: 13751
		private const string dualshock4ControllerNameAlt = "PlayStation Controller";

		// Token: 0x040035B8 RID: 13752
		private const string dualSenseController = "Sony DualSense";

		// Token: 0x040035B9 RID: 13753
		private const string switchControllerName = "Nintendo Controller";

		// Token: 0x040035BA RID: 13754
		private const string switchProControllerName = "Nintendo Switch Pro Controller";

		// Token: 0x040035BB RID: 13755
		private const string switchProControllerNameAlt = "Pro Controller";

		// Token: 0x040035BC RID: 13756
		private const string stadiaControllerName = "Stadia Controller";

		// Token: 0x040035BD RID: 13757
		private const string defaultControllerName = "Xbox One Controller";

		// Token: 0x040035BE RID: 13758
		private static readonly Dictionary<string, string> keyboardRawNameToGlyphName = new Dictionary<string, string>();

		// Token: 0x040035BF RID: 13759
		private static readonly Dictionary<string, string> mouseElementRenameMap;

		// Token: 0x040035C0 RID: 13760
		private static readonly List<ActionElementMap> resultsList;

		// Token: 0x0200091E RID: 2334
		private struct GlyphKey : IEquatable<Glyphs.GlyphKey>
		{
			// Token: 0x060034D1 RID: 13521 RVA: 0x000DF918 File Offset: 0x000DDB18
			public GlyphKey(string deviceName, int elementId)
			{
				this.deviceName = deviceName;
				this.elementId = elementId;
			}

			// Token: 0x060034D2 RID: 13522 RVA: 0x000DF928 File Offset: 0x000DDB28
			public bool Equals(Glyphs.GlyphKey other)
			{
				return string.Equals(this.deviceName, other.deviceName) && this.elementId == other.elementId;
			}

			// Token: 0x060034D3 RID: 13523 RVA: 0x000DF94D File Offset: 0x000DDB4D
			public override bool Equals(object obj)
			{
				return obj != null && obj is Glyphs.GlyphKey && this.Equals((Glyphs.GlyphKey)obj);
			}

			// Token: 0x060034D4 RID: 13524 RVA: 0x000DF96A File Offset: 0x000DDB6A
			public override int GetHashCode()
			{
				return ((this.deviceName != null) ? this.deviceName.GetHashCode() : 0) * 397 ^ this.elementId;
			}

			// Token: 0x040035C1 RID: 13761
			public readonly string deviceName;

			// Token: 0x040035C2 RID: 13762
			public readonly int elementId;
		}
	}
}
