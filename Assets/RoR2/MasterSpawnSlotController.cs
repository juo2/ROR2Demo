using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007A8 RID: 1960
	public class MasterSpawnSlotController : NetworkBehaviour
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x000B3172 File Offset: 0x000B1372
		public int openSlotCount
		{
			get
			{
				if (NetworkServer.active)
				{
					return this.CalcOpenSlotCount();
				}
				return this._openSlotCount;
			}
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000B3188 File Offset: 0x000B1388
		private int CalcOpenSlotCount()
		{
			int num = 0;
			using (List<MasterSpawnSlotController.ISlot>.Enumerator enumerator = this.slots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOpen())
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000B31E4 File Offset: 0x000B13E4
		private void OnEnable()
		{
			base.GetComponents<MasterSpawnSlotController.ISlot>(this.slots);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000B31F4 File Offset: 0x000B13F4
		[Server]
		public void SpawnAllOpen(GameObject summonerBodyObject, Xoroshiro128Plus rng, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback = null)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.MasterSpawnSlotController::SpawnAllOpen(UnityEngine.GameObject,Xoroshiro128Plus,System.Action`2<RoR2.MasterSpawnSlotController/ISlot,RoR2.SpawnCard/SpawnResult>)' called on client");
				return;
			}
			foreach (MasterSpawnSlotController.ISlot slot in this.slots)
			{
				if (slot.IsOpen())
				{
					slot.Spawn(summonerBodyObject, rng, callback);
				}
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000B3268 File Offset: 0x000B1468
		[Server]
		public void SpawnRandomOpen(int spawnCount, Xoroshiro128Plus rng, GameObject summonerBodyObject, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback = null)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.MasterSpawnSlotController::SpawnRandomOpen(System.Int32,Xoroshiro128Plus,UnityEngine.GameObject,System.Action`2<RoR2.MasterSpawnSlotController/ISlot,RoR2.SpawnCard/SpawnResult>)' called on client");
				return;
			}
			List<MasterSpawnSlotController.ISlot> list = new List<MasterSpawnSlotController.ISlot>();
			foreach (MasterSpawnSlotController.ISlot slot in this.slots)
			{
				if (slot.IsOpen())
				{
					list.Add(slot);
				}
			}
			Util.ShuffleList<MasterSpawnSlotController.ISlot>(list, rng);
			int num = 0;
			while (num < spawnCount && num < list.Count)
			{
				list[num].Spawn(summonerBodyObject, rng, callback);
				num++;
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000B330C File Offset: 0x000B150C
		[Server]
		public void KillAll()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.MasterSpawnSlotController::KillAll()' called on client");
				return;
			}
			foreach (MasterSpawnSlotController.ISlot slot in this.slots)
			{
				slot.Kill();
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000B3374 File Offset: 0x000B1574
		private void Update()
		{
			if (NetworkServer.active)
			{
				this.Network_openSlotCount = this.CalcOpenSlotCount();
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000B339C File Offset: 0x000B159C
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x000B33AF File Offset: 0x000B15AF
		public int Network_openSlotCount
		{
			get
			{
				return this._openSlotCount;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._openSlotCount, 1U);
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000B33C4 File Offset: 0x000B15C4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this._openSlotCount);
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
				writer.WritePackedUInt32((uint)this._openSlotCount);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000B3430 File Offset: 0x000B1630
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._openSlotCount = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._openSlotCount = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002CCA RID: 11466
		public List<MasterSpawnSlotController.ISlot> slots = new List<MasterSpawnSlotController.ISlot>();

		// Token: 0x04002CCB RID: 11467
		[SyncVar]
		private int _openSlotCount;

		// Token: 0x020007A9 RID: 1961
		public interface ISlot
		{
			// Token: 0x06002974 RID: 10612
			bool IsOpen();

			// Token: 0x06002975 RID: 10613
			void Spawn(GameObject summonerBodyObject, Xoroshiro128Plus rng, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback = null);

			// Token: 0x06002976 RID: 10614
			void Kill();
		}
	}
}
