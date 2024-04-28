using System;

namespace RoR2.WwiseUtils
{
	// Token: 0x02000AB0 RID: 2736
	public struct StateSetter
	{
		// Token: 0x06003ED9 RID: 16089 RVA: 0x00103319 File Offset: 0x00101519
		public StateSetter(string name)
		{
			this.name = name;
			this.id = AkSoundEngine.GetIDFromString(name);
			this.expectedEngineValueId = 0U;
			this.valueId = this.expectedEngineValueId;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00103341 File Offset: 0x00101541
		public void FlushIfChanged()
		{
			if (!this.expectedEngineValueId.Equals(this.valueId))
			{
				this.expectedEngineValueId = this.valueId;
				AkSoundEngine.SetState(this.id, this.valueId);
			}
		}

		// Token: 0x04003D25 RID: 15653
		private readonly string name;

		// Token: 0x04003D26 RID: 15654
		private readonly uint id;

		// Token: 0x04003D27 RID: 15655
		private uint expectedEngineValueId;

		// Token: 0x04003D28 RID: 15656
		public uint valueId;
	}
}
