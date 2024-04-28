using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200077A RID: 1914
	public class AffixEarthBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002811 RID: 10257 RVA: 0x000ADD8C File Offset: 0x000ABF8C
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			AffixEarthBehavior.attachmentPrefab = Addressables.LoadAssetAsync<GameObject>("112bf5913df2135478a9785e0bc18477").WaitForCompletion();
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000ADDB0 File Offset: 0x000ABFB0
		private void FixedUpdate()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			bool flag = this.stack > 0;
			if (this.affixEarthAttachment != flag)
			{
				if (flag)
				{
					this.affixEarthAttachment = UnityEngine.Object.Instantiate<GameObject>(AffixEarthBehavior.attachmentPrefab);
					this.affixEarthAttachment.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(this.body.gameObject, null);
					return;
				}
				UnityEngine.Object.Destroy(this.affixEarthAttachment);
				this.affixEarthAttachment = null;
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000ADE1F File Offset: 0x000AC01F
		private void OnDisable()
		{
			if (this.affixEarthAttachment)
			{
				UnityEngine.Object.Destroy(this.affixEarthAttachment);
			}
		}

		// Token: 0x04002BB9 RID: 11193
		private const float baseOrbitDegreesPerSecond = 90f;

		// Token: 0x04002BBA RID: 11194
		private const float baseOrbitRadius = 3f;

		// Token: 0x04002BBB RID: 11195
		private const string projectilePath = "Prefabs/Projectiles/AffixEarthProjectile";

		// Token: 0x04002BBC RID: 11196
		private GameObject affixEarthAttachment;

		// Token: 0x04002BBD RID: 11197
		private GameObject projectilePrefab;

		// Token: 0x04002BBE RID: 11198
		private int maxProjectiles;

		// Token: 0x04002BBF RID: 11199
		private static GameObject attachmentPrefab;
	}
}
