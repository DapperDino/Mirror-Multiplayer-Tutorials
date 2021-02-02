using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController controller = null;
        [SerializeField] private Animator animator = null;

        [Header("Settings")]
        [SerializeField] private float movementSpeed = 5f;

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) { return; }

            var movement = new Vector3();

            if (Keyboard.current.wKey.isPressed) { movement.z += 1; }
            if (Keyboard.current.sKey.isPressed) { movement.z -= 1; }
            if (Keyboard.current.aKey.isPressed) { movement.x -= 1; }
            if (Keyboard.current.dKey.isPressed) { movement.x += 1; }

            controller.Move(movement * movementSpeed * Time.deltaTime);

            if (controller.velocity.magnitude > 0.2f)
            {
                transform.rotation = Quaternion.LookRotation(movement);
            }

            animator.SetBool("IsWalking", controller.velocity.magnitude > 0.2f);
        }
    }
}
