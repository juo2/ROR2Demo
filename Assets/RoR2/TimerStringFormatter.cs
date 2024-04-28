using System;
using System.Text;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000565 RID: 1381
	[CreateAssetMenu(menuName = "RoR2/Timer String Formatter")]
	public class TimerStringFormatter : ScriptableObject
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x0006C360 File Offset: 0x0006A560
		public void AppendToStringBuilder(double seconds, [NotNull] StringBuilder dest)
		{
			double num = seconds;
			if (!string.IsNullOrEmpty(this.format.prefix))
			{
				dest.Append(this.format.prefix);
			}
			if (num < 0.0)
			{
				num = -num;
				dest.Append("-");
			}
			foreach (TimerStringFormatter.Format.Unit unit in this.format.units)
			{
				double num2 = Math.Floor(num / unit.conversionRate);
				num -= num2 * unit.conversionRate;
				int num3 = (int)num2;
				uint minDigits = unit.minDigits;
				uint num4 = unit.maxDigits;
				if (num4 < minDigits)
				{
					num4 = minDigits;
				}
				if (num4 != 0U && (minDigits != 0U || num3 >= 10))
				{
					if (!string.IsNullOrEmpty(unit.prefix))
					{
						dest.Append(unit.prefix);
					}
					dest.AppendInt(num3, minDigits, num4);
					if (!string.IsNullOrEmpty(unit.suffix))
					{
						dest.Append(unit.suffix);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.format.suffix))
			{
				dest.Append(this.format.suffix);
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0006C483 File Offset: 0x0006A683
		private void Reset()
		{
			this.format = TimerStringFormatter.Format.Clone(TimerStringFormatter.defaultFormat);
		}

		// Token: 0x04001ECA RID: 7882
		public TimerStringFormatter.Format format = TimerStringFormatter.defaultFormat;

		// Token: 0x04001ECB RID: 7883
		private const double hoursPerDay = 24.0;

		// Token: 0x04001ECC RID: 7884
		private const double minutesPerDay = 1440.0;

		// Token: 0x04001ECD RID: 7885
		private const double secondsPerDay = 86400.0;

		// Token: 0x04001ECE RID: 7886
		private const double minutesPerHour = 60.0;

		// Token: 0x04001ECF RID: 7887
		private const double secondsPerMinute = 60.0;

		// Token: 0x04001ED0 RID: 7888
		private const double secondsPerSecond = 1.0;

		// Token: 0x04001ED1 RID: 7889
		private const double secondsPerCentisecond = 0.01;

		// Token: 0x04001ED2 RID: 7890
		private const double secondsPerHour = 3600.0;

		// Token: 0x04001ED3 RID: 7891
		private static readonly TimerStringFormatter.Format defaultFormat = new TimerStringFormatter.Format
		{
			prefix = "<mspace=0.5em>",
			suffix = "</mspace>",
			units = new TimerStringFormatter.Format.Unit[]
			{
				new TimerStringFormatter.Format.Unit
				{
					name = "days",
					conversionRate = 86400.0,
					maxDigits = 2147483647U,
					minDigits = 2U,
					prefix = string.Empty,
					suffix = string.Empty
				},
				new TimerStringFormatter.Format.Unit
				{
					name = "hours",
					conversionRate = 3600.0,
					maxDigits = 2U,
					minDigits = 2U,
					prefix = ":",
					suffix = string.Empty
				},
				new TimerStringFormatter.Format.Unit
				{
					name = "minutes",
					conversionRate = 60.0,
					maxDigits = 2U,
					minDigits = 2U,
					prefix = ":",
					suffix = string.Empty
				},
				new TimerStringFormatter.Format.Unit
				{
					name = "seconds",
					conversionRate = 1.0,
					maxDigits = 2U,
					minDigits = 2U,
					prefix = ":",
					suffix = string.Empty
				},
				new TimerStringFormatter.Format.Unit
				{
					name = "centiseconds",
					conversionRate = 0.01,
					maxDigits = 2U,
					minDigits = 2U,
					prefix = "<voffset=0.4em><size=40%><mspace=0.5em>.",
					suffix = "</size></voffset></mspace>"
				}
			}
		};

		// Token: 0x02000566 RID: 1382
		[Serializable]
		public struct Format
		{
			// Token: 0x06001901 RID: 6401 RVA: 0x0006C690 File Offset: 0x0006A890
			public static TimerStringFormatter.Format Clone(in TimerStringFormatter.Format src)
			{
				return new TimerStringFormatter.Format
				{
					prefix = src.prefix,
					suffix = src.suffix,
					units = ((src.units != null) ? ArrayUtils.Clone<TimerStringFormatter.Format.Unit>(src.units) : null)
				};
			}

			// Token: 0x04001ED4 RID: 7892
			[CanBeNull]
			public TimerStringFormatter.Format.Unit[] units;

			// Token: 0x04001ED5 RID: 7893
			public string prefix;

			// Token: 0x04001ED6 RID: 7894
			public string suffix;

			// Token: 0x02000567 RID: 1383
			[Serializable]
			public struct Unit
			{
				// Token: 0x04001ED7 RID: 7895
				[CanBeNull]
				public string name;

				// Token: 0x04001ED8 RID: 7896
				public double conversionRate;

				// Token: 0x04001ED9 RID: 7897
				public uint minDigits;

				// Token: 0x04001EDA RID: 7898
				public uint maxDigits;

				// Token: 0x04001EDB RID: 7899
				[CanBeNull]
				public string prefix;

				// Token: 0x04001EDC RID: 7900
				[CanBeNull]
				public string suffix;
			}
		}
	}
}
