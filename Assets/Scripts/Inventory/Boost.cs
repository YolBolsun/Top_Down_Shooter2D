using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boost
{
    public float attackSpeedModifier = 0;
    public float attackDamageModifier = 0;

    public float movementSpeedModifier = 0;

    public float armorModifier = 0;
    public float healthModifier = 0;

    public ShotBehaviour shotBehavior = ShotBehaviour.None;

    public Boost()
    {

    }
    public Boost(Boost toCopy)
    {
        this.attackSpeedModifier = toCopy.attackSpeedModifier;
        this.attackDamageModifier = toCopy.attackDamageModifier;
        this.movementSpeedModifier = toCopy.movementSpeedModifier;
        this.armorModifier = toCopy.armorModifier;
        this.healthModifier = toCopy.healthModifier;
        this.shotBehavior = toCopy.shotBehavior;
    }

    

    public enum ShotBehaviour
    {
        Rifle,
        Shotgun,
        Pistol,
        None
    }

    public override bool Equals(object obj) => this.Equals(obj as Boost);

    public override int GetHashCode() => (attackSpeedModifier, attackDamageModifier, movementSpeedModifier, armorModifier, healthModifier, shotBehavior).GetHashCode();

    public bool Equals(Boost p)
    {
        if (p is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (attackSpeedModifier == p.attackSpeedModifier) && (attackDamageModifier == p.attackDamageModifier) && (movementSpeedModifier == p.movementSpeedModifier) && (armorModifier == p.armorModifier) && (healthModifier == p.healthModifier) && (shotBehavior == p.shotBehavior);
    }

    public static bool operator ==(Boost lhs, Boost rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Boost lhs, Boost rhs)
    {
        return !(lhs == rhs);
    }
}
