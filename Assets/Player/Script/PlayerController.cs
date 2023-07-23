using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int MAXHP = 5;
    public static int HP;
    public static int slashAmount;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CapsuleCollider2D cb;
    private BoxCollider2D bb;
    public float moveSpeed;

    private bool isSlashing = false;
    public GameObject slashPointPrefab;
    private Queue<GameObject> slashPointQueue = new Queue<GameObject>();
    private GameObject targetNow;
    public float slashSpeed;
    public static int slashSum;

    public GameObject slashTailPrefab;
    private Vector3 startPos;
    private bool isSlashTailExisted;
    private GameObject slashTail;
    public GameObject dashVFXPrefab;
    public GameObject zone;

    private float damageTimer;
     
    public GameObject display;
    public Text title;

    public Animator animator;

    public static bool gameOver;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<CapsuleCollider2D>();
        bb = GetComponent<BoxCollider2D>();
        HP = MAXHP;
        slashAmount = 5;
        gameOver = false;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("Game Over");
            gameOver = true;
            title.text = "血量没有了!";
            display.SetActive(true);
            Destroy(gameObject);
            return;
        }

        animator.speed = TimeController.speedScale;

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        if (isSlashing == false)
        {
            cb.enabled = true;
            bb.enabled = true;
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
            {
                slashSum = 0;
                rb.velocity = new Vector2(0, 0);
                TimeController.speedScale = 0.1f;
                PrepareToSlash();
                if (zone.activeSelf == false)
                {
                    zone.transform.position = gameObject.transform.position;
                    zone.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isSlashing = true;
                    startPos = gameObject.transform.position;
                    zone.SetActive(false);
                    if (slashPointQueue.Count != 0)
                    {
                        targetNow = slashPointQueue.Peek();
                        GameObject _dash = Instantiate(dashVFXPrefab, gameObject.transform.position, Quaternion.identity);
                        Vector3 vec = targetNow.transform.position - gameObject.transform.position;
                        if (vec.x > 0)
                        {
                            _dash.transform.Rotate(Vector3.up, 180);
                        }
                    }
                    TimeController.speedScale = 1f;
                }
            }
            else
            {
                PlayerMove();
            }
        }
        else
        {
            cb.enabled = false;
            bb.enabled = false;
            rb.velocity = new Vector2(0, 0);
            Slash();
        }
    }

    private void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
            if (moveX > 0 && gameObject.transform.rotation.y == 0)
            {
                gameObject.transform.Rotate(Vector3.up, 180);
            }
            else if (moveX < 0 && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.Rotate(Vector3.up, -180);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void PrepareToSlash()
    {
        if (Input.GetMouseButtonUp(0) && slashAmount > 0)
        {
            slashAmount -= 1;
            GameObject _slashPoint = Instantiate(slashPointPrefab);
            _slashPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _slashPoint.transform.position = new Vector3(_slashPoint.transform.position.x, _slashPoint.transform.position.y, 0);
            slashPointQueue.Enqueue(_slashPoint);
        }
    }

    private void Slash()
    {
        if (slashPointQueue.Count != 0)
        {
            targetNow = slashPointQueue.Peek();
        }
        else
        {
            if (slashAmount <= 0)
            {
                Debug.Log("Game Over");
                gameOver = true;
                title.text = "剪刀花光了!";
                display.SetActive(true);
                Destroy(gameObject);
                return;
            }
            isSlashing = false;
            return;
        }
        Vector3 vec = targetNow.transform.position - gameObject.transform.position;
        if (vec.x > 0 && gameObject.transform.rotation.y == 0)
        {
            gameObject.transform.Rotate(Vector3.up, 180);
        }
        else if (vec.x < 0 && gameObject.transform.rotation.y != 0)
        {
            gameObject.transform.Rotate(Vector3.up, -180);
        }
        if (targetNow != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetNow.transform.position, slashSpeed * TimeController.speedScale * Time.deltaTime);
            
            if (isSlashTailExisted == false)
            {
                isSlashTailExisted = true;
                slashTail = Instantiate(slashTailPrefab);
                slashTail.transform.position = startPos;
                float rotZ = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                slashTail.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
            else
            {
                slashTail.transform.localScale = new Vector3((gameObject.transform.position - startPos).magnitude, slashTail.transform.localScale.y, slashTail.transform.localScale.z);
            }
        }
        //Debug.Log(vec.magnitude);
        if (vec.magnitude < 0.15f)
        {
            //Debug.Log("已到达");
            GameObject _slashPoint = slashPointQueue.Dequeue();
            slashTail.transform.localScale = new Vector3((targetNow.transform.position - startPos).magnitude, slashTail.transform.localScale.y, slashTail.transform.localScale.z);
            slashTail.GetComponent<SlashTail>().Fade();
            startPos = _slashPoint.transform.position;
            isSlashTailExisted = false;
            Destroy(_slashPoint);
        }
    }

    public void Damaged(int damage)
    {
        if (damageTimer <= 0)
        {
            HP -= damage;
            damageTimer = 2f;
            HeartContorller.SetHeart(HP);
            StartCoroutine(flash());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Damaged(1);
        }
    }

    IEnumerator flash()
    {
        while (damageTimer > 0)
        {
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.8f);
            yield return new WaitForSeconds(0.2f);
        }
        sr.color = new Color(1, 1, 1, 1);
    }
}
