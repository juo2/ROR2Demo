using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007FF RID: 2047
	public class PickupDisplay : MonoBehaviour
	{
		// Token: 0x06002C1A RID: 11290 RVA: 0x000BC940 File Offset: 0x000BAB40
		public void SetPickupIndex(PickupIndex newPickupIndex, bool newHidden = false)
		{
			if (this.pickupIndex == newPickupIndex && this.hidden == newHidden)
			{
				return;
			}
			this.pickupIndex = newPickupIndex;
			this.hidden = newHidden;
			this.RebuildModel();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000BC96E File Offset: 0x000BAB6E
		private void DestroyModel()
		{
			if (this.modelObject)
			{
				UnityEngine.Object.Destroy(this.modelObject);
				this.modelObject = null;
				this.modelRenderer = null;
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000BC998 File Offset: 0x000BAB98
		private void RebuildModel()
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
			GameObject y = null;
			if (pickupDef != null)
			{
				y = (this.hidden ? PickupCatalog.GetHiddenPickupDisplayPrefab() : pickupDef.displayPrefab);
			}
			if (this.modelPrefab != y)
			{
				this.DestroyModel();
				this.modelPrefab = y;
				this.modelScale = base.transform.lossyScale.x;
				if (!this.dontInstantiatePickupModel && this.modelPrefab != null)
				{
					this.modelObject = UnityEngine.Object.Instantiate<GameObject>(this.modelPrefab);
					this.modelRenderer = this.modelObject.GetComponentInChildren<Renderer>();
					if (this.modelRenderer)
					{
						this.modelObject.transform.rotation = Quaternion.identity;
						Vector3 size = this.modelRenderer.bounds.size;
						float num = size.x * size.y * size.z;
						if (num <= 1E-45f)
						{
							Debug.LogError("PickupDisplay bounds are zero! This is not allowed!");
							num = 1f;
						}
						this.modelScale *= Mathf.Pow(PickupDisplay.idealVolume, 0.33333334f) / Mathf.Pow(num, 0.33333334f);
						if (this.highlight)
						{
							this.highlight.targetRenderer = this.modelRenderer;
						}
					}
					this.modelObject.transform.parent = base.transform;
					this.modelObject.transform.localPosition = this.localModelPivotPosition;
					this.modelObject.transform.localRotation = Quaternion.identity;
					this.modelObject.transform.localScale = new Vector3(this.modelScale, this.modelScale, this.modelScale);
				}
			}
			if (this.tier1ParticleEffect)
			{
				this.tier1ParticleEffect.SetActive(false);
			}
			if (this.tier2ParticleEffect)
			{
				this.tier2ParticleEffect.SetActive(false);
			}
			if (this.tier3ParticleEffect)
			{
				this.tier3ParticleEffect.SetActive(false);
			}
			if (this.equipmentParticleEffect)
			{
				this.equipmentParticleEffect.SetActive(false);
			}
			if (this.lunarParticleEffect)
			{
				this.lunarParticleEffect.SetActive(false);
			}
			if (this.voidParticleEffect)
			{
				this.voidParticleEffect.SetActive(false);
			}
			ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
			EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
			if (itemIndex != ItemIndex.None)
			{
				switch (ItemCatalog.GetItemDef(itemIndex).tier)
				{
				case ItemTier.Tier1:
					if (this.tier1ParticleEffect)
					{
						this.tier1ParticleEffect.SetActive(true);
					}
					break;
				case ItemTier.Tier2:
					if (this.tier2ParticleEffect)
					{
						this.tier2ParticleEffect.SetActive(true);
					}
					break;
				case ItemTier.Tier3:
					if (this.tier3ParticleEffect)
					{
						this.tier3ParticleEffect.SetActive(true);
					}
					break;
				case ItemTier.VoidTier1:
				case ItemTier.VoidTier2:
				case ItemTier.VoidTier3:
				case ItemTier.VoidBoss:
					if (this.voidParticleEffect)
					{
						this.voidParticleEffect.SetActive(true);
					}
					break;
				}
			}
			else if (equipmentIndex != EquipmentIndex.None && this.equipmentParticleEffect)
			{
				this.equipmentParticleEffect.SetActive(true);
			}
			if (this.bossParticleEffect)
			{
				this.bossParticleEffect.SetActive(pickupDef != null && pickupDef.isBoss);
			}
			if (this.lunarParticleEffect)
			{
				this.lunarParticleEffect.SetActive(pickupDef != null && pickupDef.isLunar);
			}
			if (this.highlight)
			{
				this.highlight.isOn = true;
				this.highlight.pickupIndex = this.pickupIndex;
			}
			foreach (ParticleSystem particleSystem in this.coloredParticleSystems)
			{
				particleSystem.gameObject.SetActive(this.modelPrefab != null);
				particleSystem.main.startColor = ((pickupDef != null) ? pickupDef.baseColor : PickupCatalog.invalidPickupColor);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000BCDA8 File Offset: 0x000BAFA8
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x000BCDB0 File Offset: 0x000BAFB0
		public Renderer modelRenderer { get; private set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000BCDB9 File Offset: 0x000BAFB9
		private Vector3 localModelPivotPosition
		{
			get
			{
				return Vector3.up * this.verticalWave.Evaluate(this.localTime);
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000BCDD6 File Offset: 0x000BAFD6
		private void Start()
		{
			this.localTime = 0f;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000BCDE4 File Offset: 0x000BAFE4
		private void Update()
		{
			this.localTime += Time.deltaTime;
			if (this.modelObject)
			{
				Transform transform = this.modelObject.transform;
				Vector3 localEulerAngles = transform.localEulerAngles;
				localEulerAngles.y = this.spinSpeed * this.localTime;
				transform.localEulerAngles = localEulerAngles;
				transform.localPosition = this.localModelPivotPosition;
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000BCE48 File Offset: 0x000BB048
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, base.transform.lossyScale);
			Gizmos.DrawWireCube(Vector3.zero, PickupDisplay.idealModelBox);
			Gizmos.matrix = matrix;
		}

		// Token: 0x04002E8B RID: 11915
		[Tooltip("The vertical motion of the display model.")]
		public Wave verticalWave;

		// Token: 0x04002E8C RID: 11916
		public bool dontInstantiatePickupModel;

		// Token: 0x04002E8D RID: 11917
		[Tooltip("The speed in degrees/second at which the display model rotates about the y axis.")]
		public float spinSpeed = 75f;

		// Token: 0x04002E8E RID: 11918
		public GameObject tier1ParticleEffect;

		// Token: 0x04002E8F RID: 11919
		public GameObject tier2ParticleEffect;

		// Token: 0x04002E90 RID: 11920
		public GameObject tier3ParticleEffect;

		// Token: 0x04002E91 RID: 11921
		public GameObject equipmentParticleEffect;

		// Token: 0x04002E92 RID: 11922
		public GameObject lunarParticleEffect;

		// Token: 0x04002E93 RID: 11923
		public GameObject bossParticleEffect;

		// Token: 0x04002E94 RID: 11924
		public GameObject voidParticleEffect;

		// Token: 0x04002E95 RID: 11925
		[Tooltip("The particle system to tint.")]
		public ParticleSystem[] coloredParticleSystems;

		// Token: 0x04002E96 RID: 11926
		private PickupIndex pickupIndex = PickupIndex.none;

		// Token: 0x04002E97 RID: 11927
		private bool hidden;

		// Token: 0x04002E98 RID: 11928
		public Highlight highlight;

		// Token: 0x04002E99 RID: 11929
		private static readonly Vector3 idealModelBox = Vector3.one;

		// Token: 0x04002E9A RID: 11930
		private static readonly float idealVolume = PickupDisplay.idealModelBox.x * PickupDisplay.idealModelBox.y * PickupDisplay.idealModelBox.z;

		// Token: 0x04002E9B RID: 11931
		private GameObject modelObject;

		// Token: 0x04002E9D RID: 11933
		private GameObject modelPrefab;

		// Token: 0x04002E9E RID: 11934
		private float modelScale;

		// Token: 0x04002E9F RID: 11935
		private float localTime;
	}
}
