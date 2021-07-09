using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private
    bool input;
    [SerializeField]
    GameObject previousWeapon;

    [SerializeField]
    List<GameObject> weapons = new List<GameObject>();
    #endregion

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
    void MyInput()
    {
        
        
        if(Input.GetKey(KeyCode.Alpha1) && weapons[0].activeSelf == false)
        {
            
            weapons[0].SetActive(true);
            previousWeapon.SetActive(false);
            previousWeapon = weapons[0];
        }
        else if (Input.GetKey(KeyCode.Alpha2) && weapons[1].gameObject.activeSelf == false)
        {
           
            weapons[1].SetActive(true);
            previousWeapon.SetActive(false);
            previousWeapon = weapons[1];
        }
        else if (Input.GetKey(KeyCode.Alpha3) && weapons[2].activeSelf == false)
        {
            
            weapons[2].SetActive(true);
            previousWeapon.SetActive(false);
            previousWeapon = weapons[2];
        }
        else if (Input.GetKey(KeyCode.Alpha4) && weapons[3].activeSelf == false)
        {

            weapons[3].SetActive(true);
            previousWeapon.SetActive(false);
            previousWeapon = weapons[3];
        }


    }

}
