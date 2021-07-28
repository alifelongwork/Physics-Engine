using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsForm
{
    public class VMM
    {
        private static bool CanMult(float[,] matrixA, float[,] matrixB)
        {
            if(matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                return false;
            }
            return true;
        }
        public static float[,] MatrixMultiplication(float[,] matrixA, float[,] matrixB)
        {
            if(!CanMult(matrixA, matrixB))
            {
                return null;
            }

            float[,] matrixC = new float[matrixA.GetLength(0), matrixB.GetLength(1)];

            int curRowC = 0;
            int curColC = 0;

            while(curRowC < matrixC.GetLength(0))
            {          
                float eleSum = 0f;
                for (int curEle = 0; curEle < matrixB.GetLength(0); curEle++)
                {
                    eleSum += matrixA[curRowC, curEle] *
                        matrixB[curEle, curColC];
                }

                matrixC[curRowC, curColC] = eleSum;

                if(curColC == matrixC.GetLength(1) - 1)
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

        public float[,] Translate(float dx, float dy)
        {
            float[,] transMatrix = { { 1, 0, dx},
                                     { 0, 1, dy },
                                     { 0, 0, 1 } };

            return null;
        }
    }
}
