using System;
using System.Text;
using HG;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CCE RID: 3278
	public class ChargeIndicatorController : MonoBehaviour
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00132E1E File Offset: 0x0013101E
		// (set) Token: 0x06004AB1 RID: 19121 RVA: 0x00132E26 File Offset: 0x00131026
		public uint chargeValue { get; set; }

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00132E30 File Offset: 0x00131030
		private void Update()
		{
			if (this.holdoutZoneController)
			{
				this.chargeValue = (uint)this.holdoutZoneController.displayChargePercent;
				this.isCharging = this.holdoutZoneController.isAnyoneCharging;
			}
			Color color = this.spriteBaseColor;
			Color color2 = this.textBaseColor;
			if (!this.isCharged)
			{
				if (this.flashWhenNotCharging)
				{
					this.flashStopwatch += Time.deltaTime;
					color = (((int)(this.flashStopwatch * this.flashFrequency) % 2 == 0) ? this.spriteFlashColor : this.spriteBaseColor);
				}
				if (this.isCharging)
				{
					color = this.spriteChargingColor;
					color2 = this.textChargingColor;
				}
				if (this.disableTextWhenNotCharging)
				{
					this.chargingText.enabled = this.isCharging;
				}
				else
				{
					this.chargingText.enabled = true;
				}
			}
			else
			{
				color = this.spriteChargedColor;
				if (this.disableTextWhenCharged)
				{
					this.chargingText.enabled = false;
				}
			}
			if (this.chargingText.enabled)
			{
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				stringBuilder.AppendUint(this.chargeValue, 1U, 3U).Append("%");
				this.chargingText.SetText(stringBuilder);
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			}
			SpriteRenderer[] array = this.iconSprites;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].color = color;
			}
			this.chargingText.color = color2;
		}

		// Token: 0x04004771 RID: 18289
		[Header("Cached Values")]
		public SpriteRenderer[] iconSprites;

		// Token: 0x04004772 RID: 18290
		public TextMeshPro chargingText;

		// Token: 0x04004773 RID: 18291
		public HoldoutZoneController holdoutZoneController;

		// Token: 0x04004774 RID: 18292
		[Header("Color Values")]
		public Color spriteBaseColor;

		// Token: 0x04004775 RID: 18293
		public Color spriteFlashColor;

		// Token: 0x04004776 RID: 18294
		public Color spriteChargingColor;

		// Token: 0x04004777 RID: 18295
		public Color spriteChargedColor;

		// Token: 0x04004778 RID: 18296
		public Color textBaseColor;

		// Token: 0x04004779 RID: 18297
		public Color textChargingColor;

		// Token: 0x0400477A RID: 18298
		[Header("Options")]
		public bool disableTextWhenNotCharging;

		// Token: 0x0400477B RID: 18299
		public bool disableTextWhenCharged;

		// Token: 0x0400477C RID: 18300
		public bool flashWhenNotCharging;

		// Token: 0x0400477D RID: 18301
		public float flashFrequency;

		// Token: 0x0400477E RID: 18302
		[Header("Control Values - Don't assign via inspector!")]
		public bool isCharging;

		// Token: 0x0400477F RID: 18303
		public bool isCharged;

		// Token: 0x04004781 RID: 18305
		private float flashStopwatch;
	}
}
