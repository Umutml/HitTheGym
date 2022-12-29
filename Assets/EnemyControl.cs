using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] public int enemyLevel;
    Animator enemyAnimator;
    PlayerMovement playerScript;
    [SerializeField] TextMeshProUGUI enemylvlText;
    [SerializeField] private bool isBoss;
    [SerializeField]private float tweenForceTime;
    [SerializeField]private float tweenForceZ;
    [SerializeField]private float TweenjumpForce;
    private Rigidbody enemyRb;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemylvlText.text = "Lv." + enemyLevel.ToString();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && enemyLevel <= playerScript.playerLevel)
        {
            if (isBoss)
            {
                playerScript.playerAnimatior.SetTrigger("combatHit");
                other.gameObject.GetComponent<AudioSource>().Play();
                transform.DOJump(transform.position + Vector3.forward * tweenForceZ, TweenjumpForce, 1, tweenForceTime).OnComplete(()=>{ Destroy(gameObject); });
                enemyAnimator.SetTrigger("enemyDead");
                GameManager.instance.isgameStarted = false;
                // GO NEXT LEVEL AFTER SOME TIME
                // ADD RAGDOLL MAYBE
                // WIN LEVEL GO NEXT
                return;
            }
            
            playerScript.playerAnimatior.SetTrigger("combatHit");
            other.gameObject.GetComponent<AudioSource>().Play();        // PLAY PLAYER HIT ANIM - HIT SOUND - ENEMY DEAD ANIM
            enemyAnimator.SetTrigger("enemyDead");
            transform.DOJump(transform.position + new Vector3(Random.Range(-1,2),0,1) * tweenForceZ, TweenjumpForce, 1, tweenForceTime).OnComplete(() => { Destroy(gameObject); });
            playerScript.playerLevel += enemyLevel;
            playerScript.transform.localScale *= 1.05f;
            playerScript.lvlText.text = "Lv." + playerScript.playerLevel.ToString();        // UPDATING PLAYER LVL AND TEXT
            enemyLevel = 0;
            enemylvlText.text = "Lv." + enemyLevel.ToString();              // Enemy LVL 0 AFTER DEATH
            Destroy(gameObject, 1f);
            this.tag = "DeadEnemy";
        }
        else if (other.CompareTag("Player") && enemyLevel > playerScript.playerLevel)
        {
            GameManager.instance.isGameOver = true;
            enemyAnimator.SetTrigger("Shoot");
            GetComponent<AudioSource>().Play();                         //ENEMY SHOOT SOUND AND PLAYER DEAD ANIM GAME OVER!
            playerScript.playerAnimatior.SetTrigger("playerDead");
            //Game Over
        }
    }

}
