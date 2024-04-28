using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200058A RID: 1418
	public class DifficultyDef
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0006DF17 File Offset: 0x0006C117
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0006DF1F File Offset: 0x0006C11F
		public bool countsAsHardMode { get; private set; }

		// Token: 0x0600196F RID: 6511 RVA: 0x0006DF28 File Offset: 0x0006C128
		public DifficultyDef(float scalingValue, string nameToken, string iconPath, string descriptionToken, Color color, string serverTag, bool countsAsHardMode)
		{
			this.scalingValue = scalingValue;
			this.descriptionToken = descriptionToken;
			this.nameToken = nameToken;
			this.iconPath = iconPath;
			this.color = color;
			this.serverTag = serverTag;
			this.countsAsHardMode = countsAsHardMode;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006DF65 File Offset: 0x0006C165
		public Sprite GetIconSprite()
		{
			if (!this.foundIconSprite)
			{
				this.iconSprite = LegacyResourcesAPI.Load<Sprite>(this.iconPath);
				this.foundIconSprite = this.iconSprite;
			}
			return this.iconSprite;
		}

		// Token: 0x04001FD6 RID: 8150
		public readonly float scalingValue;

		// Token: 0x04001FD7 RID: 8151
		public readonly string descriptionToken;

		// Token: 0x04001FD8 RID: 8152
		public readonly string nameToken;

		// Token: 0x04001FD9 RID: 8153
		public readonly string iconPath;

		// Token: 0x04001FDA RID: 8154
		public readonly Color color;

		// Token: 0x04001FDB RID: 8155
		public readonly string serverTag;

		// Token: 0x04001FDD RID: 8157
		private Sprite iconSprite;

		// Token: 0x04001FDE RID: 8158
		private bool foundIconSprite;
	}
}
