using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected GameObject iceCube;

    protected bool movingRight;
    protected Animator m_animator;

    //Freeze properties
    protected bool freezeable = false;
    protected Vector3 freezeCubeOffset;
    protected Vector3 freezeCubeScale;
    public bool isFrozen = false;
    protected float freezeTime = 8;

    public void Start()
    {
        m_animator = GetComponent<Animator>();
        freezeCubeOffset = new Vector3();
        freezeCubeScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public virtual void Update() { 

    }

    public virtual void Die() {
        transform.localScale = new Vector3(0, 0, 0);
        OnDestroy();
    }

    public virtual void SetSpeed(float number) {
        speed = number;
    }

    public virtual void OnDestroy()
    {
        // We can delete enemies in a list later here
    }
    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        //handle freezies
        if (freezeable)
        {
            HandleFreezing(col);
        }
    }

    void HandleFreezing(Collider2D col)
    {
        if (col.tag == "IceProjectile" && !isFrozen)
        {
            GameObject cube = Instantiate(iceCube, transform.position + freezeCubeOffset, Quaternion.identity, transform);
            cube.transform.localScale = freezeCubeScale;
            isFrozen = true;
            if (m_animator != null)
            {
                m_animator.speed = 0;
            }
            gameObject.layer = LayerMask.NameToLayer("Platforms");
            Destroy(cube, freezeTime);
            StartCoroutine(unFreeze(freezeTime));

        }
    }

    IEnumerator unFreeze(float time)
    {
        yield return new WaitForSeconds(time);
        isFrozen = false;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        if (m_animator != null)
        {
            m_animator.speed = 1;
        }
    }
}


