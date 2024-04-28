using System;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006B8 RID: 1720
	[DisallowMultipleComponent]
	public class EffectComponent : MonoBehaviour
	{
		// Token: 0x0600217E RID: 8574 RVA: 0x0009041C File Offset: 0x0008E61C
		private void Start()
		{
			if (this.effectData == null)
			{
				Debug.LogErrorFormat(base.gameObject, "Object {0} should not be instantiated by means other than EffectManager.SpawnEffect. This WILL result in an NRE!!! Use EffectManager.SpawnEffect or don't use EffectComponent!!!!!", new object[]
				{
					base.gameObject
				});
			}
			Transform transform = null;
			if ((this.positionAtReferencedTransform | this.parentToReferencedTransform) && this.effectData != null)
			{
				transform = this.effectData.ResolveChildLocatorTransformReference();
			}
			if (transform)
			{
				if (this.positionAtReferencedTransform)
				{
					base.transform.position = transform.position;
					base.transform.rotation = transform.rotation;
				}
				if (this.parentToReferencedTransform)
				{
					base.transform.SetParent(transform, true);
				}
			}
			if (this.applyScale && this.effectData != null)
			{
				float scale = this.effectData.scale;
				if (!this.disregardZScale)
				{
					base.transform.localScale = new Vector3(scale, scale, scale);
					return;
				}
				base.transform.localScale = new Vector3(scale, scale, base.transform.localScale.z);
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x00090518 File Offset: 0x0008E718
		[ContextMenu("Attempt to Upgrade Sfx Setup")]
		private void AttemptToUpgradeSfxSetup()
		{
			AkEvent[] components = base.GetComponents<AkEvent>();
			int num = 1281810935;
			if (components.Length == 1 && components[0].triggerList.Count == 1 && components[0].triggerList[0] == num)
			{
				string objectName = components[0].data.WwiseObjectReference.ObjectName;
				this.soundName = objectName;
				UnityEngine.Object.DestroyImmediate(components[0]);
				UnityEngine.Object.DestroyImmediate(base.GetComponent<AkGameObj>());
				Rigidbody component = base.GetComponent<Rigidbody>();
				if (component.isKinematic)
				{
					UnityEngine.Object.DestroyImmediate(component);
				}
				EditorUtil.SetDirty(this);
				EditorUtil.SetDirty(base.gameObject);
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000905B4 File Offset: 0x0008E7B4
		private void OnValidate()
		{
			if (!Application.isPlaying && this.effectIndex != EffectIndex.Invalid)
			{
				this.effectIndex = EffectIndex.Invalid;
			}
			if (!Application.isPlaying)
			{
				bool flag = base.GetComponent<AkGameObj>();
				bool flag2 = base.GetComponent<OrbEffect>();
				if (flag && !flag2)
				{
					AkEvent[] components = base.GetComponents<AkEvent>();
					int item = 1281810935;
					if (components.Length == 1 && components[0].triggerList.Contains(item))
					{
						Debug.LogWarningFormat(base.gameObject, "Effect {0} has an attached AkGameObj. You probably want to use the soundName field of its EffectComponent instead.", new object[]
						{
							Util.GetGameObjectHierarchyName(base.gameObject)
						});
					}
				}
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00090642 File Offset: 0x0008E842
		public GameObject GetReferencedObject()
		{
			if (!this.didResolveReferencedObject)
			{
				this.referencedObject = this.effectData.ResolveNetworkedObjectReference();
				this.didResolveReferencedObject = true;
			}
			return this.referencedObject;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0009066A File Offset: 0x0008E86A
		public Transform GetReferencedChildTransform()
		{
			if (!this.didResolveReferencedChildTransform)
			{
				this.referencedChildTransform = this.effectData.ResolveChildLocatorTransformReference();
				this.didResolveReferencedChildTransform = true;
			}
			return this.referencedChildTransform;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00090692 File Offset: 0x0008E892
		public GameObject GetReferencedHurtBoxGameObject()
		{
			if (!this.didResolveReferencedHurtBox)
			{
				this.referencedHurtBoxGameObject = this.effectData.ResolveHurtBoxReference();
				this.didResolveReferencedHurtBox = true;
			}
			return this.referencedHurtBoxGameObject;
		}

		// Token: 0x040026E0 RID: 9952
		[Tooltip("This is assigned to the prefab automatically by EffectCatalog at runtime. Do not set this value manually.")]
		[HideInInspector]
		public EffectIndex effectIndex = EffectIndex.Invalid;

		// Token: 0x040026E1 RID: 9953
		[NonSerialized]
		public EffectData effectData;

		// Token: 0x040026E2 RID: 9954
		[Tooltip("Positions the effect at the transform referenced by the effect data if available.")]
		public bool positionAtReferencedTransform;

		// Token: 0x040026E3 RID: 9955
		[Tooltip("Parents the effect to the transform object referenced by the effect data if available.")]
		public bool parentToReferencedTransform;

		// Token: 0x040026E4 RID: 9956
		[Tooltip("Causes this object to adopt the scale received in the effectdata.")]
		public bool applyScale;

		// Token: 0x040026E5 RID: 9957
		[Tooltip("The sound to play whenever this effect is dispatched, regardless of whether or not it actually ends up spawning.")]
		public string soundName;

		// Token: 0x040026E6 RID: 9958
		[Tooltip("Ignore Z scale when adopting scale values from effectdata.")]
		public bool disregardZScale;

		// Token: 0x040026E7 RID: 9959
		private bool didResolveReferencedObject;

		// Token: 0x040026E8 RID: 9960
		private GameObject referencedObject;

		// Token: 0x040026E9 RID: 9961
		private bool didResolveReferencedChildTransform;

		// Token: 0x040026EA RID: 9962
		private Transform referencedChildTransform;

		// Token: 0x040026EB RID: 9963
		private bool didResolveReferencedHurtBox;

		// Token: 0x040026EC RID: 9964
		private GameObject referencedHurtBoxGameObject;
	}
}
