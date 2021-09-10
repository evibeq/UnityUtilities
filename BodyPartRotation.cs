//based on https://www.youtube.com/watch?v=9hTnlp9_wX8
using UnityEngine;

public class BodyPartRotation : MonoBehaviour
{
    public float speed;

    private Vector2 direction;
    public Transform target;

    void Update()
    {
        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
