using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000775 RID: 1909
	public class InteractionProcFilter : MonoBehaviour
	{
		// Token: 0x04002B9D RID: 11165
		[Tooltip("Whether or not OnInteractionBegin for this object should trigger extra gameplay effects like Fireworks and Squid Polyp.")]
		public bool shouldAllowOnInteractionBeginProc = true;
	}
}
