using System;
using System.Collections.Generic;
using Zio;

namespace RoR2
{
	// Token: 0x020009C1 RID: 2497
	public struct FileReference : IEquatable<FileReference>
	{
		// Token: 0x06003917 RID: 14615 RVA: 0x000EE6EE File Offset: 0x000EC8EE
		public bool Equals(FileReference other)
		{
			return this.fileSystem.Equals(other.fileSystem) && this.path.Equals(other.path);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000EE716 File Offset: 0x000EC916
		public override bool Equals(object other)
		{
			return other is FileReference && this.Equals((FileReference)other);
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000EE72E File Offset: 0x000EC92E
		public override int GetHashCode()
		{
			return (-990633296 * -1521134295 + EqualityComparer<IFileSystem>.Default.GetHashCode(this.fileSystem)) * -1521134295 + EqualityComparer<UPath>.Default.GetHashCode(this.path);
		}

		// Token: 0x040038D1 RID: 14545
		public IFileSystem fileSystem;

		// Token: 0x040038D2 RID: 14546
		public UPath path;
	}
}
