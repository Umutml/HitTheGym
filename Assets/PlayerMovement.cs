using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimatior;
    public int playerLevel; // Its updated at the start from game manager
    [SerializeField] public TextMeshProUGUI lvlText;
    [SerializeField] float scaleMultiplier;

    void Start()
    {
        playerLevel = GameManager.instance.startLevel;
        lvlText.text = "Lv." + playerLevel.ToString();
        playerAnimatior = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerRunAnimController();
    }

    private void PlayerRunAnimController()
    {
        if (GameManager.instance.isgameStarted)
            playerAnimatior.SetBool("isRunning", true);
        else
            playerAnimatior.SetBool("isRunning", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.localScale = ScaleCalculate(playerLevel);
        
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject,0.5f);
            playerLevel++;
            lvlText.text = "Lv." + playerLevel.ToString();
        }
        if (other.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            //Diamond++
            //DotweenAnimateUI
        }
    }

    private Vector3 ScaleCalculate(int level)
    {
        float playerScale = 1 + level * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 1, 3f);
        Vector3 scale = playerScale * Vector3.one;
        return scale;
    }
}
