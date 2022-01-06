using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiceController : MonoBehaviour
{
    public GameObject[] obj;
    public int[] dices = new int[5];
    void Start()
    {
        int i;
        for (i = 0; i < 5;i++)
        {
            Instantiate(obj[i], new Vector3(-(9 - i) * 200, -300, 0), Quaternion.identity);
            dices[i] = 1;
        }
        

    }


        void Update()
        {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Invoke("show", 0.5f);
        }
            
        }

        void show()
    {
        Debug.Log(dices[0] + " " + dices[1] + " " + dices[2] + " " + dices[3] + " " + dices[4]);
    }
}
