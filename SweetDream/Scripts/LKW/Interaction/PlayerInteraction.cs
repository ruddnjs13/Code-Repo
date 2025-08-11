using System;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   [SerializeField] private InputReaderSO _inputReader;
   [SerializeField] private Transform _interactionTrm;
   [SerializeField] private float _interactionRadius;
   [SerializeField] private LayerMask _interactionLayer;
   [SerializeField] private GameObject _interactMark;

   private Player _player;

   public bool canInteract { get; private set; } = true;

   private IInteractable _interactItemObject = null;
   private IInteractable _previousInteractItemObject = null;

   private void Awake()
   {
      _player = GetComponentInParent<Player>();
   }

   private void OnEnable()
   {
      _inputReader.InteractionEvent += HandleInteractEvent;
   }

   private void OnDisable()
   {
      _inputReader.InteractionEvent -= HandleInteractEvent;
   }

   private void HandleInteractEvent()
   {
      if (canInteract && _interactItemObject != null)
      {
         canInteract = false;
         _interactMark.SetActive(false);
         _interactItemObject.Interact();
      }
   }

   private void Update()
   {
      CheckInteractableObject();
   }

   private void CheckInteractableObject()
   {
      Collider2D collider = Physics2D.OverlapCircle(_interactionTrm.position
         , _interactionRadius, _interactionLayer);
      if (canInteract && collider != null)
      {
         _interactMark.SetActive(true);
         _interactItemObject = collider.GetComponent<IInteractable>();
         _previousInteractItemObject = _interactItemObject;
      }
      else if (collider == null)
      {
         _interactMark.SetActive(false);
         _interactItemObject = null;
         canInteract = true;
         if (_previousInteractItemObject != null)
         {
            _previousInteractItemObject.EndInteract();
         }
      }
   }

#if UNITY_EDITOR
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(_interactionTrm.position, _interactionRadius);
      Gizmos.color = Color.white;
   }

#endif
}
