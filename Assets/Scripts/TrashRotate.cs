using UnityEngine;

public class TrashRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50f;

    private void Update()
    {
        transform.Rotate(-1 * Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}