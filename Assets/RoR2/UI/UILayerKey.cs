using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000DA5 RID: 3493
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class UILayerKey : MonoBehaviour
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x0014B86B File Offset: 0x00149A6B
		// (set) Token: 0x0600500C RID: 20492 RVA: 0x0014B873 File Offset: 0x00149A73
		public bool representsTopLayer { get; private set; }

		// Token: 0x0600500D RID: 20493 RVA: 0x0014B87C File Offset: 0x00149A7C
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014B88A File Offset: 0x00149A8A
		private void Start()
		{
			UILayerKey.RefreshTopLayerForEventSystem(this.eventSystemLocator.eventSystem);
			if (this.representsTopLayer)
			{
				this.onBeginRepresentTopLayer.Invoke();
				return;
			}
			this.onEndRepresentTopLayer.Invoke();
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0014B8BB File Offset: 0x00149ABB
		private void OnEnable()
		{
			InstanceTracker.Add<UILayerKey>(this);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014B8C3 File Offset: 0x00149AC3
		private void OnDisable()
		{
			InstanceTracker.Remove<UILayerKey>(this);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014B8CB File Offset: 0x00149ACB
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			MPEventSystemManager.availability.CallWhenAvailable(delegate
			{
				ReadOnlyCollection<MPEventSystem> readOnlyInstancesList = MPEventSystem.readOnlyInstancesList;
				for (int i = 0; i < readOnlyInstancesList.Count; i++)
				{
					UILayerKey.topLayerRepresentations[readOnlyInstancesList[i]] = null;
				}
			});
			RoR2Application.onLateUpdate += UILayerKey.StaticLateUpdate;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014B908 File Offset: 0x00149B08
		private static void StaticLateUpdate()
		{
			ReadOnlyCollection<MPEventSystem> readOnlyInstancesList = MPEventSystem.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				UILayerKey.RefreshTopLayerForEventSystem(readOnlyInstancesList[i]);
			}
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014B938 File Offset: 0x00149B38
		private static void RefreshTopLayerForEventSystem(MPEventSystem eventSystem)
		{
			int num = int.MinValue;
			UILayerKey uilayerKey = null;
			UILayerKey uilayerKey2 = UILayerKey.topLayerRepresentations[eventSystem];
			List<UILayerKey> instancesList = InstanceTracker.GetInstancesList<UILayerKey>();
			for (int i = 0; i < instancesList.Count; i++)
			{
				UILayerKey uilayerKey3 = instancesList[i];
				if (!(uilayerKey3.eventSystemLocator.eventSystem != eventSystem) && uilayerKey3.layer.priority > num)
				{
					uilayerKey = uilayerKey3;
					num = uilayerKey3.layer.priority;
				}
			}
			if (uilayerKey != uilayerKey2)
			{
				if (uilayerKey2)
				{
					uilayerKey2.onEndRepresentTopLayer.Invoke();
					uilayerKey2.representsTopLayer = false;
				}
				uilayerKey2 = (UILayerKey.topLayerRepresentations[eventSystem] = uilayerKey);
				if (uilayerKey2)
				{
					uilayerKey2.representsTopLayer = true;
					uilayerKey2.onBeginRepresentTopLayer.Invoke();
				}
			}
		}

		// Token: 0x04004CBB RID: 19643
		public UILayer layer;

		// Token: 0x04004CBC RID: 19644
		public UnityEvent onBeginRepresentTopLayer;

		// Token: 0x04004CBD RID: 19645
		public UnityEvent onEndRepresentTopLayer;

		// Token: 0x04004CBE RID: 19646
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004CC0 RID: 19648
		private static readonly Dictionary<MPEventSystem, UILayerKey> topLayerRepresentations = new Dictionary<MPEventSystem, UILayerKey>();
	}
}
