using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000776 RID: 1910
	public class Interactor : NetworkBehaviour
	{
		// Token: 0x060027AC RID: 10156 RVA: 0x000AC4B8 File Offset: 0x000AA6B8
		public GameObject FindBestInteractableObject(Ray raycastRay, float maxRaycastDistance, Vector3 overlapPosition, float overlapRadius)
		{
			LayerMask interactable = LayerIndex.CommonMasks.interactable;
			RaycastHit raycastHit;
			if (Physics.Raycast(raycastRay, out raycastHit, maxRaycastDistance, interactable, QueryTriggerInteraction.Collide))
			{
				GameObject entity = EntityLocator.GetEntity(raycastHit.collider.gameObject);
				if (entity)
				{
					IInteractable component = entity.GetComponent<IInteractable>();
					if (component != null && ((MonoBehaviour)component).isActiveAndEnabled && component.GetInteractability(this) != Interactability.Disabled)
					{
						return entity;
					}
				}
			}
			Collider[] array = Physics.OverlapSphere(overlapPosition, overlapRadius, interactable, QueryTriggerInteraction.Collide);
			int num = array.Length;
			GameObject result = null;
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				Collider collider = array[i];
				GameObject entity2 = EntityLocator.GetEntity(collider.gameObject);
				if (entity2)
				{
					IInteractable component2 = entity2.GetComponent<IInteractable>();
					if (component2 != null && ((MonoBehaviour)component2).isActiveAndEnabled && component2.GetInteractability(this) != Interactability.Disabled && !component2.ShouldIgnoreSpherecastForInteractibility(this))
					{
						float num3 = Vector3.Dot((collider.transform.position - overlapPosition).normalized, raycastRay.direction);
						if (num3 > num2)
						{
							num2 = num3;
							result = entity2.gameObject;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000AC5DC File Offset: 0x000AA7DC
		[Command]
		public void CmdInteract(GameObject interactableObject)
		{
			this.PerformInteraction(interactableObject);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000AC5E8 File Offset: 0x000AA7E8
		[Server]
		private void PerformInteraction(GameObject interactableObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Interactor::PerformInteraction(UnityEngine.GameObject)' called on client");
				return;
			}
			if (!interactableObject)
			{
				return;
			}
			bool flag = false;
			bool anyInteractionSucceeded = false;
			foreach (IInteractable interactable in interactableObject.GetComponents<IInteractable>())
			{
				Interactability interactability = interactable.GetInteractability(this);
				if (interactability == Interactability.Available)
				{
					interactable.OnInteractionBegin(this);
					GlobalEventManager.instance.OnInteractionBegin(this, interactable, interactableObject);
					anyInteractionSucceeded = true;
				}
				flag |= (interactability > Interactability.Disabled);
			}
			if (flag)
			{
				this.CallRpcInteractionResult(anyInteractionSucceeded);
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000AC66A File Offset: 0x000AA86A
		[ClientRpc]
		private void RpcInteractionResult(bool anyInteractionSucceeded)
		{
			if (!anyInteractionSucceeded && CameraRigController.IsObjectSpectatedByAnyCamera(base.gameObject))
			{
				Util.PlaySound("Play_UI_insufficient_funds", RoR2Application.instance.gameObject);
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000AC691 File Offset: 0x000AA891
		public void AttemptInteraction(GameObject interactableObject)
		{
			if (NetworkServer.active)
			{
				this.PerformInteraction(interactableObject);
				return;
			}
			this.CallCmdInteract(interactableObject);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000AC6BC File Offset: 0x000AA8BC
		protected static void InvokeCmdCmdInteract(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdInteract called on client.");
				return;
			}
			((Interactor)obj).CmdInteract(reader.ReadGameObject());
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000AC6E8 File Offset: 0x000AA8E8
		public void CallCmdInteract(GameObject interactableObject)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdInteract called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdInteract(interactableObject);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)Interactor.kCmdCmdInteract);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(interactableObject);
			base.SendCommandInternal(networkWriter, 0, "CmdInteract");
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000AC772 File Offset: 0x000AA972
		protected static void InvokeRpcRpcInteractionResult(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcInteractionResult called on server.");
				return;
			}
			((Interactor)obj).RpcInteractionResult(reader.ReadBoolean());
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000AC79C File Offset: 0x000AA99C
		public void CallRpcInteractionResult(bool anyInteractionSucceeded)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcInteractionResult called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)Interactor.kRpcRpcInteractionResult);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(anyInteractionSucceeded);
			this.SendRPCInternal(networkWriter, 0, "RpcInteractionResult");
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000AC810 File Offset: 0x000AAA10
		static Interactor()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(Interactor), Interactor.kCmdCmdInteract, new NetworkBehaviour.CmdDelegate(Interactor.InvokeCmdCmdInteract));
			Interactor.kRpcRpcInteractionResult = 804118976;
			NetworkBehaviour.RegisterRpcDelegate(typeof(Interactor), Interactor.kRpcRpcInteractionResult, new NetworkBehaviour.CmdDelegate(Interactor.InvokeRpcRpcInteractionResult));
			NetworkCRC.RegisterBehaviour("Interactor", 0);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000AC880 File Offset: 0x000AAA80
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002B9E RID: 11166
		public float maxInteractionDistance = 1f;

		// Token: 0x04002B9F RID: 11167
		private static int kCmdCmdInteract = 591229007;

		// Token: 0x04002BA0 RID: 11168
		private static int kRpcRpcInteractionResult;
	}
}
