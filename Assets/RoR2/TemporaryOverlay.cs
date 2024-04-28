using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C2 RID: 2242
	public class TemporaryOverlay : MonoBehaviour
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x000D4292 File Offset: 0x000D2492
		private void Start()
		{
			this.SetupMaterial();
			if (this.inspectorCharacterModel)
			{
				this.AddToCharacerModel(this.inspectorCharacterModel);
			}
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000D42B3 File Offset: 0x000D24B3
		private void SetupMaterial()
		{
			if (!this.materialInstance)
			{
				this.materialInstance = new Material(this.originalMaterial);
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000D42D3 File Offset: 0x000D24D3
		public void AddToCharacerModel(CharacterModel characterModel)
		{
			this.SetupMaterial();
			if (characterModel)
			{
				characterModel.temporaryOverlays.Add(this);
				this.isAssigned = true;
				this.assignedCharacterModel = characterModel;
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000D42FD File Offset: 0x000D24FD
		public void RemoveFromCharacterModel()
		{
			if (this.assignedCharacterModel)
			{
				this.assignedCharacterModel.temporaryOverlays.Remove(this);
				this.isAssigned = false;
				this.assignedCharacterModel = null;
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000D432C File Offset: 0x000D252C
		private void OnDestroy()
		{
			this.RemoveFromCharacterModel();
			if (this.materialInstance)
			{
				UnityEngine.Object.Destroy(this.materialInstance);
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000D434C File Offset: 0x000D254C
		private void Update()
		{
			if (this.animateShaderAlpha)
			{
				this.stopwatch += Time.deltaTime;
				float value = this.alphaCurve.Evaluate(this.stopwatch / this.duration);
				this.materialInstance.SetFloat("_ExternalAlpha", value);
				if (this.stopwatch >= this.duration && (this.destroyComponentOnEnd || this.destroyObjectOnEnd))
				{
					if (this.destroyEffectPrefab)
					{
						ChildLocator component = base.GetComponent<ChildLocator>();
						if (component)
						{
							Transform transform = component.FindChild(this.destroyEffectChildString);
							if (transform)
							{
								EffectManager.SpawnEffect(this.destroyEffectPrefab, new EffectData
								{
									origin = transform.position,
									rotation = transform.rotation
								}, true);
							}
						}
					}
					if (this.destroyObjectOnEnd)
					{
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
					UnityEngine.Object.Destroy(this);
				}
			}
		}

		// Token: 0x04003355 RID: 13141
		public Material originalMaterial;

		// Token: 0x04003356 RID: 13142
		[HideInInspector]
		public Material materialInstance;

		// Token: 0x04003357 RID: 13143
		private bool isAssigned;

		// Token: 0x04003358 RID: 13144
		private CharacterModel assignedCharacterModel;

		// Token: 0x04003359 RID: 13145
		public CharacterModel inspectorCharacterModel;

		// Token: 0x0400335A RID: 13146
		public bool animateShaderAlpha;

		// Token: 0x0400335B RID: 13147
		public AnimationCurve alphaCurve;

		// Token: 0x0400335C RID: 13148
		public float duration;

		// Token: 0x0400335D RID: 13149
		public bool destroyComponentOnEnd;

		// Token: 0x0400335E RID: 13150
		public bool destroyObjectOnEnd;

		// Token: 0x0400335F RID: 13151
		public GameObject destroyEffectPrefab;

		// Token: 0x04003360 RID: 13152
		public string destroyEffectChildString;

		// Token: 0x04003361 RID: 13153
		private float stopwatch;
	}
}
