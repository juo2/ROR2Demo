using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200066B RID: 1643
	public class CombineMesh : MonoBehaviour
	{
		// Token: 0x06002002 RID: 8194 RVA: 0x00089CC8 File Offset: 0x00087EC8
		private void Start()
		{
			Renderer component = base.GetComponent<Renderer>();
			MeshFilter[] componentsInChildren = base.GetComponentsInChildren<MeshFilter>();
			CombineInstance[] array = new CombineInstance[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				array[i].mesh = componentsInChildren[i].sharedMesh;
				array[i].transform = componentsInChildren[i].transform.localToWorldMatrix;
				componentsInChildren[i].gameObject.SetActive(false);
			}
			MeshFilter meshFilter = base.gameObject.AddComponent<MeshFilter>();
			meshFilter.mesh = new Mesh();
			meshFilter.mesh.CombineMeshes(array, true, true, true);
			component.material = componentsInChildren[0].GetComponent<Renderer>().sharedMaterial;
			base.gameObject.SetActive(true);
		}
	}
}
