using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camp : MonoBehaviour
{
    #region Vars
    [HideInInspector] public Transform _transform;

    bool _spendingEnergy;
    public int base_needs = 50;
    public int instock = 10;
    public int base_maxstock = 35;

    public Dictionary<string, sUpgrades> upgrades = new Dictionary<string, sUpgrades> {
        { "Camp Batery", new sUpgrades(4, "Camp Batery", 10, new Impact(10, Impact.ImpactType.CBC)) },
        { "Repair Tool", new sUpgrades(2, "Repair Tool", 30, new Impact( 5, Impact.ImpactType.PWS)) },
        { "Tiny Batery", new sUpgrades(2, "Tiny Batery",  5, new Impact( 5, Impact.ImpactType.PBC)) },
        { "Turbine",     new sUpgrades(1, "Turbine",     20, new Impact(-8, Impact.ImpactType.needs)) },
    };

    public int needs { get { return base_needs - upgrades["Turbine"].installed * upgrades["Turbine"].upgrades.impact.value; } }
    public int maxstock { get { return base_maxstock + upgrades["Camp Batery"].installed * upgrades["Camp Batery"].upgrades.impact.value; } }

    [SerializeField] Slider _progression;

    #endregion
    #region MonoFunctions
    void Start()
    {
        _transform = GetComponent<Transform>();
        OnUpgradeOrGiveEnergy();
    }
    private void Update()
    {
        if ((GameManager.inst.player._transform.position - _transform.position).sqrMagnitude > 25)
        {
            CanvasManager.inst.EnableCsInfo(false);
            return; // player out of range
        }        

        if (!_spendingEnergy)
        {
            CanvasManager.inst.EnableCsInfo(true);

            if (Input.GetButtonDown("Submit"))
            {
                _spendingEnergy = true;
                CanvasManager.inst.EnableCsInfo(false);
                CameraManager.inst.SetTransitionToCamp(ShowSpendEnergy);

                GameManager.inst.player.canMove = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                _spendingEnergy = false;
                CanvasManager.inst.EnableCsInfo(true);
                CanvasManager.inst.EnableCsSpendEnergy(false);

                GameManager.inst.player.canMove = true;

                CameraManager.inst.SetTransitionToPlayer();
            }
        }
    }
    #endregion
    #region Functions
    private void ShowSpendEnergy()
    {
        CanvasManager.inst.EnableCsSpendEnergy(true);
        CanvasManager.inst.SelectSpendables();
    }

    private void OnUpgradeOrGiveEnergy()
    {
        _progression.value = Mathf.Clamp01((float)instock / needs);
    }
    #endregion

    #region Structs
    public struct Upgrades
    {
        public string name;
        public int cost;
        public Impact impact;

        public Upgrades(string name, int cost, Impact impact)
        {
            this.name = name;
            this.cost = cost;
            this.impact = impact;
        }
    }
    public struct Impact
    {
        public int value;
        // needs, campBateryCapacity, playerWeaponStrengh, playerBateryCapacity
        public enum ImpactType { needs, CBC, PWS, PBC }
        public ImpactType type;

        public Impact(int value, ImpactType type)
        {
            this.value = value;
            this.type = type;
        }
    }
    public struct sUpgrades
    {
        public int installed;
        public int maxInstall;
        public Upgrades upgrades;

        public sUpgrades(int maxInstall, Upgrades upgrades) : this()
        {
            installed = 0;
            this.maxInstall = maxInstall;
            this.upgrades = upgrades;
        }

        public sUpgrades(int maxInstall, string name, int cost, Impact impact) : this()
        {
            installed = 0;
            this.maxInstall = maxInstall;
            this.upgrades = new Upgrades(name, cost, impact);
        }
    }
    #endregion
}
