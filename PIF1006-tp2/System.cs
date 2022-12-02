using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIF1006_tp2
{
    public class System
    {
        public Matrix2D A { get; private set; }
        public Matrix2D B { get; private set; }

        public System(Matrix2D a, Matrix2D b)
        {
            // Doit rester tel quel
            // ss
            A = a;
            B = b;
        }

        public bool IsValid()
        {
            // À compléter (0.25 pt)
            // Doit vérifier si la matrix A est carrée et si B est une matrice avec le même nb
            // de ligne que A et une seule colonne, sinon cela retourne faux.
            // Avant d'agir avec le système, il faut toujours faire cette validation
            return A.IsSquare() && B.Matrix.GetLength(0) == A.Matrix.GetLength(0) && B.Matrix.GetLength(1) == 1;
        }

        public Matrix2D SolveUsingCramer()
        {
            // À compléter (1 pt)
            // Doit retourner une matrice X de même dimension que B avec les valeurs des inconnus
            double detA = A.Determinant();

            Matrix2D result = new Matrix2D("Cramer", B.Matrix.GetLength(0), 1);
            for (int c = 0; c < A.Matrix.GetLength(1); c++)
            {
                Matrix2D matrix = A.Clone();
                for (int l = 0; l < B.Matrix.GetLength(0); l++)
                    matrix.Matrix[l, c] = B.Matrix[l, 0];
                result.Matrix[c, 0] = matrix.Determinant() / detA;
            }
            return result;
        }

        public Matrix2D SolveUsingInverseMatrix()
        {
            // À compléter (0.25 pt)
            // Doit retourner une matrice X de même dimension que B avec les valeurs des inconnus
            return B.Clone();
        }

        public Matrix2D SolveUsingGauss()
        {
            // À compléter (1 pts)
            // Doit retourner une matrice X de même dimension que B avec les valeurs des inconnus 
            Matrix2D A1 = A.Clone();
            Matrix2D B1 = B.WithName("Gauss");
            for (int i = 0; i < A1.Matrix.GetLength(0); i++)
            {
                for (int l = 0; l < A1.Matrix.GetLength(0); l++)
                {
                    if (l == i) continue;
                    double d = A1.Matrix[l, i] / A1.Matrix[i, i];
                    B1.Matrix[l, 0] = B1.Matrix[l, 0] - (d * B1.Matrix[i, 0]);
                    for (int c = i; c < A1.Matrix.GetLength(1); c++)
                        A1.Matrix[l, c] = A1.Matrix[l, c] - (d * A1.Matrix[i, c]);
                }

                B1.Matrix[i, 0] = B1.Matrix[i, 0] / A1.Matrix[i, i];
                for (int c = A1.Matrix.GetLength(1) - 1; c >= i; c--)
                    A1.Matrix[i, c] = A1.Matrix[i, c] / A1.Matrix[i, i];
            }

            return B1;
        }

        public override string ToString()
        {
            // À compléter (0.5 pt)
            // Devrait retourner en format:
            // 
            // 3x1 + 5x2 + 7x3 = 9
            // 6x1 + 2x2 + 5x3 = -1
            // 5x1 + 4x2 + 5x3 = 5
            string result = "";
            for(int l = 0; l < A.Matrix.GetLength(0); l++)
            {
                for (int c = 0; c < A.Matrix.GetLength(1); c++)
                {
                    if (c != 0) result += (A.Matrix[l, c] < 0 ? " - " : " + ");
                    else if (A.Matrix[l, c] < 0) result += "-";
                    result += Math.Abs(A.Matrix[l, c]) + "x" + (l + 1);
                }
                result += " = " + B.Matrix[l, 0] + "\n";
            }
            return result;
        }
    }
}
