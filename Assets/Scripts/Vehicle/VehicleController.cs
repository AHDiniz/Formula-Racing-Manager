using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;
using PathCreation;
using FormulaManager.Management;

namespace FormulaManager.Vehicle
{
    public class VehicleController : MonoBehaviour
    {
        [System.Serializable]
        public enum Pace
        {
            HoldingBack,
            SlowingDown,
            Neutral,
            Pushing,
            PushingHard
        }

        [Header("References")]
        [SerializeField] private PathCreator path;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;

        [Header("Performance Stats")]
        [SerializeField] private Driver driver;
        [SerializeField] private float minBaseSpeed, maxBaseSpeed;
        [SerializeField] private Pace initialPace = Pace.Neutral;

        [Header("Sector Detection")]
        [SerializeField] private LayerMask sectorMask;

        [Header("Movement Settings")]
        [SerializeField] private bool goBackwards = true;

        private float speed = 0, distanceTravelled = 0f, speedScale = 1, speedMultiplier = 0;
        private float maxSpeed = 0, paceMultiplier = .9f;
        private Pace pace;
        private Team team;

        public float SpeedScale { get => speedScale; set => speedScale = value; }
        public float Speed { get => speed; }
        public Pace CurrentPace { get => pace; set => pace = value; }
        public Driver Driver { get => driver; set => driver = value; }
        public PathCreator Path { get => path; set => path = value; }

        private void OnEnable()
        {
            team = driver.CurrentTeam;
            path.pathUpdated += OnPathChanged;
            maxSpeed = ((float)team.Speed / 20) * maxBaseSpeed;
            pace = initialPace;
            speedMultiplier = ((float)driver.Experience / 20) * ((float)driver.Talent / 20) + ((float)(team.AeroSensibility) / 20) * ((float)(team.Downforce) / 20);

            // This is being made to balance out the speed of the cars:
            // Probably should use animation curves to treat this...
            if (speedMultiplier < .25f)
                speedMultiplier *= 3f;
            else if (speedMultiplier < .5f)
                speedMultiplier *= 1.5f;
            else if (speedMultiplier > 1f)
                speedMultiplier *= .75f;

            Debug.Log("Multiplier " + driver.Name + ": " + speedMultiplier);
        }

        private void Update()
        {
            switch (pace)
            {
                case Pace.HoldingBack:
                    paceMultiplier = .8f;
                    break;
                case Pace.SlowingDown:
                    paceMultiplier = .85f;
                    break;
                case Pace.Pushing:
                    paceMultiplier = .95f;
                    break;
                case Pace.PushingHard:
                    paceMultiplier = 1f;
                    break;
                default:
                    paceMultiplier = .9f;
                    break;
            }
            
            float targetSpeed = SpeedScale * maxSpeed * speedMultiplier;
            speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 1 / ((float)team.Acceleration / 20));
            speed = Mathf.Clamp(speed, minBaseSpeed, maxSpeed);
            if (!goBackwards) distanceTravelled += speed * paceMultiplier * Time.deltaTime;
            else distanceTravelled += -speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        private void OnPathChanged()
        {
            distanceTravelled = path.path.GetClosestDistanceAlongPath(transform.position);
        }

        // TODO: Call this in a race event manager:
        public void Launch()
        {
            speed = maxSpeed;
        }
    }
}