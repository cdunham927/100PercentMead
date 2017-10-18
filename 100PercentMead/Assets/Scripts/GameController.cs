using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public List<string> enemyList;

	void Start () {
        enemyList = new List<string>();

        enemyList.Add("Wandering Slime");
        enemyList.Add("Bandits");
        enemyList.Add("Baby Dragon");
        enemyList.Add("Ent");
        enemyList.Add("Siren");
        enemyList.Add("Pack of Wolves");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
