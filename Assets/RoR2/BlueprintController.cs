using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005F5 RID: 1525
	public class BlueprintController : MonoBehaviour
	{
		// Token: 0x06001BDE RID: 7134 RVA: 0x00076A89 File Offset: 0x00074C89
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00076A98 File Offset: 0x00074C98
		private void Update()
		{
			Material sharedMaterial = this.ok ? this.okMaterial : this.invalidMaterial;
			for (int i = 0; i < this.renderers.Length; i++)
			{
				this.renderers[i].sharedMaterial = sharedMaterial;
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00076ADD File Offset: 0x00074CDD
		public void PushState(Vector3 position, Quaternion rotation, bool ok)
		{
			this.transform.position = position;
			this.transform.rotation = rotation;
			this.ok = ok;
		}

		// Token: 0x040021A8 RID: 8616
		[NonSerialized]
		public bool ok;

		// Token: 0x040021A9 RID: 8617
		public Material okMaterial;

		// Token: 0x040021AA RID: 8618
		public Material invalidMaterial;

		// Token: 0x040021AB RID: 8619
		public Renderer[] renderers;

		// Token: 0x040021AC RID: 8620
		private new Transform transform;
	}
}
