using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class BattleController : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    
    public AudioClip audioClipFanfare;
    public AudioClip audioClipBattle;
    public AudioClip audioClipBossBattle;
    public AudioClip audioClipRegular;

    public AudioClip audioClipHeal;
    public AudioClip audioClipHitPlayer;
    public AudioClip audioClipHitMonster;
    public AudioClip audioClipHitBoss;

    public DialogController dialogController;
    
    public CinematicManager cinematicManager;
    
    public GameObject arthur;

    public GameObject arrowPrefab;
    public GameObject arthurPrefab;
    public GameObject lancelotPrefab;
    public GameObject lancelotEnemyPrefab;
    
    public Image buttonAttack;
    public Image buttonDefend;
    public Image buttonHeal;
    public Image buttonApple;
    
    private Random _random = new Random((uint)DateTime.Now.Millisecond);
    
    private List<GameObject> _players = new List<GameObject>();
    private List<Enemy> _enemies = new List<Enemy>();

    private int _activePlayerIndex = -1;
    private GameObject _activePlayer;

    private int _activeEnemyIndex = -1;
    private Enemy _activeEnemy;
    private Tweener _activeEnemyColorTweener;
    
    private bool _isBattleInProgress = false;
    private bool _isPlayerTurn = false;

    private bool _isPlayerActing = false;
    
    public bool isBattleInProgress => _isBattleInProgress;
    
    public GameObject battleMenu;

    private List<Image> _buttons = new List<Image>();
    private List<Image> _activeButtons = new List<Image>();

    private int _selectedButtonIndex = -1;

    private bool _isPlayerTurnFinished = false;

    private int _selectedPlayerIndex = -1;
    private GameObject _selectedPlayer;

    private bool _isSelectingPlayerEnabled = false;

    private bool _isBattleStartedWithLancelot = false;
    private bool _isBattleStartedWithApple = false;
    private bool _isLancelotKillingStarted = false;
    
    private int _aidIsMoving = Animator.StringToHash("IsMoving");
    private int _aidIsAttacking = Animator.StringToHash("IsAttacking");
    
    private void Awake()
    {
        _buttons = new List<Image> {buttonAttack, buttonDefend, buttonHeal, buttonApple};
        _buttons.ForEach(x => x.transform.parent = null);
    }

    private void PrepareButtonsForArthur()
    {
        _buttons.ForEach(x => x.transform.parent = null);
        _activeButtons = new List<Image> { buttonAttack, buttonDefend };
        if (GameState.Instance.HasApple)
        {
            _activeButtons.Add(buttonApple);
        }
        _activeButtons.ForEach(x => x.transform.parent = battleMenu.transform);
        SelectButton(0);
    }
    
    private void PrepareButtonsForLancelot()
    {
        _buttons.ForEach(x => x.transform.parent = null);
        _activeButtons = new List<Image> { buttonHeal, buttonDefend };
        _activeButtons.ForEach(x => x.transform.parent = battleMenu.transform);
        SelectButton(0);
    }    

    private void SelectNextButton()
    {
        _selectedButtonIndex++;
        if (_selectedButtonIndex == _activeButtons.Count)
        {
            _selectedButtonIndex = 0;
        }
        SelectButton(_selectedButtonIndex);
    }

    private void SelectPrevButton()
    {
        _selectedButtonIndex--;
        if (_selectedButtonIndex == -1)
        {
            _selectedButtonIndex = _activeButtons.Count - 1;
        }
        SelectButton(_selectedButtonIndex);
    }

    private void ButtonClick()
    {
        if (_activeButtons[_selectedButtonIndex] == buttonAttack)
        {
            StartCoroutine(Attack());
        }
        else if (_activeButtons[_selectedButtonIndex] == buttonDefend)
        {
            Defend();
        }
        else if (_activeButtons[_selectedButtonIndex] == buttonHeal)
        {
            StartCoroutine(Heal());
        }
        else if (_activeButtons[_selectedButtonIndex] == buttonApple)
        {
            StartCoroutine(EatApple());
        }
    }

    private IEnumerator Attack()
    {
        _isPlayerActing = true;

        var selectedEnemy = _enemies.First();

        var positionPrev = _activePlayer.transform.position;
        
        var animator = _activePlayer.GetComponent<Animator>();
        
        animator.SetBool(_aidIsMoving, false);
        animator.SetBool(_aidIsAttacking, true);
        
        yield return _activePlayer.transform.DOMove(selectedEnemy.transform.position, Constants.SpeedAttack).SetSpeedBased().WaitForCompletion();
        
        audioSourceSound.clip = audioClipHitPlayer;
        audioSourceSound.Play();
        
        selectedEnemy.Damage(GameState.Instance.ArthurDamage);

        if (selectedEnemy.health == 0)
        {
            _enemies.Remove(selectedEnemy);
            Destroy(selectedEnemy.gameObject);
        }
        
        yield return _activePlayer.transform.DOMove(positionPrev, Constants.SpeedAttack).SetSpeedBased().WaitForCompletion();
        
        animator.SetBool(_aidIsMoving, false);
        animator.SetBool(_aidIsAttacking, false);

        
        _isPlayerActing = false;
        
        _isPlayerTurnFinished = true;
    }

    private void Defend()
    {
        _isPlayerTurnFinished = true;
    }

    private IEnumerator Heal()
    {
        yield return SelectPlayerForHeal();

        var animator = _activePlayer.GetComponent<Animator>();
        
        animator.SetBool(_aidIsMoving, false);
        animator.SetBool(_aidIsAttacking, true);

        yield return new WaitForSeconds(0.5f);
        
        if (_selectedPlayerIndex == 0)
        {
            GameState.Instance.HealArthur(GameState.Instance.LancelotHeal); 
        }
        else
        {
            GameState.Instance.HealLancelot(GameState.Instance.LancelotHeal);
        }

        audioSourceSound.clip = audioClipHeal;
        audioSourceSound.Play();
        
        animator.SetBool(_aidIsAttacking, false);        
        
        _isPlayerTurnFinished = true;
    }

    private IEnumerator EatApple()
    {
        GameState.Instance.FullHealArthur();
        
        _isPlayerTurnFinished = true;

        yield return null;
    }

    private IEnumerator SelectPlayerForHeal()
    {
        // HACK: Drop input.
        yield return null;
        
        _isSelectingPlayerEnabled = true;

        GameObject gameObjectArrow = Instantiate(arrowPrefab);
        
        _selectedPlayerIndex = 0;
        _selectedPlayer = _players[_selectedPlayerIndex];
        
        var selectedPlayerBounds = _selectedPlayer.GetComponent<SpriteRenderer>().bounds;
        gameObjectArrow.transform.position = new Vector2(selectedPlayerBounds.center.x, selectedPlayerBounds.center.y + selectedPlayerBounds.extents.y);
        
        if (_players.Count > 1)
        {
            bool isSelectionFinished = false;
            while (!isSelectionFinished)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isSelectionFinished = true;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _selectedPlayerIndex++;
                    if (_selectedPlayerIndex >= _players.Count)
                    {
                        _selectedPlayerIndex = 0;
                    }

                    _selectedPlayer = _players[_selectedPlayerIndex];
                    
                    selectedPlayerBounds = _selectedPlayer.GetComponent<SpriteRenderer>().bounds;
                    gameObjectArrow.transform.position = new Vector2(selectedPlayerBounds.center.x, selectedPlayerBounds.center.y + selectedPlayerBounds.extents.y);                    
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _selectedPlayerIndex--;
                    if (_selectedPlayerIndex < 0)
                    {
                        _selectedPlayerIndex = _players.Count - 1;
                    }
                    
                    _selectedPlayer = _players[_selectedPlayerIndex];
                    
                    selectedPlayerBounds = _selectedPlayer.GetComponent<SpriteRenderer>().bounds;
                    gameObjectArrow.transform.position = new Vector2(selectedPlayerBounds.center.x, selectedPlayerBounds.center.y + selectedPlayerBounds.extents.y);                    
                }
                
                yield return null;
            }
        }

        Destroy(gameObjectArrow);
        
        _isSelectingPlayerEnabled = false;
    }
    
    private void SelectButton(int buttonIndex)
    {
        _buttons.ForEach(x => x.color = Color.white);
        _selectedButtonIndex = buttonIndex;
        _activeButtons[_selectedButtonIndex].color = Color.red;
    }
    
    public void StartBattle(List<Enemy> enemies, bool isStartedByMonster)
    {
        StartCoroutine(StartBattleCoroutine(null, enemies, isStartedByMonster));
    }

    public void StartBattle(GameObject gameObjectLancelot, List<Enemy> enemies, bool isStartedByMonster)
    {
        StartCoroutine(StartBattleCoroutine(gameObjectLancelot, enemies, isStartedByMonster));
    }
    
    private IEnumerator StartBattleCoroutine(GameObject gameObjectLancelot, List<Enemy> enemies, bool isStartedByMonster)
    {
        audioSourceMusic.Stop();
        
        if (isStartedByMonster)
        {
            audioSourceMusic.clip = audioClipBattle;
        }
        else
        {
            audioSourceMusic.clip = audioClipBossBattle;
        }

        audioSourceMusic.Play();
            
        _isBattleStartedWithLancelot = GameState.Instance.LancelotHealth > 0;
        _isBattleStartedWithApple = GameState.Instance.HasApple;
            
        Reset();
        
        _isBattleInProgress = true;
        
        _enemies = enemies;
        
        cinematicManager.StartCinematic();
        
        yield return SpawnAllies(gameObjectLancelot);
        
        cinematicManager.StopCinematic();

        while (_isBattleInProgress)
        {
            _activePlayerIndex++;
            if (_activePlayerIndex >= _players.Count)
            {
                _activePlayerIndex = 0;
            }
            _activePlayer = _players[_activePlayerIndex];

            if (_activePlayerIndex == 0)
            {
                PrepareButtonsForArthur();
            }
            else
            {
                PrepareButtonsForLancelot();
            }
            
            _isPlayerTurn = true;
            
            yield return PlayerTurn();

            _isPlayerTurn = false;

            _selectedPlayerIndex = -1;
            _selectedPlayer = null;
            
            if (_enemies.Count > 0)
            {
                _activeEnemyIndex++;
                if (_activeEnemyIndex >= _enemies.Count)
                {
                    _activeEnemyIndex = 0;
                }

                _activeEnemy = _enemies[_activeEnemyIndex];

                yield return EnemyTurn(isStartedByMonster);

                if (GameState.Instance.ArthurHealth == 0)
                {
                    if (isStartedByMonster)
                    {
                        GameState.Instance.ending = Ending.A;
                    }
                    else if (_isBattleStartedWithApple && _isBattleStartedWithLancelot)
                    {
                        GameState.Instance.ending = Ending.B;
                    }
                    else if (!_isBattleStartedWithApple)
                    {
                        GameState.Instance.ending = Ending.F;
                    }
                    else if (!_isBattleStartedWithLancelot)
                    {
                        GameState.Instance.ending = Ending.D;
                    }

                    SceneManager.LoadScene("End");
                }
            }
            else
            {
                if (GameState.Instance.ArthurHasExcalibur && GameState.Instance.LancelotHealth > 0)
                {
                    GameState.Instance.KillLancelot();
                    
                    _enemies.ForEach(x => Destroy(x.gameObject));
                    _enemies.Clear();

                    var lancelot = _players[1];
                    _players.RemoveAt(1);
                    var lancelotPosition = lancelot.transform.position;
                    Destroy(lancelot);

                    var lancelotEnemy = Instantiate(lancelotEnemyPrefab);
                    lancelotEnemy.transform.position = lancelotPosition;
                    _enemies.Add(lancelotEnemy.GetComponent<Enemy>());

                    _isLancelotKillingStarted = true;

                    yield return dialogController.Show(DialogLineAvatar.Arthur, "Arthur", "It demands blood!");
                    yield return dialogController.Show(DialogLineAvatar.Lancelot, "Lancelot", "Arthur, stop!");
                }
                else
                {
                    if (_isLancelotKillingStarted)
                    {
                        GameState.Instance.ending = Ending.E;
                        SceneManager.LoadScene("End");
                    }
                    else
                    {
                        yield return FinishBattle();
                    }
                }
            }
        }
    }

    private IEnumerator FinishBattle()
    {
        audioSourceMusic.Stop();
        audioSourceMusic.clip = audioClipFanfare;
        audioSourceMusic.Play();
        
        yield return new WaitForSeconds(3.0f);
        
        cinematicManager.StartCinematic();

        if (_players.Count > 1)
        {
            var animator = _players[1].GetComponent<Animator>();
            
            animator.SetBool(_aidIsMoving, true);
            animator.SetBool(_aidIsAttacking, false);
            
            yield return _players[1].transform.DOMove(_players[0].transform.position, Constants.SpeedWalk).SetSpeedBased().WaitForCompletion();
            
            animator.SetBool(_aidIsMoving, false);
            animator.SetBool(_aidIsAttacking, false);            
        }
                
        var arthurPosition = _players[0].transform.position;
                
        cinematicManager.StopCinematic();
                
        Reset();
                
        _isBattleInProgress = false;

        arthur.SetActive(true);
        arthur.transform.position = arthurPosition;
        
        audioSourceMusic.Stop();
        audioSourceMusic.clip = audioClipRegular;
        audioSourceMusic.Play();        
    }

    private void Reset()
    {
        _enemies.ForEach(x => Destroy(x.gameObject));
        _enemies.Clear();
        
        _players.ForEach(x => Destroy(x));
        _players.Clear();
        
        _activePlayer = null;
        _activePlayerIndex = -1;
        
        _activeEnemy = null;
        _activeEnemyIndex = -1;

        _isPlayerActing = false;
        _isPlayerTurn = false;
        
        _isPlayerTurnFinished = false;

        _isSelectingPlayerEnabled = false;

        _selectedPlayerIndex = -1;
        _selectedPlayer = null;
    }

    private IEnumerator PlayerTurn()
    {
        _isPlayerTurnFinished = false;
        
        while (!_isPlayerTurnFinished)
        {
            yield return null;
        }
    }

    private IEnumerator EnemyTurn(bool isMonster)
    {
        // Special case for the Lancelot battle.
        if (_activeEnemy.damage == 0)
        {
            yield break;
        }
        
        var positionPrev = _activeEnemy.transform.position;

        GameObject targetPlayer;

        if (GameState.Instance.ArthurHasExcalibur)
        {
            targetPlayer = _players.First();
        }
        else
        {
            targetPlayer = _players[_random.NextInt(0, _players.Count)];   
        }

        var animator = _activeEnemy.GetComponent<Animator>();
        animator.SetBool(_aidIsMoving, false);
        animator.SetBool(_aidIsAttacking, true);
        
        yield return _activeEnemy.transform.DOMove(targetPlayer.transform.position, Constants.SpeedAttack).SetSpeedBased().WaitForCompletion();

        if (isMonster)
        {
            audioSourceSound.clip = audioClipHitMonster;
        }
        else
        {
            audioSourceSound.clip = audioClipHitBoss;
        }

        audioSourceSound.Play();
        
        if (targetPlayer == _players[0])
        {
            GameState.Instance.DamageArthur(_activeEnemy.damage);
        }
        else
        {
            GameState.Instance.DamageLancelot(_activeEnemy.damage);
        }
        
        yield return _activeEnemy.transform.DOMove(positionPrev, Constants.SpeedAttack).SetSpeedBased().WaitForCompletion();
        
        animator.SetBool(_aidIsAttacking, false);
    }
    
    private IEnumerator SpawnAllies(GameObject gameObjectLancelotPredefined)
    {
        var arthurPosition = arthur.transform.position;

        if (GameState.Instance.LancelotHealth > 0)
        {
            var battleSpot = FindObjectsOfType<BattleSpot>().Where(x => !x.GetComponent<BattleSpot>().IsTouching)
                .OrderBy(x => _random.NextInt()).First();

            GameObject gameObjectLancelot;

            if (gameObjectLancelotPredefined != null)
            {
                gameObjectLancelot = gameObjectLancelotPredefined;
            }
            else
            {
                gameObjectLancelot = Instantiate(lancelotPrefab);
                gameObjectLancelot.transform.position = arthurPosition;
                var tweenerLancelot = gameObjectLancelot.transform.DOMove(battleSpot.transform.position, Constants.SpeedRun)
                    .SetSpeedBased();

                var animator = gameObjectLancelot.GetComponent<Animator>();
                animator.SetBool(_aidIsMoving, true);
                animator.SetBool(_aidIsAttacking, false);
                
                yield return tweenerLancelot.WaitForCompletion();
                
                animator.SetBool(_aidIsMoving, false);
                animator.SetBool(_aidIsAttacking, false);                
            }
            
            _players.Add(gameObjectLancelot);
        }
        

        arthur.SetActive(false);
        
        GameObject gameObjectArthur = Instantiate(arthurPrefab);
        gameObjectArthur.transform.position = arthurPosition;
        
        _players.Insert(0, gameObjectArthur);
    }

    private void Update()
    {
        battleMenu.SetActive(_isBattleInProgress && _isPlayerTurn && !_isPlayerActing && !dialogController.IsDialogInProgress);

        if (_isPlayerTurn && !_isPlayerActing && !_isSelectingPlayerEnabled && !dialogController.IsDialogInProgress)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectPrevButton();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextButton();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                ButtonClick();
            }
        }
    }
}
