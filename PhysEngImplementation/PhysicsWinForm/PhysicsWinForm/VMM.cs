using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsWinForm
{
    public class VMM
    {
        public float[,] Transpose(float[,] transThisMatrix)
        {
            float[,] transposed = new float[transThisMatrix.GetLength(0), transThisMatrix.GetLength(1)];

            for (int i = 0; i < transposed.GetLength(0); i++)
            {
                for (int j = 0; j < transposed.GetLength(1); j++)
                {
                    if(i != j && i < transposed.GetLength(1))
                    {
                        transposed[i, j] = transThisMatrix[j, i];
                    }
                    else if(i == j)
                    {
                        transposed[i, j] = transThisMatrix[i, j];
                    }
                }
            }

            return transposed;
        }
        
        public float[,] DotProduct(float[,] VMA, float[,] VMB)
        {
            VMA = Transpose(VMA);
            return MatrixMultiplication(VMA, VMB);
        }
        public float[,] MatrixAddition(float[,] matrixA, float[,] matrixB)
        {
            if(matrixA.GetLength(0) != matrixB.GetLength(0) && matrixA.GetLength(1) != matrixB.GetLength(1))
            {
                return null;
            }
            float[,] matrixC = new float[matrixA.GetLength(0), matrixA.GetLength(1)];

            for (int i = 0; i < matrixC.GetLength(0); i++)
            {
                for (int j = 0; j < matrixC.GetLength(1); j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return matrixC;
        }

        public float[,] MatrixSubtraction(float[,] matrixA, float[,] matrixB)
        {
            if (matrixA.GetLength(0) != matrixB.GetLength(0) && matrixA.GetLength(1) != matrixB.GetLength(1))
            {
                return null;
            }
            float[,] matrixC = new float[matrixA.GetLength(0), matrixA.GetLength(1)];

            for (int i = 0; i < matrixC.GetLength(0); i++)
            {
                for (int j = 0; j < matrixC.GetLength(1); j++)
                {
                    matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return matrixC;
        }
        public float[,] MatrixMultiplication(float[,] matrixA, float[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                return null;
            }

            float[,] matrixC = new float[matrixA.GetLength(0), matrixB.GetLength(1)];

            int curRowC = 0;
            int curColC = 0;

            while (curRowC < matrixC.GetLength(0))
            {
                float eleSum = 0;
                for (int curEle = 0; curEle < matrixA.GetLength(1); curEle++)
                {
                    eleSum += matrixA[curRowC, curEle] *
                        matrixB[curEle, curColC];


                }

                matrixC[curRowC, curColC] = eleSum;

                if (curColC == matrixC.GetLength(1) - 1)
                {
                    curColC = 0;
                    curRowC++;
                }
                else
                {
                    curColC++;
                }
            }

            return matrixC;
        }

        public float[,] Translate(float x, float y, float dx, float dy)
        {
            float[,] moveThis = new float[,] { { x }, { y }, { 1 } };
            float[,] transMatrix = { { 1, 0, dx },
                                     { 0, 1, dy },
                                     { 0, 0, 1 } };

            return MatrixMultiplication(transMatrix, moveThis);
        }

        public float[,] Rotate(float x, float y, float theta)
        {
            float[,] rotateThis = new float[,] { { x }, { y }, { 1 } };
            float radians = ((float)Math.PI * theta) / 180f;
            float[,] rotateMatrix = { {(float)Math.Cos(radians), -(float)Math.Sin(radians), 0 },
                                      {(float)Math.Sin(radians), (float)Math.Cos(radians), 0 },
                                      {0, 0, 1 } };
            return MatrixMultiplication(rotateMatrix, rotateThis);
        }

        public float SquareMagnitude(float[,] vector)
        {
            float sqMag = 0f;

            for (int i = 0; i < vector.GetLength(0); i++)
            {
                sqMag += vector[i, 0] * vector[i, 0];
            }
            
            return sqMag;
        }
    }
}
