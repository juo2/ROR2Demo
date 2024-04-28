using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Rewired;

namespace RoR2
{
	// Token: 0x02000930 RID: 2352
	public static class InputCatalog
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x000E0EE4 File Offset: 0x000DF0E4
		static InputCatalog()
		{
			InputCatalog.<.cctor>g__Add|2_0("MoveHorizontal", "ACTION_MOVE_HORIZONTAL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("MoveVertical", "ACTION_MOVE_VERTICAL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("AimHorizontalMouse", "ACTION_AIM_HORIZONTAL_MOUSE", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("AimVerticalMouse", "ACTION_AIM_VERTICAL_MOUSE", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("AimHorizontalStick", "ACTION_AIM_HORIZONTAL_STICK", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("AimVerticalStick", "ACTION_AIM_VERTICAL_STICK", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Jump", "ACTION_JUMP", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Sprint", "ACTION_SPRINT", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Interact", "ACTION_INTERACT", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Equipment", "ACTION_EQUIPMENT", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("PrimarySkill", "ACTION_PRIMARY_SKILL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("SecondarySkill", "ACTION_SECONDARY_SKILL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("UtilitySkill", "ACTION_UTILITY_SKILL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("SpecialSkill", "ACTION_SPECIAL_SKILL", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Info", "ACTION_INFO", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("Ping", "ACTION_PING", AxisRange.Full);
			InputCatalog.<.cctor>g__Add|2_0("MoveHorizontal", "ACTION_MOVE_HORIZONTAL_POSITIVE", AxisRange.Positive);
			InputCatalog.<.cctor>g__Add|2_0("MoveHorizontal", "ACTION_MOVE_HORIZONTAL_NEGATIVE", AxisRange.Negative);
			InputCatalog.<.cctor>g__Add|2_0("MoveVertical", "ACTION_MOVE_VERTICAL_POSITIVE", AxisRange.Positive);
			InputCatalog.<.cctor>g__Add|2_0("MoveVertical", "ACTION_MOVE_VERTICAL_NEGATIVE", AxisRange.Negative);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000E103C File Offset: 0x000DF23C
		public static string GetActionNameToken(string actionName, AxisRange axisRange = AxisRange.Full)
		{
			string result;
			if (InputCatalog.actionToToken.TryGetValue(new InputCatalog.ActionAxisPair(actionName, axisRange), out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Bad action/axis pair {0} {1}.", actionName, axisRange));
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000E1076 File Offset: 0x000DF276
		[CompilerGenerated]
		internal static void <.cctor>g__Add|2_0(string actionName, string token, AxisRange axisRange)
		{
			InputCatalog.actionToToken[new InputCatalog.ActionAxisPair(actionName, axisRange)] = token;
		}

		// Token: 0x04003607 RID: 13831
		private static readonly Dictionary<InputCatalog.ActionAxisPair, string> actionToToken = new Dictionary<InputCatalog.ActionAxisPair, string>();

		// Token: 0x02000931 RID: 2353
		private struct ActionAxisPair : IEquatable<InputCatalog.ActionAxisPair>
		{
			// Token: 0x06003531 RID: 13617 RVA: 0x000E108A File Offset: 0x000DF28A
			public ActionAxisPair([NotNull] string actionName, AxisRange axisRange)
			{
				this.actionName = actionName;
				this.axisRange = axisRange;
			}

			// Token: 0x06003532 RID: 13618 RVA: 0x000E109A File Offset: 0x000DF29A
			public bool Equals(InputCatalog.ActionAxisPair other)
			{
				return string.Equals(this.actionName, other.actionName) && this.axisRange == other.axisRange;
			}

			// Token: 0x06003533 RID: 13619 RVA: 0x000E10BF File Offset: 0x000DF2BF
			public override bool Equals(object obj)
			{
				return obj != null && obj is InputCatalog.ActionAxisPair && this.Equals((InputCatalog.ActionAxisPair)obj);
			}

			// Token: 0x06003534 RID: 13620 RVA: 0x000E10DC File Offset: 0x000DF2DC
			public override int GetHashCode()
			{
				return ((-1879861323 * -1521134295 + base.GetHashCode()) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.actionName)) * -1521134295 + this.axisRange.GetHashCode();
			}

			// Token: 0x04003608 RID: 13832
			[NotNull]
			private readonly string actionName;

			// Token: 0x04003609 RID: 13833
			private readonly AxisRange axisRange;
		}
	}
}
