using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EntityStates.Missions.Moon;
using EntityStates.MoonElevator;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007BC RID: 1980
	[RequireComponent(typeof(EntityStateMachine))]
	public class MoonBatteryMissionController : NetworkBehaviour
	{
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060029E4 RID: 10724 RVA: 0x000B4D3C File Offset: 0x000B2F3C
		// (remove) Token: 0x060029E5 RID: 10725 RVA: 0x000B4D70 File Offset: 0x000B2F70
		public static event Action onInstanceChangedGlobal;

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000B4DA3 File Offset: 0x000B2FA3
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000B4DAA File Offset: 0x000B2FAA
		public static MoonBatteryMissionController instance { get; private set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000B4DB2 File Offset: 0x000B2FB2
		public int numChargedBatteries
		{
			get
			{
				return this._numChargedBatteries;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000B4DBA File Offset: 0x000B2FBA
		public int numRequiredBatteries
		{
			get
			{
				return this._numRequiredBatteries;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x000B4DC2 File Offset: 0x000B2FC2
		public string objectiveToken
		{
			get
			{
				return this._objectiveToken;
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000B4DCC File Offset: 0x000B2FCC
		private void Awake()
		{
			this.batteryHoldoutZones = new HoldoutZoneController[this.moonBatteries.Length];
			this.batteryStateMachines = new EntityStateMachine[this.moonBatteries.Length];
			for (int i = 0; i < this.moonBatteries.Length; i++)
			{
				GameObject gameObject = this.moonBatteries[i];
				this.batteryHoldoutZones[i] = gameObject.GetComponent<HoldoutZoneController>();
				this.batteryStateMachines[i] = gameObject.GetComponent<EntityStateMachine>();
			}
			this.elevatorStateMachines = new EntityStateMachine[this.elevators.Length];
			for (int j = 0; j < this.elevators.Length; j++)
			{
				this.elevatorStateMachines[j] = this.elevators[j].GetComponent<EntityStateMachine>();
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000B4E74 File Offset: 0x000B3074
		private void OnEnable()
		{
			MoonBatteryMissionController.instance = SingletonHelper.Assign<MoonBatteryMissionController>(MoonBatteryMissionController.instance, this);
			Action action = MoonBatteryMissionController.onInstanceChangedGlobal;
			if (action != null)
			{
				action();
			}
			ObjectivePanelController.collectObjectiveSources += this.OnCollectObjectiveSources;
			for (int i = 0; i < this.batteryHoldoutZones.Length; i++)
			{
				this.batteryHoldoutZones[i].onCharged.AddListener(new UnityAction<HoldoutZoneController>(this.OnBatteryCharged));
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000B4EE4 File Offset: 0x000B30E4
		private void OnDisable()
		{
			MoonBatteryMissionController.instance = SingletonHelper.Unassign<MoonBatteryMissionController>(MoonBatteryMissionController.instance, this);
			Action action = MoonBatteryMissionController.onInstanceChangedGlobal;
			if (action != null)
			{
				action();
			}
			ObjectivePanelController.collectObjectiveSources -= this.OnCollectObjectiveSources;
			for (int i = 0; i < this.batteryHoldoutZones.Length; i++)
			{
				this.batteryHoldoutZones[i].onCharged.RemoveListener(new UnityAction<HoldoutZoneController>(this.OnBatteryCharged));
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000B4F54 File Offset: 0x000B3154
		private void OnCollectObjectiveSources(CharacterMaster master, List<ObjectivePanelController.ObjectiveSourceDescriptor> objectiveSourcesList)
		{
			if (this._numChargedBatteries > 0 && this._numChargedBatteries < this._numRequiredBatteries)
			{
				objectiveSourcesList.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					master = master,
					objectiveType = typeof(MoonBatteryMissionObjectiveTracker),
					source = this
				});
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000B4FA8 File Offset: 0x000B31A8
		private void OnBatteryCharged(HoldoutZoneController holdoutZone)
		{
			this.Network_numChargedBatteries = this._numChargedBatteries + 1;
			if (this._numChargedBatteries >= this._numRequiredBatteries && NetworkServer.active)
			{
				for (int i = 0; i < this.batteryHoldoutZones.Length; i++)
				{
					if (this.batteryHoldoutZones[i].enabled)
					{
						this.batteryHoldoutZones[i].FullyChargeHoldoutZone();
						this.batteryHoldoutZones[i].onCharged.RemoveListener(new UnityAction<HoldoutZoneController>(this.OnBatteryCharged));
					}
				}
				this.batteryHoldoutZones = new HoldoutZoneController[0];
				for (int j = 0; j < this.batteryStateMachines.Length; j++)
				{
					if (!(this.batteryStateMachines[j].state is MoonBatteryComplete))
					{
						this.batteryStateMachines[j].SetNextState(new MoonBatteryDisabled());
					}
				}
				for (int k = 0; k < this.elevatorStateMachines.Length; k++)
				{
					this.elevatorStateMachines[k].SetNextState(new InactiveToReady());
				}
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000B5098 File Offset: 0x000B3298
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x000B50AB File Offset: 0x000B32AB
		public int Network_numChargedBatteries
		{
			get
			{
				return this._numChargedBatteries;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._numChargedBatteries, 1U);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000B50C0 File Offset: 0x000B32C0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this._numChargedBatteries);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this._numChargedBatteries);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000B512C File Offset: 0x000B332C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._numChargedBatteries = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._numChargedBatteries = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D34 RID: 11572
		[SerializeField]
		private GameObject[] moonBatteries;

		// Token: 0x04002D35 RID: 11573
		[SerializeField]
		private GameObject[] elevators;

		// Token: 0x04002D36 RID: 11574
		[SerializeField]
		private int _numRequiredBatteries;

		// Token: 0x04002D37 RID: 11575
		[SerializeField]
		private string _objectiveToken;

		// Token: 0x04002D38 RID: 11576
		private HoldoutZoneController[] batteryHoldoutZones;

		// Token: 0x04002D39 RID: 11577
		private EntityStateMachine[] batteryStateMachines;

		// Token: 0x04002D3A RID: 11578
		private EntityStateMachine[] elevatorStateMachines;

		// Token: 0x04002D3B RID: 11579
		[SyncVar]
		private int _numChargedBatteries;
	}
}
