using System;

namespace RoR2
{
	// Token: 0x020009C2 RID: 2498
	public struct LoadUserProfileOperationResult
	{
		// Token: 0x040038D3 RID: 14547
		public string fileName;

		// Token: 0x040038D4 RID: 14548
		public long fileLength;

		// Token: 0x040038D5 RID: 14549
		public UserProfile userProfile;

		// Token: 0x040038D6 RID: 14550
		public Exception exception;

		// Token: 0x040038D7 RID: 14551
		public string failureContents;
	}
}
