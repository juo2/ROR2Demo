using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007F7 RID: 2039
	public class PaintDetailsBelow : MonoBehaviour
	{
		// Token: 0x06002BFA RID: 11258 RVA: 0x000BC193 File Offset: 0x000BA393
		public void OnEnable()
		{
			PaintDetailsBelow.painterList.Add(this);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000BC1A0 File Offset: 0x000BA3A0
		public void OnDisable()
		{
			PaintDetailsBelow.painterList.Remove(this);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000BC1AE File Offset: 0x000BA3AE
		public static List<PaintDetailsBelow> GetPainters()
		{
			return PaintDetailsBelow.painterList;
		}

		// Token: 0x04002E67 RID: 11879
		[Tooltip("Influence radius in world coordinates")]
		public float influenceOuter = 2f;

		// Token: 0x04002E68 RID: 11880
		public float influenceInner = 1f;

		// Token: 0x04002E69 RID: 11881
		[Tooltip("Which detail layer")]
		public int layer;

		// Token: 0x04002E6A RID: 11882
		[Tooltip("Density, from 0-1")]
		public float density = 0.5f;

		// Token: 0x04002E6B RID: 11883
		public float densityPower = 1f;

		// Token: 0x04002E6C RID: 11884
		private static List<PaintDetailsBelow> painterList = new List<PaintDetailsBelow>();
	}
}
