using System;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000564 RID: 1380
	[CreateAssetMenu(menuName = "RoR2/SurvivorDef")]
	public class SurvivorDef : ScriptableObject
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0006C12B File Offset: 0x0006A32B
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0006C133 File Offset: 0x0006A333
		public SurvivorIndex survivorIndex { get; set; } = SurvivorIndex.None;

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x00062756 File Offset: 0x00060956
		[Obsolete(".name should not be used. Use .cachedName instead. If retrieving the value from the engine is absolutely necessary, cast to ScriptableObject first.", true)]
		public new string name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0006C13C File Offset: 0x0006A33C
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0006C144 File Offset: 0x0006A344
		public string cachedName
		{
			get
			{
				return this._cachedName;
			}
			set
			{
				base.name = value;
				this._cachedName = value;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006C154 File Offset: 0x0006A354
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006C154 File Offset: 0x0006A354
		private void OnValidate()
		{
			this._cachedName = base.name;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0006C164 File Offset: 0x0006A364
		[ContextMenu("Auto Populate Tokens")]
		public void AutoPopulateTokens()
		{
			string arg = base.name.ToUpperInvariant();
			this.displayNameToken = this.displayPrefab.GetComponent<CharacterBody>().baseNameToken;
			this.descriptionToken = string.Format("{0}_DESCRIPTION", arg);
			this.outroFlavorToken = string.Format("{0}_OUTRO_FLAVOR", arg);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		[ContextMenu("Upgrade unlockableName to unlockableDef")]
		public void UpgradeUnlockableNameToUnlockableDef()
		{
			if (!string.IsNullOrEmpty(this.unlockableName) && !this.unlockableDef)
			{
				UnlockableDef exists = LegacyResourcesAPI.Load<UnlockableDef>("UnlockableDefs/" + this.unlockableName);
				if (exists)
				{
					this.unlockableDef = exists;
					this.unlockableName = null;
				}
			}
			EditorUtil.SetDirty(this);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006C214 File Offset: 0x0006A414
		public ExpansionDef GetRequiredExpansion()
		{
			ExpansionRequirementComponent component = this.bodyPrefab.GetComponent<ExpansionRequirementComponent>();
			if (!component)
			{
				return null;
			}
			return component.requiredExpansion;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0006C240 File Offset: 0x0006A440
		public EntitlementDef GetRequiredEntitlement()
		{
			ExpansionDef requiredExpansion = this.GetRequiredExpansion();
			if (!requiredExpansion)
			{
				return null;
			}
			return requiredExpansion.requiredEntitlement;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006C264 File Offset: 0x0006A464
		public bool CheckUserHasRequiredEntitlement(NetworkUser networkUser)
		{
			EntitlementDef requiredEntitlement = this.GetRequiredEntitlement();
			return !requiredEntitlement || EntitlementManager.networkUserEntitlementTracker.UserHasEntitlement(networkUser, requiredEntitlement);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006C290 File Offset: 0x0006A490
		public bool CheckUserHasRequiredEntitlement(LocalUser localUser)
		{
			EntitlementDef requiredEntitlement = this.GetRequiredEntitlement();
			return !requiredEntitlement || EntitlementManager.localUserEntitlementTracker.UserHasEntitlement(localUser, requiredEntitlement);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006C2BC File Offset: 0x0006A4BC
		public bool CheckRequiredExpansionEnabled(NetworkUser networkUser = null)
		{
			ExpansionDef requiredExpansion = this.GetRequiredExpansion();
			if (!requiredExpansion)
			{
				return true;
			}
			if (requiredExpansion.enabledChoice == null)
			{
				return false;
			}
			if (networkUser && !EntitlementManager.networkUserEntitlementTracker.UserHasEntitlement(networkUser, requiredExpansion.requiredEntitlement))
			{
				return false;
			}
			RuleBook ruleBook = PreGameController.instance ? PreGameController.instance.readOnlyRuleBook : (Run.instance ? Run.instance.ruleBook : null);
			return ruleBook == null || ruleBook.IsChoiceActive(requiredExpansion.enabledChoice);
		}

		// Token: 0x04001EBE RID: 7870
		private string _cachedName;

		// Token: 0x04001EBF RID: 7871
		public GameObject bodyPrefab;

		// Token: 0x04001EC0 RID: 7872
		public GameObject displayPrefab;

		// Token: 0x04001EC1 RID: 7873
		[Obsolete("Use 'unlockableDef' instead.")]
		public string unlockableName = "";

		// Token: 0x04001EC2 RID: 7874
		public UnlockableDef unlockableDef;

		// Token: 0x04001EC3 RID: 7875
		public string displayNameToken;

		// Token: 0x04001EC4 RID: 7876
		public string descriptionToken;

		// Token: 0x04001EC5 RID: 7877
		public string outroFlavorToken;

		// Token: 0x04001EC6 RID: 7878
		public string mainEndingEscapeFailureFlavorToken;

		// Token: 0x04001EC7 RID: 7879
		public Color primaryColor;

		// Token: 0x04001EC8 RID: 7880
		public float desiredSortPosition;

		// Token: 0x04001EC9 RID: 7881
		public bool hidden;
	}
}
