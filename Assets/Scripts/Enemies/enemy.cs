using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!test.instance.isTouchEnemy)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true; 
            //Destroy(gameObject);
        }
        byTrample();
    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
//;           //test.instance.canControlJoyStick = false;
//            if (test.instance.isTrample())
//            {
//                gameObject.layer = 0;
//                gameObject.GetComponent<BoxCollider2D>().enabled = false;
//                //Debug.Log('1');
//                test.instance.isTouchEnemy = false;
//                animator.SetBool("die", true);//动画后面有动画事件函数
//                //return;
//                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
//                //GameManager.instance.decreasePlayerHp();
//                //Destroy(gameObject);
//            }
            
//        }
//    }

    private void byTrample()
    {
        if (test.instance.isTrample())
        {
            //test.instance.trampleBounce();
            gameObject.layer = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log('1');
            test.instance.isTouchEnemy = false;
            animator.SetBool("die", true);
        }
    }

    public void destory()
    {
        Destroy(gameObject);
    }

}
