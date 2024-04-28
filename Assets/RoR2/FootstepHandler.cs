using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006E0 RID: 1760
	[RequireComponent(typeof(ChildLocator))]
	public class FootstepHandler : MonoBehaviour
	{
		// Token: 0x060022C5 RID: 8901 RVA: 0x00096318 File Offset: 0x00094518
		private void Start()
		{
			this.childLocator = base.GetComponent<ChildLocator>();
			if (base.GetComponent<CharacterModel>())
			{
				this.body = base.GetComponent<CharacterModel>().body;
				this.bodyInventory = (this.body ? this.body.inventory : null);
			}
			this.animator = base.GetComponent<Animator>();
			if (this.enableFootstepDust)
			{
				this.footstepDustInstanceTransform = UnityEngine.Object.Instantiate<GameObject>(this.footstepDustPrefab, base.transform).transform;
				this.footstepDustInstanceParticleSystem = this.footstepDustInstanceTransform.GetComponent<ParticleSystem>();
				this.footstepDustInstanceShakeEmitter = this.footstepDustInstanceTransform.GetComponent<ShakeEmitter>();
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000963C4 File Offset: 0x000945C4
		public void Footstep(AnimationEvent animationEvent)
		{
			if ((double)animationEvent.animatorClipInfo.weight > 0.5)
			{
				this.Footstep(animationEvent.stringParameter, (GameObject)animationEvent.objectReferenceParameter);
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x00096404 File Offset: 0x00094604
		public void Footstep(string childName, GameObject footstepEffect)
		{
			if (!this.body)
			{
				return;
			}
			Transform transform = this.childLocator.FindChild(childName);
			int childIndex = this.childLocator.FindChildIndex(childName);
			if (transform)
			{
				Color color = Color.gray;
				RaycastHit raycastHit = default(RaycastHit);
				Vector3 position = transform.position;
				position.y += 1.5f;
				Debug.DrawRay(position, Vector3.down);
				if (Physics.Raycast(new Ray(position, Vector3.down), out raycastHit, 4f, LayerIndex.world.mask | LayerIndex.water.mask, QueryTriggerInteraction.Collide))
				{
					if (this.bodyInventory && this.bodyInventory.GetItemCount(RoR2Content.Items.Hoof) > 0 && childName == "FootR")
					{
						Util.PlaySound("Play_item_proc_hoof", this.body.gameObject);
					}
					if (footstepEffect)
					{
						EffectData effectData = new EffectData();
						effectData.origin = raycastHit.point;
						effectData.rotation = Util.QuaternionSafeLookRotation(raycastHit.normal);
						effectData.SetChildLocatorTransformReference(this.body.gameObject, childIndex);
						EffectManager.SpawnEffect(footstepEffect, effectData, false);
					}
					SurfaceDef objectSurfaceDef = SurfaceDefProvider.GetObjectSurfaceDef(raycastHit.collider, raycastHit.point);
					bool flag = false;
					if (objectSurfaceDef)
					{
						color = objectSurfaceDef.approximateColor;
						if (objectSurfaceDef.footstepEffectPrefab)
						{
							EffectManager.SpawnEffect(objectSurfaceDef.footstepEffectPrefab, new EffectData
							{
								origin = raycastHit.point,
								scale = this.body.radius
							}, false);
							flag = true;
						}
						if (!string.IsNullOrEmpty(objectSurfaceDef.materialSwitchString))
						{
							AkSoundEngine.SetSwitch("material", objectSurfaceDef.materialSwitchString, this.body.gameObject);
						}
					}
					else
					{
						Debug.LogFormat("{0} is missing surface def", new object[]
						{
							raycastHit.collider.gameObject
						});
					}
					if (this.footstepDustInstanceTransform && !flag)
					{
						this.footstepDustInstanceTransform.position = raycastHit.point;
						this.footstepDustInstanceParticleSystem.main.startColor = color;
						this.footstepDustInstanceParticleSystem.Play();
						if (this.footstepDustInstanceShakeEmitter)
						{
							this.footstepDustInstanceShakeEmitter.StartShake();
						}
					}
				}
				Util.PlaySound((!string.IsNullOrEmpty(this.sprintFootstepOverrideString) && this.body.isSprinting) ? this.sprintFootstepOverrideString : this.baseFootstepString, this.body.gameObject);
				return;
			}
			Debug.LogWarningFormat("Object {0} lacks ChildLocator entry \"{1}\" to handle Footstep event!", new object[]
			{
				base.gameObject.name,
				childName
			});
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000966BC File Offset: 0x000948BC
		public void Footlift(AnimationEvent animationEvent)
		{
			if ((double)animationEvent.animatorClipInfo.weight > 0.5)
			{
				this.Footlift();
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000966E9 File Offset: 0x000948E9
		public void Footlift()
		{
			Util.PlaySound((!string.IsNullOrEmpty(this.sprintFootliftOverrideString) && this.body.isSprinting) ? this.sprintFootliftOverrideString : this.baseFootliftString, this.body.gameObject);
		}

		// Token: 0x040027E0 RID: 10208
		public string baseFootstepString;

		// Token: 0x040027E1 RID: 10209
		public string baseFootliftString;

		// Token: 0x040027E2 RID: 10210
		public string sprintFootstepOverrideString;

		// Token: 0x040027E3 RID: 10211
		public string sprintFootliftOverrideString;

		// Token: 0x040027E4 RID: 10212
		public bool enableFootstepDust;

		// Token: 0x040027E5 RID: 10213
		public GameObject footstepDustPrefab;

		// Token: 0x040027E6 RID: 10214
		private ChildLocator childLocator;

		// Token: 0x040027E7 RID: 10215
		private Inventory bodyInventory;

		// Token: 0x040027E8 RID: 10216
		private Animator animator;

		// Token: 0x040027E9 RID: 10217
		private Transform footstepDustInstanceTransform;

		// Token: 0x040027EA RID: 10218
		private ParticleSystem footstepDustInstanceParticleSystem;

		// Token: 0x040027EB RID: 10219
		private ShakeEmitter footstepDustInstanceShakeEmitter;

		// Token: 0x040027EC RID: 10220
		private CharacterBody body;
	}
}
