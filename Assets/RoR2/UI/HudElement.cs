using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D1B RID: 3355
	[DisallowMultipleComponent]
	public class HudElement : MonoBehaviour
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x0013BCEF File Offset: 0x00139EEF
		// (set) Token: 0x06004C75 RID: 19573 RVA: 0x0013BCF7 File Offset: 0x00139EF7
		public HUD hud
		{
			get
			{
				return this._hud;
			}
			set
			{
				this._hud = value;
				if (this._hud)
				{
					this.targetBodyObject = this._hud.targetBodyObject;
					return;
				}
				this.targetBodyObject = null;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x0013BD26 File Offset: 0x00139F26
		// (set) Token: 0x06004C77 RID: 19575 RVA: 0x0013BD2E File Offset: 0x00139F2E
		public GameObject targetBodyObject
		{
			get
			{
				return this._targetBodyObject;
			}
			set
			{
				this._targetBodyObject = value;
				if (this._targetBodyObject)
				{
					this._targetCharacterBody = this._targetBodyObject.GetComponent<CharacterBody>();
				}
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x0013BD55 File Offset: 0x00139F55
		// (set) Token: 0x06004C79 RID: 19577 RVA: 0x0013BD5D File Offset: 0x00139F5D
		public CharacterBody targetCharacterBody
		{
			get
			{
				return this._targetCharacterBody;
			}
			set
			{
				this._targetCharacterBody = value;
				if (this.targetCharacterBody)
				{
					this._targetBodyObject = this.targetCharacterBody.gameObject;
				}
			}
		}

		// Token: 0x0400497E RID: 18814
		private HUD _hud;

		// Token: 0x0400497F RID: 18815
		private GameObject _targetBodyObject;

		// Token: 0x04004980 RID: 18816
		private CharacterBody _targetCharacterBody;
	}
}
