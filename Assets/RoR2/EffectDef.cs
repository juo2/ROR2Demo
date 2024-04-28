using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000597 RID: 1431
	public class EffectDef
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x0006F8B3 File Offset: 0x0006DAB3
		public EffectDef()
		{
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006F8C2 File Offset: 0x0006DAC2
		public EffectDef(GameObject effectPrefab)
		{
			this.prefab = effectPrefab;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0006F8D8 File Offset: 0x0006DAD8
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0006F8E0 File Offset: 0x0006DAE0
		public EffectIndex index { get; internal set; } = EffectIndex.Invalid;

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0006F8E9 File Offset: 0x0006DAE9
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0006F8F4 File Offset: 0x0006DAF4
		public GameObject prefab
		{
			get
			{
				return this._prefab;
			}
			set
			{
				if (!value)
				{
					throw new ArgumentNullException("Prefab is invalid.");
				}
				if (this._prefab == value)
				{
					return;
				}
				EffectComponent component = value.GetComponent<EffectComponent>();
				if (!component)
				{
					throw new ArgumentException(string.Format("Prefab \"{0}\" does not have EffectComponent attached.", value));
				}
				this._prefab = value;
				this.prefabEffectComponent = component;
				this.prefabVfxAttributes = this._prefab.GetComponent<VFXAttributes>();
				this.prefabName = this._prefab.name;
				this.spawnSoundEventName = this.prefabEffectComponent.soundName;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0006F97F File Offset: 0x0006DB7F
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0006F987 File Offset: 0x0006DB87
		public EffectComponent prefabEffectComponent { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0006F990 File Offset: 0x0006DB90
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0006F998 File Offset: 0x0006DB98
		public VFXAttributes prefabVfxAttributes { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006F9A1 File Offset: 0x0006DBA1
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0006F9A9 File Offset: 0x0006DBA9
		public string prefabName { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0006F9B2 File Offset: 0x0006DBB2
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0006F9BA File Offset: 0x0006DBBA
		public string spawnSoundEventName { get; private set; }

		// Token: 0x04002016 RID: 8214
		private GameObject _prefab;

		// Token: 0x0400201B RID: 8219
		public Func<EffectData, bool> cullMethod;
	}
}
