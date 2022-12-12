using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arena : MonoBehaviour
{/*
    [SerializeField] [Range(2, 60)] int _fightersNumber;
    [SerializeField] private Targeter _targeter;
    [SerializeField] private List<Fighter> _availableFighters = new List<Fighter>();
    [SerializeField] private LogContainer _logContainer;
    [SerializeField] private DamageViewer _damageViewer;

    private List<Fighter> _chosenFighters = new List<Fighter>();
    private Fighter _player;

    public static bool WinnerFound { get; private set; }

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void OnEnable()
    {
        ChooseFighters();

        _targeter.Init(_chosenFighters);
        _targeter.TargetsUnavailable += OnTargetsUnavailable;

        Randomize();

        foreach (var fighter in _chosenFighters)
        {
            fighter.Attacking += OnAttacking;
            fighter.Init(_targeter);
        }
    }

    private void OnDisable()
    {
        _targeter.TargetsUnavailable -= OnTargetsUnavailable;

        foreach (var fighter in _chosenFighters)
        {
            fighter.Attacking -= OnAttacking;
        }
    }

    private void Start()
    {
        foreach (var fighter in _chosenFighters)
        {
            _targeter.FindLessAttackableTarget(fighter);
        }
    }

    private void Randomize()
    {
        for (int i = 0; i < _chosenFighters.Count; i++)
        {
            int newIndex = Random.Range(0, _chosenFighters.Count);
            var tempotaryValue = _chosenFighters[newIndex];
            _chosenFighters[newIndex] = _chosenFighters[i];
            _chosenFighters[i] = tempotaryValue;
        }
    }

    private void OnTargetsUnavailable(Fighter winner)
    {
        WinnerFound = true;
        /*
        Debug.Log($"No targets { winner.Name} ?= {_player.Name}");

        if(winner.Name == _player.Name)
        {
           // winner.Level.LevelUp();
        }
    }

    private void OnAttacking(Fighter attacker, Fighter defender)
    {
        Log log = new Log();
        var attackSequence = new AttackSequence(attacker, defender);

        attackSequence.AttackPhase(ref log, out int damage);

        if (damage > 0)
        {
            attackSequence.DefencePhase(ref log, damage, out int finalDamage);
            attackSequence.ApproveDamagePhase(finalDamage);

            _damageViewer.ShowDamage(finalDamage, defender.transform.position);
        }

        _logContainer.AddLog(log);
    }

    private void ChooseFighters()
    {
        InitMode();
        InitPlayer();

        if(_player != null)
        {
            var player = _availableFighters.FirstOrDefault(x => x.Name == _player.Name);
            _chosenFighters.Add(player);
         //   var health= player.Health.Config.LoadHealth();
           // player.Health.SetHealth(health);
         //   player.Health.InitView();
            player.gameObject.SetActive(true);
            _fightersNumber--;
        }
        

        for(int i = 0; i < _fightersNumber; i++)
        {
            var fighter = _availableFighters[Random.Range(0, _availableFighters.Count)];

            if(!_chosenFighters.Contains(fighter))
            {
                _chosenFighters.Add(fighter);
            //    fighter.Health.InitView();
                fighter.gameObject.SetActive(true);
            }
            else
            {
                i--;
            }
        }
    }

    private void InitMode()
    {
        _fightersNumber = PlayerPrefs.GetInt("ArenaMode");
    }

    private void InitPlayer()
    {
        var data = new PlayerData().Load();

        if(data != null)
        {
            _player = data.Fighter;
        }
        else
        {
            Debug.LogError("Player data is null");
        }
    }*/
}
