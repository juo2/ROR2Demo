using System;

namespace RoR2
{
	// Token: 0x02000752 RID: 1874
	public interface ICharacterGravityParameterProvider
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060026E0 RID: 9952
		// (set) Token: 0x060026E1 RID: 9953
		CharacterGravityParameters gravityParameters { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060026E2 RID: 9954
		bool useGravity { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060026E3 RID: 9955
		bool isFlying { get; }
	}
}
