using System;
using System.Runtime.InteropServices;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000009 RID: 9
public class MaulingRock : NetworkBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x0000286E File Offset: 0x00000A6E
	public override void OnStartServer()
	{
		base.OnStartServer();
		this.Networkscale = UnityEngine.Random.Range(this.scaleVarianceLow, this.scaleVarianceHigh);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000288D File Offset: 0x00000A8D
	private void Start()
	{
		this.myHealthComponent = base.GetComponent<HealthComponent>();
		base.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000028C0 File Offset: 0x00000AC0
	private void FixedUpdate()
	{
		if (NetworkServer.active && this.myHealthComponent != null && !this.myHealthComponent.alive)
		{
			if (this.deathEffect != null)
			{
				EffectManager.SpawnEffect(this.deathEffect, new EffectData
				{
					origin = base.transform.position,
					scale = this.blastRadius
				}, true);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000026ED File Offset: 0x000008ED
	private void UNetVersion()
	{
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600001D RID: 29 RVA: 0x00002978 File Offset: 0x00000B78
	// (set) Token: 0x0600001E RID: 30 RVA: 0x0000298B File Offset: 0x00000B8B
	public float Networkscale
	{
		get
		{
			return this.scale;
		}
		[param: In]
		set
		{
			base.SetSyncVar<float>(value, ref this.scale, 1U);
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000029A0 File Offset: 0x00000BA0
	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		if (forceAll)
		{
			writer.Write(this.scale);
			return true;
		}
		bool flag = false;
		if ((base.syncVarDirtyBits & 1U) != 0U)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			writer.Write(this.scale);
		}
		if (!flag)
		{
			writer.WritePackedUInt32(base.syncVarDirtyBits);
		}
		return flag;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002A0C File Offset: 0x00000C0C
	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (initialState)
		{
			this.scale = reader.ReadSingle();
			return;
		}
		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			this.scale = reader.ReadSingle();
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000026ED File Offset: 0x000008ED
	public override void PreStartClient()
	{
	}

	// Token: 0x04000023 RID: 35
	private HealthComponent myHealthComponent;

	// Token: 0x04000024 RID: 36
	public GameObject deathEffect;

	// Token: 0x04000025 RID: 37
	public float blastRadius = 1f;

	// Token: 0x04000026 RID: 38
	public float damage = 10f;

	// Token: 0x04000027 RID: 39
	public float damageCoefficient = 1f;

	// Token: 0x04000028 RID: 40
	public float scaleVarianceLow = 0.8f;

	// Token: 0x04000029 RID: 41
	public float scaleVarianceHigh = 1.2f;

	// Token: 0x0400002A RID: 42
	public float verticalOffset;

	// Token: 0x0400002B RID: 43
	[SyncVar]
	private float scale;
}
