using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GreaterWispMonster
{
	// Token: 0x02000346 RID: 838
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x00040B78 File Offset: 0x0003ED78
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.modelLocator)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("Mask");
					transform.gameObject.SetActive(true);
					transform.GetComponent<AnimateShaderAlpha>().timeMax = DeathState.duration;
					if (this.initialEffect)
					{
						this.initialEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.initialEffect, transform.position, transform.rotation, transform);
					}
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00040C04 File Offset: 0x0003EE04
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= DeathState.duration && NetworkServer.active)
			{
				if (this.deathEffect)
				{
					EffectManager.SpawnEffect(this.deathEffect, new EffectData
					{
						origin = base.transform.position
					}, true);
				}
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00040C65 File Offset: 0x0003EE65
		public override void OnExit()
		{
			base.OnExit();
			if (this.initialEffectInstance)
			{
				EntityState.Destroy(this.initialEffectInstance);
			}
		}

		// Token: 0x040012BF RID: 4799
		[SerializeField]
		public GameObject initialEffect;

		// Token: 0x040012C0 RID: 4800
		[SerializeField]
		public GameObject deathEffect;

		// Token: 0x040012C1 RID: 4801
		private static float duration = 2f;

		// Token: 0x040012C2 RID: 4802
		private GameObject initialEffectInstance;
	}
}
