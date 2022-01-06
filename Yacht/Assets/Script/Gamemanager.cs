using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public Text[] point = new Text[15];
    public Text reroll;
    public Button[] button = new Button[14];

    DiceController diceController;
    DiceBehavior[] diceBehavior = new DiceBehavior[6];

    public int[] num = new int[5];
    public int[] hm = new int[7];
    int[] y = new int[15];
    int resultpoint = 0;
    int sum = 0;
    int allpoint = 0;
    int firstbonus = 0;
    public int throwdice=0;
    void Start()
    {
        diceController = GameObject.Find("DiceController").GetComponent<DiceController>();
        int i;
        for (i = 0; i <= 13; i++)
        {
            y[i] = 0;
        }
        button[1].onClick.AddListener(delegate { Choose(1); });
        button[2].onClick.AddListener(delegate { Choose(2); });
        button[3].onClick.AddListener(delegate { Choose(3); });
        button[4].onClick.AddListener(delegate { Choose(4); });
        button[5].onClick.AddListener(delegate { Choose(5); });
        button[6].onClick.AddListener(delegate { Choose(6); });
        button[7].onClick.AddListener(delegate { Choose(7); });
        button[8].onClick.AddListener(delegate { Choose(8); });
        button[9].onClick.AddListener(delegate { Choose(9); });
        button[10].onClick.AddListener(delegate { Choose(10); });
        button[11].onClick.AddListener(delegate { Choose(11); });
        button[12].onClick.AddListener(delegate { Choose(12); });
        button[13].onClick.AddListener(delegate { Choose(13); });

        int k;
        for (k = 1; k <= 13; k++)
            if (y[k]==0)
            button[k].transform.Translate(300, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {

            Invoke("Calpoint", 1f);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Invoke("changereroll", 0.1f);
        }

        if (y[0] == 13)
            Endgame();

    }

    public void Calculate()
    {
        sum = 0;

        int i;
        for (i = 1; i <= 6; i++)
            hm[i] = 0;
        for (i = 0; i <= 4; i++)
            num[i] = 0;
        for (i = 0; i < 5; i++)
        {
            num[i] = diceController.dices[i];
            sum = sum + diceController.dices[i];
            hm[diceController.dices[i]]++;
        }
    }
    public int prepoint(int a)
    {
        int point = 0;
        int i, j;
        if (a < 7)
        {
            for (i = 0; i < 5; i++) if (num[i] == a) point = point + a;
        }
        if (a == 7)
        {
            int x;
            for (j = 1; j <= 3; j++)
            {
                x = 1;
                for (i = j; i <= j + 3; i++)
                    x *= hm[i];

                if (x > 0)
                {
                    point = 30;
                    break;
                }
            }
        }
        if (a == 8)
        {
            int x;
            for (j = 1; j <= 2; j++)
            {
                x = 1;
                for (i = j; i <= j + 4; i++)
                    x *= hm[i];

                if (x > 0)
                {
                    point = 40;
                    break;
                }
            }

        }
        if (a == 9)
        {
            for (i = 1; i <= 6; i++)
                for (j = i + 1; j <= 6; j++)
                {
                    if (hm[i] * hm[j] == 6)
                        point = 25;
                }
        }
        if (a == 10)
        {
            for (i = 1; i <= 6; i++)
                if (hm[i] >= 3)
                    point = sum;
        }
        if (a == 11)
        {
            for (i = 1; i <= 6; i++)
                if (hm[i] >= 4)
                    point = sum;
        }
        if (a == 12) // Case : Yacht
        {
            for (i = 1; i <= 6; i++)
                if (hm[i] == 5)
                {
                    point = 50;
                    break;
                }
        }
        if (a == 13)
        {
            point = sum;
        }




        return point;

    }
    void Calpoint()
    {
        Calculate();
        int i;
        for (i = 1; i <= 13; i++)
            if (y[i] == 0)
            {
                point[i].text = prepoint(i).ToString("D");
                button[i].transform.Translate(-300, 0, 0);
            }
    }

    void Choose(int a)
    {
        allpoint = prepoint(a) + allpoint;
        if (a < 7)
            resultpoint = resultpoint + prepoint(a);
        if (resultpoint >= 63 && firstbonus == 0)
        {
            allpoint = allpoint + 35;
            point[14].text = "<color=#ff0000>35</color>";
            firstbonus++;
        }
        else if (firstbonus == 0 && resultpoint < 63)
        {
            point[14].text = (resultpoint - 63).ToString("D");
        }
        point[0].text = allpoint.ToString("D");
        point[a].text = "<color=#ff0000>" + point[a].text + "</color>";
        y[a]++;
        point[0].text = allpoint.ToString("D");
        Destroy(button[a].gameObject);

        int i;
        int k;
        for (k = 1; k <= 13; k++)
            if (y[k] == 0)
                button[k].transform.Translate(300, 0, 0);
        for (i = 1; i <= 13; i++)
            if (y[i] == 0)
                point[i].text = "-";

        if (y[0]<12)
        Invoke("Throwdice", 1f);

        y[0]++;

    }
    void Throwdice()
    {
        int l;
        for (l = 1; l <= 5; l++)
            diceBehavior[l] = GameObject.Find("Dice_"+l.ToString("D")+"(Clone)").GetComponent<DiceBehavior>();

        for (l = 1; l <= 5; l++)
        {
            diceBehavior[l].rolling = 1;
            diceBehavior[l].canrolling = 2;
            changereroll();
        }
        Debug.Log("1");


    }
    void changereroll()
    {
        diceBehavior[0] = GameObject.Find("Dice_" + "1" + "(Clone)").GetComponent<DiceBehavior>();
        reroll.text = diceBehavior[0].canrolling.ToString("D");
    }
    void Endgame()
    {
        point[0].text = "<color=#ff0000>" + point[0].text + "</color>";
    }

        
}
