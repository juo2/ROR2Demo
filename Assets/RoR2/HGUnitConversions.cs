using System;

// Token: 0x0200006A RID: 106
public static class HGUnitConversions
{
	// Token: 0x040001D0 RID: 464
	public static readonly double milesToKilometers = 1.609344;

	// Token: 0x040001D1 RID: 465
	public static readonly double kilometersToMeters = 1000.0;

	// Token: 0x040001D2 RID: 466
	public static readonly double milesToMeters = HGUnitConversions.milesToKilometers * HGUnitConversions.kilometersToMeters;

	// Token: 0x040001D3 RID: 467
	public static readonly double hoursToMinutes = 60.0;

	// Token: 0x040001D4 RID: 468
	public static readonly double minutesToSeconds = 60.0;

	// Token: 0x040001D5 RID: 469
	public static readonly double hoursToSeconds = HGUnitConversions.hoursToMinutes * HGUnitConversions.minutesToSeconds;
}
