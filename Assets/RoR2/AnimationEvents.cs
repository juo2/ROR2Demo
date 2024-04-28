using System;
using System.Collections;
using Generics.Dynamics;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005CA RID: 1482
	public class AnimationEvents : MonoBehaviour
	{
		// Token: 0x06001ACD RID: 6861 RVA: 0x00073104 File Offset: 0x00071304
		private void Start()
		{
			this.childLocator = base.GetComponent<ChildLocator>();
			this.entityLocator = base.GetComponent<EntityLocator>();
			this.meshRenderer = base.GetComponentInChildren<Renderer>();
			this.characterModel = base.GetComponent<CharacterModel>();
			if (this.characterModel && this.characterModel.body)
			{
				this.bodyObject = this.characterModel.body.gameObject;
				this.modelLocator = this.bodyObject.GetComponent<ModelLocator>();
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00073188 File Offset: 0x00071388
		public void UpdateIKState(AnimationEvent animationEvent)
		{
			IIKTargetBehavior component = this.childLocator.FindChild(animationEvent.stringParameter).GetComponent<IIKTargetBehavior>();
			if (component != null)
			{
				component.UpdateIKState(animationEvent.intParameter);
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000731BB File Offset: 0x000713BB
		public void PlaySound(string soundString)
		{
			Util.PlaySound(soundString, this.soundCenter ? this.soundCenter : this.bodyObject);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000731DF File Offset: 0x000713DF
		public void NormalizeToFloor()
		{
			if (this.modelLocator)
			{
				this.modelLocator.normalizeToFloor = true;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000731FC File Offset: 0x000713FC
		public void SetIK(AnimationEvent animationEvent)
		{
			if (this.modelLocator && this.modelLocator.modelTransform)
			{
				bool intParameter = animationEvent.intParameter != 0;
				InverseKinematics component = this.modelLocator.modelTransform.GetComponent<InverseKinematics>();
				StriderLegController component2 = this.modelLocator.modelTransform.GetComponent<StriderLegController>();
				if (component)
				{
					component.enabled = intParameter;
				}
				if (component2)
				{
					component2.enabled = intParameter;
				}
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00073278 File Offset: 0x00071478
		public void RefreshAllIK()
		{
			foreach (IKTargetPassive iktargetPassive in base.GetComponentsInChildren<IKTargetPassive>())
			{
				iktargetPassive.UpdateIKTargetPosition();
				iktargetPassive.UpdateYOffset();
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000732A8 File Offset: 0x000714A8
		public void CreateEffect(AnimationEvent animationEvent)
		{
			Transform transform = base.transform;
			int num = -1;
			if (!string.IsNullOrEmpty(animationEvent.stringParameter))
			{
				num = this.childLocator.FindChildIndex(animationEvent.stringParameter);
				if (num != -1)
				{
					transform = this.childLocator.FindChild(num);
				}
			}
			bool transmit = animationEvent.intParameter != 0;
			EffectData effectData = new EffectData();
			effectData.origin = transform.position;
			effectData.SetChildLocatorTransformReference(this.bodyObject, num);
			EffectManager.SpawnEffect((GameObject)animationEvent.objectReferenceParameter, effectData, transmit);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0007332C File Offset: 0x0007152C
		public void CreatePrefab(AnimationEvent animationEvent)
		{
			GameObject gameObject = (GameObject)animationEvent.objectReferenceParameter;
			string stringParameter = animationEvent.stringParameter;
			int intParameter = animationEvent.intParameter;
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild(stringParameter);
				if (transform)
				{
					if (intParameter == 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
						return;
					}
					UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, transform.rotation).transform.parent = transform;
					return;
				}
				else if (gameObject)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform.position, base.transform.rotation);
					return;
				}
			}
			else if (gameObject)
			{
				UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform.position, base.transform.rotation);
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000733F4 File Offset: 0x000715F4
		public void ItemDrop()
		{
			if (NetworkServer.active && this.entityLocator)
			{
				IChestBehavior component = this.entityLocator.entity.GetComponent<IChestBehavior>();
				if ((Component)component)
				{
					component.ItemDrop();
					return;
				}
				Debug.Log("Parent has no item drops!");
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00073444 File Offset: 0x00071644
		public void BeginPrint(AnimationEvent animationEvent)
		{
			if (this.meshRenderer)
			{
				Material material = (Material)animationEvent.objectReferenceParameter;
				float floatParameter = animationEvent.floatParameter;
				float maxPrintHeight = (float)animationEvent.intParameter;
				this.meshRenderer.material = material;
				this.printTime = 0f;
				MaterialPropertyBlock printPropertyBlock = new MaterialPropertyBlock();
				base.StartCoroutine(this.startPrint(floatParameter, maxPrintHeight, printPropertyBlock));
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000734A6 File Offset: 0x000716A6
		private IEnumerator startPrint(float maxPrintTime, float maxPrintHeight, MaterialPropertyBlock printPropertyBlock)
		{
			if (this.meshRenderer)
			{
				while (this.printHeight < maxPrintHeight)
				{
					this.printTime += Time.deltaTime;
					this.printHeight = this.printTime / maxPrintTime * maxPrintHeight;
					this.meshRenderer.GetPropertyBlock(printPropertyBlock);
					printPropertyBlock.Clear();
					printPropertyBlock.SetFloat("_SliceHeight", this.printHeight);
					this.meshRenderer.SetPropertyBlock(printPropertyBlock);
					yield return new WaitForEndOfFrame();
				}
			}
			yield break;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000734CC File Offset: 0x000716CC
		public void SetChildEnable(AnimationEvent animationEvent)
		{
			string stringParameter = animationEvent.stringParameter;
			bool active = animationEvent.intParameter > 0;
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild(stringParameter);
				if (transform)
				{
					transform.gameObject.SetActive(active);
				}
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0007351C File Offset: 0x0007171C
		public void SwapMaterial(AnimationEvent animationEvent)
		{
			Material material = (Material)animationEvent.objectReferenceParameter;
			if (this.meshRenderer)
			{
				this.meshRenderer.material = material;
			}
		}

		// Token: 0x040020FD RID: 8445
		public GameObject soundCenter;

		// Token: 0x040020FE RID: 8446
		private GameObject bodyObject;

		// Token: 0x040020FF RID: 8447
		private CharacterModel characterModel;

		// Token: 0x04002100 RID: 8448
		private ChildLocator childLocator;

		// Token: 0x04002101 RID: 8449
		private EntityLocator entityLocator;

		// Token: 0x04002102 RID: 8450
		private Renderer meshRenderer;

		// Token: 0x04002103 RID: 8451
		private ModelLocator modelLocator;

		// Token: 0x04002104 RID: 8452
		private float printHeight;

		// Token: 0x04002105 RID: 8453
		private float printTime;
	}
}
