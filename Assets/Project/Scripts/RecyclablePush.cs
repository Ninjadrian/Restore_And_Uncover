using Unity.VisualScripting;
using UnityEngine;

public class RecyclablePush : MonoBehaviour
{
    Rigidbody rb;
    public float impulse = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyPush(Collision collision, float force)
    {
        Vector3 contactNormal = collision.contacts[0].normal;

        Vector3 pushDirection = -contactNormal;

        pushDirection.y = 0;

        rb.AddForce(pushDirection * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Vacuum") || collision.collider.CompareTag("Player"))
        {
            ApplyPush(collision, impulse);
        }
    }


}
