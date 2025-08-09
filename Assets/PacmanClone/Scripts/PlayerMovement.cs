using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection = Vector3.forward;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector3.right;
    }

    void FixedUpdate()
    {
        // Check ahead — only move if not blocked
        if (!IsBlockedAhead())
        {
            transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
        }

        // Rotate to face direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRot, 10f * Time.deltaTime);
        }
    }

    bool IsBlockedAhead()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Ray ray = new Ray(origin, moveDirection);
        Debug.DrawRay(origin, moveDirection * 0.6f, Color.red);

        // Optional: only block if tag == "Wall"
        if (Physics.Raycast(ray, out RaycastHit hit, 0.6f))
        {
            if (hit.collider.CompareTag("Wall"))
                return true;
        }

        return false;
    }
}
