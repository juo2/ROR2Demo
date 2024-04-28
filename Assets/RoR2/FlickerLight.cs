using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006DB RID: 1755
	public class FlickerLight : MonoBehaviour
	{
		// Token: 0x0600229F RID: 8863 RVA: 0x000958B4 File Offset: 0x00093AB4
		private void Start()
		{
			this.initialLightIntensity = this.light.intensity;
			this.randomPhase = UnityEngine.Random.Range(0f, 6.2831855f);
			for (int i = 0; i < this.sinWaves.Length; i++)
			{
				Wave[] array = this.sinWaves;
				int num = i;
				array[num].cycleOffset = array[num].cycleOffset + this.randomPhase;
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00095918 File Offset: 0x00093B18
		private void Update()
		{
			this.stopwatch += Time.deltaTime;
			float num = this.initialLightIntensity;
			for (int i = 0; i < this.sinWaves.Length; i++)
			{
				num *= 1f + this.sinWaves[i].Evaluate(this.stopwatch);
			}
			this.light.intensity = num;
		}

		// Token: 0x040027BD RID: 10173
		public Light light;

		// Token: 0x040027BE RID: 10174
		public Wave[] sinWaves;

		// Token: 0x040027BF RID: 10175
		private float initialLightIntensity;

		// Token: 0x040027C0 RID: 10176
		private float stopwatch;

		// Token: 0x040027C1 RID: 10177
		private float randomPhase;
	}
}
