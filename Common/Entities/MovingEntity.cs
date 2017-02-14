using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Common.System;

namespace Common.Entities
{
    public class MovingEntity : BaseGameEntity
    {
        #region Protected Fields

        protected double _mass;
        // a vector that is perpendicular to the heading vector
        protected Vector2D _side;

        protected Vector2D _heading;
        #endregion
        public MovingEntity(int id) : base(id)
        {
        }

        public override void Update()
        {
            // Do Nothing
        }

        public bool RotateHeadingToFacePosition(Vector2D target)
        {
            var toTarget = Vector2D.Normalize(target - _position);
            // determine the angle between the heading vector and the target
            var angle = Math.Cos(Heading.Dot(toTarget));

            // if the player is already facing the target
            if (angle < 0.00001) return true;

            // clamp the amount to turn to the max turn rate
            if (angle > MaxTurnRate)
            {
                angle = MaxTurnRate;
            }
            
            // use a rotation matrix to rotate the players heading
            var rotationMatrix = new Matrix2D();
            // direction of rotataion has to be determined when creating the rotation matrix
            rotationMatrix.Rotate(angle * Heading.Sign(toTarget));
            var tempHeading = Heading;
            var tempVelocity = Velocity;
            rotationMatrix.TransformVector(ref tempHeading);
            rotationMatrix.TransformVector(ref tempVelocity);

            Heading = tempHeading;
            Velocity = tempVelocity;

            // finally recreate the side
            _side = Heading.Perpendicular;
            return false;
        }


        #region Properties        
        /// <summary>
        /// Gets or sets the velocity of the moving entity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public Vector2D Velocity { get; set; }

        /// <summary>
        /// Gets the mass.
        /// </summary>
        /// <value>
        /// The mass.
        /// </value>
        public double Mass => _mass;

        /// <summary>
        /// Gets or sets the maximum speed of the moving entity.
        /// </summary>
        /// <value>
        /// The maximum speed.
        /// </value>
        public double MaxSpeed { get; set; }

        /// <summary>
        /// Gets or sets the maximum force.
        /// </summary>
        /// <value>
        /// The maximum force.
        /// </value>
        public double MaxForce { get; set; }

        /// <summary>
        /// Gets if this moving entity's speed is maxed out.
        /// </summary>
        /// <value>
        /// <c>true</c> if this entity's speed maxed out; otherwise, <c>false</c>.
        /// </value>
        public bool IsSpeedMaxedOut => MaxSpeed * MaxSpeed >= Velocity.LengthSquared;

        /// <summary>
        /// Gets or sets the speed.
        /// This different than velocity and is just a scalar value of the entity. 
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        public Vector2D Heading
        {
            get
            {
                return _heading;
            }
            set
            {
                Debug.Assert((value.LengthSquared - 1.0) < 0.00001);
                _heading = value;

                // side vector must always be perdendicular
                _side = _heading.Perpendicular;
            }
        }

        /// <summary>
        /// Gets the side vector.
        /// This vector is perpendicular to the heading vector
        /// </summary>
        /// <value>
        /// The side vector perpendicular to the heading.
        /// </value>
        public Vector2D Side => _side;

        /// <summary>
        /// Gets or sets the maximum turn rate.
        /// </summary>
        /// <value>
        /// The maximum turn rate.
        /// </value>
        public double MaxTurnRate { get; set; }

        #endregion
    }
}
