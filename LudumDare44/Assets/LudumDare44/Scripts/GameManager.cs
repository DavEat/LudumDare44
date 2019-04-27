using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public enum States { menu, play, paused, gameover }
    public States states;

    public PlayerInteraction player;
    public Camp camp;

    private void Awake()
    {
        inst = this;
    }

    private void FixedUpdate()
    {
        
    }
}
