using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] protected CameraTracking cam;
    [SerializeField] protected float panDuration = 2f;
    [SerializeField] protected GameObject menuUI;
    [SerializeField] protected GameObject portal;

    void Start()
    {
        cam.FixedY = 2f;
    }

    public void PlayGame()
    {
        Debug.Log("Play Game button clicked!");
        StartCoroutine(PanCameraDown());
    }

    protected IEnumerator PanCameraDown()
    {
        if (menuUI != null) menuUI.SetActive(false); 

        float startY = cam.FixedY;
        float targetY = -2f;
        float elapsed = 0f;

        while (elapsed < panDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / panDuration;
            cam.FixedY = Mathf.Lerp(startY, targetY, t);
            yield return null;
        }

        cam.FixedY = targetY;
        SceneStart();
    }

    protected void SceneStart()
    {
        Instantiate(portal, new Vector3(-5, -3, 0), Quaternion.identity);
    }
}
