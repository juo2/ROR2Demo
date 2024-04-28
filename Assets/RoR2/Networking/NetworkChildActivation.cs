using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C3F RID: 3135
	public class NetworkChildActivation : NetworkBehaviour
	{
		// Token: 0x060046F2 RID: 18162 RVA: 0x00125508 File Offset: 0x00123708
		private void BuildTrackersForChildren(GameObject[] newTrackedChildren)
		{
			if (this.trackedChildren == newTrackedChildren)
			{
				return;
			}
			this.trackedChildren = newTrackedChildren;
			for (int i = 0; i < this.trackers.Length; i++)
			{
				UnityEngine.Object.Destroy(this.trackers[i]);
			}
			Array.Resize<NetworkChildActivation.GameObjectActiveTracker>(ref this.trackers, newTrackedChildren.Length);
			for (int j = 0; j < newTrackedChildren.Length; j++)
			{
				GameObject gameObject = newTrackedChildren[j];
				if (gameObject)
				{
					NetworkChildActivation.GameObjectActiveTracker gameObjectActiveTracker = gameObject.AddComponent<NetworkChildActivation.GameObjectActiveTracker>();
					gameObjectActiveTracker.index = j;
					gameObjectActiveTracker.networkChildActivation = this;
					this.trackers[j] = gameObjectActiveTracker;
					this.SetChildActiveState(j, gameObject.gameObject.activeInHierarchy);
				}
			}
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x0012559E File Offset: 0x0012379E
		private void SetChildActiveState(int index, bool active)
		{
			this.childrenActiveStates[index] = active;
			base.SetDirtyBit(NetworkChildActivation.activeStatesDirtyBit);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x001255B4 File Offset: 0x001237B4
		private void Awake()
		{
			Array.Resize<bool>(ref this.childrenActiveStates, this.children.Length);
			Array.Clear(this.childrenActiveStates, 0, this.childrenActiveStates.Length);
			if (NetworkServer.active)
			{
				this.BuildTrackersForChildren(this.children);
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x001255F0 File Offset: 0x001237F0
		private void OnDestroy()
		{
			if (NetworkServer.active)
			{
				this.BuildTrackersForChildren(Array.Empty<GameObject>());
			}
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x00125604 File Offset: 0x00123804
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			base.OnSerialize(writer, initialState);
			uint num = initialState ? NetworkChildActivation.allDirtyBits : base.syncVarDirtyBits;
			writer.WritePackedUInt32(num);
			if ((num & NetworkChildActivation.activeStatesDirtyBit) > 0U)
			{
				writer.WriteBitArray(this.childrenActiveStates, this.childrenActiveStates.Length);
			}
			return num > 0U;
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00125658 File Offset: 0x00123858
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
			if ((reader.ReadPackedUInt32() & NetworkChildActivation.activeStatesDirtyBit) > 0U)
			{
				reader.ReadBitArray(this.childrenActiveStates);
				for (int i = 0; i < this.childrenActiveStates.Length; i++)
				{
					GameObject gameObject = this.children[i];
					if (gameObject)
					{
						try
						{
							gameObject.SetActive(this.childrenActiveStates[i]);
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
					}
				}
			}
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040044A8 RID: 17576
		[Tooltip("The list of child objects this object will handle activating and deactivating over the network. Client and server must have matching lists (ie DO NOT CHANGE AT RUNTIME.)")]
		public GameObject[] children = Array.Empty<GameObject>();

		// Token: 0x040044A9 RID: 17577
		private NetworkChildActivation.GameObjectActiveTracker[] trackers = Array.Empty<NetworkChildActivation.GameObjectActiveTracker>();

		// Token: 0x040044AA RID: 17578
		private bool[] childrenActiveStates = Array.Empty<bool>();

		// Token: 0x040044AB RID: 17579
		private static readonly uint activeStatesDirtyBit = 1U;

		// Token: 0x040044AC RID: 17580
		private static readonly uint allDirtyBits = NetworkChildActivation.activeStatesDirtyBit;

		// Token: 0x040044AD RID: 17581
		private GameObject[] trackedChildren = Array.Empty<GameObject>();

		// Token: 0x02000C40 RID: 3136
		private class GameObjectActiveTracker : MonoBehaviour
		{
			// Token: 0x060046FC RID: 18172 RVA: 0x0012571E File Offset: 0x0012391E
			private void OnEnable()
			{
				if (this.networkChildActivation)
				{
					this.networkChildActivation.SetChildActiveState(this.index, true);
				}
			}

			// Token: 0x060046FD RID: 18173 RVA: 0x0012573F File Offset: 0x0012393F
			private void OnDisable()
			{
				if (this.networkChildActivation)
				{
					this.networkChildActivation.SetChildActiveState(this.index, false);
				}
			}

			// Token: 0x040044AE RID: 17582
			public NetworkChildActivation networkChildActivation;

			// Token: 0x040044AF RID: 17583
			public int index = -1;
		}
	}
}
