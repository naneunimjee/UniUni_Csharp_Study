using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stagePoint;
    public int totalPoint;
    public int stageIndex;
    public int health;
    public PlayerMove playerMove;

    public void NextStage()
    {
        stageIndex++;

        totalPoint += stagePoint;
        stagePoint = 0;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            //Health down
            HealthDown();

            //Player Reposition
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = new Vector3(3, 1, 0);

            if (health == 0)
            {
                Debug.Log("게임오버");
            }
        }
    }


    public void HealthDown()
    {
        if (health > 1)
            health--;
        else
            playerMove.OnDie();

    }

}
