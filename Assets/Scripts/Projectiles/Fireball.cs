using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuniorProject_01
{
    public class Fireball : Projectile
    {
        //damage
        private float damage = 5f;
        private float maxDamage = 25f;
        private float velocity = 5;
        private float damageScale = 25;
        //size
        private float size = 0.2f;

        private bool gatDirection = false;
        public Vector3 defoultDirection;

        private bool isGrowing = true;
        private Rigidbody rb;


        //defoult setings
        private float defoultDamage;
        private float defoultVelocity;
        private float defoultSize;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            InitDefoultSetings();
        }

        private void OnEnable()
        {
            Debug.Log("Ffireball enable");
        }

        private void OnDisable()
        {
            BackToDefoult();
        }

        void Update()
        {
            if (isGrowing) PowerUp();
            transform.localScale = new Vector3(size, size, size);
        }

        private void PowerUp()
        {
            if (damage < maxDamage)
            {
                Debug.Log("Size = " + size + ", damage = " + damage);

                damage += damageScale*Time.deltaTime;
                velocity = damage;
                if (size < 1f)
                {
                    size += Time.deltaTime;
                }
            }

            else
            {
                if(gatDirection)
                {
                    Fire(defoultDirection);
                }

                else
                {
                    Fire(transform.right);
                }
            }
        }

        public void SetDirection(Vector3 direction)
        {
            defoultDirection = direction;
            gatDirection = true;
            isGrowing = true;
        }

        public void Fire(Vector3 direction)
        {
            rb.useGravity = true;
            isGrowing = false;
            rb.AddForce(direction * velocity);

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Distructuble>() != null)
            {
                collision.gameObject.GetComponent<Distructuble>().GetDamage(damage);
            }
            Debug.Log(collision.gameObject.tag + ". Damsge -  " + damage + ". Size - " + size);

            Destroy(gameObject);
        }


        //for pool
        private void InitDefoultSetings()
        {
            defoultDamage = damage;
            defoultVelocity = velocity;
            defoultSize = size;
        }

        private void BackToDefoult()
        {
            size = defoultSize;
            damage = defoultDamage;
            velocity = defoultVelocity;
        }




    }
}
