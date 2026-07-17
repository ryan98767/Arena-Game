using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private float fixedY;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float xOffset = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixedY = transform.position.y;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x + xOffset, fixedY, transform.position.z);
    }
}
