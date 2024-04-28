using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200068A RID: 1674
	[ExecuteAlways]
	public class DamageNumberManager : MonoBehaviour
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0008C983 File Offset: 0x0008AB83
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x0008C98A File Offset: 0x0008AB8A
		public static DamageNumberManager instance { get; private set; }

		// Token: 0x060020B5 RID: 8373 RVA: 0x0008C992 File Offset: 0x0008AB92
		private void OnEnable()
		{
			DamageNumberManager.instance = SingletonHelper.Assign<DamageNumberManager>(DamageNumberManager.instance, this);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0008C9A4 File Offset: 0x0008ABA4
		private void OnDisable()
		{
			DamageNumberManager.instance = SingletonHelper.Unassign<DamageNumberManager>(DamageNumberManager.instance, this);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0008C9B6 File Offset: 0x0008ABB6
		private void Awake()
		{
			this.ps = base.GetComponent<ParticleSystem>();
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0008C9C4 File Offset: 0x0008ABC4
		public void SpawnDamageNumber(float amount, Vector3 position, bool crit, TeamIndex teamIndex, DamageColorIndex damageColorIndex)
		{
			Color a = DamageColor.FindColor(damageColorIndex);
			Color b = Color.white;
			if (teamIndex != TeamIndex.None)
			{
				if (teamIndex == TeamIndex.Monster)
				{
					b = new Color(0.5568628f, 0.29411766f, 0.6039216f);
				}
			}
			else
			{
				b = Color.gray;
			}
			this.ps.Emit(new ParticleSystem.EmitParams
			{
				position = position,
				startColor = a * b,
				applyShapeToPosition = true
			}, 1);
			this.ps.GetCustomParticleData(this.customData, ParticleSystemCustomData.Custom1);
			this.customData[this.customData.Count - 1] = new Vector4(1f, 0f, amount, crit ? 1f : 0f);
			this.ps.SetCustomParticleData(this.customData, ParticleSystemCustomData.Custom1);
		}

		// Token: 0x040025FC RID: 9724
		private List<Vector4> customData = new List<Vector4>();

		// Token: 0x040025FD RID: 9725
		private ParticleSystem ps;
	}
}
