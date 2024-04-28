using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000690 RID: 1680
	public class DebugOverlay : MonoBehaviour
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x0008D5C9 File Offset: 0x0008B7C9
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0008D5D7 File Offset: 0x0008B7D7
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			DebugOverlay.defaultWireMaterial = LegacyResourcesAPI.Load<Material>("Materials/UI/matDebugUI");
			GameObject gameObject = new GameObject("DebugOverlay");
			DebugOverlay.instance = gameObject.AddComponent<DebugOverlay>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0008D602 File Offset: 0x0008B802
		public static DebugOverlay.MeshDrawer GetMeshDrawer()
		{
			return new DebugOverlay.MeshDrawer(DebugOverlay.instance.transform);
		}

		// Token: 0x04002621 RID: 9761
		private static DebugOverlay instance;

		// Token: 0x04002622 RID: 9762
		private new Transform transform;

		// Token: 0x04002623 RID: 9763
		public static Material defaultWireMaterial;

		// Token: 0x02000691 RID: 1681
		public class MeshDrawer : IDisposable
		{
			// Token: 0x17000296 RID: 662
			// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0008D613 File Offset: 0x0008B813
			// (set) Token: 0x060020DA RID: 8410 RVA: 0x0008D61B File Offset: 0x0008B81B
			public Transform transform { get; private set; }

			// Token: 0x060020DB RID: 8411 RVA: 0x0008D624 File Offset: 0x0008B824
			public MeshDrawer(Transform parent)
			{
				this.gameObject = new GameObject("MeshDrawer");
				this.transform = this.gameObject.transform;
				this.transform.SetParent(parent);
				this.meshFilter = this.gameObject.AddComponent<MeshFilter>();
				this.meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
				this.material = DebugOverlay.defaultWireMaterial;
			}

			// Token: 0x060020DC RID: 8412 RVA: 0x0008D691 File Offset: 0x0008B891
			public void Dispose()
			{
				this.mesh = null;
				UnityEngine.Object.Destroy(this.gameObject);
				this.gameObject = null;
				this.transform = null;
				this.meshFilter = null;
				this.meshRenderer = null;
			}

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x060020DD RID: 8413 RVA: 0x0008D6C1 File Offset: 0x0008B8C1
			// (set) Token: 0x060020DE RID: 8414 RVA: 0x0008D6DD File Offset: 0x0008B8DD
			public bool enabled
			{
				get
				{
					return this.gameObject && this.gameObject.activeSelf;
				}
				set
				{
					if (this.gameObject)
					{
						this.gameObject.SetActive(value);
					}
				}
			}

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x060020DF RID: 8415 RVA: 0x0008D6F8 File Offset: 0x0008B8F8
			// (set) Token: 0x060020E0 RID: 8416 RVA: 0x0008D708 File Offset: 0x0008B908
			public Mesh mesh
			{
				get
				{
					return this.meshFilter.sharedMesh;
				}
				set
				{
					if (this.meshFilter.sharedMesh == value)
					{
						return;
					}
					if (this.meshFilter.sharedMesh && this.hasMeshOwnership)
					{
						UnityEngine.Object.Destroy(this.meshFilter.sharedMesh);
					}
					this.meshFilter.sharedMesh = value;
				}
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0008D75F File Offset: 0x0008B95F
			// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0008D76C File Offset: 0x0008B96C
			public Material material
			{
				get
				{
					return this.meshRenderer.sharedMaterial;
				}
				set
				{
					this.meshRenderer.sharedMaterial = value;
				}
			}

			// Token: 0x04002624 RID: 9764
			private GameObject gameObject;

			// Token: 0x04002626 RID: 9766
			private MeshFilter meshFilter;

			// Token: 0x04002627 RID: 9767
			private MeshRenderer meshRenderer;

			// Token: 0x04002628 RID: 9768
			public bool hasMeshOwnership;
		}
	}
}
