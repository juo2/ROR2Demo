using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008CC RID: 2252
	public class TreebotSunBuffGranter : MonoBehaviour
	{
		// Token: 0x06003276 RID: 12918 RVA: 0x000D5250 File Offset: 0x000D3450
		private void Start()
		{
			this.FindSun();
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000D5264 File Offset: 0x000D3464
		private void FixedUpdate()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer = 1f / this.raycastFrequency;
				this.CheckSunlight();
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000D529D File Offset: 0x000D349D
		private void FindSun()
		{
			this.sun = RenderSettings.sun;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000D52AC File Offset: 0x000D34AC
		private void CheckSunlight()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (!this.sun)
			{
				this.FindSun();
				if (!this.sun)
				{
					return;
				}
			}
			bool flag = !Physics.Raycast(base.transform.position, -this.sun.transform.forward, 1000f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
			if (this.buffDef)
			{
				if (this.hadSunlight && !flag)
				{
					this.characterBody.RemoveBuff(this.buffDef.buffIndex);
				}
				else if (flag && !this.hadSunlight)
				{
					this.characterBody.AddBuff(this.buffDef.buffIndex);
				}
			}
			this.hadSunlight = flag;
		}

		// Token: 0x0400339F RID: 13215
		public Transform referenceTransform;

		// Token: 0x040033A0 RID: 13216
		public float raycastFrequency;

		// Token: 0x040033A1 RID: 13217
		public BuffDef buffDef;

		// Token: 0x040033A2 RID: 13218
		private const float raycastLength = 1000f;

		// Token: 0x040033A3 RID: 13219
		private float timer;

		// Token: 0x040033A4 RID: 13220
		private bool hadSunlight;

		// Token: 0x040033A5 RID: 13221
		private Light sun;

		// Token: 0x040033A6 RID: 13222
		private CharacterBody characterBody;
	}
}
