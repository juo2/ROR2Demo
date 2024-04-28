using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B20 RID: 2848
	public class OrbManager : MonoBehaviour
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x0010CF03 File Offset: 0x0010B103
		// (set) Token: 0x060040F5 RID: 16629 RVA: 0x0010CF0A File Offset: 0x0010B10A
		public static OrbManager instance { get; private set; }

		// Token: 0x060040F6 RID: 16630 RVA: 0x0010CF12 File Offset: 0x0010B112
		private void OnEnable()
		{
			if (!OrbManager.instance)
			{
				OrbManager.instance = this;
				return;
			}
			Debug.LogErrorFormat(this, "Duplicate instance of singleton class {0}. Only one should exist at a time.", new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x0010CF46 File Offset: 0x0010B146
		private void OnDisable()
		{
			if (OrbManager.instance == this)
			{
				OrbManager.instance = null;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x0010CF5B File Offset: 0x0010B15B
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x0010CF63 File Offset: 0x0010B163
		public float time { get; private set; }

		// Token: 0x060040FA RID: 16634 RVA: 0x0010CF6C File Offset: 0x0010B16C
		private void FixedUpdate()
		{
			this.time += Time.fixedDeltaTime;
			for (int i = 0; i < this.orbsWithFixedUpdateBehavior.Count; i++)
			{
				this.orbsWithFixedUpdateBehavior[i].FixedUpdate();
			}
			if (this.nextOrbArrival <= this.time)
			{
				this.nextOrbArrival = float.PositiveInfinity;
				for (int j = this.travelingOrbs.Count - 1; j >= 0; j--)
				{
					Orb orb = this.travelingOrbs[j];
					if (orb.arrivalTime <= this.time)
					{
						this.travelingOrbs.RemoveAt(j);
						IOrbFixedUpdateBehavior orbFixedUpdateBehavior = orb as IOrbFixedUpdateBehavior;
						if (orbFixedUpdateBehavior != null)
						{
							this.orbsWithFixedUpdateBehavior.Remove(orbFixedUpdateBehavior);
						}
						orb.OnArrival();
					}
					else if (this.nextOrbArrival > orb.arrivalTime)
					{
						this.nextOrbArrival = orb.arrivalTime;
					}
				}
			}
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0010D046 File Offset: 0x0010B246
		public void ForceImmediateArrival(Orb orb)
		{
			orb.OnArrival();
			this.travelingOrbs.Remove(orb);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0010D05C File Offset: 0x0010B25C
		public void AddOrb(Orb orb)
		{
			orb.Begin();
			orb.arrivalTime = this.time + orb.duration;
			this.travelingOrbs.Add(orb);
			IOrbFixedUpdateBehavior orbFixedUpdateBehavior = orb as IOrbFixedUpdateBehavior;
			if (orbFixedUpdateBehavior != null)
			{
				this.orbsWithFixedUpdateBehavior.Add(orbFixedUpdateBehavior);
			}
			if (this.nextOrbArrival > orb.arrivalTime)
			{
				this.nextOrbArrival = orb.arrivalTime;
			}
		}

		// Token: 0x04003F72 RID: 16242
		private List<Orb> travelingOrbs = new List<Orb>();

		// Token: 0x04003F73 RID: 16243
		private float nextOrbArrival = float.PositiveInfinity;

		// Token: 0x04003F74 RID: 16244
		private List<IOrbFixedUpdateBehavior> orbsWithFixedUpdateBehavior = new List<IOrbFixedUpdateBehavior>();
	}
}
