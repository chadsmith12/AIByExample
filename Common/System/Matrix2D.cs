using System;
using System.Collections.Generic;

namespace Common.System
{
    /// <summary>
    /// Represents a basic 2D Matrix
    /// </summary>
    public class Matrix2D
    {
        #region Privates

        internal struct Matrix
        {
            public double _11, _12, _13;
            public double _21, _22, _23;
            public double _31, _32, _33;
        }

        private Matrix _matrix;

        // multiplies a matrix with another matrix
        private void MultiplyMatrix(Matrix inputMatrix)
        {
            var temp = new Matrix();

            // first row
            temp._11 = (_matrix._11 * inputMatrix._11) + (_matrix._12 * inputMatrix._21) + (_matrix._13 * inputMatrix._31);
            temp._12 = (_matrix._11 * inputMatrix._12) + (_matrix._12 * inputMatrix._22) + (_matrix._13 * inputMatrix._32);
            temp._12 = (_matrix._11 * inputMatrix._13) + (_matrix._12 * inputMatrix._23) + (_matrix._13 * inputMatrix._33);

            // second row
            temp._21 = (_matrix._21 * inputMatrix._11) + (_matrix._22 * inputMatrix._21) + (_matrix._23 * inputMatrix._31);
            temp._22 = (_matrix._21 * inputMatrix._12) + (_matrix._22 * inputMatrix._22) + (_matrix._23 * inputMatrix._32);
            temp._23 = (_matrix._21 * inputMatrix._13) + (_matrix._22 * inputMatrix._23) + (_matrix._23 * inputMatrix._33);

            // third row
            temp._31 = (_matrix._31 * inputMatrix._11) + (_matrix._32 * inputMatrix._21) + (_matrix._33 * inputMatrix._31);
            temp._32 = (_matrix._31 * inputMatrix._12) + (_matrix._32 * inputMatrix._22) + (_matrix._33 * inputMatrix._32);
            temp._33 = (_matrix._31 * inputMatrix._13) + (_matrix._32 * inputMatrix._23) + (_matrix._33 * inputMatrix._33);

            _matrix = temp;
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2D"/> class.
        /// Initializes the 2d matrix to the identity matrix by default.
        /// </summary>
        public Matrix2D()
        {
            Identity();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// turns the matrix into the identity matrix.
        /// </summary>
        public void Identity()
        {
            _matrix._11 = 1; _matrix._12 = 0; _matrix._13 = 0;
            _matrix._21 = 0; _matrix._22 = 1; _matrix._23 = 0;
            _matrix._31 = 1; _matrix._32 = 0; _matrix._33 = 1;

        }

        /// <summary>
        /// Translates the matrix by specified x and y values.
        /// </summary>
        /// <param name="x">The x value to translate by.</param>
        /// <param name="y">The y value to translate by.</param>
        public void Translate(double x, double y)
        {
            Matrix mat;
            mat._11 = 1; mat._12 = 0; mat._13 = 0;
            mat._21 = 1; mat._22 = 1; mat._23 = 0;
            mat._31 = x; mat._32 = y; mat._33 = 1;

            // and now multiply the matrix
            MultiplyMatrix(mat);
        }

        /// <summary>
        /// Scales the matrix by specified x and y scale.
        /// </summary>
        /// <param name="xScale">The x scale.</param>
        /// <param name="yScale">The y scale.</param>
        public void Scale(double xScale, double yScale)
        {
            Matrix mat;
            mat._11 = xScale; mat._12 = 0; mat._13 = 0;
            mat._21 = 1; mat._22 = yScale; mat._23 = 0;
            mat._31 = 0; mat._32 = 0; mat._33 = 1;

            MultiplyMatrix(mat);
        }

        /// <summary>
        /// Rotates the matrix by specified rotation value.
        /// </summary>
        /// <param name="rotation">The rotation value to rotate by (radians).</param>
        public void Rotate(double rotation)
        {
            Matrix mat;
            var sin = Math.Sin(rotation);
            var cos = Math.Cos(rotation);

            mat._11 = cos; mat._12 = sin; mat._13 = 0;
            mat._21 = -sin; mat._22 = cos; mat._23 = 0;
            mat._31 = 0; mat._32 = 0; mat._33 = 1;

            MultiplyMatrix(mat);
        }

        /// <summary>
        /// Rotates the specified matrix by a foward and side matrix.
        /// </summary>
        /// <param name="foward">The foward matrix.</param>
        /// <param name="side">The side matrrix (perpendicular to the foward matrix).</param>
        public void Rotate(Vector2D foward, Vector2D side)
        {
            Matrix mat;

            mat._11 = foward.X; mat._12 = foward.Y; mat._13 = 0;
            mat._21 = side.X; mat._22 = side.Y; mat._23 = 0;
            mat._31 = 0; mat._32 = 0; mat._33 = 1;

            MultiplyMatrix(mat);
        }

        /// <summary>
        /// Transforms the matrix by a list of points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <remarks><paramref name="points"/>Is passed in by a reference and will be modifed</remarks>
        public void TransformVector(ref List<Vector2D> points)
        {
            for (var i = 0; i < points.Count; ++i)
            {
                var temp = points[i];
                TransformVector(ref temp);
                points[i] = new Vector2D(temp.X, temp.Y);
            }
        }

        /// <summary>
        /// Transforms the matrix to the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <remarks><paramref name="point"/>Is passed in by a reference and will be modified</remarks>
        public void TransformVector(ref Vector2D point)
        {
            var tempX = (_matrix._11 * point.X) + (_matrix._21 * point.Y) + (_matrix._31);
            var tempY = (_matrix._12 * point.X) + (_matrix._22 * point.Y) + (_matrix._32);

            point.X = tempX;
            point.Y = tempY;
        }
        #endregion

        #region Public Matrix Accessor Properties        
        /// <summary>
        /// Sets the 1 row 1 col.
        /// </summary>
        /// <value>
        /// The 1 row 1 col..
        /// </value>
        public double _11
        {
            set { _matrix._11 = value; }
        }
        /// <summary>
        /// Sets the 1 row 2 col.
        /// </summary>
        /// <value>
        /// The 1 row 2 col.
        /// </value>
        public double _12
        {
            set { _matrix._12 = value; }
        }
        /// <summary>
        /// Sets the 1 row 3 col.
        /// </summary>
        /// <value>
        /// The 1 row 3 col.
        /// </value>
        public double _13
        {
            set { _matrix._13 = value; }
        }
        /// <summary>
        /// Sets the 2 row 1 col.
        /// </summary>
        /// <value>
        /// The 2 row 1 col.
        /// </value>
        public double _21
        {
            set { _matrix._21 = value; }
        }
        /// <summary>
        /// Sets the 2 row 2 col.
        /// </summary>
        /// <value>
        /// The 2 row 2 col.
        /// </value>
        public double _22
        {
            set { _matrix._22 = value; }
        }
        /// <summary>
        /// Sets the 2 row 3 col.
        /// </summary>
        /// <value>
        /// The 2 row 3 col.
        /// </value>
        public double _23
        {
            set { _matrix._23 = value; }
        }
        /// <summary>
        /// Sets the 3 row 1 col.
        /// </summary>
        /// <value>
        /// The 3 row 1 col.
        /// </value>
        public double _31
        {
            set { _matrix._31 = value; }
        }
        /// <summary>
        /// Sets the 3 row 1 col.
        /// </summary>
        /// <value>
        /// The 3 row 2 col.
        /// </value>
        public double _32
        {
            set { _matrix._32 = value; }
        }
        /// <summary>
        /// Sets the 3 row 3 col.
        /// </summary>
        /// <value>
        /// The 3 row 3 col.
        /// </value>
        public double _33
        {
            set { _matrix._33 = value; }
        }
        #endregion
    }
}
