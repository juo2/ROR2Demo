using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200054E RID: 1358
	[CreateAssetMenu(menuName = "RoR2/ItemTierDef")]
	public class ItemTierDef : ScriptableObject
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0006B884 File Offset: 0x00069A84
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0006B907 File Offset: 0x00069B07
		public ItemTier tier
		{
			get
			{
				if (this._tier == ItemTier.AssignedAtRuntime)
				{
					Debug.LogError("ItemTierDef '" + base.name + "' has a tier of 'AssignedAtRuntime'.  Attempting to fix...");
					ItemTierDef itemTierDef = ItemTierCatalog.FindTierDef(base.name);
					this._tier = ((itemTierDef != null) ? itemTierDef._tier : this._tier);
					if (this._tier != ItemTier.AssignedAtRuntime)
					{
						Debug.LogError(string.Format("Able to fix ItemTierDef '{0}' (_tier = {1}).  This is probably because the asset is being duplicated across bundles.", base.name, this._tier));
					}
				}
				return this._tier;
			}
			set
			{
				this._tier = value;
			}
		}

		// Token: 0x04001E49 RID: 7753
		[SerializeField]
		[FormerlySerializedAs("tier")]
		private ItemTier _tier;

		// Token: 0x04001E4A RID: 7754
		public Texture bgIconTexture;

		// Token: 0x04001E4B RID: 7755
		public ColorCatalog.ColorIndex colorIndex;

		// Token: 0x04001E4C RID: 7756
		public ColorCatalog.ColorIndex darkColorIndex;

		// Token: 0x04001E4D RID: 7757
		public bool isDroppable;

		// Token: 0x04001E4E RID: 7758
		public bool canScrap;

		// Token: 0x04001E4F RID: 7759
		public bool canRestack;

		// Token: 0x04001E50 RID: 7760
		public ItemTierDef.PickupRules pickupRules;

		// Token: 0x04001E51 RID: 7761
		public GameObject highlightPrefab;

		// Token: 0x04001E52 RID: 7762
		public GameObject dropletDisplayPrefab;

		// Token: 0x0200054F RID: 1359
		public enum PickupRules
		{
			// Token: 0x04001E54 RID: 7764
			Default,
			// Token: 0x04001E55 RID: 7765
			ConfirmFirst,
			// Token: 0x04001E56 RID: 7766
			ConfirmAll
		}
	}
}
