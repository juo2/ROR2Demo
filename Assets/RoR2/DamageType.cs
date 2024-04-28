using System;

namespace RoR2
{
	// Token: 0x0200057B RID: 1403
	[Flags]
	public enum DamageType : uint
	{
		// Token: 0x04001F57 RID: 8023
		Generic = 0U,
		// Token: 0x04001F58 RID: 8024
		NonLethal = 1U,
		// Token: 0x04001F59 RID: 8025
		BypassArmor = 2U,
		// Token: 0x04001F5A RID: 8026
		ResetCooldownsOnKill = 4U,
		// Token: 0x04001F5B RID: 8027
		SlowOnHit = 8U,
		// Token: 0x04001F5C RID: 8028
		WeakPointHit = 16U,
		// Token: 0x04001F5D RID: 8029
		Stun1s = 32U,
		// Token: 0x04001F5E RID: 8030
		BypassBlock = 64U,
		// Token: 0x04001F5F RID: 8031
		IgniteOnHit = 128U,
		// Token: 0x04001F60 RID: 8032
		Freeze2s = 256U,
		// Token: 0x04001F61 RID: 8033
		ClayGoo = 512U,
		// Token: 0x04001F62 RID: 8034
		BleedOnHit = 1024U,
		// Token: 0x04001F63 RID: 8035
		Silent = 2048U,
		// Token: 0x04001F64 RID: 8036
		PoisonOnHit = 4096U,
		// Token: 0x04001F65 RID: 8037
		PercentIgniteOnHit = 8192U,
		// Token: 0x04001F66 RID: 8038
		WeakOnHit = 16384U,
		// Token: 0x04001F67 RID: 8039
		Nullify = 32768U,
		// Token: 0x04001F68 RID: 8040
		VoidDeath = 65536U,
		// Token: 0x04001F69 RID: 8041
		AOE = 131072U,
		// Token: 0x04001F6A RID: 8042
		BypassOneShotProtection = 262144U,
		// Token: 0x04001F6B RID: 8043
		BonusToLowHealth = 524288U,
		// Token: 0x04001F6C RID: 8044
		BlightOnHit = 1048576U,
		// Token: 0x04001F6D RID: 8045
		FallDamage = 2097152U,
		// Token: 0x04001F6E RID: 8046
		CrippleOnHit = 4194304U,
		// Token: 0x04001F6F RID: 8047
		ApplyMercExpose = 8388608U,
		// Token: 0x04001F70 RID: 8048
		Shock5s = 16777216U,
		// Token: 0x04001F71 RID: 8049
		LunarSecondaryRootOnHit = 33554432U,
		// Token: 0x04001F72 RID: 8050
		DoT = 67108864U,
		// Token: 0x04001F73 RID: 8051
		SuperBleedOnCrit = 134217728U,
		// Token: 0x04001F74 RID: 8052
		GiveSkullOnKill = 268435456U,
		// Token: 0x04001F75 RID: 8053
		FruitOnHit = 536870912U,
		// Token: 0x04001F76 RID: 8054
		OutOfBounds = 1073741824U
	}
}
