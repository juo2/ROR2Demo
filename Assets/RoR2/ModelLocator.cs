using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x020007B6 RID: 1974
	[DisallowMultipleComponent]
	public class ModelLocator : MonoBehaviour, ILifeBehavior
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000B4750 File Offset: 0x000B2950
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x000B4758 File Offset: 0x000B2958
		public Transform modelTransform
		{
			get
			{
				return this._modelTransform;
			}
			set
			{
				if (this._modelTransform == value)
				{
					return;
				}
				if (this.modelDestructionNotifier != null)
				{
					this.modelDestructionNotifier.subscriber = null;
					UnityEngine.Object.Destroy(this.modelDestructionNotifier);
					this.modelDestructionNotifier = null;
				}
				this._modelTransform = value;
				if (this._modelTransform)
				{
					this.modelDestructionNotifier = this._modelTransform.gameObject.AddComponent<ModelLocator.DestructionNotifier>();
					this.modelDestructionNotifier.subscriber = this;
				}
				Action<Transform> action = this.onModelChanged;
				if (action == null)
				{
					return;
				}
				action(this._modelTransform);
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060029C5 RID: 10693 RVA: 0x000B47E4 File Offset: 0x000B29E4
		// (remove) Token: 0x060029C6 RID: 10694 RVA: 0x000B481C File Offset: 0x000B2A1C
		public event Action<Transform> onModelChanged;

		// Token: 0x060029C7 RID: 10695 RVA: 0x000B4851 File Offset: 0x000B2A51
		private void Awake()
		{
			this.characterMotor = base.GetComponent<CharacterMotor>();
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000B485F File Offset: 0x000B2A5F
		public void Start()
		{
			if (this.modelTransform)
			{
				this.modelParentTransform = this.modelTransform.parent;
				if (!this.dontDetatchFromParent)
				{
					this.modelTransform.parent = null;
				}
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000B4894 File Offset: 0x000B2A94
		private void UpdateModelTransform(float deltaTime)
		{
			if (this.modelTransform && this.modelParentTransform)
			{
				Vector3 position = this.modelParentTransform.position;
				Quaternion quaternion = this.modelParentTransform.rotation;
				this.UpdateTargetNormal();
				this.SmoothNormals(deltaTime);
				quaternion = Quaternion.FromToRotation(Vector3.up, this.currentNormal) * quaternion;
				this.modelTransform.SetPositionAndRotation(position, quaternion);
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000B4904 File Offset: 0x000B2B04
		private void SmoothNormals(float deltaTime)
		{
			this.currentNormal = Vector3.SmoothDamp(this.currentNormal, this.targetNormal, ref this.normalSmoothdampVelocity, this.normalSmoothdampTime, float.PositiveInfinity, deltaTime);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000B4930 File Offset: 0x000B2B30
		private void UpdateTargetNormal()
		{
			if (this.normalizeToFloor && this.characterMotor)
			{
				this.targetNormal = (this.characterMotor.isGrounded ? this.characterMotor.estimatedGroundNormal : Vector3.up);
				this.targetNormal = Vector3.RotateTowards(Vector3.up, this.targetNormal, this.normalMaxAngleDelta * 0.017453292f, float.PositiveInfinity);
				return;
			}
			this.targetNormal = Vector3.up;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000B49AA File Offset: 0x000B2BAA
		public void LateUpdate()
		{
			if (this.autoUpdateModelTransform)
			{
				this.UpdateModelTransform(Time.deltaTime);
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000B49C0 File Offset: 0x000B2BC0
		private void OnDestroy()
		{
			if (this.modelTransform)
			{
				if (this.preserveModel)
				{
					if (!this.noCorpse)
					{
						this.modelTransform.gameObject.AddComponent<Corpse>();
					}
					this.modelTransform = null;
					return;
				}
				UnityEngine.Object.Destroy(this.modelTransform.gameObject);
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000B4A13 File Offset: 0x000B2C13
		public void OnDeathStart()
		{
			if (!this.dontReleaseModelOnDeath)
			{
				this.preserveModel = true;
			}
		}

		// Token: 0x04002D10 RID: 11536
		[SerializeField]
		[Tooltip("The transform of the child gameobject which acts as the model for this entity.")]
		[FormerlySerializedAs("modelTransform")]
		[Header("Cached Model Values")]
		private Transform _modelTransform;

		// Token: 0x04002D11 RID: 11537
		private ModelLocator.DestructionNotifier modelDestructionNotifier;

		// Token: 0x04002D12 RID: 11538
		[Tooltip("The transform of the child gameobject which acts as the base for this entity's model. If provided, this will be detached from the hierarchy and positioned to match this object's position.")]
		public Transform modelBaseTransform;

		// Token: 0x04002D14 RID: 11540
		[Tooltip("Whether or not to update the model transforms automatically.")]
		[Header("Update Properties")]
		public bool autoUpdateModelTransform = true;

		// Token: 0x04002D15 RID: 11541
		[Tooltip("Forces the model to remain in hierarchy, rather that detaching on start. You usually don't want this for anything that moves.")]
		public bool dontDetatchFromParent;

		// Token: 0x04002D16 RID: 11542
		private Transform modelParentTransform;

		// Token: 0x04002D17 RID: 11543
		[Header("Corpse Properties")]
		[Tooltip("Only matters if preserveModel=true. Prevents the addition of a Corpse component to the model when this object is destroyed.")]
		public bool noCorpse;

		// Token: 0x04002D18 RID: 11544
		[Tooltip("If true, ownership of the model will not be relinquished by death.")]
		public bool dontReleaseModelOnDeath;

		// Token: 0x04002D19 RID: 11545
		[Tooltip("Prevents the model from being destroyed when this object is destroyed. This is rarely used, as character death states are usually responsible for snatching the model away and leaving this ModelLocator with nothing to destroy.")]
		public bool preserveModel;

		// Token: 0x04002D1A RID: 11546
		[Header("Normal Properties")]
		[Tooltip("Allows the model to align to the floor.")]
		public bool normalizeToFloor;

		// Token: 0x04002D1B RID: 11547
		public float normalSmoothdampTime = 0.1f;

		// Token: 0x04002D1C RID: 11548
		[Range(0f, 90f)]
		public float normalMaxAngleDelta = 90f;

		// Token: 0x04002D1D RID: 11549
		private Vector3 normalSmoothdampVelocity;

		// Token: 0x04002D1E RID: 11550
		private Vector3 targetNormal = Vector3.up;

		// Token: 0x04002D1F RID: 11551
		private Vector3 currentNormal = Vector3.up;

		// Token: 0x04002D20 RID: 11552
		private CharacterMotor characterMotor;

		// Token: 0x020007B7 RID: 1975
		private class DestructionNotifier : MonoBehaviour
		{
			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000B4A68 File Offset: 0x000B2C68
			// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000B4A5F File Offset: 0x000B2C5F
			public ModelLocator subscriber { private get; set; }

			// Token: 0x060029D2 RID: 10706 RVA: 0x000B4A70 File Offset: 0x000B2C70
			private void OnDestroy()
			{
				if (this.subscriber != null)
				{
					this.subscriber.modelTransform = null;
				}
			}
		}
	}
}
