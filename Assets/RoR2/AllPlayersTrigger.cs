using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005C5 RID: 1477
	[RequireComponent(typeof(Rigidbody))]
	public class AllPlayersTrigger : MonoBehaviour
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x00072C94 File Offset: 0x00070E94
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.collisionQueueServer = new Queue<Collider>();
				this.triggerActiveServer = false;
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00002A4D File Offset: 0x00000C4D
		private void OnEnable()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00072CAF File Offset: 0x00070EAF
		private void OnTriggerStay(Collider other)
		{
			if (base.enabled)
			{
				this.collisionQueueServer.Enqueue(other);
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00072CC8 File Offset: 0x00070EC8
		private void FixedUpdate()
		{
			if (!Run.instance)
			{
				return;
			}
			int num = 0;
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			while (this.collisionQueueServer.Count > 0)
			{
				Collider collider = this.collisionQueueServer.Dequeue();
				if (collider)
				{
					CharacterBody component = collider.GetComponent<CharacterBody>();
					if (component && component.isPlayerControlled && !list.Contains(component))
					{
						list.Add(component);
						num++;
					}
				}
			}
			CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
			bool flag = num == Run.instance.livingPlayerCount && num != 0;
			if (this.triggerActiveServer != flag)
			{
				this.triggerActiveServer = flag;
				UnityEvent unityEvent = this.triggerActiveServer ? this.onTriggerStart : this.onTriggerEnd;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x040020E0 RID: 8416
		public UnityEvent onTriggerStart;

		// Token: 0x040020E1 RID: 8417
		public UnityEvent onTriggerEnd;

		// Token: 0x040020E2 RID: 8418
		private Queue<Collider> collisionQueueServer;

		// Token: 0x040020E3 RID: 8419
		private bool triggerActiveServer;
	}
}
