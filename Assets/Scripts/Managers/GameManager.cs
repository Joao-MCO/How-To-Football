using System.Text.RegularExpressions;
using System.Collections;
using System.Timers;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using HTF;

public class GameManager : MonoBehaviour{
    public static GameManager Instance;

    [SerializeField] private TeamSO homeTeam, awayTeam;

    [SerializeField] private int scoreLimit = 5;
    
    public bool gameIsOn = true;

    public int ScoreHome { get; private set; } = 0;

    public int ScoreAway { get; private set; } = 0;

    private GameObject player;

    public int coolTime = 2;

    private float _timeLeftPower, _timeLeftShow;
    private bool _isPower, _isShowing, _isGiant;

    public int cooldownPower = 3;
    public int cooldownShow = 12;

    public GameObject[] poderes;
    public Instantiate[] lugares;
    private int _indexLugares, _powerIndex, _lastIndex;
    public int playerController;

    public GameObject wind, estacaHome, estacaAway;

    public GameObject homePlayer, awayPlayer;

    [FormerlySerializedAs("type")]
    [Header("Game Objects")] 
    [Space] 
    [SerializeField] private GameObject bola;

    [Header("UI Objects")] 
    [Space] 
    [SerializeField] public GameObject gol;
    
    
    private void Awake (){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject, 1f);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        AudioManager.Instance.Play("Apito");
        _timeLeftPower = cooldownPower;
        _timeLeftShow = cooldownShow;
        _isShowing = false;
        _indexLugares = 0;
        _powerIndex =-1;
        _lastIndex = -1;
        _isGiant = false;
        playerController = 0;
    }

    public void Score(MatchSide ms){
        if(ms == MatchSide.Away){
            ScoreHome += 1;
        }else{
            ScoreAway += 1;
        }
        if(_powerIndex == 0 && _isPower) player.transform.localScale = new Vector3(player.transform.localScale.x/2f, player.transform.localScale.y/2f, 1f);
        _timeLeftPower = 0;
        _timeLeftShow = cooldownShow;
        _isShowing = false;
        _indexLugares = 0;
        _powerIndex =-1;
        _lastIndex = -1;
        _isGiant = false;
        playerController = 0;
        estacaHome.SetActive(false);
        estacaAway.SetActive(false);
        wind.SetActive(false);
        StartCoroutine(Gol());
    }

    public void EndGame(String nome){
        gameIsOn = false;     
        UiManager.Instance.InTheEnd(nome);
        Destroy(bola);
        Destroy(UiManager.Instance);
        Destroy(gameObject, 1f);
        return;
    }
    private void Update() {
        UiManager.Instance.OnScore();
        if(ScoreAway == scoreLimit) EndGame(awayTeam.teamName);
        if (ScoreHome == scoreLimit) EndGame(homeTeam.teamName);
        if(Input.GetKeyUp(KeyCode.Escape)){
            if(gameIsOn){
                BotoesManager.Instance.Pause();
                gameIsOn = false;
            }else{
                BotoesManager.Instance.UnPause();
                gameIsOn = true;
            }
        }

        if(!_isShowing){
            _timeLeftShow -= Time.deltaTime;
        }

        if(_timeLeftShow < 0 && !_isShowing){
            _powerIndex = UnityEngine.Random.Range(0, 3);
            while(_lastIndex == _powerIndex || _powerIndex == 3){
                _powerIndex = UnityEngine.Random.Range(0, 2);
            }
            _lastIndex = _powerIndex;
            lugares[_indexLugares].Inicia(poderes[_powerIndex]);
            _isShowing = true;
            _indexLugares++;
            if(_indexLugares > 2) _indexLugares = 0;
            _timeLeftShow = cooldownShow;
        }

        if(_isPower){
            _timeLeftPower -= Time.deltaTime;
        }
        if(_timeLeftPower < 0 && _isPower){
            if(_powerIndex == 0) player.transform.localScale = new Vector3(player.transform.localScale.x/2f, player.transform.localScale.y/2f, 1f);
            else if(_powerIndex == 1){
                estacaHome.SetActive(false);
                estacaAway.SetActive(false);
            }else if(_powerIndex == 2) wind.SetActive(false);
            
            _isGiant = false;
            _isPower = false;
            _timeLeftPower = cooldownPower;
            player = null;
            _isShowing = false;
            playerController = 0;
            return;
        }
    }

    public void Restart(){
        bola.transform.position = new Vector2(0f, 3f);
    }

    IEnumerator Gol(){
        AudioManager.Instance.Play("Gol");
        if(scoreLimit != ScoreHome && scoreLimit != ScoreAway){
            gameIsOn = false;
            Restart();
            bola.GetComponent<Rigidbody2D>().gravityScale = 0f;
            bola.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bola.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            homePlayer.transform.position = new Vector2(-5f,0f);
            awayPlayer.transform.position = new Vector2(5f,0f);
            gol.SetActive(true);
            yield return new WaitForSeconds(coolTime);
            gol.SetActive(false);
            gameIsOn = true;
            bola.GetComponent<Rigidbody2D>().gravityScale = 1f;
            AudioManager.Instance.Play("Apito");
        }
    }

    public void Poder(GameObject player, GameObject power){
        Destroy(power, 1f);
        _timeLeftPower = cooldownPower;
        _isPower = true;
        this.player = player;
        if(player.name.Contains('M')) playerController = 1;
        else playerController = -1;
        if(_powerIndex == 0 && !_isGiant){
            player.transform.localScale = new Vector3(player.transform.localScale.x*2f, player.transform.localScale.y*2f, 1f);
            _isGiant = true;
            Debug.Log("Gigante");
        }
        else if(_powerIndex == 1){
            if(playerController == 1) estacaHome.SetActive(true);
            else estacaAway.SetActive(true);
            Debug.Log("Gelo");

        }else if(_powerIndex == 2){
            wind.SetActive(true);
            Debug.Log("Vento");

        }
        return;
    }
}