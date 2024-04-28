using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GrandParentSun
{
	// Token: 0x0200034C RID: 844
	public abstract class GrandParentSunBase : BaseState
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000413EC File Offset: 0x0003F5EC
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x000413F4 File Offset: 0x0003F5F4
		private protected GrandParentSunController sunController { protected get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x000413FD File Offset: 0x0003F5FD
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00041405 File Offset: 0x0003F605
		private protected Transform vfxRoot { protected get; private set; }

		// Token: 0x06000F1F RID: 3871 RVA: 0x00041410 File Offset: 0x0003F610
		public override void OnEnter()
		{
			base.OnEnter();
			this.sunController = base.GetComponent<GrandParentSunController>();
			this.sunController.enabled = this.shouldEnableSunController;
			this.vfxRoot = base.transform.Find("VfxRoot");
			if (this.enterEffectPrefab)
			{
				EffectManager.SimpleImpactEffect(this.enterEffectPrefab, this.vfxRoot.position, Vector3.up, false);
			}
			this.SetVfxScale(this.desiredVfxScale);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0004148B File Offset: 0x0003F68B
		public override void Update()
		{
			base.Update();
			this.SetVfxScale(this.desiredVfxScale);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldEnableSunController
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000F22 RID: 3874
		protected abstract float desiredVfxScale { get; }

		// Token: 0x06000F23 RID: 3875 RVA: 0x000414A0 File Offset: 0x0003F6A0
		private void SetVfxScale(float newScale)
		{
			newScale = Mathf.Max(newScale, 0.01f);
			if (this.vfxRoot)
			{
				if (this.vfxRoot.transform.localScale.x == newScale)
				{
					return;
				}
				this.vfxRoot.transform.localScale = new Vector3(newScale, newScale, newScale);
			}
		}

		// Token: 0x040012EB RID: 4843
		[SerializeField]
		public GameObject enterEffectPrefab;
	}
}
