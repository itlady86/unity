using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject panelMenu;

    public enum Choice { MENU, INIT, PLAY, GAMEOVER };
    private Choice choice; 


    public void PlayClicked()
    {

    } 

    public void SwitchChoise(Choice newChoice)
    {
        EndGame();
        Begin(newChoice);
    }

    void Begin(Choice newChoice)
    {
        switch(newChoice)
        {
            case Choice.MENU:
                panelMenu.SetActive(true);
                break;
            case Choice.INIT:
                break;
            case Choice.PLAY:
                break;
            case Choice.GAMEOVER:
                break;
        }
    }
    void EndGame()
    {
        switch (choice)
        {
            case Choice.MENU:
                panelMenu.SetActive(false);
                break;
            case Choice.INIT:
                break;
            case Choice.PLAY:
                break;
            case Choice.GAMEOVER:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SwitchChoise(Choice.MENU);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
