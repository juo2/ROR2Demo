using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200068F RID: 1679
	public class DebuffZone : MonoBehaviour
	{
		// Token: 0x060020D2 RID: 8402 RVA: 0x000026ED File Offset: 0x000008ED
		private void Awake()
		{
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0008D52C File Offset: 0x0008B72C
		private void OnTriggerEnter(Collider other)
		{
			if (NetworkServer.active)
			{
				if (!this.buffType)
				{
					return;
				}
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component)
				{
					component.AddTimedBuff(this.buffType.buffIndex, this.buffDuration);
					Util.PlaySound(this.buffApplicationSoundString, component.gameObject);
					if (this.buffApplicationEffectPrefab)
					{
						EffectManager.SpawnEffect(this.buffApplicationEffectPrefab, new EffectData
						{
							origin = component.mainHurtBox.transform.position,
							scale = component.radius
						}, true);
					}
				}
			}
		}

		// Token: 0x0400261D RID: 9757
		[Tooltip("The buff type to grant")]
		public BuffDef buffType;

		// Token: 0x0400261E RID: 9758
		[Tooltip("The buff duration")]
		public float buffDuration;

		// Token: 0x0400261F RID: 9759
		public string buffApplicationSoundString;

		// Token: 0x04002620 RID: 9760
		public GameObject buffApplicationEffectPrefab;
	}
}
