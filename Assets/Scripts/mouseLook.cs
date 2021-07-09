using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    Transform fpsCamera;
    Vector3 lookAngles;
    #endregion


    #region Public Variables
    public float sensitivity;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       LockAndUnlock();
       MouseControl();
    }

    void LockAndUnlock()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void MouseControl()
    {
        lookAngles.x += Input.GetAxis("Mouse Y") * sensitivity * -1; //PLayer Watch Up&&Down
        lookAngles.y += Input.GetAxis("Mouse X") * sensitivity;// Player Watch Left or Right
        lookAngles.x = Mathf.Clamp(lookAngles.x, -50f, 50f);
        fpsCamera.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, lookAngles.y, 0f);

    }
}
