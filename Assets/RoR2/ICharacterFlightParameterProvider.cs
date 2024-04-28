using System;

namespace RoR2
{
	// Token: 0x02000750 RID: 1872
	public interface ICharacterFlightParameterProvider
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060026D9 RID: 9945
		// (set) Token: 0x060026DA RID: 9946
		CharacterFlightParameters flightParameters { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060026DB RID: 9947
		bool isFlying { get; }
	}
}
