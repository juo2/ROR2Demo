using System;
using Generics.Dynamics;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200075A RID: 1882
	public class IKSetWeightFromAnimatorFloat : MonoBehaviour
	{
		// Token: 0x060026F4 RID: 9972 RVA: 0x000A8F7C File Offset: 0x000A717C
		private void Update()
		{
			float @float = this.animator.GetFloat(this.animatorFloat);
			Core.Chain[] otherChains = this.ik.otherChains;
			for (int i = 0; i < otherChains.Length; i++)
			{
				otherChains[i].weight = @float;
			}
		}

		// Token: 0x04002AC8 RID: 10952
		public Animator animator;

		// Token: 0x04002AC9 RID: 10953
		public string animatorFloat;

		// Token: 0x04002ACA RID: 10954
		public InverseKinematics ik;
	}
}
