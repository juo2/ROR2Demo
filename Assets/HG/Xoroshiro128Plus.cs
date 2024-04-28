using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000005 RID: 5
public class Xoroshiro128Plus
{
	// Token: 0x06000005 RID: 5 RVA: 0x000020A9 File Offset: 0x000002A9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ulong RotateLeft(ulong x, int k)
	{
		return x << k | x >> 64 - k;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020BB File Offset: 0x000002BB
	public Xoroshiro128Plus(ulong seed)
	{
		this.ResetSeed(seed);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020CA File Offset: 0x000002CA
	public Xoroshiro128Plus(Xoroshiro128Plus other)
	{
		this.state0 = other.state0;
		this.state1 = other.state1;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020EA File Offset: 0x000002EA
	public void ResetSeed(ulong seed)
	{
		Xoroshiro128Plus.initializer.x = seed;
		this.state0 = Xoroshiro128Plus.initializer.Next();
		this.state1 = Xoroshiro128Plus.initializer.Next();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
	public ulong Next()
	{
		ulong num = this.state0;
		ulong num2 = this.state1;
		ulong result = num + num2;
		num2 ^= num;
		this.state0 = (Xoroshiro128Plus.RotateLeft(num, 24) ^ num2 ^ num2 << 16);
		this.state1 = Xoroshiro128Plus.RotateLeft(num2, 37);
		return result;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
	public void Jump()
	{
		ulong num = 0UL;
		ulong num2 = 0UL;
		for (int i = 0; i < Xoroshiro128Plus.JUMP.Length; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				if ((Xoroshiro128Plus.JUMP[i] & 1UL) << j != 0UL)
				{
					num ^= this.state0;
					num2 ^= this.state1;
				}
				this.Next();
			}
		}
		this.state0 = num;
		this.state1 = num2;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
	public void LongJump()
	{
		ulong num = 0UL;
		ulong num2 = 0UL;
		for (int i = 0; i < Xoroshiro128Plus.LONG_JUMP.Length; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				if ((Xoroshiro128Plus.LONG_JUMP[i] & 1UL) << j != 0UL)
				{
					num ^= this.state0;
					num2 ^= this.state1;
				}
				this.Next();
			}
		}
		this.state0 = num;
		this.state1 = num2;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002236 File Offset: 0x00000436
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static double ToDouble01Fast(ulong x)
	{
		return BitConverter.Int64BitsToDouble((long)(4607182418800017408UL | x >> 12));
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000224B File Offset: 0x0000044B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static double ToDouble01(ulong x)
	{
		return (x >> 11) * 1.1102230246251565E-16;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000225D File Offset: 0x0000045D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float ToFloat01(uint x)
	{
		return (x >> 8) * 5.9604645E-08f;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000F RID: 15 RVA: 0x0000226A File Offset: 0x0000046A
	public bool nextBool
	{
		get
		{
			return this.nextLong < 0L;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002276 File Offset: 0x00000476
	public uint nextUint
	{
		get
		{
			return (uint)this.Next();
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000011 RID: 17 RVA: 0x0000227F File Offset: 0x0000047F
	public int nextInt
	{
		get
		{
			return (int)this.Next();
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002288 File Offset: 0x00000488
	public ulong nextUlong
	{
		get
		{
			return this.Next();
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000013 RID: 19 RVA: 0x00002288 File Offset: 0x00000488
	public long nextLong
	{
		get
		{
			return (long)this.Next();
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000014 RID: 20 RVA: 0x00002290 File Offset: 0x00000490
	public double nextNormalizedDouble
	{
		get
		{
			return Xoroshiro128Plus.ToDouble01Fast(this.Next());
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000015 RID: 21 RVA: 0x0000229D File Offset: 0x0000049D
	public float nextNormalizedFloat
	{
		get
		{
			return Xoroshiro128Plus.ToFloat01(this.nextUint);
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000022AA File Offset: 0x000004AA
	public float RangeFloat(float minInclusive, float maxInclusive)
	{
		return minInclusive + (maxInclusive - minInclusive) * this.nextNormalizedFloat;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000022B8 File Offset: 0x000004B8
	public int RangeInt(int minInclusive, int maxExclusive)
	{
		return minInclusive + (int)this.RangeUInt32Uniform((uint)(maxExclusive - minInclusive));
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000022C5 File Offset: 0x000004C5
	public long RangeLong(long minInclusive, long maxExclusive)
	{
		return minInclusive + (long)this.RangeUInt64Uniform((ulong)(maxExclusive - minInclusive));
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000022D4 File Offset: 0x000004D4
	private ulong RangeUInt64Uniform(ulong maxExclusive)
	{
		if (maxExclusive == 0UL)
		{
			throw new ArgumentOutOfRangeException("Range cannot have size of zero.");
		}
		int num = Xoroshiro128Plus.CalcRequiredBits(maxExclusive);
		int num2 = 64 - num;
		ulong num3;
		do
		{
			num3 = this.nextUlong >> num2;
		}
		while (num3 >= maxExclusive);
		return num3;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000230C File Offset: 0x0000050C
	private uint RangeUInt32Uniform(uint maxExclusive)
	{
		if (maxExclusive == 0U)
		{
			throw new ArgumentOutOfRangeException("Range cannot have size of zero.");
		}
		int num = Xoroshiro128Plus.CalcRequiredBits(maxExclusive);
		int num2 = 32 - num;
		uint num3;
		do
		{
			num3 = this.nextUint >> num2;
		}
		while (num3 >= maxExclusive);
		return num3;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002344 File Offset: 0x00000544
	private static int[] GenerateLogTable()
	{
		int[] array = new int[256];
		array[0] = (array[1] = 0);
		for (int i = 2; i < 256; i++)
		{
			array[i] = 1 + array[i / 2];
		}
		array[0] = -1;
		return array;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002388 File Offset: 0x00000588
	private static int CalcRequiredBits(ulong v)
	{
		int num = 0;
		while (v != 0UL)
		{
			v >>= 1;
			num++;
		}
		return num;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000023A8 File Offset: 0x000005A8
	private static int CalcRequiredBits(uint v)
	{
		int num = 0;
		while (v != 0U)
		{
			v >>= 1;
			num++;
		}
		return num;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000023C6 File Offset: 0x000005C6
	public ref T NextElementUniform<T>(T[] array)
	{
		return ref array[this.RangeInt(0, array.Length)];
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000023D8 File Offset: 0x000005D8
	public T NextElementUniform<T>(List<T> list)
	{
		return list[this.RangeInt(0, list.Count)];
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000023ED File Offset: 0x000005ED
	public T NextElementUniform<T>(IList<T> list)
	{
		return list[this.RangeInt(0, list.Count)];
	}

	// Token: 0x04000002 RID: 2
	private ulong state0;

	// Token: 0x04000003 RID: 3
	private ulong state1;

	// Token: 0x04000004 RID: 4
	private static readonly SplitMix64 initializer = new SplitMix64();

	// Token: 0x04000005 RID: 5
	private const ulong JUMP0 = 16109378705422636197UL;

	// Token: 0x04000006 RID: 6
	private const ulong JUMP1 = 1659688472399708668UL;

	// Token: 0x04000007 RID: 7
	private static readonly ulong[] JUMP = new ulong[]
	{
		16109378705422636197UL,
		1659688472399708668UL
	};

	// Token: 0x04000008 RID: 8
	private const ulong LONG_JUMP0 = 15179817016004374139UL;

	// Token: 0x04000009 RID: 9
	private const ulong LONG_JUMP1 = 15987667697637423809UL;

	// Token: 0x0400000A RID: 10
	private static readonly ulong[] LONG_JUMP = new ulong[]
	{
		15179817016004374139UL,
		15987667697637423809UL
	};

	// Token: 0x0400000B RID: 11
	private static readonly int[] logTable256 = Xoroshiro128Plus.GenerateLogTable();
}
