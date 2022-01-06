using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBehavior : MonoBehaviour
{
    public SpriteRenderer srenderer;
    DiceController diceController;
    public int numdice;
    public int dicenum = 1;
    public int rolling = 0;
    public int canrolling = 2;
    public float py = 0f;
    public int mode = 0;
    void changeNum()
    {
        int ran = Random.Range(0, 6);
        srenderer.sprite = sprites[ran];
        dicenum = ran + 1;
    }
    void rolldice(int i)
    {
        if (i != 50)
        {
            py = py + (25 - i)/5f;
            this.transform.position = new Vector3(-(10 - numdice) * 200, (-30 + py)*10, 0f);
            this.transform.rotation = Quaternion.Euler(0, 0, (int)Random.Range(0, 360));
            changeNum();
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            rolling = -1;
            mode = 0;
        }



    }
    // Start is called before the first frame update

    public Sprite[] sprites;
    void Start()
    {
        srenderer = GetComponent<SpriteRenderer>();
        diceController = GameObject.Find("DiceController").GetComponent<DiceController>();

        rolling++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&&canrolling>0)
        {
            if (mode == 1)
            {
                rolling = 1;
            }
            canrolling--;

        }
        if (rolling >= 1)
        {
            rolldice(rolling);
            rolling = rolling + 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            diceController.dices[numdice - 1] = dicenum;

        }




    }
    private void OnMouseDown()
    {
        if (mode == 1)
        {
            mode = 0;
            this.transform.position = new Vector3(-(10 - numdice) * 200, -300, 0f);
        }
        else if (mode == 0)
        {
            mode = 1;
            this.transform.position = new Vector3(-(10 - numdice) * 200, -200, 0f);
        }
    }
}


