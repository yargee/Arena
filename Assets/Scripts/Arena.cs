using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arena : MonoBehaviour
{
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

    private void OnTargetsUnavailable()
    {
        WinnerFound = true;
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
            player.gameObject.SetActive(true);
            _fightersNumber--;
        }
        

        for(int i = 0; i < _fightersNumber; i++)
        {
            var fighter = _availableFighters[Random.Range(0, _availableFighters.Count)];

            if(!_chosenFighters.Contains(fighter))
            {
                _chosenFighters.Add(fighter);
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
            _player = data.View;
        }
        else
        {
            Debug.LogError("Player data is null");
        }
    }
}

public class AttackSequence
{
    private Fighter _attacker;
    private Fighter _defender;

    public AttackSequence(Fighter attacker, Fighter defender)
    {
        _attacker = attacker;
        _defender = defender;
    }

    public void AttackPhase(ref Log log, out int outcomingDamage)
    {
        outcomingDamage = 0;

        if (_attacker.Defeated || _defender.Defeated) return;

        if (CombatCalculator.IsAttackSuccessfull(_attacker.Weapon, _attacker.Characteristics.DexterityModifier))
        {
            outcomingDamage = CombatCalculator.CalculateBaseDamage(_attacker.Weapon, _attacker.Characteristics.StrenghModifier);
            log.UpdateAttackLog(_attacker.Name, ConstantKeys.AttackStatus.Hit, outcomingDamage);

            if (CombatCalculator.IsCriticalStrike(_attacker.Weapon, _attacker.Characteristics.DexterityModifier))
            {
                outcomingDamage = CombatCalculator.CalculateCriticalDamage(_attacker.Weapon, outcomingDamage);
                log.UpdateAttackLog(_attacker.Name, ConstantKeys.AttackStatus.CriticalHit, outcomingDamage);
            }
        }
        else
        {
            outcomingDamage = 0;
            _defender.SetAnimation(ConstantKeys.Animations.Block);
            log.UpdateAttackLog(_attacker.Name);
        }
    }

    public void DefencePhase(ref Log log, int incomingDamage, out int finalDamage)
    {
        finalDamage = 0;

        if (_defender.Defeated)
        {
            _attacker.LoseTarget();
            log.UpdateDefenceLog(_defender.Name, 0, ConstantKeys.DefenceStatus.Dead);
            return;
        }

        if (CombatCalculator.IsAttackEvaded(_defender.Characteristics.DexterityModifier))
        {
            log.UpdateDefenceLog(_defender.Name);
            finalDamage = 0;
            _defender.SetAnimation(ConstantKeys.Animations.Block);
            return;
        }

        if (_defender.Weapon.TwoHanded && CombatCalculator.IsAttackParried(_defender.Weapon, _defender.Characteristics.StrenghModifier))
        {
            finalDamage = CombatCalculator.CalculateParriedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.Parry);
            return;
        }

        if(!_defender.Weapon.TwoHanded && CombatCalculator.IsAttackBlocked(_defender.Characteristics.StrenghModifier))
        {
            finalDamage = CombatCalculator.CalculateBlockedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.Block);
            _defender.SetAnimation(ConstantKeys.Animations.Block);
            return;
        }

        finalDamage = incomingDamage;
        log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.FullDamage);
    }

    public void ApproveDamagePhase(int damage)
    {
        _defender.TakeDamage(damage - _defender.Characteristics.ConstitutionModifier);

        if (_defender.Defeated)
        {
            _attacker.LoseTarget();
            return;
        }
    }
}
