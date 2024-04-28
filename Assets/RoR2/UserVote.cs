using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008FA RID: 2298
	[Serializable]
	public struct UserVote : IEquatable<UserVote>
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000DAAE1 File Offset: 0x000D8CE1
		public bool receivedVote
		{
			get
			{
				return this.voteChoiceIndex >= 0;
			}
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000DAAEF File Offset: 0x000D8CEF
		public bool Equals(UserVote other)
		{
			return other.networkUserObject.Equals(this.networkUserObject) && other.voteChoiceIndex.Equals(this.voteChoiceIndex);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000DAB18 File Offset: 0x000D8D18
		public override bool Equals(object obj)
		{
			UserVote? userVote;
			return obj is UserVote? && userVote.GetValueOrDefault().Equals(this);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000DAB51 File Offset: 0x000D8D51
		public override int GetHashCode()
		{
			return (-555733029 * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(this.networkUserObject)) * -1521134295 + this.voteChoiceIndex.GetHashCode();
		}

		// Token: 0x040034D9 RID: 13529
		public GameObject networkUserObject;

		// Token: 0x040034DA RID: 13530
		public int voteChoiceIndex;
	}
}
