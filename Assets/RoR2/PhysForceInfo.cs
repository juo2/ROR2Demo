using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000986 RID: 2438
	public struct PhysForceInfo
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000E98C4 File Offset: 0x000E7AC4
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x000E98D5 File Offset: 0x000E7AD5
		public bool ignoreGroundStick
		{
			get
			{
				return (this.flags & PhysForceInfo.ignoreGroundStickFlag) != 0;
			}
			set
			{
				PhysForceInfo.SetFlag(ref this.flags, PhysForceInfo.ignoreGroundStickFlag, value);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000E98E8 File Offset: 0x000E7AE8
		// (set) Token: 0x06003754 RID: 14164 RVA: 0x000E98F9 File Offset: 0x000E7AF9
		public bool disableAirControlUntilCollision
		{
			get
			{
				return (this.flags & PhysForceInfo.disableAirControlUntilCollisionFlag) != 0;
			}
			set
			{
				PhysForceInfo.SetFlag(ref this.flags, PhysForceInfo.disableAirControlUntilCollisionFlag, value);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000E990C File Offset: 0x000E7B0C
		// (set) Token: 0x06003756 RID: 14166 RVA: 0x000E991D File Offset: 0x000E7B1D
		public bool massIsOne
		{
			get
			{
				return (this.flags & PhysForceInfo.massIsOneFlag) != 0;
			}
			set
			{
				PhysForceInfo.SetFlag(ref this.flags, PhysForceInfo.massIsOneFlag, value);
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000E9930 File Offset: 0x000E7B30
		private static void SetFlag(ref int flags, int flag, bool value)
		{
			flags = (value ? (flags | flag) : (flags & ~flag));
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000E9944 File Offset: 0x000E7B44
		public static PhysForceInfo Create()
		{
			return new PhysForceInfo
			{
				force = Vector3.zero,
				flags = 0,
				ignoreGroundStick = false,
				disableAirControlUntilCollision = false,
				massIsOne = false
			};
		}

		// Token: 0x040037B0 RID: 14256
		[SerializeField]
		public Vector3 force;

		// Token: 0x040037B1 RID: 14257
		[SerializeField]
		private int flags;

		// Token: 0x040037B2 RID: 14258
		private static readonly int ignoreGroundStickFlag = 1;

		// Token: 0x040037B3 RID: 14259
		private static readonly int disableAirControlUntilCollisionFlag = 2;

		// Token: 0x040037B4 RID: 14260
		private static readonly int massIsOneFlag = 4;
	}
}
