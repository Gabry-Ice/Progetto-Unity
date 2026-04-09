using System;
using UnityEngine;

namespace MiciomaXD
{
    [Serializable]
    public class FSMContext
    {
        public FSMContext(DoorFSM fsm, Transform player)
        {
            this.fsm = fsm;
            this.player = player;
        }
        public FSMContext(DoorFSM fsm)
        {
            this.fsm = fsm;
        }

        public DoorFSM fsm;
        public AudioSource doorAudioSource;
        public AudioClip openDoorSFX;
        public AudioClip closeDoorSFX;
        public Transform doorPivot;
        public float openingSpeed = 2f;
        public float angleAperture = 90f;
        public bool openRequest;
        public bool closeRequest;
        public Transform player;
        public AnimationCurve movementCurve;
    }
}