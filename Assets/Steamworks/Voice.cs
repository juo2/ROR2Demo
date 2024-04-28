using System;
using System.Diagnostics;
using System.IO;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000175 RID: 373
	public class Voice
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00038293 File Offset: 0x00036493
		public uint OptimalSampleRate
		{
			get
			{
				return this.client.native.user.GetVoiceOptimalSampleRate();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000382AA File Offset: 0x000364AA
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x000382B2 File Offset: 0x000364B2
		public bool WantsRecording
		{
			get
			{
				return this._wantsrecording;
			}
			set
			{
				this._wantsrecording = value;
				if (value)
				{
					this.client.native.user.StartVoiceRecording();
					return;
				}
				this.client.native.user.StopVoiceRecording();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000382E9 File Offset: 0x000364E9
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x000382F1 File Offset: 0x000364F1
		public DateTime LastVoiceRecordTime { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000382FC File Offset: 0x000364FC
		public TimeSpan TimeSinceLastVoiceRecord
		{
			get
			{
				return DateTime.Now.Subtract(this.LastVoiceRecordTime);
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003831C File Offset: 0x0003651C
		internal Voice(Client client)
		{
			this.client = client;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00038374 File Offset: 0x00036574
		public unsafe void Update()
		{
			if (this.OnCompressedData == null && this.OnUncompressedData == null)
			{
				return;
			}
			if (this.UpdateTimer.Elapsed.TotalSeconds < 0.10000000149011612)
			{
				return;
			}
			this.UpdateTimer.Reset();
			this.UpdateTimer.Start();
			uint num = 0U;
			uint num2 = 0U;
			VoiceResult voiceResult = this.client.native.user.GetAvailableVoice(out num2, out num, (this.DesiredSampleRate == 0U) ? this.OptimalSampleRate : this.DesiredSampleRate);
			if (voiceResult == VoiceResult.NotRecording || voiceResult == VoiceResult.NotInitialized)
			{
				this.IsRecording = false;
				return;
			}
			byte[] array;
			byte* value;
			if ((array = this.ReadCompressedBuffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			byte[] array2;
			byte* value2;
			if ((array2 = this.ReadUncompressedBuffer) == null || array2.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &array2[0];
			}
			voiceResult = this.client.native.user.GetVoice(this.OnCompressedData != null, (IntPtr)((void*)value), 131072U, out num2, this.OnUncompressedData != null, (IntPtr)((void*)value2), 131072U, out num, (this.DesiredSampleRate == 0U) ? this.OptimalSampleRate : this.DesiredSampleRate);
			array2 = null;
			array = null;
			this.IsRecording = true;
			if (voiceResult == VoiceResult.OK)
			{
				if (this.OnCompressedData != null && num2 > 0U)
				{
					this.OnCompressedData(this.ReadCompressedBuffer, (int)num2);
				}
				if (this.OnUncompressedData != null && num > 0U)
				{
					this.OnUncompressedData(this.ReadUncompressedBuffer, (int)num);
				}
				this.LastVoiceRecordTime = DateTime.Now;
			}
			if (voiceResult == VoiceResult.NotRecording || voiceResult == VoiceResult.NotInitialized)
			{
				this.IsRecording = false;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003850F File Offset: 0x0003670F
		public bool Decompress(byte[] input, MemoryStream output, uint samepleRate = 0U)
		{
			return this.Decompress(input, 0, input.Length, output, samepleRate);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003851E File Offset: 0x0003671E
		public bool Decompress(byte[] input, int inputsize, MemoryStream output, uint samepleRate = 0U)
		{
			return this.Decompress(input, 0, inputsize, output, samepleRate);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003852C File Offset: 0x0003672C
		public unsafe bool Decompress(byte[] input, int inputoffset, int inputsize, MemoryStream output, uint samepleRate = 0U)
		{
			if (inputoffset < 0 || inputoffset >= input.Length)
			{
				throw new ArgumentOutOfRangeException("inputoffset");
			}
			if (inputsize <= 0 || inputoffset + inputsize > input.Length)
			{
				throw new ArgumentOutOfRangeException("inputsize");
			}
			byte* value;
			if (input == null || input.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &input[0];
			}
			return this.Decompress((IntPtr)((void*)value), inputoffset, inputsize, output, samepleRate);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00038590 File Offset: 0x00036790
		private unsafe bool Decompress(IntPtr input, int inputoffset, int inputsize, MemoryStream output, uint samepleRate = 0U)
		{
			if (samepleRate == 0U)
			{
				samepleRate = this.OptimalSampleRate;
			}
			uint count = 0U;
			byte[] array;
			byte* value;
			if ((array = this.UncompressBuffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			bool flag = this.client.native.user.DecompressVoice((IntPtr)((void*)((byte*)((void*)input) + inputoffset)), (uint)inputsize, (IntPtr)((void*)value), (uint)this.UncompressBuffer.Length, out count, samepleRate) != VoiceResult.OK;
			array = null;
			if (!flag)
			{
				output.Write(this.UncompressBuffer, 0, (int)count);
				return true;
			}
			return false;
		}

		// Token: 0x04000844 RID: 2116
		private const int ReadBufferSize = 131072;

		// Token: 0x04000845 RID: 2117
		internal Client client;

		// Token: 0x04000846 RID: 2118
		internal byte[] ReadCompressedBuffer = new byte[131072];

		// Token: 0x04000847 RID: 2119
		internal byte[] ReadUncompressedBuffer = new byte[131072];

		// Token: 0x04000848 RID: 2120
		internal byte[] UncompressBuffer = new byte[262144];

		// Token: 0x04000849 RID: 2121
		public Action<byte[], int> OnCompressedData;

		// Token: 0x0400084A RID: 2122
		public Action<byte[], int> OnUncompressedData;

		// Token: 0x0400084B RID: 2123
		private Stopwatch UpdateTimer = Stopwatch.StartNew();

		// Token: 0x0400084C RID: 2124
		private bool _wantsrecording;

		// Token: 0x0400084E RID: 2126
		public bool IsRecording;

		// Token: 0x0400084F RID: 2127
		public uint DesiredSampleRate;
	}
}
