using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D54 RID: 3412
	public class Nameplate : MonoBehaviour
	{
		// Token: 0x06004E38 RID: 20024 RVA: 0x00142C41 File Offset: 0x00140E41
		public void SetBody(CharacterBody body)
		{
			this.body = body;
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00142C4C File Offset: 0x00140E4C
		private void LateUpdate()
		{
			string text = "";
			Color color = this.baseColor;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			if (this.body)
			{
				text = this.body.GetDisplayName();
				flag = this.body.healthComponent.alive;
				flag2 = (!this.body.outOfCombat || !this.body.outOfDanger);
				flag3 = this.body.healthComponent.isHealthLow;
				CharacterMaster master = this.body.master;
				if (master)
				{
					PlayerCharacterMasterController component = master.GetComponent<PlayerCharacterMasterController>();
					if (component)
					{
						GameObject networkUserObject = component.networkUserObject;
						if (networkUserObject)
						{
							NetworkUser component2 = networkUserObject.GetComponent<NetworkUser>();
							if (component2)
							{
								text = component2.userName;
							}
						}
					}
					else
					{
						text = Language.GetString(this.body.baseNameToken);
					}
				}
			}
			color = (flag2 ? this.combatColor : this.baseColor);
			this.aliveObject.SetActive(flag);
			this.deadObject.SetActive(!flag);
			if (this.criticallyHurtSpriteRenderer)
			{
				this.criticallyHurtSpriteRenderer.enabled = (flag3 && flag);
				this.criticallyHurtSpriteRenderer.color = HealthBar.GetCriticallyHurtColor();
			}
			if (this.label)
			{
				this.label.text = text;
				this.label.color = color;
			}
			SpriteRenderer[] array = this.coloredSprites;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = color;
			}
		}

		// Token: 0x04004AE2 RID: 19170
		public TextMeshPro label;

		// Token: 0x04004AE3 RID: 19171
		private CharacterBody body;

		// Token: 0x04004AE4 RID: 19172
		public GameObject aliveObject;

		// Token: 0x04004AE5 RID: 19173
		public GameObject deadObject;

		// Token: 0x04004AE6 RID: 19174
		public SpriteRenderer criticallyHurtSpriteRenderer;

		// Token: 0x04004AE7 RID: 19175
		public SpriteRenderer[] coloredSprites;

		// Token: 0x04004AE8 RID: 19176
		public Color baseColor;

		// Token: 0x04004AE9 RID: 19177
		public Color combatColor;
	}
}
