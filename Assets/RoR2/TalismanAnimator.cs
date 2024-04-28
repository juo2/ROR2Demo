using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008B1 RID: 2225
	[RequireComponent(typeof(ItemFollower))]
	public class TalismanAnimator : MonoBehaviour
	{
		// Token: 0x06003168 RID: 12648 RVA: 0x000D1A90 File Offset: 0x000CFC90
		private void Start()
		{
			this.itemFollower = base.GetComponent<ItemFollower>();
			CharacterModel componentInParent = base.GetComponentInParent<CharacterModel>();
			if (componentInParent)
			{
				CharacterBody body = componentInParent.body;
				if (body)
				{
					this.equipmentSlot = body.equipmentSlot;
				}
			}
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000D1AD4 File Offset: 0x000CFCD4
		private void FixedUpdate()
		{
			if (this.equipmentSlot)
			{
				float cooldownTimer = this.equipmentSlot.cooldownTimer;
				if (this.lastCooldownTimer - cooldownTimer >= 0.5f && this.itemFollower.followerInstance)
				{
					if (this.killEffects == null || this.killEffects.Length == 0 || this.killEffects[0] == null)
					{
						this.killEffects = this.itemFollower.followerInstance.GetComponentsInChildren<ParticleSystem>();
					}
					ParticleSystem[] array = this.killEffects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Play();
					}
				}
				this.lastCooldownTimer = cooldownTimer;
			}
		}

		// Token: 0x040032EE RID: 13038
		private float lastCooldownTimer;

		// Token: 0x040032EF RID: 13039
		private EquipmentSlot equipmentSlot;

		// Token: 0x040032F0 RID: 13040
		private ItemFollower itemFollower;

		// Token: 0x040032F1 RID: 13041
		private ParticleSystem[] killEffects;
	}
}
