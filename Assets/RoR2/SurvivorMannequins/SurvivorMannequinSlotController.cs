using System;
using HG;
using UnityEngine;

namespace RoR2.SurvivorMannequins
{
	// Token: 0x02000B71 RID: 2929
	public class SurvivorMannequinSlotController : MonoBehaviour
	{
		// Token: 0x060042B6 RID: 17078 RVA: 0x001145A8 File Offset: 0x001127A8
		private void Awake()
		{
			this.currentLoadout = new Loadout();
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x001145B5 File Offset: 0x001127B5
		private void OnEnable()
		{
			NetworkUser.onLoadoutChangedGlobal += this.OnLoadoutChangedGlobal;
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x001145C8 File Offset: 0x001127C8
		private void OnDisable()
		{
			NetworkUser.onLoadoutChangedGlobal -= this.OnLoadoutChangedGlobal;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x001145DC File Offset: 0x001127DC
		private void Update()
		{
			SurvivorDef currentSurvivorDef = null;
			if (this.networkUser)
			{
				currentSurvivorDef = this.networkUser.GetSurvivorPreference();
			}
			this.currentSurvivorDef = currentSurvivorDef;
			if (this.mannequinInstanceDirty)
			{
				this.mannequinInstanceDirty = false;
				this.RebuildMannequinInstance();
			}
			if (this.loadoutDirty)
			{
				this.loadoutDirty = false;
				if (this.networkUser)
				{
					this.networkUser.networkLoadout.CopyLoadout(this.currentLoadout);
				}
				this.ApplyLoadoutToMannequinInstance();
			}
			if (this.toggleableEffect)
			{
				this.toggleableEffect.SetActive(this.networkUser);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x0011467B File Offset: 0x0011287B
		// (set) Token: 0x060042BB RID: 17083 RVA: 0x00114683 File Offset: 0x00112883
		public NetworkUser networkUser
		{
			get
			{
				return this._networkUser;
			}
			set
			{
				if (this._networkUser == value)
				{
					return;
				}
				this._networkUser = value;
				this.mannequinInstanceDirty = true;
				this.loadoutDirty = true;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x001146A4 File Offset: 0x001128A4
		// (set) Token: 0x060042BD RID: 17085 RVA: 0x001146AC File Offset: 0x001128AC
		private SurvivorDef currentSurvivorDef
		{
			get
			{
				return this._currentSurvivorDef;
			}
			set
			{
				if (this._currentSurvivorDef == value)
				{
					return;
				}
				this._currentSurvivorDef = value;
				this.mannequinInstanceDirty = true;
			}
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x001146C6 File Offset: 0x001128C6
		private void OnLoadoutChangedGlobal(NetworkUser networkUser)
		{
			if (this.networkUser != networkUser)
			{
				return;
			}
			this.loadoutDirty = true;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x001146D9 File Offset: 0x001128D9
		private void ClearMannequinInstance()
		{
			if (this.mannequinInstanceTransform)
			{
				UnityEngine.Object.Destroy(this.mannequinInstanceTransform.gameObject);
			}
			this.mannequinInstanceTransform = null;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x00114700 File Offset: 0x00112900
		private void RebuildMannequinInstance()
		{
			this.ClearMannequinInstance();
			if (this.currentSurvivorDef && this.currentSurvivorDef.displayPrefab)
			{
				this.mannequinInstanceTransform = UnityEngine.Object.Instantiate<GameObject>(this.currentSurvivorDef.displayPrefab, base.transform.position, base.transform.rotation, base.transform).transform;
				CharacterSelectSurvivorPreviewDisplayController component = this.mannequinInstanceTransform.GetComponent<CharacterSelectSurvivorPreviewDisplayController>();
				if (component)
				{
					component.networkUser = this.networkUser;
				}
				this.ApplyLoadoutToMannequinInstance();
			}
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x00114790 File Offset: 0x00112990
		private void ApplyLoadoutToMannequinInstance()
		{
			if (!this.mannequinInstanceTransform)
			{
				return;
			}
			BodyIndex bodyIndexFromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(this.currentSurvivorDef.survivorIndex);
			int skinIndex = (int)this.currentLoadout.bodyLoadoutManager.GetSkinIndex(bodyIndexFromSurvivorIndex);
			SkinDef safe = ArrayUtils.GetSafe<SkinDef>(BodyCatalog.GetBodySkins(bodyIndexFromSurvivorIndex), skinIndex);
			if (!safe)
			{
				return;
			}
			CharacterModel componentInChildren = this.mannequinInstanceTransform.GetComponentInChildren<CharacterModel>();
			if (componentInChildren)
			{
				safe.Apply(componentInChildren.gameObject);
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x00114804 File Offset: 0x00112A04
		public static void Swap(SurvivorMannequinSlotController a, SurvivorMannequinSlotController b)
		{
			if (a.mannequinInstanceTransform)
			{
				a.mannequinInstanceTransform.SetParent(b.transform, false);
			}
			if (b.mannequinInstanceTransform)
			{
				b.mannequinInstanceTransform.SetParent(a.transform, false);
			}
			Util.Swap<NetworkUser>(ref a._networkUser, ref b._networkUser);
			Util.Swap<SurvivorDef>(ref a._currentSurvivorDef, ref b._currentSurvivorDef);
			Util.Swap<Loadout>(ref a.currentLoadout, ref b.currentLoadout);
			Util.Swap<bool>(ref a.loadoutDirty, ref b.loadoutDirty);
			Util.Swap<bool>(ref a.mannequinInstanceDirty, ref b.mannequinInstanceDirty);
			Util.Swap<Transform>(ref a.mannequinInstanceTransform, ref b.mannequinInstanceTransform);
		}

		// Token: 0x040040A8 RID: 16552
		public GameObject toggleableEffect;

		// Token: 0x040040A9 RID: 16553
		private NetworkUser _networkUser;

		// Token: 0x040040AA RID: 16554
		private SurvivorDef _currentSurvivorDef;

		// Token: 0x040040AB RID: 16555
		private Loadout currentLoadout;

		// Token: 0x040040AC RID: 16556
		private bool loadoutDirty;

		// Token: 0x040040AD RID: 16557
		private Transform mannequinInstanceTransform;

		// Token: 0x040040AE RID: 16558
		private bool mannequinInstanceDirty;
	}
}
