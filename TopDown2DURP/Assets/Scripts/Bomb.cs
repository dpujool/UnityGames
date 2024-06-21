using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private TilemapRenderer destroyableWall;

    Animator anim;
    float tiempo;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Que la animación coincida con el tiempo
        if (anim.GetCurrentAnimatorClipInfo(0).Length != 0)
        {
            AnimatorClipInfo[] AuxClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            tiempo = anim.GetCurrentAnimatorStateInfo(0).normalizedTime * AuxClipInfo[0].clip.length;

            if (tiempo > AuxClipInfo[0].clip.length)
            {
                print(tiempo + " " + AuxClipInfo[0].clip.length);
                destroyableWall.gameObject.SetActive(false);
                Destroy(gameObject);
                
            }
        }
    }

    public void ActiveBomb()
    {
        gameObject.SetActive(true);
    }
}
