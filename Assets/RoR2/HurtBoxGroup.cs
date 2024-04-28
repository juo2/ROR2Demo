using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200074C RID: 1868
	public class HurtBoxGroup : MonoBehaviour, ILifeBehavior
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x000A8B94 File Offset: 0x000A6D94
		public void OnDeathStart()
		{
			int hurtBoxesDeactivatorCounter = this.hurtBoxesDeactivatorCounter + 1;
			this.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000A8BB1 File Offset: 0x000A6DB1
		// (set) Token: 0x060026CA RID: 9930 RVA: 0x000A8BBC File Offset: 0x000A6DBC
		public int hurtBoxesDeactivatorCounter
		{
			get
			{
				return this._hurtBoxesDeactivatorCounter;
			}
			set
			{
				bool flag = this._hurtBoxesDeactivatorCounter <= 0;
				bool flag2 = value <= 0;
				this._hurtBoxesDeactivatorCounter = value;
				if (flag != flag2)
				{
					this.SetHurtboxesActive(flag2);
				}
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000A8BF0 File Offset: 0x000A6DF0
		private void SetHurtboxesActive(bool active)
		{
			for (int i = 0; i < this.hurtBoxes.Length; i++)
			{
				this.hurtBoxes[i].gameObject.SetActive(active);
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000A8C24 File Offset: 0x000A6E24
		public void OnValidate()
		{
			int num = 0;
			if (this.hurtBoxes == null)
			{
				this.hurtBoxes = Array.Empty<HurtBox>();
			}
			short num2 = 0;
			while ((int)num2 < this.hurtBoxes.Length)
			{
				if (!this.hurtBoxes[(int)num2])
				{
					Debug.LogWarningFormat("Object {0} HurtBoxGroup hurtbox #{1} is missing.", new object[]
					{
						Util.GetGameObjectHierarchyName(base.gameObject),
						num2
					});
				}
				else
				{
					this.hurtBoxes[(int)num2].hurtBoxGroup = this;
					this.hurtBoxes[(int)num2].indexInGroup = num2;
					if (this.hurtBoxes[(int)num2].isBullseye)
					{
						num++;
					}
				}
				num2 += 1;
			}
			if (this.bullseyeCount != num)
			{
				this.bullseyeCount = num;
			}
			if (!this.mainHurtBox)
			{
				IEnumerable<HurtBox> source = from v in this.hurtBoxes
				where v
				where v.isBullseye
				select v;
				IEnumerable<HurtBox> source2 = from v in source
				where v.transform.parent.name.ToLower(CultureInfo.InvariantCulture) == "chest"
				select v;
				this.mainHurtBox = (source2.FirstOrDefault<HurtBox>() ?? source.FirstOrDefault<HurtBox>());
			}
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000A8D6C File Offset: 0x000A6F6C
		public HurtBoxGroup.VolumeDistribution GetVolumeDistribution()
		{
			return new HurtBoxGroup.VolumeDistribution(this.hurtBoxes);
		}

		// Token: 0x04002AB6 RID: 10934
		[Tooltip("The hurtboxes in this group. This really shouldn't be set manually.")]
		public HurtBox[] hurtBoxes;

		// Token: 0x04002AB7 RID: 10935
		[Tooltip("The most important hurtbox in this group, usually a good center-of-mass target like the chest.")]
		public HurtBox mainHurtBox;

		// Token: 0x04002AB8 RID: 10936
		[HideInInspector]
		public int bullseyeCount;

		// Token: 0x04002AB9 RID: 10937
		private int _hurtBoxesDeactivatorCounter;

		// Token: 0x0200074D RID: 1869
		public class VolumeDistribution
		{
			// Token: 0x1700035E RID: 862
			// (get) Token: 0x060026CF RID: 9935 RVA: 0x000A8D79 File Offset: 0x000A6F79
			// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000A8D81 File Offset: 0x000A6F81
			public float totalVolume { get; private set; }

			// Token: 0x060026D1 RID: 9937 RVA: 0x000A8D8C File Offset: 0x000A6F8C
			public VolumeDistribution(HurtBox[] hurtBoxes)
			{
				this.totalVolume = 0f;
				for (int i = 0; i < hurtBoxes.Length; i++)
				{
					this.totalVolume += hurtBoxes[i].volume;
				}
				this.hurtBoxes = (HurtBox[])hurtBoxes.Clone();
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x060026D2 RID: 9938 RVA: 0x000A8DE0 File Offset: 0x000A6FE0
			public Vector3 randomVolumePoint
			{
				get
				{
					float num = UnityEngine.Random.Range(0f, this.totalVolume);
					HurtBox hurtBox = this.hurtBoxes[0];
					float num2 = 0f;
					for (int i = 0; i < this.hurtBoxes.Length; i++)
					{
						num2 += this.hurtBoxes[i].volume;
						if (num2 <= num)
						{
							hurtBox = this.hurtBoxes[i];
							break;
						}
					}
					return hurtBox.randomVolumePoint;
				}
			}

			// Token: 0x04002ABB RID: 10939
			private HurtBox[] hurtBoxes;
		}
	}
}
