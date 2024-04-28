using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B84 RID: 2948
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileCallOnOwnerNearby : MonoBehaviour
	{
		// Token: 0x06004317 RID: 17175 RVA: 0x00116724 File Offset: 0x00114924
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			ProjectileCallOnOwnerNearby.Filter filter = ProjectileCallOnOwnerNearby.Filter.None;
			if (NetworkServer.active)
			{
				filter |= ProjectileCallOnOwnerNearby.Filter.Server;
			}
			if (NetworkClient.active)
			{
				filter |= ProjectileCallOnOwnerNearby.Filter.Client;
			}
			if ((this.filter & filter) == ProjectileCallOnOwnerNearby.Filter.None)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x00116766 File Offset: 0x00114966
		private void OnDisable()
		{
			this.SetState(ProjectileCallOnOwnerNearby.State.Outside);
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x0011676F File Offset: 0x0011496F
		private void SetState(ProjectileCallOnOwnerNearby.State newState)
		{
			if (this.state == newState)
			{
				return;
			}
			this.state = newState;
			if (this.state == ProjectileCallOnOwnerNearby.State.Inside)
			{
				UnityEvent unityEvent = this.onOwnerEnter;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
				return;
			}
			else
			{
				UnityEvent unityEvent2 = this.onOwnerExit;
				if (unityEvent2 == null)
				{
					return;
				}
				unityEvent2.Invoke();
				return;
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x001167AC File Offset: 0x001149AC
		private void FixedUpdate()
		{
			ProjectileCallOnOwnerNearby.State state = ProjectileCallOnOwnerNearby.State.Outside;
			if (this.projectileController.owner)
			{
				float num = this.radius * this.radius;
				if ((base.transform.position - this.projectileController.owner.transform.position).sqrMagnitude < num)
				{
					state = ProjectileCallOnOwnerNearby.State.Inside;
				}
			}
			this.SetState(state);
		}

		// Token: 0x04004131 RID: 16689
		public ProjectileCallOnOwnerNearby.Filter filter;

		// Token: 0x04004132 RID: 16690
		public float radius;

		// Token: 0x04004133 RID: 16691
		public UnityEvent onOwnerEnter;

		// Token: 0x04004134 RID: 16692
		public UnityEvent onOwnerExit;

		// Token: 0x04004135 RID: 16693
		private ProjectileCallOnOwnerNearby.State state;

		// Token: 0x04004136 RID: 16694
		private bool ownerInRadius;

		// Token: 0x04004137 RID: 16695
		private ProjectileController projectileController;

		// Token: 0x02000B85 RID: 2949
		[Flags]
		public enum Filter
		{
			// Token: 0x04004139 RID: 16697
			None = 0,
			// Token: 0x0400413A RID: 16698
			Server = 1,
			// Token: 0x0400413B RID: 16699
			Client = 2
		}

		// Token: 0x02000B86 RID: 2950
		private enum State
		{
			// Token: 0x0400413D RID: 16701
			Outside,
			// Token: 0x0400413E RID: 16702
			Inside
		}
	}
}
