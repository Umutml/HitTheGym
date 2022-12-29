using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    public Animator playerAnimatior;
    public int playerLevel; // Its updated at the start from game manager
    [SerializeField] public TextMeshProUGUI lvlText;

    void Start()
    {
        playerLevel = GameManager.instance.startLevel;
        lvlText.text = "Lv." + playerLevel.ToString();
        playerAnimatior = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerRunAnimController();
        PlayerKeyboardMove();
        //transform.localScale *= playerLevel * 1f;
    }

    private void PlayerKeyboardMove()
    {
        float axisH = Input.GetAxis("Horizontal");
        if (axisH != 0)
            transform.Translate(Vector3.right * turnSpeed * axisH * Time.deltaTime);
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
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject,0.5f);
            transform.localScale += new Vector3(0.03f, 0, 0);
            transform.localScale += new Vector3(0, 0.015f, 0);
            playerLevel++;
            lvlText.text = "Lv." + playerLevel.ToString();
        }
    }
}
