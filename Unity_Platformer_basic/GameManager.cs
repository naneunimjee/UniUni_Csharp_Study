using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int stagePoint;
    public int totalPoint;
    public int stageIndex;
    public int health;
    public PlayerMove playerMove;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UItotal;
    public TextMeshProUGUI UIstage;
    public GameObject RestartBtn;

    public void Update()
    {
        UItotal.text = (totalPoint + stagePoint).ToString();
        UIstage.text = "Stage" + (stageIndex + 1).ToString();
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            //Change Stage
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            PlayerReposition();

            UIstage.text = "Stage" + (stageIndex + 1).ToString();
        }

        else //Stage Clear
        {
            //player control lock
            Time.timeScale = 0;

            //Retry?
            RestartBtn.SetActive(true);
            TextMeshProUGUI BtnText = RestartBtn.GetComponentInChildren<TextMeshProUGUI>();
            BtnText.text = "Game Clear";
        }


        totalPoint += stagePoint;
        stagePoint = 0;

        Debug.Log("다음스테이지로");

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            if (health > 1)
            {
                //Player Reposition
                PlayerReposition();
            }

            //UI Image Delete
            UIhealth[health - 1].color = new Color(1, 1, 1, 0);

            //Health down
            HealthDown();

        }
    }


    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            //UI Image Delete
            UIhealth[health].color = new Color(1, 1, 1, 0);
        }
        else
        {
            health--;
            UIhealth[health].color = new Color(1, 1, 1, 0);
            //Player Die Effect
            playerMove.OnDie();

            //Result UI 
            Debug.Log("게임오버");

            //Retry Button UI
            RestartBtn.SetActive(true);
        }

    }

    void PlayerReposition()
    {
        //Player Reposition
        playerMove.VelocityZero();
        playerMove.transform.position = new Vector3(3, 1, 0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
