using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Image _fade = null;

    [SerializeField] TextMeshProUGUI _elect_prod = null;
    [SerializeField] TextMeshProUGUI _elect_stock = null;
    [SerializeField] TextMeshProUGUI _food_prod = null;
    [SerializeField] TextMeshProUGUI _food_stock = null;
    [SerializeField] TextMeshProUGUI _gear_stock = null;

    /*[SerializeField] TextMeshProUGUI _add_elect_prod = null;
    [SerializeField] TextMeshProUGUI _add_elect_stock = null;
    [SerializeField] TextMeshProUGUI _add_food_prod = null;
    [SerializeField] TextMeshProUGUI _add_food_stock = null;
    [SerializeField] TextMeshProUGUI _add_gear_stock = null;*/

    [SerializeField] SelectableObj[] _humans = new SelectableObj[0];
    [SerializeField] SelectableObj[] _robots = new SelectableObj[0];

    public Dictionary<string, SelectableObj> humans = new Dictionary<string, SelectableObj>();
    public Dictionary<string, SelectableObj> robots = new Dictionary<string, SelectableObj>();


    public static int humanCount = 2, robotCount = 4;

    public static int base_food_prod = -1, food_prod_by_human = -4, food_prod_bonus = 0;
    public static int base_elec_prod = 16, elec_prod_by_robot = -2, elec_prod_bonus = 0;


    public static int v_food_prod { get {  return base_food_prod * humanCount + food_prod_by_human * humanCount + food_prod_bonus * humanCount; } }
    public static int v_elect_prod { get {  return base_elec_prod + elec_prod_by_robot * robotCount + elec_prod_bonus; } }


    public int v_elect_stock = 0;
    public int v_food_stock = 0;
    public int v_gear_stock = 0;



    private void Start()
    {
        Init();

        for (int i = 0; i < _humans.Length; i++)
            humans.Add(_humans[i].name, _humans[i]);

        for (int i = 0; i < _robots.Length; i++)
            robots.Add(_robots[i].name, _robots[i]);
    }

    public void Wait()
    {
        StartCoroutine(NextDay());
    }

    void Init()
    {
        _elect_prod.text = (v_elect_prod > 0 ? "+" : "") + v_elect_prod + "%";
        _elect_stock.text = "" + v_elect_stock + "%";
        _food_prod.text = (v_food_prod > 0 ? "+" : "") + v_food_prod;
        _food_stock.text = "" + v_food_stock;
        _gear_stock.text = "" + v_gear_stock;
    }

    public bool AssignNewData(UIObject.Target[] data)
    {
        int t_elect_stock = v_elect_stock;
        int t_food_stock = v_food_stock;
        int t_gear_stock = v_gear_stock;

        for (int i = 0; i < data.Length; i++)
        {
            switch(data[i].resources)
            {
                case Resources.elect_prod:
                    elec_prod_bonus += data[i].value;
                    break;
                case Resources.elect_stock:
                    if (t_elect_stock + data[i].value < 0)
                        return false;
                    t_elect_stock += data[i].value;
                    break;
                case Resources.food_prod:
                    food_prod_bonus += data[i].value;
                    break;
                case Resources.food_stock:
                    if (t_food_stock + data[i].value < 0)
                        return false;
                    t_food_stock += data[i].value;
                    break;
                case Resources.gear_stock:
                    if (t_gear_stock + data[i].value < 0)
                        return false;
                    t_gear_stock += data[i].value;
                    break;
            }
        }

        v_elect_stock = t_elect_stock;
        v_food_stock = t_food_stock;
        v_gear_stock = t_gear_stock;

        StartCoroutine(NextDay());

        return true;
    }

    void EndOfDay()
    {
        v_elect_stock += v_elect_prod;
        v_food_stock += v_food_prod;

        if (v_food_stock <=0 && humans.Count > 0)
        {
            if (v_food_stock < v_food_prod / (float)humanCount)
            {
                humanCount = 0;
                Debug.Log("Kill 2 human =" + v_food_stock);

                foreach (SelectableObj so in _humans)
                    so.Removed();
                humans.Clear();
            }
            else
            {
                humanCount--;
                Debug.Log("Kill 1 human =" + v_food_stock);

                SelectableObj so = humans[humans.Keys.First()];
                humans.Remove(humans.Keys.First());
                so.Removed();
            }
        }

        if (v_food_stock < 0)
        {
            v_food_stock = 0;
            _food_stock.color = Color.red;
        }
        else _food_stock.color = Color.white;
    }

    IEnumerator NextDay()
    {
        _fade.enabled = true;
        yield return Fade(_fade, FadeType.In, .6f);
        EndOfDay();
        Init();
        yield return Fade(_fade, FadeType.Out, .6f);
        _fade.enabled = false;
    }

    public enum FadeType { In = 0, Out = 1 }
    public static IEnumerator Fade(Graphic obj, FadeType fade, float duration, float mult = 1)
    {
        for (float i = 0; i <= duration; i += Time.unscaledDeltaTime)
        {
                if (obj != null)
                    obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.Abs(((int)fade) - (i / duration)) * mult);
            yield return null;
        }
        if (obj != null)
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.Abs(((int)fade) - 1) * mult);
    }
}