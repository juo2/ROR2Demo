using System;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008FB RID: 2299
	public class SyncListUserVote : SyncListStruct<UserVote>
	{
		// Token: 0x060033EA RID: 13290 RVA: 0x000DAB89 File Offset: 0x000D8D89
		public override void SerializeItem(NetworkWriter writer, UserVote item)
		{
			writer.Write(item.networkUserObject);
			writer.WritePackedUInt32((uint)item.voteChoiceIndex);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000DABA4 File Offset: 0x000D8DA4
		public override UserVote DeserializeItem(NetworkReader reader)
		{
			return new UserVote
			{
				networkUserObject = reader.ReadGameObject(),
				voteChoiceIndex = (int)reader.ReadPackedUInt32()
			};
		}
	}
}
