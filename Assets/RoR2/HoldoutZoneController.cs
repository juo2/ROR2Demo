using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200072D RID: 1837
	public class HoldoutZoneController : BaseZoneBehavior
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x000A66AE File Offset: 0x000A48AE
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x000A66B6 File Offset: 0x000A48B6
		public float currentRadius { get; private set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000A66BF File Offset: 0x000A48BF
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x000A66C7 File Offset: 0x000A48C7
		public bool isAnyoneCharging { get; private set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000A66D0 File Offset: 0x000A48D0
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x000A66D8 File Offset: 0x000A48D8
		public TeamIndex chargingTeam { get; set; } = TeamIndex.Player;

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000A66E1 File Offset: 0x000A48E1
		public int displayChargePercent
		{
			get
			{
				return Mathf.Clamp(Mathf.FloorToInt(this.charge * 99f), 0, 99);
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x0600262D RID: 9773 RVA: 0x000A66FC File Offset: 0x000A48FC
		// (remove) Token: 0x0600262E RID: 9774 RVA: 0x000A6734 File Offset: 0x000A4934
		public event HoldoutZoneController.CalcRadiusDelegate calcRadius;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600262F RID: 9775 RVA: 0x000A676C File Offset: 0x000A496C
		// (remove) Token: 0x06002630 RID: 9776 RVA: 0x000A67A4 File Offset: 0x000A49A4
		public event HoldoutZoneController.CalcChargeRateDelegate calcChargeRate;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06002631 RID: 9777 RVA: 0x000A67DC File Offset: 0x000A49DC
		// (remove) Token: 0x06002632 RID: 9778 RVA: 0x000A6814 File Offset: 0x000A4A14
		public event HoldoutZoneController.CalcAccumulatedChargeDelegate calcAccumulatedCharge;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06002633 RID: 9779 RVA: 0x000A684C File Offset: 0x000A4A4C
		// (remove) Token: 0x06002634 RID: 9780 RVA: 0x000A6884 File Offset: 0x000A4A84
		public event HoldoutZoneController.CalcColorDelegate calcColor;

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000A68B9 File Offset: 0x000A4AB9
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x000A68C1 File Offset: 0x000A4AC1
		public float charge
		{
			get
			{
				return this._charge;
			}
			private set
			{
				this.Network_charge = value;
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000A68CA File Offset: 0x000A4ACA
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			HoldoutZoneController.sharedColorPropertyBlock = new MaterialPropertyBlock();
			ObjectivePanelController.collectObjectiveSources += HoldoutZoneController.OnCollectObjectiveSources;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000A68E7 File Offset: 0x000A4AE7
		private void Awake()
		{
			if (this.radiusIndicator)
			{
				this.baseIndicatorColor = this.radiusIndicator.sharedMaterial.GetColor("_TintColor");
			}
			this.buffWard = base.GetComponent<BuffWard>();
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000A691D File Offset: 0x000A4B1D
		private void Start()
		{
			if (this.applyFocusConvergence)
			{
				base.gameObject.AddComponent<HoldoutZoneController.FocusConvergenceController>();
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000A6933 File Offset: 0x000A4B33
		private void OnEnable()
		{
			if (this.radiusIndicator)
			{
				this.radiusIndicator.enabled = true;
				this.radiusIndicator.gameObject.SetActive(true);
			}
			this.currentRadius = 0f;
			InstanceTracker.Add<HoldoutZoneController>(this);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000A6970 File Offset: 0x000A4B70
		private void OnDisable()
		{
			InstanceTracker.Remove<HoldoutZoneController>(this);
			this.currentRadius = 0f;
			if (this.radiusIndicator)
			{
				this.radiusIndicator.enabled = false;
				this.radiusIndicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000A69B0 File Offset: 0x000A4BB0
		private void UpdateHealingNovas(bool isCharging)
		{
			if (this.applyHealingNova)
			{
				bool flag = false;
				for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
				{
					bool flag2 = Util.GetItemCountForTeam(teamIndex, RoR2Content.Items.TPHealingNova.itemIndex, false, true) > 0 && isCharging;
					flag = (flag || flag2);
					if (NetworkServer.active)
					{
						ref GameObject ptr = ref this.healingNovaGeneratorsByTeam[(int)teamIndex];
						if (flag2 != ptr)
						{
							if (flag2)
							{
								ptr = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/TeleporterHealNovaGenerator"), this.healingNovaRoot ?? base.transform);
								ptr.GetComponent<TeamFilter>().teamIndex = teamIndex;
								NetworkServer.Spawn(ptr);
							}
							else
							{
								UnityEngine.Object.Destroy(ptr);
								ptr = null;
							}
						}
					}
				}
				if (this.healingNovaItemEffect)
				{
					this.healingNovaItemEffect.SetActive(flag);
				}
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000A6A74 File Offset: 0x000A4C74
		private void FixedUpdate()
		{
			int num = HoldoutZoneController.CountLivingPlayers(this.chargingTeam);
			int num2 = HoldoutZoneController.CountPlayersInRadius(this, base.transform.position, this.currentRadius * this.currentRadius, this.chargingTeam);
			this.isAnyoneCharging = (num2 > 0);
			if (Run.instance)
			{
				float num3 = this.baseRadius + this.charge * this.chargeRadiusDelta;
				if (Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse2)
				{
					num3 *= 0.5f;
				}
				HoldoutZoneController.CalcRadiusDelegate calcRadiusDelegate = this.calcRadius;
				if (calcRadiusDelegate != null)
				{
					calcRadiusDelegate(ref num3);
				}
				this.currentRadius = Mathf.Max(Mathf.SmoothDamp(this.currentRadius, num3, ref this.radiusVelocity, this.radiusSmoothTime, float.PositiveInfinity, Time.fixedDeltaTime), this.minimumRadius);
			}
			if (this.radiusIndicator)
			{
				float num4 = 2f * this.currentRadius;
				this.radiusIndicator.transform.localScale = new Vector3(num4, num4, num4);
			}
			if (NetworkServer.active && this.buffWard)
			{
				this.buffWard.Networkradius = this.currentRadius;
			}
			if (NetworkServer.active)
			{
				float num5 = this.baseChargeDuration;
				float num6;
				if (this.isAnyoneCharging && num > 0)
				{
					num6 = Mathf.Pow((float)num2 / (float)num, this.playerCountScaling) / num5;
				}
				else
				{
					num6 = -this.dischargeRate;
				}
				HoldoutZoneController.CalcChargeRateDelegate calcChargeRateDelegate = this.calcChargeRate;
				if (calcChargeRateDelegate != null)
				{
					calcChargeRateDelegate(ref num6);
				}
				this.charge = Mathf.Clamp01(this.charge + num6 * Time.fixedDeltaTime);
				float charge = this.charge;
				HoldoutZoneController.CalcAccumulatedChargeDelegate calcAccumulatedChargeDelegate = this.calcAccumulatedCharge;
				if (calcAccumulatedChargeDelegate != null)
				{
					calcAccumulatedChargeDelegate(ref charge);
				}
				this.charge = charge;
			}
			Color value = this.baseIndicatorColor;
			HoldoutZoneController.CalcColorDelegate calcColorDelegate = this.calcColor;
			if (calcColorDelegate != null)
			{
				calcColorDelegate(ref value);
			}
			HoldoutZoneController.sharedColorPropertyBlock.SetColor("_TintColor", value);
			if (this.radiusIndicator)
			{
				this.radiusIndicator.SetPropertyBlock(HoldoutZoneController.sharedColorPropertyBlock);
			}
			bool flag = this.charge >= 1f;
			if (this.wasCharged != flag)
			{
				this.wasCharged = flag;
				if (flag)
				{
					HoldoutZoneController.HoldoutZoneControllerChargedUnityEvent holdoutZoneControllerChargedUnityEvent = this.onCharged;
					if (holdoutZoneControllerChargedUnityEvent != null)
					{
						holdoutZoneControllerChargedUnityEvent.Invoke(this);
					}
				}
			}
			this.UpdateHealingNovas(this.isAnyoneCharging);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000A6CAC File Offset: 0x000A4EAC
		private void OnDrawGizmos()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Color color = Gizmos.color;
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = new Color(0.75f, 0f, 0f, 0.5f);
			Gizmos.DrawWireSphere(Vector3.zero, this.baseRadius);
			Gizmos.color = color;
			Gizmos.matrix = matrix;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000A6D0C File Offset: 0x000A4F0C
		private static bool IsPointInChargingRadius(HoldoutZoneController holdoutZoneController, Vector3 origin, float chargingRadiusSqr, Vector3 point)
		{
			HoldoutZoneController.HoldoutZoneShape holdoutZoneShape = holdoutZoneController.holdoutZoneShape;
			if (holdoutZoneShape != HoldoutZoneController.HoldoutZoneShape.Sphere)
			{
				if (holdoutZoneShape == HoldoutZoneController.HoldoutZoneShape.VerticalTube)
				{
					point.y = 0f;
					origin.y = 0f;
					if ((point - origin).sqrMagnitude <= chargingRadiusSqr)
					{
						return true;
					}
				}
			}
			else if ((point - origin).sqrMagnitude <= chargingRadiusSqr)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000A6D6A File Offset: 0x000A4F6A
		private static bool IsBodyInChargingRadius(HoldoutZoneController holdoutZoneController, Vector3 origin, float chargingRadiusSqr, CharacterBody characterBody)
		{
			return HoldoutZoneController.IsPointInChargingRadius(holdoutZoneController, origin, chargingRadiusSqr, characterBody.corePosition);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000A6D7C File Offset: 0x000A4F7C
		private static int CountLivingPlayers(TeamIndex teamIndex)
		{
			int num = 0;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				if (teamMembers[i].body.isPlayerControlled)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000A6DBC File Offset: 0x000A4FBC
		private static int CountPlayersInRadius(HoldoutZoneController holdoutZoneController, Vector3 origin, float chargingRadiusSqr, TeamIndex teamIndex)
		{
			int num = 0;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				TeamComponent teamComponent = teamMembers[i];
				if (teamComponent.body.isPlayerControlled && HoldoutZoneController.IsBodyInChargingRadius(holdoutZoneController, origin, chargingRadiusSqr, teamComponent.body))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000A6E0D File Offset: 0x000A500D
		public bool IsBodyInChargingRadius(CharacterBody body)
		{
			return body && HoldoutZoneController.IsBodyInChargingRadius(this, base.transform.position, this.currentRadius * this.currentRadius, body);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000A6E38 File Offset: 0x000A5038
		[Server]
		public void FullyChargeHoldoutZone()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HoldoutZoneController::FullyChargeHoldoutZone()' called on client");
				return;
			}
			this.charge = 1f;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000A6E5C File Offset: 0x000A505C
		private static void OnCollectObjectiveSources(CharacterMaster master, List<ObjectivePanelController.ObjectiveSourceDescriptor> objectiveSourcesList)
		{
			List<HoldoutZoneController> instancesList = InstanceTracker.GetInstancesList<HoldoutZoneController>();
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				HoldoutZoneController holdoutZoneController = instancesList[i];
				if (holdoutZoneController.showObjective && holdoutZoneController.chargingTeam == master.teamIndex)
				{
					objectiveSourcesList.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
					{
						master = master,
						objectiveType = typeof(HoldoutZoneController.ChargeHoldoutZoneObjectiveTracker),
						source = holdoutZoneController
					});
				}
				i++;
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000A6ED1 File Offset: 0x000A50D1
		public override bool IsInBounds(Vector3 position)
		{
			return HoldoutZoneController.IsPointInChargingRadius(this, base.transform.position, this.currentRadius * this.currentRadius, position);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000A6F4C File Offset: 0x000A514C
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x000A6F5F File Offset: 0x000A515F
		public float Network_charge
		{
			get
			{
				return this._charge;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._charge, 1U);
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000A6F74 File Offset: 0x000A5174
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			if (forceAll)
			{
				writer.Write(this._charge);
				return true;
			}
			bool flag2 = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag2)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag2 = true;
				}
				writer.Write(this._charge);
			}
			if (!flag2)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag2 || flag;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000A6FEC File Offset: 0x000A51EC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
			if (initialState)
			{
				this._charge = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._charge = reader.ReadSingle();
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00077BBF File Offset: 0x00075DBF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x04002A04 RID: 10756
		public HoldoutZoneController.HoldoutZoneShape holdoutZoneShape;

		// Token: 0x04002A05 RID: 10757
		[Tooltip("The base radius of this charging sphere. Players must be within this radius to charge this zone.")]
		public float baseRadius;

		// Token: 0x04002A06 RID: 10758
		[Tooltip("No modifiers can reduce the radius below this size")]
		public float minimumRadius;

		// Token: 0x04002A07 RID: 10759
		[Tooltip("The overall change to the radius from 0% charge to 100% charge.")]
		public float chargeRadiusDelta;

		// Token: 0x04002A08 RID: 10760
		[Tooltip("How long it takes for this zone to finish charging without any modifiers.")]
		public float baseChargeDuration;

		// Token: 0x04002A09 RID: 10761
		[Tooltip("Approximately how long it should take to change from any given radius to the desired one.")]
		public float radiusSmoothTime;

		// Token: 0x04002A0A RID: 10762
		[Tooltip("An object instance which will be used to represent the clear radius.")]
		public Renderer radiusIndicator;

		// Token: 0x04002A0B RID: 10763
		[Tooltip("The child object to enable when healing nova should be active.")]
		public GameObject healingNovaItemEffect;

		// Token: 0x04002A0C RID: 10764
		public Transform healingNovaRoot;

		// Token: 0x04002A0D RID: 10765
		public string inBoundsObjectiveToken = "OBJECTIVE_CHARGE_TELEPORTER";

		// Token: 0x04002A0E RID: 10766
		public string outOfBoundsObjectiveToken = "OBJECTIVE_CHARGE_TELEPORTER_OOB";

		// Token: 0x04002A0F RID: 10767
		public bool showObjective = true;

		// Token: 0x04002A10 RID: 10768
		public bool applyFocusConvergence;

		// Token: 0x04002A11 RID: 10769
		public bool applyHealingNova = true;

		// Token: 0x04002A12 RID: 10770
		[Range(0f, 3.4028235E+38f)]
		public float playerCountScaling = 1f;

		// Token: 0x04002A13 RID: 10771
		[Tooltip("If the zone is empty, this is the rate at which the charge decreases (a negative value will increase charge)")]
		public float dischargeRate;

		// Token: 0x04002A14 RID: 10772
		public HoldoutZoneController.HoldoutZoneControllerChargedUnityEvent onCharged;

		// Token: 0x04002A1C RID: 10780
		private BuffWard buffWard;

		// Token: 0x04002A1D RID: 10781
		private static MaterialPropertyBlock sharedColorPropertyBlock;

		// Token: 0x04002A1E RID: 10782
		private Color baseIndicatorColor;

		// Token: 0x04002A1F RID: 10783
		private float radiusVelocity;

		// Token: 0x04002A20 RID: 10784
		private bool wasCharged;

		// Token: 0x04002A21 RID: 10785
		private GameObject[] healingNovaGeneratorsByTeam = new GameObject[5];

		// Token: 0x04002A22 RID: 10786
		[SyncVar]
		private float _charge;

		// Token: 0x0200072E RID: 1838
		public enum HoldoutZoneShape
		{
			// Token: 0x04002A24 RID: 10788
			Sphere,
			// Token: 0x04002A25 RID: 10789
			VerticalTube,
			// Token: 0x04002A26 RID: 10790
			Count
		}

		// Token: 0x0200072F RID: 1839
		// (Invoke) Token: 0x0600264F RID: 9807
		public delegate void CalcRadiusDelegate(ref float radius);

		// Token: 0x02000730 RID: 1840
		// (Invoke) Token: 0x06002653 RID: 9811
		public delegate void CalcChargeRateDelegate(ref float rate);

		// Token: 0x02000731 RID: 1841
		// (Invoke) Token: 0x06002657 RID: 9815
		public delegate void CalcAccumulatedChargeDelegate(ref float charge);

		// Token: 0x02000732 RID: 1842
		// (Invoke) Token: 0x0600265B RID: 9819
		public delegate void CalcColorDelegate(ref Color color);

		// Token: 0x02000733 RID: 1843
		[Serializable]
		public class HoldoutZoneControllerChargedUnityEvent : UnityEvent<HoldoutZoneController>
		{
		}

		// Token: 0x02000734 RID: 1844
		private class ChargeHoldoutZoneObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x17000353 RID: 851
			// (get) Token: 0x0600265F RID: 9823 RVA: 0x000A703D File Offset: 0x000A523D
			private HoldoutZoneController holdoutZoneController
			{
				get
				{
					return (HoldoutZoneController)this.sourceDescriptor.source;
				}
			}

			// Token: 0x06002660 RID: 9824 RVA: 0x000A7050 File Offset: 0x000A5250
			private bool ShouldBeFlashing()
			{
				bool flag = true;
				if (this.sourceDescriptor.master && this.holdoutZoneController)
				{
					flag = this.holdoutZoneController.IsBodyInChargingRadius(this.sourceDescriptor.master.GetBody());
				}
				return !flag;
			}

			// Token: 0x06002661 RID: 9825 RVA: 0x000A70A0 File Offset: 0x000A52A0
			protected override string GenerateString()
			{
				this.lastPercent = this.holdoutZoneController.displayChargePercent;
				string text = string.Format(Language.GetString(this.holdoutZoneController.inBoundsObjectiveToken), this.lastPercent);
				if (this.ShouldBeFlashing())
				{
					text = string.Format(Language.GetString(this.holdoutZoneController.outOfBoundsObjectiveToken), this.lastPercent);
					if ((int)(Time.time * 12f) % 2 == 0)
					{
						text = string.Format("<style=cDeath>{0}</style>", text);
					}
				}
				return text;
			}

			// Token: 0x06002662 RID: 9826 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool IsDirty()
			{
				return true;
			}

			// Token: 0x04002A27 RID: 10791
			private int lastPercent = -1;
		}

		// Token: 0x02000735 RID: 1845
		private class FocusConvergenceController : MonoBehaviour
		{
			// Token: 0x06002664 RID: 9828 RVA: 0x000A7134 File Offset: 0x000A5334
			private void Awake()
			{
				this.holdoutZoneController = base.GetComponent<HoldoutZoneController>();
			}

			// Token: 0x06002665 RID: 9829 RVA: 0x000A7144 File Offset: 0x000A5344
			private void OnEnable()
			{
				this.enabledTime = Run.FixedTimeStamp.now;
				this.holdoutZoneController.calcRadius += this.ApplyRadius;
				this.holdoutZoneController.calcChargeRate += this.ApplyRate;
				this.holdoutZoneController.calcColor += this.ApplyColor;
			}

			// Token: 0x06002666 RID: 9830 RVA: 0x000A71A4 File Offset: 0x000A53A4
			private void OnDisable()
			{
				this.holdoutZoneController.calcColor -= this.ApplyColor;
				this.holdoutZoneController.calcChargeRate -= this.ApplyRate;
				this.holdoutZoneController.calcRadius -= this.ApplyRadius;
			}

			// Token: 0x06002667 RID: 9831 RVA: 0x000A71F6 File Offset: 0x000A53F6
			private void ApplyRadius(ref float radius)
			{
				if (this.currentFocusConvergenceCount > 0)
				{
					radius /= HoldoutZoneController.FocusConvergenceController.convergenceRadiusDivisor * (float)this.currentFocusConvergenceCount;
				}
			}

			// Token: 0x06002668 RID: 9832 RVA: 0x000A7213 File Offset: 0x000A5413
			private void ApplyColor(ref Color color)
			{
				color = Color.Lerp(color, HoldoutZoneController.FocusConvergenceController.convergenceMaterialColor, HoldoutZoneController.FocusConvergenceController.colorCurve.Evaluate(this.currentValue));
			}

			// Token: 0x06002669 RID: 9833 RVA: 0x000A723B File Offset: 0x000A543B
			private void ApplyRate(ref float rate)
			{
				if (this.currentFocusConvergenceCount > 0)
				{
					rate *= 1f + HoldoutZoneController.FocusConvergenceController.convergenceChargeRateBonus * (float)this.currentFocusConvergenceCount;
				}
			}

			// Token: 0x0600266A RID: 9834 RVA: 0x000A7260 File Offset: 0x000A5460
			private void FixedUpdate()
			{
				this.currentFocusConvergenceCount = Util.GetItemCountForTeam(this.holdoutZoneController.chargingTeam, RoR2Content.Items.FocusConvergence.itemIndex, true, false);
				if (this.enabledTime.timeSince < HoldoutZoneController.FocusConvergenceController.startupDelay)
				{
					this.currentFocusConvergenceCount = 0;
				}
				this.currentFocusConvergenceCount = Mathf.Min(this.currentFocusConvergenceCount, HoldoutZoneController.FocusConvergenceController.cap);
				float target = ((float)this.currentFocusConvergenceCount > 0f) ? 1f : 0f;
				float num = Mathf.MoveTowards(this.currentValue, target, HoldoutZoneController.FocusConvergenceController.rampUpTime * Time.fixedDeltaTime);
				if (this.currentValue <= 0f && num > 0f)
				{
					Util.PlaySound("Play_item_lunar_focusedConvergence", base.gameObject);
				}
				this.currentValue = num;
			}

			// Token: 0x04002A28 RID: 10792
			private static readonly float convergenceRadiusDivisor = 2f;

			// Token: 0x04002A29 RID: 10793
			private static readonly float convergenceChargeRateBonus = 0.3f;

			// Token: 0x04002A2A RID: 10794
			private static readonly Color convergenceMaterialColor = new Color(0f, 3.9411764f, 5f, 1f);

			// Token: 0x04002A2B RID: 10795
			private static readonly float rampUpTime = 5f;

			// Token: 0x04002A2C RID: 10796
			private static readonly float startupDelay = 3f;

			// Token: 0x04002A2D RID: 10797
			private static readonly int cap = 3;

			// Token: 0x04002A2E RID: 10798
			private float currentValue;

			// Token: 0x04002A2F RID: 10799
			private HoldoutZoneController holdoutZoneController;

			// Token: 0x04002A30 RID: 10800
			private int currentFocusConvergenceCount;

			// Token: 0x04002A31 RID: 10801
			private Run.FixedTimeStamp enabledTime;

			// Token: 0x04002A32 RID: 10802
			private static readonly AnimationCurve colorCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}
	}
}
