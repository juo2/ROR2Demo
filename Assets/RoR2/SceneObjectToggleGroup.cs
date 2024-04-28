using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200087D RID: 2173
	public class SceneObjectToggleGroup : NetworkBehaviour
	{
		// Token: 0x06002F9A RID: 12186 RVA: 0x000CACC4 File Offset: 0x000C8EC4
		static SceneObjectToggleGroup()
		{
			NetworkManagerSystem.onServerSceneChangedGlobal += SceneObjectToggleGroup.OnServerSceneChanged;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000CACE4 File Offset: 0x000C8EE4
		private static void OnServerSceneChanged(string sceneName)
		{
			while (SceneObjectToggleGroup.activationsQueue.Count > 0)
			{
				SceneObjectToggleGroup sceneObjectToggleGroup = SceneObjectToggleGroup.activationsQueue.Dequeue();
				if (sceneObjectToggleGroup)
				{
					sceneObjectToggleGroup.ApplyActivations();
				}
			}
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000CAD1C File Offset: 0x000C8F1C
		private void Awake()
		{
			SceneObjectToggleGroup.activationsQueue.Enqueue(this);
			int num = 0;
			for (int i = 0; i < this.toggleGroups.Length; i++)
			{
				num += this.toggleGroups[i].objects.Length;
			}
			this.allToggleableObjects = new GameObject[num];
			this.activations = new bool[num];
			this.internalToggleGroups = new SceneObjectToggleGroup.ToggleGroupRange[this.toggleGroups.Length];
			int start = 0;
			for (int j = 0; j < this.toggleGroups.Length; j++)
			{
				GameObject[] objects = this.toggleGroups[j].objects;
				SceneObjectToggleGroup.ToggleGroupRange toggleGroupRange = default(SceneObjectToggleGroup.ToggleGroupRange);
				toggleGroupRange.start = start;
				toggleGroupRange.count = objects.Length;
				toggleGroupRange.minEnabled = this.toggleGroups[j].minEnabled;
				toggleGroupRange.maxEnabled = this.toggleGroups[j].maxEnabled;
				this.internalToggleGroups[j] = toggleGroupRange;
				foreach (GameObject gameObject in objects)
				{
					this.allToggleableObjects[start++] = gameObject;
				}
			}
			if (NetworkServer.active)
			{
				this.Generate();
			}
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000CAE4A File Offset: 0x000C904A
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (!NetworkServer.active)
			{
				this.ApplyActivations();
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000CAE60 File Offset: 0x000C9060
		[Server]
		private void Generate()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SceneObjectToggleGroup::Generate()' called on client");
				return;
			}
			for (int i = 0; i < this.internalToggleGroups.Length; i++)
			{
				SceneObjectToggleGroup.ToggleGroupRange toggleGroupRange = this.internalToggleGroups[i];
				int num = Run.instance.stageRng.RangeInt(toggleGroupRange.minEnabled, toggleGroupRange.maxEnabled + 1);
				List<int> list = SceneObjectToggleGroup.<Generate>g__RangeList|12_0(toggleGroupRange.start, toggleGroupRange.count);
				Util.ShuffleList<int>(list, Run.instance.stageRng);
				for (int j = num - 1; j >= 0; j--)
				{
					this.activations[list[j]] = true;
					list.RemoveAt(j);
				}
				for (int k = 0; k < list.Count; k++)
				{
					this.activations[list[k]] = false;
				}
			}
			base.SetDirtyBit(1U);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000CAF38 File Offset: 0x000C9138
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = initialState ? 1U : base.syncVarDirtyBits;
			writer.Write((byte)num);
			if ((num & 1U) != 0U)
			{
				writer.WriteBitArray(this.activations);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000CAF74 File Offset: 0x000C9174
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadByte() & 1) != 0)
			{
				reader.ReadBitArray(this.activations);
			}
			this.ApplyActivations();
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000CAF94 File Offset: 0x000C9194
		private void ApplyActivations()
		{
			for (int i = 0; i < this.allToggleableObjects.Length; i++)
			{
				GameObject gameObject = this.allToggleableObjects[i];
				if (gameObject)
				{
					gameObject.SetActive(this.activations[i]);
				}
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000CAFD4 File Offset: 0x000C91D4
		[CompilerGenerated]
		internal static List<int> <Generate>g__RangeList|12_0(int start, int count)
		{
			List<int> list = new List<int>(count);
			int i = start;
			int num = start + count;
			while (i < num)
			{
				list.Add(i);
				i++;
			}
			return list;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400315F RID: 12639
		public GameObjectToggleGroup[] toggleGroups;

		// Token: 0x04003160 RID: 12640
		private const byte enabledObjectsDirtyBit = 1;

		// Token: 0x04003161 RID: 12641
		private const byte initialStateMask = 1;

		// Token: 0x04003162 RID: 12642
		private static readonly Queue<SceneObjectToggleGroup> activationsQueue = new Queue<SceneObjectToggleGroup>();

		// Token: 0x04003163 RID: 12643
		private GameObject[] allToggleableObjects;

		// Token: 0x04003164 RID: 12644
		private bool[] activations;

		// Token: 0x04003165 RID: 12645
		private SceneObjectToggleGroup.ToggleGroupRange[] internalToggleGroups;

		// Token: 0x0200087E RID: 2174
		private struct ToggleGroupRange
		{
			// Token: 0x04003166 RID: 12646
			public int start;

			// Token: 0x04003167 RID: 12647
			public int count;

			// Token: 0x04003168 RID: 12648
			public int minEnabled;

			// Token: 0x04003169 RID: 12649
			public int maxEnabled;
		}
	}
}
