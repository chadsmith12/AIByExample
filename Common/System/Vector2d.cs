using System;

namespace Common.System
{
    /// <summary>
    /// Represents a 2d Vector and defines the common operations on 2d Vectors.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    public struct Vector2D : IEquatable<Vector2D>
    {
        #region Fields
        /// <summary>
        /// The x coordinate
        /// </summary>
        public double X;
        /// <summary>
        /// The y coordinate
        /// </summary>
        public double Y;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> with its coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates the dot product.
        /// </summary>
        /// <param name="v2">The v2 Vector.</param>
        /// <returns>The Dot product (X * v2.X + Y * v2.Y)</returns>
        public double Dot(Vector2D v2)
        {
            return X * v2.X + Y * v2.Y;
        }

        /// <summary>
        /// Gets the sign of the current vector to another vector.
        /// </summary>
        /// <param name="v2">The v2 Vector.</param>
        /// <returns>Positive number if v2 is clockwise of this vector, otherwise; false (anticlockwise)</returns>
        public int Sign(Vector2D v2)
        {
            if (Y * v2.X > X * v2.Y)
                return -1;

            return 1;
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="v2">The v2 Vector.</param>
        /// <returns>The distance between this vector and v2.</returns>
        public double Distance(Vector2D v2)
        {
            var yDist = v2.Y - Y;
            var xDist = v2.X - X;

            return Math.Sqrt(yDist * yDist + xDist * xDist);
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="v1">The v1 Vector.</param>
        /// <param name="v2">The v2 Vector.</param>
        /// <returns>The distance between v1 and v2.</returns>
        public static double Distance(Vector2D v1, Vector2D v2)
        {
            return v1.Distance(v2);
        }

        /// <summary>
        /// Calculates the Euclidean distance squared between two vectors.
        /// </summary>
        /// <param name="v2">The v2. Vector</param>
        /// <returns>The distance squared between this vector and v2.</returns>
        public double DistanceSquared(Vector2D v2)
        {
            var yDist = v2.Y - Y;
            var xDist = v2.X - X;

            return yDist * yDist + xDist * xDist;
        }

        /// <summary>
        /// Calculates the Euclidean distance squared between two vectors.
        /// </summary>
        /// <param name="v1">The v1 Vector.</param>
        /// <param name="v2">The v2 Vector.</param>
        /// <returns>The distance squared between v1 and v2.</returns>
        public static double DistanceSquared(Vector2D v1, Vector2D v2)
        {
            return v1.DistanceSquared(v2);
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            if (Length > double.Epsilon)
            {
                X /= Length;
                Y /= Length;
            }
        }

        /// <summary>
        /// Truncates the vector so that its length does not exceed the max.
        /// </summary>
        /// <param name="max">The maximum of the vector.</param>
        public void Truncate(double max)
        {
            if (Length > max)
            {
                Normalize();
                this *= max;
            }
        }

        /// <summary>
        /// Given a normalized vector, will reflect the current vector.
        /// Reflects like a ball bouncing off a wall.
        /// </summary>
        /// <param name="normal">The normalized Vector.</param>
        public void Reflect(Vector2D normal)
        {
            this += 2.0 * Dot(normal) * normal.Reversed;
        }

        /// <summary>
        /// Normalizes the specified v1 vector.
        /// </summary>
        /// <param name="v1">The v1 vector.</param>
        /// <returns>Normalized version of the v1 Vector</returns>
        public static Vector2D Normalize(Vector2D v1)
        {
            var tempVec = v1;

            tempVec.Normalize();
            return tempVec;
        }
        #endregion

        #region Operator Overloads

        /// <summary>
        /// Implements the operator - overload; returns the opposite of a vector.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>
        /// -v
        /// </returns>
        public static Vector2D operator -(Vector2D v)
        {
            return new Vector2D(-v.X, -v.Y);
        }

        /// <summary>
        /// Implements the operator - overload; subtracts two vectors.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// v1 - v2
        /// </returns>
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Implements the operator +; adds two vectors.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// v1 + v2
        /// </returns>
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X +  v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Implements the operator *; multiply a vector by it's scalar value.
        /// </summary>
        /// <param name="v">The Vector.</param>
        /// <param name="x">The Scalar Value.</param>
        /// <returns>
        /// v * x
        /// </returns>
        public static Vector2D operator *(Vector2D v, double x)
        {
            return new Vector2D(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Implements the operator *; multiply a scalar value by a vector.
        /// </summary>
        /// <param name="x">The Scalar Value.</param>
        /// <param name="v">The Vector.</param>
        /// <returns>
        /// x * v
        /// </returns>
        public static Vector2D operator *(double x, Vector2D v)
        {
            return new Vector2D(v.X * x, v.Y * x);
        }

        /// <summary>
        /// Implements the operator /; divide a vector by a scalar value.
        /// </summary>
        /// <param name="v">The Vector.</param>
        /// <param name="x">The Scalar.</param>
        /// <returns>
        /// v / x.
        /// </returns>
        public static Vector2D operator /(Vector2D v, double x)
        {
            return new Vector2D(v.X / x, v.Y / x);
        }

        /// <summary>
        /// Implements the operator ==; check vector equality.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// v1 == v2.
        /// </returns>
        public static bool operator ==(Vector2D lhs, Vector2D rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Implements the operator !=; check vector inequality.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// lhs != rhs.
        /// </returns>
        public static bool operator !=(Vector2D lhs, Vector2D rhs)
        {
            return !lhs.Equals(rhs);
        }

        #endregion

        #region IEquatable
        /// <summary>
        /// Indicates whether the current Vector is equal to another Vector.
        /// </summary>
        /// <param name="other">Other Vector to compare with this Vector.</param>
        /// <returns>
        /// true if the current Vector is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector2D other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this Vector.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this Vector.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this Vector; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2D && Equals((Vector2D) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
        #endregion

        #region General Overrides        
        /// <summary>
        /// Returns a <see cref="string" /> that represents this Vector.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this Vector.
        /// </returns>
        public override string ToString()
        {
            return $"[Vector2D] X({X}) Y({Y})";
        }

        #endregion

        #region Properties        
        /// <summary>
        /// Returns a Zero Vector.
        /// </summary>
        /// <value>
        /// New Vector that is Zeroed Out.
        /// </value>
        public static Vector2D Zero => new Vector2D(0, 0);

        /// <summary>
        /// Gets the length of the Vector.
        /// </summary>
        /// <value>
        /// The length (Sqrt(X*X + Y*Y)).
        /// </value>
        public double Length => Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Gets the length of the Vector squared.
        /// </summary>
        /// <value>
        /// The length squared (X*X + Y*Y)
        /// </value>
        public double LengthSquared => X * X + Y * Y;

        /// <summary>
        /// Gets the vector perpendicular to this one.
        /// </summary>
        /// <value>
        /// Vector perpendicular to this vector.
        /// </value>
        public Vector2D Perpendicular => new Vector2D(-Y, X);

        /// <summary>
        /// Gets the reversed form of the vector.
        /// </summary>
        /// <value>
        /// The reversed form of the vector.
        /// </value>
        public Vector2D Reversed => new Vector2D(-X, -Y);
        #endregion
    }
}
