using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIF1006_tp2
{
    public class Matrix2D
    {
        public double[,] Matrix { get; private set; }
        public string Name { get; private set; }

        public Matrix2D(string name, int lines, int columns)
        {
            // Doit rester tel quel

            Matrix = new double[lines, columns];
            Name = name;
        }

        public Matrix2D Transpose()
        {
            int w = Matrix.GetLength(0);
            int h = Matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = Matrix[i, j];
                }
            }

            Matrix2D TransMatrix2D = new Matrix2D("T", result.GetLength(0), result.GetLength(1));

            return TransMatrix2D;
            // À compléter (0.25 pt)
            // Doit retourner une matrice qui est la transposée de celle de l'objet
        }

        public bool IsSquare()
        {
            // À compléter (0.25 pt)
            // Doit retourner vrai si la matrice est une matrice carrée, sinon faux
            return Matrix.GetLength(0) == Matrix.GetLength(1);
        }

        public double Determinant()
        {
            // À compléter (2 pts)
            // Aura sans doute des méthodes suppl. privée à ajouter,
            // notamment de nature récursive. La matrice doit être carrée de prime à bord.
        }

        public Matrix2D Comatrix()
        {
            // À compléter (1 pt)
            // Doit retourner une matrice qui est la comatrice de celle de l'objet
        }

        public Matrix2D Inverse()
        {
            // À compléter (0.25 pt)
            // Doit retourner une matrice qui est l'inverse de celle de l'objet;
            // Si le déterminant est nul ou la matrice non carrée, on retourne null.
        }

        public override string ToString()
        {
            // À compléter (0.25 pt)
            // Doit retourner l'équivalent textuel/visuel d'une matrice.
            // P.ex.:
            // A:
            // | 3 5 7 |
            // | 6 2 5 |
            // | 5 4 5 |
        }
    }
}
