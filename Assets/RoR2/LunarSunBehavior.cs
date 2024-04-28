using System;
using System.Collections.Generic;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000780 RID: 1920
	public class LunarSunBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06002832 RID: 10290 RVA: 0x000AE74C File Offset: 0x000AC94C
		// (remove) Token: 0x06002833 RID: 10291 RVA: 0x000AE784 File Offset: 0x000AC984
		public event Action<LunarSunBehavior> onDisabled;

		// Token: 0x06002834 RID: 10292 RVA: 0x000AE7B9 File Offset: 0x000AC9B9
		public static int GetMaxProjectiles(Inventory inventory)
		{
			return 2 + inventory.GetItemCount(DLC1Content.Items.LunarSun);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000AE7C8 File Offset: 0x000AC9C8
		public void InitializeOrbiter(ProjectileOwnerOrbiter orbiter, LunarSunProjectileController controller)
		{
			LunarSunBehavior.<>c__DisplayClass19_0 CS$<>8__locals1 = new LunarSunBehavior.<>c__DisplayClass19_0();
			CS$<>8__locals1.controller = controller;
			float num = this.body.radius + 2f + UnityEngine.Random.Range(0.25f, 0.25f * (float)this.stack);
			float num2 = num / 2f;
			num2 *= num2;
			float degreesPerSecond = 180f * Mathf.Pow(0.9f, num2);
			Quaternion lhs = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up);
			Quaternion rhs = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 0f), Vector3.forward);
			Vector3 planeNormal = lhs * rhs * Vector3.up;
			float initialDegreesFromOwnerForward = UnityEngine.Random.Range(0f, 360f);
			orbiter.Initialize(planeNormal, num, degreesPerSecond, initialDegreesFromOwnerForward);
			this.onDisabled += CS$<>8__locals1.<InitializeOrbiter>g__DestroyOrbiter|0;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000AE8A0 File Offset: 0x000ACAA0
		private void Awake()
		{
			base.enabled = false;
			this.projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LunarSunProjectile");
			ulong seed = Run.instance.seed ^ (ulong)((long)Run.instance.stageClearCount);
			this.transformRng = new Xoroshiro128Plus(seed);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnEnable()
		{
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000AE8E7 File Offset: 0x000ACAE7
		private void OnDisable()
		{
			Action<LunarSunBehavior> action = this.onDisabled;
			if (action != null)
			{
				action(this);
			}
			this.onDisabled = null;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000AE904 File Offset: 0x000ACB04
		private void FixedUpdate()
		{
			this.projectileTimer += Time.fixedDeltaTime;
			if (!this.body.master.IsDeployableLimited(DeployableSlot.LunarSunBomb) && this.projectileTimer > 3f / (float)this.stack)
			{
				this.projectileTimer = 0f;
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					projectilePrefab = this.projectilePrefab,
					crit = this.body.RollCrit(),
					damage = this.body.damage * 3.6f,
					damageColorIndex = DamageColorIndex.Item,
					force = 0f,
					owner = base.gameObject,
					position = this.body.transform.position,
					rotation = Quaternion.identity
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			this.transformTimer += Time.fixedDeltaTime;
			if (this.transformTimer > 60f)
			{
				this.transformTimer = 0f;
				if (this.body.master && this.body.inventory)
				{
					List<ItemIndex> list = new List<ItemIndex>(this.body.inventory.itemAcquisitionOrder);
					ItemIndex itemIndex = ItemIndex.None;
					Util.ShuffleList<ItemIndex>(list, this.transformRng);
					foreach (ItemIndex itemIndex2 in list)
					{
						if (itemIndex2 != DLC1Content.Items.LunarSun.itemIndex)
						{
							ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex2);
							if (itemDef && itemDef.tier != ItemTier.NoTier)
							{
								itemIndex = itemIndex2;
								break;
							}
						}
					}
					if (itemIndex != ItemIndex.None)
					{
						this.body.inventory.RemoveItem(itemIndex, 1);
						this.body.inventory.GiveItem(DLC1Content.Items.LunarSun, 1);
						CharacterMasterNotificationQueue.SendTransformNotification(this.body.master, itemIndex, DLC1Content.Items.LunarSun.itemIndex, CharacterMasterNotificationQueue.TransformationType.LunarSun);
					}
				}
			}
		}

		// Token: 0x04002BD6 RID: 11222
		private const float secondsPerTransform = 60f;

		// Token: 0x04002BD7 RID: 11223
		private const float secondsPerProjectile = 3f;

		// Token: 0x04002BD8 RID: 11224
		private const string projectilePath = "Prefabs/Projectiles/LunarSunProjectile";

		// Token: 0x04002BD9 RID: 11225
		private const int baseMaxProjectiles = 2;

		// Token: 0x04002BDA RID: 11226
		private const int maxProjectilesPerStack = 1;

		// Token: 0x04002BDB RID: 11227
		private const float baseOrbitDegreesPerSecond = 180f;

		// Token: 0x04002BDC RID: 11228
		private const float orbitDegreesPerSecondFalloff = 0.9f;

		// Token: 0x04002BDD RID: 11229
		private const float baseOrbitRadius = 2f;

		// Token: 0x04002BDE RID: 11230
		private const float orbitRadiusPerStack = 0.25f;

		// Token: 0x04002BDF RID: 11231
		private const float maxInclinationDegrees = 0f;

		// Token: 0x04002BE0 RID: 11232
		private const float baseDamageCoefficient = 3.6f;

		// Token: 0x04002BE2 RID: 11234
		private float projectileTimer;

		// Token: 0x04002BE3 RID: 11235
		private float transformTimer;

		// Token: 0x04002BE4 RID: 11236
		private GameObject projectilePrefab;

		// Token: 0x04002BE5 RID: 11237
		private Xoroshiro128Plus transformRng;
	}
}
