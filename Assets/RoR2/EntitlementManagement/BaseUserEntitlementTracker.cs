using System;
using System.Collections.Generic;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C89 RID: 3209
	public abstract class BaseUserEntitlementTracker<TUser> : IDisposable where TUser : class
	{
		// Token: 0x06004967 RID: 18791
		protected abstract void SubscribeToUserDiscovered();

		// Token: 0x06004968 RID: 18792
		protected abstract void SubscribeToUserLost();

		// Token: 0x06004969 RID: 18793
		protected abstract void UnsubscribeFromUserDiscovered();

		// Token: 0x0600496A RID: 18794
		protected abstract void UnsubscribeFromUserLost();

		// Token: 0x0600496B RID: 18795
		protected abstract IList<TUser> GetCurrentUsers();

		// Token: 0x0600496C RID: 18796 RVA: 0x0012E198 File Offset: 0x0012C398
		public BaseUserEntitlementTracker(IUserEntitlementResolver<TUser>[] entitlementResolvers)
		{
			this.entitlementResolvers = ArrayUtils.Clone<IUserEntitlementResolver<TUser>>(entitlementResolvers);
			this.SubscribeToUserDiscovered();
			this.SubscribeToUserLost();
			for (int i = 0; i < entitlementResolvers.Length; i++)
			{
				entitlementResolvers[i].onEntitlementsChanged += this.UpdateAllUserEntitlements;
			}
			IList<TUser> currentUsers = this.GetCurrentUsers();
			for (int j = 0; j < currentUsers.Count; j++)
			{
				this.OnUserDiscovered(currentUsers[j]);
			}
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x0012E218 File Offset: 0x0012C418
		public virtual void Dispose()
		{
			this.disposed = true;
			for (int i = this.entitlementResolvers.Length - 1; i >= 0; i--)
			{
				this.entitlementResolvers[i].onEntitlementsChanged -= this.UpdateAllUserEntitlements;
			}
			this.UnsubscribeFromUserLost();
			this.UnsubscribeFromUserDiscovered();
			IList<TUser> currentUsers = this.GetCurrentUsers();
			for (int j = 0; j < currentUsers.Count; j++)
			{
				this.OnUserLost(currentUsers[j]);
			}
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0012E28C File Offset: 0x0012C48C
		protected virtual void OnUserDiscovered(TUser user)
		{
			try
			{
				List<EntitlementDef> value = new List<EntitlementDef>();
				this.userToEntitlements.Add(user, value);
				this.UpdateUserEntitlements(user);
			}
			catch (Exception message)
			{
				BaseUserEntitlementTracker<TUser>.LogError(message);
			}
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0012E2CC File Offset: 0x0012C4CC
		protected virtual void OnUserLost(TUser user)
		{
			try
			{
				this.userToEntitlements.Remove(user);
			}
			catch (Exception message)
			{
				BaseUserEntitlementTracker<TUser>.LogError(message);
			}
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x0012E300 File Offset: 0x0012C500
		public void UpdateAllUserEntitlements()
		{
			IList<TUser> currentUsers = this.GetCurrentUsers();
			for (int i = 0; i < currentUsers.Count; i++)
			{
				this.UpdateUserEntitlements(currentUsers[i]);
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x0012E334 File Offset: 0x0012C534
		protected void UpdateUserEntitlements(TUser user)
		{
			List<EntitlementDef> list;
			if (!this.userToEntitlements.TryGetValue(user, out list))
			{
				return;
			}
			list.Clear();
			foreach (IUserEntitlementResolver<TUser> userEntitlementResolver in this.entitlementResolvers)
			{
				foreach (EntitlementDef entitlementDef in EntitlementCatalog.entitlementDefs)
				{
					if (!list.Contains(entitlementDef) && userEntitlementResolver.CheckUserHasEntitlement(user, entitlementDef))
					{
						list.Add(entitlementDef);
					}
				}
			}
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0012E3D4 File Offset: 0x0012C5D4
		public bool UserHasEntitlement([NotNull] TUser user, [NotNull] EntitlementDef entitlementDef)
		{
			if (!entitlementDef)
			{
				throw new ArgumentNullException("entitlementDef");
			}
			List<EntitlementDef> list;
			return user != null && this.userToEntitlements.TryGetValue(user, out list) && list.Contains(entitlementDef);
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0012E418 File Offset: 0x0012C618
		public bool AnyUserHasEntitlement([NotNull] EntitlementDef entitlementDef)
		{
			IList<TUser> currentUsers = this.GetCurrentUsers();
			for (int i = 0; i < currentUsers.Count; i++)
			{
				if (this.UserHasEntitlement(currentUsers[i], entitlementDef))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0012E450 File Offset: 0x0012C650
		protected static void LogError(object message)
		{
			RoR2Application.onNextUpdate += delegate()
			{
				Debug.LogError(message);
			};
		}

		// Token: 0x04004636 RID: 17974
		private Dictionary<TUser, List<EntitlementDef>> userToEntitlements = new Dictionary<TUser, List<EntitlementDef>>();

		// Token: 0x04004637 RID: 17975
		private IUserEntitlementResolver<TUser>[] entitlementResolvers;

		// Token: 0x04004638 RID: 17976
		private bool disposed;
	}
}
