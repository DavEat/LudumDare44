using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Menu inst;
    private void Awake() { inst = this; }

    int _step = 0;

    string[] _texts = 
        {
            "Your little group is on the road \nto reach a better place to live.",
            "You stop for the first time, the battery of your cars are almost empty.",
        };

    string[] _textsEnd =
    {
            "You are finally ready for the last step.",
            "You ride two full day and night and empty all the battery,\nand...",
            "You found a switable place, with some work it could become a nice community.",
            
            "Thank you for playing <3",
            "A game made by\n\nDavid Mestdagh\n\nduring the\nLudum Dare 44",
            "Restart ?",
        };

    bool starting = true;
    bool ending = false;

    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _fade;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (starting)
            {
                _step++;
                if (_step < _texts.Length)
                    _text.text = _texts[_step];
                else
                {
                    starting = false;
                    StartCoroutine(FirstFadeOut());
                }
            }
            else if (ending)
            {
                _step++;
                if (_step < _textsEnd.Length)
                    _text.text = _textsEnd[_step];
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    IEnumerator FirstFadeOut()
    {
        yield return Utility.Fade(new Graphic[] { _fade, _text }, Fade.Out, .6f);
        _fade.enabled = false;
    }

    public void End()
    {
        _fade.enabled = true;
        StartCoroutine(Utility.Fade(new Graphic[] { _fade, _text }, Fade.In, .6f));
        ending = true;
        _step = 0;
        _text.text = _textsEnd[_step];
    }
}
