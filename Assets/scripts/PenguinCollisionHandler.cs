using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PenguinCollisionHandler : MonoBehaviour
{
    public Transform kannettavaEsine;

    [SerializeField] private float distanceAboveHead = 1.2f;
    [SerializeField] private float dropDistance = 1.4f;

    private CharacterController controller;
    private Collider carriedCollider;
    private Rigidbody carriedRigidbody;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (kannettavaEsine == null && hit.gameObject.CompareTag("Pickable"))
        {
            kannettavaEsine = hit.transform;

            carriedCollider = kannettavaEsine.GetComponent<Collider>();
            if (carriedCollider)
                carriedCollider.enabled = false;

            carriedRigidbody = kannettavaEsine.GetComponent<Rigidbody>();
            if (carriedRigidbody)
            {
                // STOP motion BEFORE making kinematic
#pragma warning disable CS0618 // Type or member is obsolete
                carriedRigidbody.velocity = Vector3.zero;
#pragma warning restore CS0618 // Type or member is obsolete
                carriedRigidbody.angularVelocity = Vector3.zero;

                carriedRigidbody.isKinematic = true;
                carriedRigidbody.useGravity = false;
            }
        }
    }

    void Update()
    {
        if (kannettavaEsine != null)
        {
            Vector3 headPos = transform.TransformPoint(Vector3.up * distanceAboveHead);
            kannettavaEsine.position = headPos;
        }

        if (Input.GetKeyDown(KeyCode.Q) && kannettavaEsine != null)
        {
            if (carriedCollider)
                carriedCollider.enabled = true;

            if (carriedRigidbody)
            {
                carriedRigidbody.isKinematic = false;
                carriedRigidbody.useGravity = true;
            }

            // Drop above ground
            Vector3 dropPos = transform.TransformPoint(Vector3.forward * dropDistance + Vector3.up * 0.5f);
            kannettavaEsine.position = dropPos;

            kannettavaEsine = null;
            carriedCollider = null;
            carriedRigidbody = null;
        }
    }
}
