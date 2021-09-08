using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FormulaManager.Stats;
using PathCreation;
using FormulaManager.Race;

namespace FormulaManager.Vehicle
{
    public class VehicleController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PathCreator path;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;

        [Header("Performance Stats")]
        [SerializeField] private Team team;
        [SerializeField] private Driver driver;
        [SerializeField] private float minBaseSpeed, maxBaseSpeed;

        [Header("Sector Detection")]
        [SerializeField] private LayerMask sectorMask;

        [Header("Movement Settings")]
        [SerializeField] private bool goBackwards = true;

        private float speed = 0, distanceTravelled = 0f, speedScale = 1;
        private float maxSpeed = 0;

        public float SpeedScale { get => speedScale; set => speedScale = value; }
        public float Speed { get => speed; }

        private void Start()
        {
            path.pathUpdated += OnPathChanged;
            maxSpeed = ((float)team.Speed / 20) * maxBaseSpeed;
            Launch();
        }

        private void Update()
        {
            float targetSpeed = SpeedScale * maxSpeed * ((float)driver.Experience / 20) * ((float)driver.Talent / 20);
            speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 1 / ((float)team.Acceleration / 20));
            speed = Mathf.Clamp(speed, minBaseSpeed, maxSpeed);
            if (!goBackwards) distanceTravelled += speed * Time.deltaTime;
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