using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    private GameObject player;
    private int MAXHP;
    private int HP;
    [SerializeField] private float speed;
    public GameObject AnimalPrefab;
    private SpriteRenderer sr;
    private Animator animator;
    public ParticleSystem hairBallParticle;
    public GameObject hitPrefab;

    //int t;//����

    //public int a = 9;//����

    /*��ʼ��˳��
     * 
     * �������
     * ����������縳ֵ
     * awake
     */

    private void Awake()//���ؽű�ʵ��ʱ���ã�ֻ����һ�������ڳ��������ɳ���ʱ���Ż����װ�ڴ������ϵĽű�ʵ��
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();//
        animator = gameObject.GetComponent<Animator>();//
        MAXHP = Random.Range(1, 4);
        HP = MAXHP;
    }

    private void Start()
    {
        sr.sprite = AnimalLib.hairBalls[HP];//
    }

    private void Update()
    {
        animator.speed = TimeController.speedScale;//
        Died();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * TimeController.speedScale * Time.fixedDeltaTime);
            /*Vector3 dir = (player.transform.position - gameObject.transform.position).normalized * (speed * Time.fixedDeltaTime);
            gameObject.transform.position += dir;*/
            if (player.transform.position.x > gameObject.transform.position.x && gameObject.transform.rotation.y == 0)
            {
                gameObject.transform.Rotate(Vector3.up, 180);
            }
            else if (player.transform.position.x < gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.Rotate(Vector3.up, -180);
            }
        }
    }

    public void Damaged(int damage)
    {
        HP -= damage;
        hairBallParticle.Play();//
        sr.sprite = AnimalLib.hairBalls[HP];//
    }

    private void Died()
    {
        if (HP <= 0)
        {
            CameraController.EnemyTarget.transform.position = gameObject.transform.position;
            CameraController.SwitchCamera();//
            Instantiate(AnimalPrefab, CameraController.EnemyTarget.transform.position, Quaternion.identity);
            Instantiate(hitPrefab, CameraController.EnemyTarget.transform.position, Quaternion.identity);
            PlayerController.slashAmount += 1;
            PointCalulater.scissors += 1;//
            PlayerController.slashSum += 1;//
            int point = PlayerController.slashSum * (10 + PlayerController.slashSum) + Random.Range(-2, 2);
            PointPlus.displayText.text = "+" + point.ToString();
            PointPlus.displayExistTimer = 2;
            PointSlider.levelPoint += point;//
            Destroy(gameObject);
            return;
        }
    }
}
