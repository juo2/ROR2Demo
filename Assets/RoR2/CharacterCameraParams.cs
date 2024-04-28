using System;
using HG;
using HG.BlendableTypes;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000527 RID: 1319
	[CreateAssetMenu(menuName = "RoR2/CharacterCameraParams")]
	public class CharacterCameraParams : ScriptableObject
	{
		// Token: 0x060017F8 RID: 6136 RVA: 0x000693C0 File Offset: 0x000675C0
		private void Awake()
		{
			this.UpdateVersion();
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000693C8 File Offset: 0x000675C8
		private void UpdateVersion()
		{
			if (this.version < 1)
			{
				CharacterCameraParams.SetBlendableFromValue(out this.data.minPitch, this.minPitch, CharacterCameraParams.minPitchDefault);
				CharacterCameraParams.SetBlendableFromValue(out this.data.maxPitch, this.maxPitch, CharacterCameraParams.maxPitchDefault);
				CharacterCameraParams.SetBlendableFromValue(out this.data.wallCushion, this.wallCushion, CharacterCameraParams.wallCushionDefault);
				CharacterCameraParams.SetBlendableFromValue(out this.data.pivotVerticalOffset, this.pivotVerticalOffset, CharacterCameraParams.pivotVerticalOffsetDefault);
				CharacterCameraParams.SetBlendableFromValue(out this.data.idealLocalCameraPos, this.standardLocalCameraPos, CharacterCameraParams.standardLocalCameraPosDefault);
				this.version = 1;
				return;
			}
			this.minPitch = this.data.minPitch.value;
			this.maxPitch = this.data.maxPitch.value;
			this.wallCushion = this.data.wallCushion.value;
			this.pivotVerticalOffset = this.data.pivotVerticalOffset.value;
			this.standardLocalCameraPos = this.data.idealLocalCameraPos.value;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000694DE File Offset: 0x000676DE
		private static void SetBlendableFromValue(out BlendableFloat dest, float src, float defaultValue)
		{
			dest.value = src;
			dest.alpha = (src.Equals(defaultValue) ? 0f : 1f);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00069503 File Offset: 0x00067703
		private static void SetBlendableFromValue(out BlendableVector3 dest, Vector3 src, Vector3 defaultValue)
		{
			dest.value = src;
			dest.alpha = (src.Equals(defaultValue) ? 0f : 1f);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00069528 File Offset: 0x00067728
		private static void SetBlendableFromValue(out BlendableBool dest, bool src, bool defaultValue)
		{
			dest.value = src;
			dest.alpha = (src.Equals(defaultValue) ? 0f : 1f);
		}

		// Token: 0x04001D9A RID: 7578
		[HideInInspector]
		[SerializeField]
		private int version;

		// Token: 0x04001D9B RID: 7579
		public CharacterCameraParamsData data;

		// Token: 0x04001D9C RID: 7580
		private static readonly float minPitchDefault = -70f;

		// Token: 0x04001D9D RID: 7581
		private static readonly float maxPitchDefault = 70f;

		// Token: 0x04001D9E RID: 7582
		private static readonly float wallCushionDefault = 0.1f;

		// Token: 0x04001D9F RID: 7583
		private static readonly float pivotVerticalOffsetDefault = 1.6f;

		// Token: 0x04001DA0 RID: 7584
		private static readonly Vector3 standardLocalCameraPosDefault = new Vector3(0f, 0f, -5f);

		// Token: 0x04001DA1 RID: 7585
		[Obsolete("Use data.minPitch instead.", false)]
		[ShowFieldObsolete]
		public float minPitch = CharacterCameraParams.minPitchDefault;

		// Token: 0x04001DA2 RID: 7586
		[Obsolete("Use data.maxPitch instead.", false)]
		[ShowFieldObsolete]
		public float maxPitch = CharacterCameraParams.maxPitchDefault;

		// Token: 0x04001DA3 RID: 7587
		[Obsolete("Use data.wallCushion instead.", false)]
		[ShowFieldObsolete]
		public float wallCushion = CharacterCameraParams.wallCushionDefault;

		// Token: 0x04001DA4 RID: 7588
		[ShowFieldObsolete]
		[Obsolete("Use data.pivotVerticalOffset instead.", false)]
		public float pivotVerticalOffset = CharacterCameraParams.pivotVerticalOffsetDefault;

		// Token: 0x04001DA5 RID: 7589
		[ShowFieldObsolete]
		[Obsolete("Use data.standardLocalCameraPos instead.", false)]
		public Vector3 standardLocalCameraPos = CharacterCameraParams.standardLocalCameraPosDefault;
	}
}
