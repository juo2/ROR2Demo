using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B0 RID: 2224
	[RequireComponent(typeof(VehicleSeat))]
	[RequireComponent(typeof(EntityStateMachine))]
	public sealed class SurvivorPodController : NetworkBehaviour, ICameraStateProvider
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x000D1828 File Offset: 0x000CFA28
		// (set) Token: 0x06003155 RID: 12629 RVA: 0x000D1830 File Offset: 0x000CFA30
		public EntityStateMachine characterStateMachine { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000D1839 File Offset: 0x000CFA39
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000D1841 File Offset: 0x000CFA41
		public VehicleSeat vehicleSeat { get; private set; }

		// Token: 0x06003158 RID: 12632 RVA: 0x000D184C File Offset: 0x000CFA4C
		private void Awake()
		{
			this.stateMachine = base.GetComponent<EntityStateMachine>();
			this.vehicleSeat = base.GetComponent<VehicleSeat>();
			this.vehicleSeat.onPassengerEnter += this.OnPassengerEnter;
			this.vehicleSeat.onPassengerExit += this.OnPassengerExit;
			this.vehicleSeat.enterVehicleAllowedCheck.AddCallback(new CallbackCheck<Interactability, CharacterBody>.CallbackDelegate(this.CheckEnterAllowed));
			this.vehicleSeat.exitVehicleAllowedCheck.AddCallback(new CallbackCheck<Interactability, CharacterBody>.CallbackDelegate(this.CheckExitAllowed));
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000D18D7 File Offset: 0x000CFAD7
		private void OnPassengerEnter(GameObject passenger)
		{
			this.UpdateCameras(passenger);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000D18E0 File Offset: 0x000CFAE0
		private void OnPassengerExit(GameObject passenger)
		{
			this.UpdateCameras(null);
			this.vehicleSeat.enabled = false;
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000D18F5 File Offset: 0x000CFAF5
		private void CheckEnterAllowed(CharacterBody characterBody, ref Interactability? resultOverride)
		{
			resultOverride = new Interactability?(Interactability.Disabled);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000D1903 File Offset: 0x000CFB03
		private void CheckExitAllowed(CharacterBody characterBody, ref Interactability? resultOverride)
		{
			resultOverride = new Interactability?(this.exitAllowed ? Interactability.Available : Interactability.Disabled);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000D191C File Offset: 0x000CFB1C
		private void Update()
		{
			this.UpdateCameras(this.vehicleSeat.currentPassengerBody ? this.vehicleSeat.currentPassengerBody.gameObject : null);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000D194C File Offset: 0x000CFB4C
		private void UpdateCameras(GameObject characterBodyObject)
		{
			foreach (CameraRigController cameraRigController in CameraRigController.readOnlyInstancesList)
			{
				if (characterBodyObject && cameraRigController.target == characterBodyObject)
				{
					cameraRigController.SetOverrideCam(this, 0f);
				}
				else if (cameraRigController.IsOverrideCam(this))
				{
					cameraRigController.SetOverrideCam(null, 0.05f);
				}
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000D19CC File Offset: 0x000CFBCC
		void ICameraStateProvider.GetCameraState(CameraRigController cameraRigController, ref CameraState cameraState)
		{
			Vector3 position = base.transform.position;
			Vector3 position2 = this.cameraBone.position;
			Vector3 direction = position2 - position;
			Ray ray = new Ray(position, direction);
			Vector3 position3 = position2;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, direction.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				position3 = ray.GetPoint(Mathf.Max(raycastHit.distance - 0.25f, 0.25f));
			}
			cameraState = new CameraState
			{
				position = position3,
				rotation = this.cameraBone.rotation,
				fov = 60f
			};
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x0000CF8A File Offset: 0x0000B18A
		bool ICameraStateProvider.IsUserLookAllowed(CameraRigController cameraRigController)
		{
			return false;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0000B4B7 File Offset: 0x000096B7
		bool ICameraStateProvider.IsUserControlAllowed(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x0000B4B7 File Offset: 0x000096B7
		bool ICameraStateProvider.IsHudAllowed(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000D1A80 File Offset: 0x000CFC80
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040032EB RID: 13035
		private EntityStateMachine stateMachine;

		// Token: 0x040032EC RID: 13036
		[Tooltip("The bone which controls the camera during the entry animation.")]
		public Transform cameraBone;

		// Token: 0x040032ED RID: 13037
		public bool exitAllowed;
	}
}
