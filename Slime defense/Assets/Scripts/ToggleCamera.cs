using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public GameObject Player;
    public GameObject TowerUI;
    public Transform PlaceForCamera;
    Vector3 startPosition = new Vector3 (30.18f, 28.5f, 11.2f);
    Quaternion startRotation;
    bool canvasActivity;
    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (!Player.GetComponent<Player>().isDead)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale != 0f)
            {
                if (transform.parent == null)
                {
                    firstFaceView();
                }
                else
                {
                    thirdFaceView();
                }
            }
        }
    }

    public void firstFaceView()
    {
        canvasActivity = TowerUI.GetComponent<Canvas>().enabled;

        transform.parent = Player.transform;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        GetComponent<CameraRotation>().rotationX = 0;
        GetComponent<CameraRotation>().rotationY = 0;
        transform.position = PlaceForCamera.position;

        GetComponent<CameraRotation>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        TowerUI.GetComponent<Canvas>().enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void thirdFaceView()
    {
        transform.parent = null;
        transform.position = startPosition;
        transform.rotation = startRotation;

        GetComponent<CameraRotation>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        TowerUI.GetComponent<Canvas>().enabled = canvasActivity;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
