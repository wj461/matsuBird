﻿using System;
using UnityEngine;

namespace InGame
{
    internal enum BirdState
    {
        Fly,
        Land
    }

    public class bird : MonoBehaviour
    {
        private int aboveFire;
        private Vector2 aboveFireSpeed;
        private Collider2D collider2D;
        private Vector2 initSpeed;

        private Rigidbody2D rigidbody2D;
        private BirdState state = BirdState.Fly;

        private void Awake()
        {
            collider2D = GetComponent<Collider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            state = BirdState.Fly;
        }

        private void Start()
        {
            rigidbody2D.velocity = initSpeed;
        }

        private void Update()
        {
            switch (state)
            {
                case BirdState.Fly:
                    if (aboveFire == 0)
                        rigidbody2D.velocity = initSpeed;
                    else
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, aboveFireSpeed.y);
                    break;
                case BirdState.Land:
                    rigidbody2D.velocity = Vector2.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("ground"))
                SetState(BirdState.Land);
        }

        private void SetState(BirdState state)
        {
            this.state = state;
        }

        public void EnterFire()
        {
            aboveFire++;
        }

        public void ExitFire()
        {
            aboveFire--;
        }

        public void SetInitSpeed(Vector2 gameConfigBirdSpeed)
        {
            initSpeed = gameConfigBirdSpeed;
        }

        public void SetAboveFireSpeed(Vector2 gameConfigAboveFireSpeed)
        {
            aboveFireSpeed = gameConfigAboveFireSpeed;
        }
    }
}