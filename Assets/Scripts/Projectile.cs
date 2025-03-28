using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    [Tooltip("Whether this projectile follows an arc or shoots straight.")]
    [SerializeField] bool isParabolic;
    [Tooltip("The arc the projectile will take.")]
    [SerializeField] AnimationCurve missileArc;
    [Tooltip("The minimum time the missile will be in travel.")]
    [SerializeField] float missileTravelTime;
    [Tooltip("How many meters of distance adds 1 more second of travel time.")]
    [SerializeField] float timeModifier;

    // The position the projectile starts at.
    Vector2 startPosition;
    // How far to the enemy in meters.
    float distanceToEnemy;
    // How long the projectile has been in flight.
    float currentTime = 0;
    // A reference to the target.
    CombatCharacter target;
    // A reference to the Combat Action that cast this ability.
    CombatActions combatAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the starting point where the projectile spawns.
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer tracking travel time by how much time passed between this frame and the last.
        currentTime += Time.deltaTime;

        // Calculate how much time the projectile has been in the air as a percentage of the total travel time.
        float travelPercentage = currentTime / missileTravelTime;

        // If the travel percentage is any less than 100% of the travel distance...
        if (travelPercentage < 1)
        {
            // Check to see if the path is a parabolic arc or not.
            if (isParabolic)
            {
                // If it is, use the missileArc to determine the height.
                // I'm multiplying the result by the total distance divided by two. So at 50% travel time, the missile will be at its highest point and the Evalulate() returns 0.5 on my particular curve.
                // I then multiply that by the distance / 2. The projectile will now go higher if the distance is longer. In this case, the max height is 25% of the distance to the target.
                float height = missileArc.Evaluate(travelPercentage) * (distanceToEnemy / 2);

                // Calcuate the new position for the arrow. We use Vector2.Lerp which just linearly interpolates between two locations.
                // We use the percentage of travel time to determine at what location it is. So at 50% travel time, it's 50% of the way to the target.
                Vector2 newPosition = Vector2.Lerp(startPosition, target.transform.position, travelPercentage);

                // Make sure we add the calculated height to the y axis of the calculated position. Without this, our arrow will just go straight.
                newPosition.y += height;

                // Calculate the velocity vector of the arrow, the actual direction it's traveling.
                Vector2 velocityVector = newPosition - (Vector2)transform.position;

                // Calculate the angle from the horizon that the angle is pointing up or down.
                float angle = Mathf.Atan2(velocityVector.y, velocityVector.x) * Mathf.Rad2Deg;

                // Rotate the arrow to face in the direction of its velocity vector.
                transform.rotation = Quaternion.Euler(0, 0, angle);

                // Finally, move the projectile to the calculated location and height.
                transform.position = newPosition;
            }
            else
            {
                // Otherwise, just simply set the location to the percentage of distance to the target that is equal to the percentage of travel time.
                // In other words, it'll just travel in a straight line.
                transform.position = Vector2.Lerp(startPosition, target.transform.position, travelPercentage);
            }
        }
        else
        {
            // Finally, apply damage, end the turn, and destroy the projectile.
            target.TakeDamage(combatAction.Damage);
            TurnManager.instance.EndTurn();
            Destroy(gameObject);
        }
    }

    void GetDistanceToTarget()
    {
        // Caucluates the distance to the enemy for travel time and arrow height calculations.
        distanceToEnemy = Vector2.Distance(target.transform.position, startPosition);
        // Add extra time to the travel duration based on target distance.
        missileTravelTime += distanceToEnemy / timeModifier;
    }

    // Receive information on the target and the associated combat action with this attack.
    public void SetTargetAndStats(CombatCharacter target, CombatActions combatAction)
    {
        this.target = target;
        this.combatAction = combatAction;
        GetDistanceToTarget();
    }
}