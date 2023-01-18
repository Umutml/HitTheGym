using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using DG.Tweening;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] public int enemyLevel;
    Animator enemyAnimator;
    PlayerMovement playerScript;
    [SerializeField] TextMeshProUGUI enemylvlText;
    [SerializeField] private bool isBoss;
    [SerializeField] private float tweenForceTime;
    [SerializeField] private float tweenForceZ;
    [SerializeField] private float TweenjumpForce;
    private float scaleMultiplier = 0.008f;
    CinemachineCameraOffset cinemachineOffset;

    void Start()
    {
        cinemachineOffset = GameObject.FindObjectOfType<CinemachineCameraOffset>();
        transform.localScale = ScaleCalculate(enemyLevel);
        enemyAnimator = GetComponent<Animator>();
        enemylvlText.text = "Lv." + enemyLevel.ToString();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyLevel <= playerScript.playerLevel)
        {
            if (isBoss)
            {
                playerScript.playerAnimatior.SetTrigger("combatHit");
                other.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(WaitForTween());                         // Force Tween after .2f
                transform.DOJump(transform.position + Vector3.forward * tweenForceZ, TweenjumpForce, 1, tweenForceTime);
                enemyAnimator.SetTrigger("enemyDead");
                GameManager.instance.isgameStarted = false;
                // GO NEXT LEVEL AFTER SOME TIME
                // ADD RAGDOLL MAYBE
                // WIN LEVEL GO NEXT
                //IF BOSS LEVEL BETTER THAN PLAYER WE ARE DOING NOTHING NOW FIX THIS
                return;
            }
            
            playerScript.playerAnimatior.SetTrigger("combatHit");
            gameObject.transform.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponent<AudioSource>().Play();        // PLAY PLAYER HIT ANIM - HIT SOUND - ENEMY DEAD ANIM
            
            enemyAnimator.SetTrigger("enemyDead");
            this.tag = "DeadEnemy";
            StartCoroutine(WaitForTween());
            
            cinemachineOffset.m_Offset.y += 0.5f;
            cinemachineOffset.m_Offset.z -= 0.5f;
            
            playerScript.playerLevel += enemyLevel;
            playerScript.lvlText.text = "Lv." + playerScript.playerLevel.ToString();        // UPDATING PLAYER LVL AND TEXT
            enemyLevel = 0;
            enemylvlText.text = "Lv." + enemyLevel.ToString();              // Enemy LVL 0 AFTER DEATH
        }
        else if (other.CompareTag("Player") && enemyLevel > playerScript.playerLevel)
        {
            GameManager.instance.isGameOver = true;
            // CAM SHAKE
            enemyAnimator.SetTrigger("Shoot");
            GetComponent<AudioSource>().Play();                         //ENEMY SHOOT SOUND AND PLAYER DEAD ANIM GAME OVER!
            playerScript.playerAnimatior.SetTrigger("playerDead");
            //Game Over
        }
        IEnumerator WaitForTween()
        {
            yield return new WaitForSeconds(0.2f);
            transform.DOJump(transform.position + new Vector3(Random.Range(-1, 2), 0, 1) * tweenForceZ, TweenjumpForce, 1, tweenForceTime).OnComplete(() => { Destroy(gameObject); });
        }
    }
    private Vector3 ScaleCalculate(int level)
    {
        float enemyScale = 1 + level * scaleMultiplier;
        enemyScale = Mathf.Clamp(enemyScale, 1, 3.5f);
        Vector3 scale = enemyScale * Vector3.one;
        return scale;
    }

}
