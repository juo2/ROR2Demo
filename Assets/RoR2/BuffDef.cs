using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000526 RID: 1318
	[CreateAssetMenu(menuName = "RoR2/BuffDef")]
	public class BuffDef : ScriptableObject
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00069331 File Offset: 0x00067531
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00069339 File Offset: 0x00067539
		public BuffIndex buffIndex { get; set; } = BuffIndex.None;

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00069342 File Offset: 0x00067542
		public bool isElite
		{
			get
			{
				return this.eliteDef != null;
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0006934D File Offset: 0x0006754D
		protected void OnValidate()
		{
			this.ReplaceIconFromPathWithDirectReference();
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00069358 File Offset: 0x00067558
		[ContextMenu("Update iconPath to iconSprite")]
		private void ReplaceIconFromPathWithDirectReference()
		{
			string text = this.iconPath;
			if (!string.IsNullOrEmpty(text))
			{
				Sprite exists = LegacyResourcesAPI.Load<Sprite>(text);
				if (exists)
				{
					this.iconSprite = exists;
					this.iconPath = string.Empty;
					EditorUtil.SetDirty(this);
				}
			}
		}

		// Token: 0x04001D91 RID: 7569
		[Obsolete("BuffDef.iconPath is deprecated and no longer functions. Use .iconSprite instead.", false)]
		public string iconPath = "Textures/ItemIcons/texNullIcon";

		// Token: 0x04001D92 RID: 7570
		public Sprite iconSprite;

		// Token: 0x04001D93 RID: 7571
		public Color buffColor = Color.white;

		// Token: 0x04001D94 RID: 7572
		public bool canStack;

		// Token: 0x04001D95 RID: 7573
		public EliteDef eliteDef;

		// Token: 0x04001D96 RID: 7574
		public bool isDebuff;

		// Token: 0x04001D97 RID: 7575
		public bool isCooldown;

		// Token: 0x04001D98 RID: 7576
		public bool isHidden;

		// Token: 0x04001D99 RID: 7577
		public NetworkSoundEventDef startSfx;
	}
}
