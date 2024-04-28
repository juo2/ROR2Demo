using System;
using HG.BlendableTypes;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000528 RID: 1320
	[Serializable]
	public struct CharacterCameraParamsData
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x000695DC File Offset: 0x000677DC
		public static void Blend(in CharacterCameraParamsData src, ref CharacterCameraParamsData dest, float alpha)
		{
			BlendableFloat.Blend(src.minPitch, ref dest.minPitch, alpha);
			BlendableFloat.Blend(src.maxPitch, ref dest.maxPitch, alpha);
			BlendableFloat.Blend(src.wallCushion, ref dest.wallCushion, alpha);
			BlendableFloat.Blend(src.pivotVerticalOffset, ref dest.pivotVerticalOffset, alpha);
			BlendableVector3.Blend(src.idealLocalCameraPos, ref dest.idealLocalCameraPos, alpha);
			BlendableFloat.Blend(src.fov, ref dest.fov, alpha);
			BlendableBool.Blend(src.isFirstPerson, ref dest.isFirstPerson, alpha);
		}

		// Token: 0x04001DA6 RID: 7590
		public BlendableFloat minPitch;

		// Token: 0x04001DA7 RID: 7591
		public BlendableFloat maxPitch;

		// Token: 0x04001DA8 RID: 7592
		public BlendableFloat wallCushion;

		// Token: 0x04001DA9 RID: 7593
		public BlendableFloat pivotVerticalOffset;

		// Token: 0x04001DAA RID: 7594
		public BlendableVector3 idealLocalCameraPos;

		// Token: 0x04001DAB RID: 7595
		public BlendableFloat fov;

		// Token: 0x04001DAC RID: 7596
		public BlendableBool isFirstPerson;

		// Token: 0x04001DAD RID: 7597
		public static readonly CharacterCameraParamsData basic = new CharacterCameraParamsData
		{
			minPitch = -70f,
			maxPitch = 70f,
			wallCushion = 0.1f,
			pivotVerticalOffset = 1.6f,
			idealLocalCameraPos = new Vector3(0f, 0f, -5f),
			fov = new BlendableFloat
			{
				value = 60f,
				alpha = 0f
			}
		};
	}
}
