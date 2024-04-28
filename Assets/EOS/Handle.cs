using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000004 RID: 4
	public abstract class Handle : IEquatable<Handle>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		public IntPtr InnerHandle { get; internal set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
		public Handle()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002089 File Offset: 0x00000289
		public Handle(IntPtr innerHandle)
		{
			this.InnerHandle = innerHandle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Handle);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020A8 File Offset: 0x000002A8
		public override int GetHashCode()
		{
			return (int)(65536L + this.InnerHandle.ToInt64());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		public bool Equals(Handle other)
		{
			return other != null && (this == other || (!(base.GetType() != other.GetType()) && this.InnerHandle == other.InnerHandle));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020FE File Offset: 0x000002FE
		public static bool operator ==(Handle lhs, Handle rhs)
		{
			if (lhs == null)
			{
				return rhs == null;
			}
			return lhs.Equals(rhs);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002111 File Offset: 0x00000311
		public static bool operator !=(Handle lhs, Handle rhs)
		{
			return !(lhs == rhs);
		}
	}
}
