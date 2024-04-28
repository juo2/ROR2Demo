using System;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x0200008A RID: 138
[Serializable]
public class SerializableBitArray
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A49D File Offset: 0x0000869D
	public int byteCount
	{
		get
		{
			return this.bytes.Length;
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000A4A7 File Offset: 0x000086A7
	public SerializableBitArray(int length)
	{
		this.bytes = new byte[length + 7 >> 3];
		this.length = length;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000A4C8 File Offset: 0x000086C8
	public SerializableBitArray(SerializableBitArray src)
	{
		if (src.bytes != null)
		{
			this.bytes = new byte[src.bytes.Length];
			src.bytes.CopyTo(this.bytes, 0);
		}
		this.length = src.length;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000A514 File Offset: 0x00008714
	public byte[] GetBytes()
	{
		byte[] array = new byte[this.bytes.Length];
		this.GetBytes(array);
		return array;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000A537 File Offset: 0x00008737
	public void GetBytes([NotNull] byte[] dest)
	{
		Buffer.BlockCopy(this.bytes, 0, dest, 0, this.bytes.Length);
	}

	// Token: 0x1700002A RID: 42
	public bool this[int index]
	{
		get
		{
			int num = index >> 3;
			int num2 = index & 7;
			return ((int)this.bytes[num] & 1 << num2) != 0;
		}
		set
		{
			int num = index >> 3;
			int num2 = index & 7;
			int num3 = (int)this.bytes[num];
			this.bytes[num] = (byte)(value ? (num3 | 1 << num2) : (num3 & ~(byte)(1 << num2)));
		}
	}

	// Token: 0x0400023B RID: 571
	[SerializeField]
	protected readonly byte[] bytes;

	// Token: 0x0400023C RID: 572
	[SerializeField]
	public readonly int length;

	// Token: 0x0400023D RID: 573
	private const int bitMask = 7;
}
