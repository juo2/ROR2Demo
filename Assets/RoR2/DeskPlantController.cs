using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000696 RID: 1686
	[RequireComponent(typeof(TeamFilter))]
	public class DeskPlantController : MonoBehaviour
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x0008D99D File Offset: 0x0008BB9D
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x0008D9A5 File Offset: 0x0008BBA5
		public TeamFilter teamFilter { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0008D9AE File Offset: 0x0008BBAE
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x0008D9B6 File Offset: 0x0008BBB6
		public int itemCount { get; set; }

		// Token: 0x060020EE RID: 8430 RVA: 0x0008D9C0 File Offset: 0x0008BBC0
		private void Awake()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.plantObject.SetActive(false);
			this.seedObject.SetActive(false);
			RaycastHit raycastHit;
			if (NetworkServer.active && Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 500f, LayerIndex.world.mask))
			{
				base.transform.position = raycastHit.point;
				base.transform.up = raycastHit.normal;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0008DA4C File Offset: 0x0008BC4C
		public void SeedPlanting()
		{
			EffectManager.SimpleEffect(this.groundFX, base.transform.position, Quaternion.identity, false);
		}

		// Token: 0x04002658 RID: 9816
		public GameObject plantObject;

		// Token: 0x04002659 RID: 9817
		public GameObject seedObject;

		// Token: 0x0400265A RID: 9818
		public GameObject groundFX;

		// Token: 0x0400265B RID: 9819
		public float healingRadius;

		// Token: 0x0400265C RID: 9820
		public float radiusIncreasePerStack = 5f;

		// Token: 0x0400265E RID: 9822
		private static readonly float seedDuration = 5f;

		// Token: 0x0400265F RID: 9823
		private static readonly float mainDuration = 10f;

		// Token: 0x04002660 RID: 9824
		private static readonly float scaleDuration = 0.5f;

		// Token: 0x04002662 RID: 9826
		private static readonly AnimationCurve linearRampUp = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x04002663 RID: 9827
		private static readonly AnimationCurve linearRampDown = AnimationCurve.Linear(0f, 1f, 1f, 0f);

		// Token: 0x04002664 RID: 9828
		private static readonly AnimationCurve easeUp = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04002665 RID: 9829
		private static readonly AnimationCurve easeDown = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

		// Token: 0x02000697 RID: 1687
		private abstract class DeskPlantBaseState : BaseState
		{
			// Token: 0x060020F2 RID: 8434
			protected abstract float CalcScale();

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x060020F3 RID: 8435
			protected abstract bool showSeedObject { get; }

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x060020F4 RID: 8436
			protected abstract bool showPlantObject { get; }

			// Token: 0x060020F5 RID: 8437 RVA: 0x0008DB23 File Offset: 0x0008BD23
			public override void OnEnter()
			{
				base.OnEnter();
				this.controller = base.GetComponent<DeskPlantController>();
				this.controller.seedObject.SetActive(this.showSeedObject);
				this.controller.plantObject.SetActive(this.showPlantObject);
			}

			// Token: 0x060020F6 RID: 8438 RVA: 0x0008DB64 File Offset: 0x0008BD64
			public override void Update()
			{
				base.Update();
				if (this.showPlantObject)
				{
					float num = this.CalcScale();
					Vector3 vector = new Vector3(num, num, num);
					Transform transform = this.controller.plantObject.transform;
					if (transform.localScale != vector)
					{
						transform.localScale = vector;
					}
				}
			}

			// Token: 0x04002666 RID: 9830
			protected DeskPlantController controller;
		}

		// Token: 0x02000698 RID: 1688
		private class SeedState : DeskPlantController.DeskPlantBaseState
		{
			// Token: 0x060020F8 RID: 8440 RVA: 0x00041555 File Offset: 0x0003F755
			protected override float CalcScale()
			{
				return 1f;
			}

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool showSeedObject
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x060020FA RID: 8442 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected override bool showPlantObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060020FB RID: 8443 RVA: 0x0008DBB6 File Offset: 0x0008BDB6
			public override void OnEnter()
			{
				base.OnEnter();
				Util.PlaySound("Play_item_proc_interstellarDeskPlant_grow", base.gameObject);
				base.GetComponent<Animation>().Play();
			}

			// Token: 0x060020FC RID: 8444 RVA: 0x0008DBDB File Offset: 0x0008BDDB
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= DeskPlantController.seedDuration)
				{
					this.outer.SetNextState(new DeskPlantController.SproutState());
				}
			}
		}

		// Token: 0x02000699 RID: 1689
		private class SproutState : DeskPlantController.DeskPlantBaseState
		{
			// Token: 0x060020FE RID: 8446 RVA: 0x0008DC10 File Offset: 0x0008BE10
			protected override float CalcScale()
			{
				float time = Mathf.Clamp01(base.age / DeskPlantController.scaleDuration);
				return DeskPlantController.linearRampUp.Evaluate(time) * DeskPlantController.easeUp.Evaluate(time);
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x060020FF RID: 8447 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected override bool showSeedObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06002100 RID: 8448 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool showPlantObject
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002101 RID: 8449 RVA: 0x0008DC46 File Offset: 0x0008BE46
			public override void OnEnter()
			{
				base.OnEnter();
				Util.PlaySound("Play_item_proc_interstellarDeskPlant_bloom", base.gameObject);
			}

			// Token: 0x06002102 RID: 8450 RVA: 0x0000EC55 File Offset: 0x0000CE55
			public override void OnExit()
			{
				base.OnExit();
			}

			// Token: 0x06002103 RID: 8451 RVA: 0x0008DC5F File Offset: 0x0008BE5F
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= DeskPlantController.scaleDuration)
				{
					this.outer.SetNextState(new DeskPlantController.MainState());
				}
			}
		}

		// Token: 0x0200069A RID: 1690
		private class MainState : DeskPlantController.DeskPlantBaseState
		{
			// Token: 0x06002105 RID: 8453 RVA: 0x00041555 File Offset: 0x0003F755
			protected override float CalcScale()
			{
				return 1f;
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06002106 RID: 8454 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected override bool showSeedObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06002107 RID: 8455 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool showPlantObject
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002108 RID: 8456 RVA: 0x0008DC8C File Offset: 0x0008BE8C
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active && !this.deskplantWard)
				{
					this.deskplantWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/DeskplantWard"), this.controller.plantObject.transform.position, Quaternion.identity);
					this.deskplantWard.GetComponent<TeamFilter>().teamIndex = this.controller.teamFilter.teamIndex;
					if (this.deskplantWard)
					{
						HealingWard component = this.deskplantWard.GetComponent<HealingWard>();
						component.healFraction = 0.05f;
						component.healPoints = 0f;
						component.Networkradius = this.controller.healingRadius + this.controller.radiusIncreasePerStack * (float)this.controller.itemCount;
						component.enabled = true;
					}
					NetworkServer.Spawn(this.deskplantWard);
				}
			}

			// Token: 0x06002109 RID: 8457 RVA: 0x0008DD71 File Offset: 0x0008BF71
			public override void OnExit()
			{
				if (this.deskplantWard)
				{
					EntityState.Destroy(this.deskplantWard);
				}
				base.OnExit();
			}

			// Token: 0x0600210A RID: 8458 RVA: 0x0008DD91 File Offset: 0x0008BF91
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= DeskPlantController.mainDuration)
				{
					this.outer.SetNextState(new DeskPlantController.DeathState());
				}
			}

			// Token: 0x04002667 RID: 9831
			private GameObject deskplantWard;
		}

		// Token: 0x0200069B RID: 1691
		private class DeathState : DeskPlantController.DeskPlantBaseState
		{
			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x0600210C RID: 8460 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected override bool showSeedObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x0600210D RID: 8461 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool showPlantObject
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600210E RID: 8462 RVA: 0x0008DDC0 File Offset: 0x0008BFC0
			protected override float CalcScale()
			{
				float time = Mathf.Clamp01(base.age / DeskPlantController.scaleDuration);
				return DeskPlantController.linearRampDown.Evaluate(time) * DeskPlantController.easeDown.Evaluate(time);
			}

			// Token: 0x0600210F RID: 8463 RVA: 0x0008DDF6 File Offset: 0x0008BFF6
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= DeskPlantController.scaleDuration)
				{
					EntityState.Destroy(base.gameObject);
				}
			}
		}
	}
}
