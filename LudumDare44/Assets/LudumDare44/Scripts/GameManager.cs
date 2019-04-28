using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public enum States { menu, play, paused, gameover }
    public States states;

    public PlayerInteraction player;
    public Camp camp;

    public int leaveLeft = 3;
    public TextMeshProUGUI textLeave;

    private void Awake()
    {
        inst = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
