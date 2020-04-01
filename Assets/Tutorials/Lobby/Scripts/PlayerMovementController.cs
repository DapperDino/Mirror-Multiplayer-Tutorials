using Mirror;
using UnityEngine;

namespace DapperDino.Tutorials.Lobby
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private CharacterController controller = null;

        private Vector2 previousInput;

        public override void OnStartAuthority()
        {
            enabled = true;

            InputManager.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
            InputManager.Controls.Player.Move.canceled += ctx => ResetMovement();
        }

        [ClientCallback]
        private void Update() => Move();

        [Client]
        private void SetMovement(Vector2 movement) => previousInput = movement;

        [Client]
        private void ResetMovement() => previousInput = Vector2.zero;

        [Client]
        private void Move()
        {
            Vector3 right = controller.transform.right;
            Vector3 forward = controller.transform.forward;
            right.y = 0f;
            forward.y = 0f;

            Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

            controller.Move(movement * movementSpeed * Time.deltaTime);
        }
    }
}
