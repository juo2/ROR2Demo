using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007FB RID: 2043
	public class ParticleSystemRandomColor : MonoBehaviour
	{
		// Token: 0x06002C0A RID: 11274 RVA: 0x000BC6A4 File Offset: 0x000BA8A4
		private void Awake()
		{
			if (this.colors.Length != 0)
			{
				Color color = this.colors[UnityEngine.Random.Range(0, this.colors.Length)];
				for (int i = 0; i < this.particleSystems.Length; i++)
				{
					this.particleSystems[i].main.startColor = color;
				}
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000BC704 File Offset: 0x000BA904
		[AssetCheck(typeof(ParticleSystemRandomColor))]
		private static void CheckParticleSystemRandomColor(AssetCheckArgs args)
		{
			ParticleSystemRandomColor particleSystemRandomColor = (ParticleSystemRandomColor)args.asset;
			for (int i = 0; i < particleSystemRandomColor.particleSystems.Length; i++)
			{
				if (!particleSystemRandomColor.particleSystems[i])
				{
					args.LogErrorFormat(args.asset, "Null particle system in slot {0}", new object[]
					{
						i
					});
				}
			}
		}

		// Token: 0x04002E82 RID: 11906
		public Color[] colors;

		// Token: 0x04002E83 RID: 11907
		public ParticleSystem[] particleSystems;
	}
}
