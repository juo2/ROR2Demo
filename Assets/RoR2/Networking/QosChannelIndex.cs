using System;

namespace RoR2.Networking
{
	// Token: 0x02000C49 RID: 3145
	public struct QosChannelIndex
	{
		// Token: 0x040044C9 RID: 17609
		public int intVal;

		// Token: 0x040044CA RID: 17610
		public static QosChannelIndex defaultReliable = new QosChannelIndex
		{
			intVal = 0
		};

		// Token: 0x040044CB RID: 17611
		public static QosChannelIndex defaultUnreliable = new QosChannelIndex
		{
			intVal = 1
		};

		// Token: 0x040044CC RID: 17612
		public static QosChannelIndex characterTransformUnreliable = new QosChannelIndex
		{
			intVal = 2
		};

		// Token: 0x040044CD RID: 17613
		public static QosChannelIndex time = new QosChannelIndex
		{
			intVal = 3
		};

		// Token: 0x040044CE RID: 17614
		public static QosChannelIndex chat = new QosChannelIndex
		{
			intVal = 4
		};

		// Token: 0x040044CF RID: 17615
		public const int viewAnglesChannel = 5;

		// Token: 0x040044D0 RID: 17616
		public static QosChannelIndex viewAngles = new QosChannelIndex
		{
			intVal = 5
		};

		// Token: 0x040044D1 RID: 17617
		public static QosChannelIndex ping = new QosChannelIndex
		{
			intVal = 6
		};

		// Token: 0x040044D2 RID: 17618
		public static QosChannelIndex effects = new QosChannelIndex
		{
			intVal = 7
		};
	}
}
