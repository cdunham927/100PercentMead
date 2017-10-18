using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    //Buttons
    public Button Lockstats;
    public Button Simulate;
    public Button Restart;
    public Button randomEnemy;
    //Sliders
    public Slider[] hpSliders;
    public Slider[] atkSliders;
    public Slider[] defSliders;
    public Slider[] lukSliders;
    //Stats
    int p1totatk;
    int p2totatk;
    int luk1Mod;
    int luk2Mod;
    //Text
    public Text[] combatText;
    public Text[] hpText;
    public Text[] atkText;
    public Text[] defText;
    public Text[] lukText;
    public Text enemyText;
    //Enemy presets
    //Enemies need: name, hp, atk, def, gold drops, item drops
    public string[,] enemies = new string[6,6];
    string[] slime = { "Wandering Slime", "4", "4", "4", "50", "Slime" };
    string[] wolves = { "Pack of Wolves", "8", "5", "2", "100", "Wolf pelt" };
    string[] bandits = { "Bandits", "9", "6", "1", "100", "Bandits tooth" };
    string[] babyDragon = { "Baby Dragon", "12", "7", "3", "150", "Dragons eye" };
    string[] ent = { "Ents!", "10", "5", "2", "75", "Tree bark" };
    string[] siren = { "Siren", "14", "6", "4", "150", "Glowing jewel" };

	void Awake ()
    {
        hpSliders[0].value = Random.Range(hpSliders[0].maxValue / 2, hpSliders[0].maxValue);
        hpSliders[1].value = Random.Range(hpSliders[1].maxValue / 2, hpSliders[1].maxValue);
        //enemies = Resources.LoadAll<GameObject>("Prefabs/Enemies");

        //Add to 2D array
        for (int i = 0; i < 6; i++)
        {
            enemies[0, i] = slime[i];
        }

        for (int i = 0; i < 6; i++)
        {
            enemies[1, i] = wolves[i];
        }

        for (int i = 0; i < 6; i++)
        {
            enemies[2, i] = bandits[i];
        }

        for (int i = 0; i < 6; i++)
        {
            enemies[3, i] = babyDragon[i];
        }

        for (int i = 0; i < 6; i++)
        {
            enemies[4, i] = ent[i];
        }

        for (int i = 0; i < 6; i++)
        {
            enemies[5, i] = siren[i];
        }
    }
	
	void Update ()
    {
        int atk1, atk2;
        int def1, def2;

        atk1 = (int)atkSliders[0].value;
        atk2 = (int)atkSliders[1].value;
        def1 = (int)defSliders[0].value;
        def2 = (int)defSliders[1].value;
        luk1Mod = (int)lukSliders[0].value;
        luk2Mod = (int)lukSliders[1].value;

        for (int i = 0; i < hpText.Length; i++)
        {
            hpText[i].text = "hp: " + hpSliders[i].value.ToString();
            atkText[i].text = "atk: " + atkSliders[i].value.ToString();
            defText[i].text = "def: " + defSliders[i].value.ToString();
            lukText[i].text = "luk: " + lukSliders[i].value.ToString();
        }

        p1totatk = atk1 - def2;
        p2totatk = atk2 - def1;

        for (int i = 0; i < hpSliders.Length; i++)
        {
            if (hpSliders[i].value == 0)
            {
                combatText[i].text = "UNIT HAS DIED";
                Simulate.interactable = false;
            }
        }
	}

    public void lockSliders()
    {
        for (int i = 0; i < hpSliders.Length; i++)
        {
            hpSliders[i].interactable = false;
            atkSliders[i].interactable = false;
            defSliders[i].interactable = false;
            lukSliders[i].interactable = false;
        }
    }

    public void newEnemy()
    {
        lukSliders[1].value = 0;
        int en = Random.Range(0, enemies.GetLength(0));
        enemyText.text = enemies[en, 0];
        hpSliders[1].value = System.Convert.ToInt32(enemies[en, 1]);
        atkSliders[1].value = System.Convert.ToInt32(enemies[en, 2]);
        defSliders[1].value = System.Convert.ToInt32(enemies[en, 3]);
    }

    public void simulateCombat()
    {
        int rnd1 = Random.Range(0, 100) + (luk1Mod - luk2Mod);
        if (rnd1 < 20)
        {
            //MISS
            combatText[0].text = "MISS. 0 DAMAGE TO PLAYER 2";
        }
        else if (rnd1 < 90)
        {
            //HIT
            combatText[0].text = "HIT. " + (p1totatk > 0 ? p1totatk.ToString() : "1") + " DAMAGE TO PLAYER 2";
            hpSliders[1].value -= (p1totatk > 0 ? p1totatk : 1);
        }
        else
        {
            //CRIT
            combatText[0].text = "CRIT. " + (p1totatk > 0 ? (p1totatk * 2).ToString() : "1") + " DAMAGE TO PLAYER 2";
            hpSliders[1].value -= (p1totatk > 0 ? (p1totatk * 2) : 2);
        }

        int rnd2 = Random.Range(0, 100) + (luk2Mod - luk1Mod);
        if (rnd2 < 20)
        {
            //MISS
            combatText[1].text = "MISS. 0 DAMAGE TO PLAYER 1";
        }
        else if (rnd2 < 90)
        {
            //HIT
            combatText[1].text = "HIT. " + (p2totatk > 0 ? p2totatk.ToString() : "1") + " DAMAGE TO PLAYER 1";
            hpSliders[0].value -= (p2totatk > 0 ? p2totatk : 1);
        }
        else
        {
            //CRIT
            combatText[1].text = "CRIT. " + (p2totatk > 0 ? (p2totatk * 2).ToString() : "2") + " DAMAGE TO PLAYER 1";
            hpSliders[0].value -= (p2totatk > 0 ? (p2totatk * 2) : 2);
        }
    }

    public void Retry()
    {
        hpSliders[0].value = Random.Range(hpSliders[0].maxValue / 2, hpSliders[0].maxValue);
        for (int i = 0; i < hpSliders.Length; i++)
        {
            atkSliders[i].interactable = true;
            defSliders[i].interactable = true;
            lukSliders[i].interactable = true;
        }
        newEnemy();
        Simulate.interactable = true;
    }
}
