using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace FormulaManager.Management
{
    public class WeekendEvent : MonoBehaviour
    {
        [SerializeField] private float durationInSeconds;
        [SerializeField] protected WeekendManager manager;
        [SerializeField] protected PathCreator path;
        
        public float DurationInSeconds { get => realDuration; }

        private float realDuration = 0f;
        private float timer = 0f;

        private void Start()
        {
            if (manager == null)
            {
                GameObject managerGameObj = GameObject.FindWithTag("Weekend Manager");
                if (managerGameObj != null)
                {
                    manager = managerGameObj.GetComponent<WeekendManager>();
                }
            }

            realDuration = manager.TimeScale * durationInSeconds;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= realDuration)
            {
                // TODO: Go to next weekend event
            }
        }

        public virtual void InitEvent() {}

        public virtual void TickEvent() {}

        public virtual void FinishEvent() {}
    }
}