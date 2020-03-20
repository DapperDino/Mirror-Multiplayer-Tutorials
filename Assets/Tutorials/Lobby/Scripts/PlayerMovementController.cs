using DapperDino.Tutorials.Lobby.Inputs;
using Mirror;
using UnityEngine;

namespace DapperDino.Tutorials.Lobby
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private Transform directionTransform = null;
        [SerializeField] private CharacterController controller = null;

        private Vector2 previousInput;

        private Controls controls;
        private Controls Controls
        {
            get
            {
                if (controls != null) { return controls; }
                return controls = new Controls();
            }
        }

        public override void OnStartAuthority()
        {
            enabled = true;

            Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
            Controls.Player.Move.canceled += ctx => ResetMovement();
        }

        [ClientCallback]
        private void OnEnable() => Controls.Enable();
        [ClientCallback]
        private void OnDisable() => Controls.Disable();
        [ClientCallback]
        private void Update() => Move();

        [Client]
        private void SetMovement(Vector2 movement) => previousInput = movement;

        [Client]
        private void ResetMovement() => previousInput = Vector2.zero;

        [Client]
        private void Move()
        {
            Vector3 right = directionTransform.right;
            Vector3 forward = directionTransform.forward;
            right.y = 0f;
            forward.y = 0f;

            Vector3 movement = right * previousInput.x + forward * previousInput.y;

            controller.Move(movement * movementSpeed * Time.deltaTime);
        }
    }
}
