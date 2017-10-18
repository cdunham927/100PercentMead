using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject {
    //Stats
    public int hp = 10;


    public float restartLevelDelay = 1f;

    private Animator animator;
    //private Vector2 touchOrigin = -Vector2.one;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();

        base.Start();
	}

    public void OnDisable()
    {

    }

    // Update is called once per frame
    void Update () {
        if (!GameManager.instance.playerTurn) return;

        int horizontal = 0;
        int vertical = 0;

//#if UNITY_STANDALONE || UNITY_WEBPLAYER

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;
//#else
        /**
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }*/

//#endif

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(2*horizontal, 2*vertical);
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            //SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }

        CheckIfGameOver();

        GameManager.instance.playerTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Town")
        {
            //Invoke("Restart", restartLevelDelay);
            //enabled = false;
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        //animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseFood(int loss)
    {
        //animator.SetTrigger("playerHit");
        hp -= loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (hp <= 0)
        {
            //SoundManager.instance.RandomizeSfx(gameOverSound1);
            //SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
