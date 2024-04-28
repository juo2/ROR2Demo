using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public struct CharacterGravityParameters : IEquatable<CharacterGravityParameters>
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x000A8ED7 File Offset: 0x000A70D7
		public bool Equals(CharacterGravityParameters other)
		{
			return this.environmentalAntiGravityGranterCount == other.environmentalAntiGravityGranterCount && this.antiGravityNeutralizerCount == other.antiGravityNeutralizerCount && this.channeledAntiGravityGranterCount == other.channeledAntiGravityGranterCount;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000A8F08 File Offset: 0x000A7108
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is CharacterGravityParameters)
			{
				CharacterGravityParameters other = (CharacterGravityParameters)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000A8F34 File Offset: 0x000A7134
		public override int GetHashCode()
		{
			return (this.environmentalAntiGravityGranterCount * 397 ^ this.antiGravityNeutralizerCount) * 397 ^ this.channeledAntiGravityGranterCount;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000A8F56 File Offset: 0x000A7156
		public bool CheckShouldUseGravity()
		{
			return this.environmentalAntiGravityGranterCount <= 0 && (this.antiGravityNeutralizerCount > 0 || this.channeledAntiGravityGranterCount <= 0);
		}

		// Token: 0x04002AC1 RID: 10945
		[Tooltip("AntiGravity granted by Zero-G environments. Provides AntiGravity if greater than zero and takes precedence over all other parameters.")]
		public int environmentalAntiGravityGranterCount;

		// Token: 0x04002AC2 RID: 10946
		[Tooltip("AntiGravity neutralizers, like debuffs. Neutralizes non-environmental AntiGravity if greater than zero.")]
		public int antiGravityNeutralizerCount;

		// Token: 0x04002AC3 RID: 10947
		[Tooltip("AntiGravity granted by body, skills, and items. Provides AntiGravity if greater than zero.")]
		public int channeledAntiGravityGranterCount;
	}
}
