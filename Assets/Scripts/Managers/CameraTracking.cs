using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] protected float fixedY;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float xOffset = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixedY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) 
        {
            xOffset = 0;
            playerTransform = transform;
        }
        else
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            xOffset = 3f;
        }
        transform.position = new Vector3(playerTransform.position.x + xOffset, fixedY, transform.position.z);
    }

    public float FixedY
    {
        get { return fixedY; }
        set { fixedY = value; }
    }
}
