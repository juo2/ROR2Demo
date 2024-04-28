using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005C7 RID: 1479
	[RequireComponent(typeof(CharacterModel))]
	public class AncientWispFireController : MonoBehaviour
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x00072E12 File Offset: 0x00071012
		private void Awake()
		{
			this.characterModel = base.GetComponent<CharacterModel>();
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00072E20 File Offset: 0x00071020
		private void Update()
		{
			bool flag = false;
			CharacterBody body = this.characterModel.body;
			if (body)
			{
				flag = body.HasBuff(JunkContent.Buffs.EnrageAncientWisp);
			}
			if (this.normalParticles)
			{
				ParticleSystem.EmissionModule emission = this.normalParticles.emission;
				if (emission.enabled == flag)
				{
					emission.enabled = !flag;
					if (!flag)
					{
						this.normalParticles.Play();
					}
				}
			}
			if (this.rageParticles)
			{
				ParticleSystem.EmissionModule emission2 = this.rageParticles.emission;
				if (emission2.enabled != flag)
				{
					emission2.enabled = flag;
					if (flag)
					{
						this.rageParticles.Play();
					}
				}
			}
			if (this.normalLight)
			{
				this.normalLight.enabled = !flag;
			}
			if (this.rageLight)
			{
				this.rageLight.enabled = flag;
			}
		}

		// Token: 0x040020E8 RID: 8424
		public ParticleSystem normalParticles;

		// Token: 0x040020E9 RID: 8425
		public Light normalLight;

		// Token: 0x040020EA RID: 8426
		public ParticleSystem rageParticles;

		// Token: 0x040020EB RID: 8427
		public Light rageLight;

		// Token: 0x040020EC RID: 8428
		private CharacterModel characterModel;
	}
}
