using System;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x020004CE RID: 1230
	[CreateAssetMenu(menuName = "RoR2/ArtifactDef")]
	public class ArtifactDef : ScriptableObject
	{
		// Token: 0x0600164A RID: 5706 RVA: 0x00062620 File Offset: 0x00060820
		public static void AttemptGrant(ref PickupDef.GrantContext context)
		{
			ArtifactDef artifactDef = ArtifactCatalog.GetArtifactDef(PickupCatalog.GetPickupDef(context.controller.pickupIndex).artifactIndex);
			Run.instance.GrantUnlockToAllParticipatingPlayers(artifactDef.unlockableDef);
			context.shouldNotify = true;
			context.shouldDestroy = true;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00062666 File Offset: 0x00060866
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0006266E File Offset: 0x0006086E
		public ArtifactIndex artifactIndex { get; set; }

		// Token: 0x0600164D RID: 5709 RVA: 0x00062678 File Offset: 0x00060878
		public virtual PickupDef CreatePickupDef()
		{
			PickupDef pickupDef = new PickupDef();
			pickupDef.internalName = "ArtifactIndex." + this.cachedName;
			pickupDef.artifactIndex = this.artifactIndex;
			pickupDef.displayPrefab = this.pickupModelPrefab;
			pickupDef.nameToken = this.nameToken;
			pickupDef.baseColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Artifact);
			pickupDef.darkColor = pickupDef.baseColor;
			pickupDef.unlockableDef = this.unlockableDef;
			pickupDef.interactContextToken = "ITEM_PICKUP_CONTEXT";
			pickupDef.iconTexture = (this.smallIconSelectedSprite ? this.smallIconSelectedSprite.texture : null);
			pickupDef.iconSprite = (this.smallIconSelectedSprite ? this.smallIconSelectedSprite : null);
			pickupDef.attemptGrant = new PickupDef.AttemptGrantDelegate(ArtifactDef.AttemptGrant);
			return pickupDef;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00062748 File Offset: 0x00060948
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00062748 File Offset: 0x00060948
		private void OnValidate()
		{
			this._cachedName = base.name;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00062756 File Offset: 0x00060956
		[Obsolete(".name should not be used. Use .cachedName instead. If retrieving the value from the engine is absolutely necessary, cast to ScriptableObject first.", true)]
		public new string name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0006275D File Offset: 0x0006095D
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00062765 File Offset: 0x00060965
		public string cachedName
		{
			get
			{
				return this._cachedName;
			}
			set
			{
				base.name = value;
				this._cachedName = value;
			}
		}

		// Token: 0x04001C09 RID: 7177
		public string nameToken;

		// Token: 0x04001C0A RID: 7178
		public string descriptionToken;

		// Token: 0x04001C0B RID: 7179
		public UnlockableDef unlockableDef;

		// Token: 0x04001C0C RID: 7180
		public ExpansionDef requiredExpansion;

		// Token: 0x04001C0D RID: 7181
		public Sprite smallIconSelectedSprite;

		// Token: 0x04001C0E RID: 7182
		public Sprite smallIconDeselectedSprite;

		// Token: 0x04001C0F RID: 7183
		[FormerlySerializedAs("worldModelPrefab")]
		public GameObject pickupModelPrefab;

		// Token: 0x04001C10 RID: 7184
		private string _cachedName;
	}
}
