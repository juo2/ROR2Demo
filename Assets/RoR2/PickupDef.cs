using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000989 RID: 2441
	public class PickupDef
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000E9EDE File Offset: 0x000E80DE
		// (set) Token: 0x06003771 RID: 14193 RVA: 0x000E9EE6 File Offset: 0x000E80E6
		public PickupIndex pickupIndex { get; set; } = PickupIndex.none;

		// Token: 0x040037C2 RID: 14274
		public string internalName;

		// Token: 0x040037C3 RID: 14275
		public GameObject displayPrefab;

		// Token: 0x040037C4 RID: 14276
		public GameObject dropletDisplayPrefab;

		// Token: 0x040037C5 RID: 14277
		public string nameToken = "???";

		// Token: 0x040037C6 RID: 14278
		public Color baseColor;

		// Token: 0x040037C7 RID: 14279
		public Color darkColor;

		// Token: 0x040037C8 RID: 14280
		public ItemIndex itemIndex = ItemIndex.None;

		// Token: 0x040037C9 RID: 14281
		public EquipmentIndex equipmentIndex = EquipmentIndex.None;

		// Token: 0x040037CA RID: 14282
		public ArtifactIndex artifactIndex = ArtifactIndex.None;

		// Token: 0x040037CB RID: 14283
		public MiscPickupIndex miscPickupIndex = MiscPickupIndex.None;

		// Token: 0x040037CC RID: 14284
		public ItemTier itemTier = ItemTier.NoTier;

		// Token: 0x040037CD RID: 14285
		public uint coinValue;

		// Token: 0x040037CE RID: 14286
		public UnlockableDef unlockableDef;

		// Token: 0x040037CF RID: 14287
		public string interactContextToken;

		// Token: 0x040037D0 RID: 14288
		public bool isLunar;

		// Token: 0x040037D1 RID: 14289
		public bool isBoss;

		// Token: 0x040037D2 RID: 14290
		public Texture iconTexture;

		// Token: 0x040037D3 RID: 14291
		public Sprite iconSprite;

		// Token: 0x040037D4 RID: 14292
		public PickupDef.AttemptGrantDelegate attemptGrant;

		// Token: 0x0200098A RID: 2442
		public struct GrantContext
		{
			// Token: 0x040037D5 RID: 14293
			public CharacterBody body;

			// Token: 0x040037D6 RID: 14294
			public GenericPickupController controller;

			// Token: 0x040037D7 RID: 14295
			public bool shouldDestroy;

			// Token: 0x040037D8 RID: 14296
			public bool shouldNotify;
		}

		// Token: 0x0200098B RID: 2443
		// (Invoke) Token: 0x06003774 RID: 14196
		public delegate void AttemptGrantDelegate(ref PickupDef.GrantContext context);
	}
}
