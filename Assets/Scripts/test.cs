using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public static test instance;

    public SimpleTouchController leftController;
    //public GameObject hpSlider;
    //public GameObject joyStick;

    public int hp;
    public float speed;
    public float jumpForce;
    public float pullForce;
    public float bounceForce;
    public float distance;
    private Collider2D coll;
    private Rigidbody2D rigi;
    private Animator animator;
    public AudioSource bgmAudioSource;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    //public bool isGround;

    public int faceDirection = 1;
    private int bounceDirection;
    private int playAudioTimes = 1;
    public bool isJump = false;
    private bool isCrouch = false;
    private bool isFall = false;
    private bool canJump;
    public bool isTouchEnemy;
    //private bool oneBounce = false;

    public bool joyStickUp = false;
    public bool canControlJoyStick = true;


    public Transform hpSpriteTransform;
    public GameObject hpPrefab;
    public List<GameObject> hpSpriteList = new List<GameObject>();

    [Header("Dash参数")]
    public float dashTime;//dash时间
    private float dashTimeLeft;//dash剩余时间
    private float lastDashTime = -10;//上次冲锋的时间
    public float dashCoolDown;
    public float dashSpeed;
    public bool isDashing;
    public Image cdImage;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        //instanceHp();
        rigi = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        bgmAudioSource = GetComponent<AudioSource>();

    }


    void FixedUpdate()
    {
        if (isTouchEnemy)
        {
            colliderEnemyBounce();
            //isTouchEnemy = false;
        }
        if (!GameManager.instance.isGameOver&&canControlJoyStick)
        {
            movement();
        }

        //trampleBounce();

        jump();
        crouch();

    }

    private void Update()
    {
        dash();
        if (isDashing)
            return;

        if (GameManager.instance.isGameOver)
        {
            isCrouch = false;
            isTouchEnemy = true;
        }
        //hpSlider.GetComponent<Slider>().value = hp;

        touchEnemy();
        preventShake();

        float h = leftController.GetTouchPosition.x;

        if (h > 0)
        {
            faceDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }else if (h < 0)
        {
            faceDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        judgeJump();

        oneJump();

        animatorController();

        enemyDirection();

        trampleBounce();

        cdImage.fillAmount += 1 / dashCoolDown * Time.deltaTime;

    }

    //判断是否是在地面
    bool isGround()
    {
        if(Physics2D.Raycast(transform.Find("leftDown").transform.position, Vector2.down, distance, groundLayer)|| Physics2D.Raycast(transform.Find("rightDown").transform.position, Vector2.down, distance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void touchEnemy()
    {
        if (Physics2D.Raycast(transform.Find("left").transform.position, Vector2.left, 2*distance, enemyLayer)|| Physics2D.Raycast(transform.Find("right").transform.position, Vector2.right, distance, enemyLayer))
        {
            isTouchEnemy = true;
            canControlJoyStick = false;

        }
    }
    //是否踩到敌人
    public bool isTrample()
    {
        if (Physics2D.OverlapCircle(transform.Find("midleDown").transform.position, distance, enemyLayer))
        {
            rigi.velocity = new Vector2(rigi.velocity.x, 0.5f * jumpForce * Time.deltaTime);
            //oneBounce = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void enemyDirection()
    {
        if (Physics2D.Raycast(transform.Find("right").transform.position,Vector2.right,0.3f,enemyLayer))
        {
            bounceDirection = 1;
;       }
        if(Physics2D.Raycast(transform.Find("left").transform.position, Vector2.left, 1f, enemyLayer))
        {
            bounceDirection = -1;
        }
    }

    //人物左右移动
    public void movement()
    {
        transform.position = new Vector3(transform.position.x + leftController.GetTouchPosition.x * speed * Time.deltaTime, transform.position.y);
    }
    //人物下蹲
    void crouch()
    {
        if (isGround())
        {
            if (leftController.GetTouchPosition.y < -0.4f)
            {
                speed = 2.5f;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                isCrouch = true;
            }
            else
            {
                if (Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer))//蹲在障碍物下的情况
                {
                    //speed = 2.5f;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<CircleCollider2D>().enabled = true;
                    isCrouch = true;
                }
                else
                {
                    speed = 5;
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    isCrouch = false;
                }

            }
        }
    }

    //判断是否可以跳跃
    void judgeJump()
    {
        if (isGround() && !Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer))//站在地面上&&头上没有障碍物
        {
            canJump = true;
            isJump = false;
            isFall = false;
        }
        else
        {
            canJump = false;
            if (rigi.velocity.y > 0)
            {
                isJump = true;
            }
            else
            {
                isFall = true;
                isJump = false;
            }
        }
    }

    //跳跃
    void jump()
    {
        if (canJump)
        {
            if (leftController.GetTouchPosition.y >= 0.6f && joyStickUp == false)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpForce * Time.deltaTime);
                if (playAudioTimes == 1)
                {
                    SoundManager.instance.PlayAudio(SoundManager.instance.jumpClip);
                    playAudioTimes++;
                }
                joyStickUp = true;
            }
        }
        canJump = false;
    }

    //摇杆抬起时，控制跳跃只能一次，除非下次重新抬起
    void oneJump()
    {
        if (leftController.GetTouchPosition.y <= 0.6f)
        {
            joyStickUp = false;
            playAudioTimes = 1;
        }
    }

    //防止人物碰到墙体抖动
    void preventShake()
    {
        if (Physics2D.Raycast(transform.Find("right").transform.position, Vector2.right, distance, groundLayer) || Physics2D.Raycast(transform.Find("left").transform.position, Vector2.left, distance, groundLayer))
        {
            rigi.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
        else
        {
            if (isGround())
            {
                rigi.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
    }

    //动画控制
    void animatorController()
    {
        if (isGround() && (leftController.GetTouchPosition.x > 0.2f || leftController.GetTouchPosition.x < -0.2f) && !isJump && !isCrouch)
        {
            animator.SetBool("running", true);
        }
        else if (isGround() && (leftController.GetTouchPosition.x < 0.2f || leftController.GetTouchPosition.x > -0.2f) && !isJump && !isCrouch)
        {
            animator.SetBool("running", false);
        }

        if (isJump)
        {
            animator.SetBool("jumping", true);
        }
        else
        {
            animator.SetBool("jumping", false);
        }

        if (isFall)
        {
            animator.SetBool("falling", true);
            animator.SetBool("isGround", false);
        }
        else
        {
            animator.SetBool("falling", false);
            animator.SetBool("isGround", true);
        }

        if (isCrouch)
        {
            animator.SetBool("crouching", true);
        }
        else
        {
            animator.SetBool("crouching", false);
        }
        if (isTouchEnemy)
        {
            animator.SetBool("isHurt", true);
            animator.SetBool("running", false);
        }
        else
        {
            animator.SetBool("isHurt", false);
        }

    }

    //碰到敌人后弹力
    public void colliderEnemyBounce()
    {
        if (!GameManager.instance.isGameOver && hp != 1)
        {
            rigi.velocity = new Vector2(250 * Time.deltaTime * -bounceDirection, rigi.velocity.y);
            //canControlJoyStick = false;
        }
    }

    //动画事件
    public void animatorEvent()
    {
        isTouchEnemy = false;
        GameManager.instance.decreasePlayerHp();
        canControlJoyStick = true;
    }

    public void trampleBounce()
    {
        if (isTrample())
        {
            Debug.Log("1");
            rigi.velocity = new Vector2(rigi.velocity.x, 0.5f * jumpForce * Time.deltaTime);
        }
    }


    public void DashButtonClick()
    {
        if (Time.time >= lastDashTime + dashCoolDown)
        {
            //ReadyToDash();
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDashTime = Time.time;
            cdImage.fillAmount = 0;
        }
    }

    void dash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                if (isGround())
                {
                    animator.SetBool("running", true);
                }
                rigi.velocity = new Vector2(dashSpeed * faceDirection, rigi.velocity.y);
                dashTimeLeft -= Time.deltaTime;
                objectPool.instance.GetFromPool();
            }
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }
}
