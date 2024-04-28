using System;

namespace RoR2.Audio
{
	// Token: 0x02000E53 RID: 3667
	public struct AkEventIdArg
	{
		// Token: 0x06005406 RID: 21510 RVA: 0x0015B092 File Offset: 0x00159292
		public static explicit operator AkEventIdArg(string eventName)
		{
			return new AkEventIdArg((eventName == null) ? 0U : AkSoundEngine.GetIDFromString(eventName));
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0015B0A5 File Offset: 0x001592A5
		public static implicit operator AkEventIdArg(uint akEventId)
		{
			return new AkEventIdArg(akEventId);
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x0015B0AD File Offset: 0x001592AD
		public static implicit operator uint(AkEventIdArg akEventIdArg)
		{
			return akEventIdArg.id;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0015B0B5 File Offset: 0x001592B5
		private AkEventIdArg(uint id)
		{
			this.id = id;
		}

		// Token: 0x04004FE5 RID: 20453
		public readonly uint id;
	}
}
