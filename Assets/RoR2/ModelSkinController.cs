using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007B9 RID: 1977
	[RequireComponent(typeof(CharacterModel))]
	[DisallowMultipleComponent]
	public class ModelSkinController : MonoBehaviour
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000B4B9D File Offset: 0x000B2D9D
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x000B4BA5 File Offset: 0x000B2DA5
		public int currentSkinIndex { get; private set; } = -1;

		// Token: 0x060029D9 RID: 10713 RVA: 0x000B4BAE File Offset: 0x000B2DAE
		private void Awake()
		{
			this.characterModel = base.GetComponent<CharacterModel>();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000B4BBC File Offset: 0x000B2DBC
		private void Start()
		{
			if (this.characterModel.body)
			{
				this.ApplySkin((int)this.characterModel.body.skinIndex);
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000B4BE6 File Offset: 0x000B2DE6
		public void ApplySkin(int skinIndex)
		{
			if (skinIndex == this.currentSkinIndex || (ulong)skinIndex >= (ulong)((long)this.skins.Length))
			{
				return;
			}
			this.skins[skinIndex].Apply(base.gameObject);
			this.currentSkinIndex = skinIndex;
		}

		// Token: 0x04002D27 RID: 11559
		public SkinDef[] skins;

		// Token: 0x04002D29 RID: 11561
		private CharacterModel characterModel;
	}
}
