using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.EngiBubbleShield
{
	// Token: 0x020003BC RID: 956
	public class Deployed : EntityState
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0004AFDE File Offset: 0x000491DE
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(Deployed.initialSoundString, base.gameObject);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004AFF8 File Offset: 0x000491F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.hasDeployed && base.fixedAge >= Deployed.delayToDeploy)
			{
				ChildLocator component = base.GetComponent<ChildLocator>();
				if (component)
				{
					component.FindChild(Deployed.childLocatorString).gameObject.SetActive(true);
					this.hasDeployed = true;
				}
			}
			if (base.fixedAge >= Deployed.lifetime && NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004B06C File Offset: 0x0004926C
		public override void OnExit()
		{
			base.OnExit();
			EffectManager.SpawnEffect(this.destroyEffectPrefab, new EffectData
			{
				origin = base.transform.position,
				rotation = base.transform.rotation,
				scale = this.destroyEffectRadius
			}, false);
			Util.PlaySound(Deployed.destroySoundString, base.gameObject);
		}

		// Token: 0x04001586 RID: 5510
		public static string childLocatorString;

		// Token: 0x04001587 RID: 5511
		public static string initialSoundString;

		// Token: 0x04001588 RID: 5512
		public static string destroySoundString;

		// Token: 0x04001589 RID: 5513
		public static float delayToDeploy;

		// Token: 0x0400158A RID: 5514
		public static float lifetime;

		// Token: 0x0400158B RID: 5515
		[SerializeField]
		public GameObject destroyEffectPrefab;

		// Token: 0x0400158C RID: 5516
		[SerializeField]
		public float destroyEffectRadius;

		// Token: 0x0400158D RID: 5517
		private bool hasDeployed;
	}
}
