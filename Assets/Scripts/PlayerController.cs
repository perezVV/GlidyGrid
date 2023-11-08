using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform movePoint;
    private float moveOffset = 2.5f;

    [SerializeField] private GameObject playerMesh;

    [SerializeField] private LayerMask collisionLayer;

    private Vector3 newCameraPos;

    [SerializeField] private GameObject gameOverText;


    [SerializeField] private GameObject[] healthPoints;
    private int healthLeft = 3;
    private bool playerHit = false;
    public bool isNotFullHealth = false;

    [SerializeField] private GameObject coinUI;
    [SerializeField] private TextMeshProUGUI coinText;
    public int coinAmt;

    [Header("SFX")] 
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip hurt;
    

    private bool playOnce = true;
    
    void Start()
    {
        coinAmt = 0;
        rb = GetComponent<Rigidbody>();
        movePoint = GameObject.FindGameObjectWithTag("MovePoint").transform;
        movePoint.parent = null;
        newCameraPos = Camera.main.transform.position;

        // healthPoints = GameObject.FindGameObjectsWithTag("Health");
    }

    private bool pressOnce = true;
    void Update()
    {
        if (playerHit)
        {
            return;
        }
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(movement.x) == 1f && pressOnce)
        {
            Collider[] colliders =
                Physics.OverlapSphere(movePoint.position + new Vector3(moveOffset * movement.x, 0f, 0f), 0.2f,
                    collisionLayer);
            if (colliders.Length == 0)
            {
                movePoint.position += new Vector3(moveOffset * movement.x, 0f, 0f);
                newCameraPos += new Vector3(moveOffset * movement.x, 0f, 0f);
                pressOnce = false;
            }
        }
        if (movement == Vector3.zero)
        {
            pressOnce = true;
        }
        if (Mathf.Abs(movement.z) == 1f && pressOnce)
        {
            Collider[] colliders =
                Physics.OverlapSphere(movePoint.position + new Vector3(0f, 0f, moveOffset * movement.z), 0.2f,
                    collisionLayer);
            if (colliders.Length == 0)
            {
                movePoint.position += new Vector3(0f, 0f, moveOffset * movement.z);
                newCameraPos += new Vector3(0f, 0f, moveOffset * movement.z);
                pressOnce = false;
            }
        }

        Vector3 z = Vector3.zero;
        Vector3 z2 = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, movePoint.position, ref z, 0.02f);
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position,
            newCameraPos, ref z2, 0.05f);

        Rotate();
        if (movement != Vector3.zero && playOnce)
        {
            SFXController.instance.PlaySFX(move, transform, 0.02f);
            playOnce = false;
        }
        else if (movement == Vector3.zero)
        {
            playOnce = true;
        }
    }

    void Rotate()
    {
        if (movement.x > 0)
        {
            playerMesh.GetComponent<Animator>().CrossFade("right", 0.05f);
        }
        else if (movement.x < 0)
        {
            playerMesh.GetComponent<Animator>().CrossFade("left", 0.05f);
        }
        else if (movement.z > 0)
        {
            playerMesh.GetComponent<Animator>().CrossFade("up", 0.05f);
        }
        else if (movement.z < 0)
        {
            playerMesh.GetComponent<Animator>().CrossFade("down", 0.05f);
        }
    }

    public void GotHit(float cooldown)
    {
        CameraShake.Shake(1f, 2f);
        IEnumerator coroutine = HitCooldown(cooldown);
        StartCoroutine(coroutine);
        StartCoroutine("DestroyHeartUI");
        isNotFullHealth = true;
        if (healthLeft == 0)
        {
            gameOverText.GetComponent<TextMeshProUGUI>().enabled = true;
            gameOverText.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator HitCooldown(float cooldown)
    {
        playerHit = true;
        SFXController.instance.PlaySFX(hurt, transform, 0.1f);
        yield return new WaitForSeconds(cooldown);
        playerHit = false;
    }

    private IEnumerator DestroyHeartUI()
    {
        GameObject destroyedHeart = healthPoints[healthLeft - 1];
        destroyedHeart.GetComponent<Animator>().CrossFade("deathUIHeart", 0.2f);
        healthLeft -= 1;
        yield return new WaitForSeconds(destroyedHeart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        destroyedHeart.SetActive(false);
    }

    public IEnumerator GainHeart()
    {
        GameObject newHeart = healthPoints[healthLeft];
        newHeart.SetActive(true);
        newHeart.GetComponent<Animator>().Play("spawnUIHeart");
        yield return new WaitForSeconds(1f);
        float currentTime = healthPoints[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        newHeart.GetComponent<Animator>().CrossFade("idleUIHeart", 1f, 0, currentTime);
        healthLeft += 1;
        if (healthLeft == 3)
        {
            isNotFullHealth = false;
        }
    }

    public IEnumerator GainCoin()
    {
        float currentTime = coinUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        coinUI.GetComponent<Animator>().Play("newcoinUICoin");
        coinAmt += 1;
        coinText.text = coinAmt.ToString();
        yield return new WaitForSeconds(0.167f);
        coinUI.GetComponent<Animator>().Play("idleUICoin", 0, currentTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
