using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000554 RID: 1364
	public abstract class MiscPickupDef : ScriptableObject
	{
		// Token: 0x060018C2 RID: 6338
		public abstract void GrantPickup(ref PickupDef.GrantContext context);

		// Token: 0x060018C3 RID: 6339 RVA: 0x0006BBF9 File Offset: 0x00069DF9
		public virtual string GetInternalName()
		{
			return "MiscPickupIndex." + base.name;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006BC0C File Offset: 0x00069E0C
		public virtual PickupDef CreatePickupDef()
		{
			return new PickupDef
			{
				internalName = this.GetInternalName(),
				coinValue = this.coinValue,
				nameToken = this.nameToken,
				displayPrefab = this.displayPrefab,
				dropletDisplayPrefab = this.dropletDisplayPrefab,
				baseColor = ColorCatalog.GetColor(this.baseColor),
				darkColor = ColorCatalog.GetColor(this.darkColor),
				interactContextToken = this.interactContextToken,
				attemptGrant = new PickupDef.AttemptGrantDelegate(this.GrantPickup),
				miscPickupIndex = this.miscPickupIndex
			};
		}

		// Token: 0x04001E60 RID: 7776
		[SerializeField]
		public uint coinValue;

		// Token: 0x04001E61 RID: 7777
		[SerializeField]
		public string nameToken;

		// Token: 0x04001E62 RID: 7778
		[SerializeField]
		public GameObject displayPrefab;

		// Token: 0x04001E63 RID: 7779
		[SerializeField]
		public GameObject dropletDisplayPrefab;

		// Token: 0x04001E64 RID: 7780
		[SerializeField]
		public ColorCatalog.ColorIndex baseColor;

		// Token: 0x04001E65 RID: 7781
		[SerializeField]
		public ColorCatalog.ColorIndex darkColor;

		// Token: 0x04001E66 RID: 7782
		[SerializeField]
		public string interactContextToken;

		// Token: 0x04001E67 RID: 7783
		[NonSerialized]
		public MiscPickupIndex miscPickupIndex;
	}
}
