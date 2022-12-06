using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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

        public Matrix2D Set(int line, int column, int value)
        {
            Matrix[line, column] = value;
            return this;
        }

        public Matrix2D Transpose()
        {
            // À compléter (0.25 pt)
            // Doit retourner une matrice qui est la transposée de celle de l'objet
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(1), Matrix.GetLength(0));

            for (int l = 0; l < Matrix.GetLength(0); l++)
                for (int c = 0; c < Matrix.GetLength(1); c++)
                    result.Matrix[c, l] = Matrix[l, c];
            return result;
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
            if(Matrix.GetLength(0) < 3)
                return (Matrix[0, 0] * Matrix[1, 1]) - (Matrix[0, 1] * Matrix[1, 0]);
            else
            {
                double result = 0;
                for(int c = 0; c < Matrix.GetLength(1); c++)
                    result += (Matrix[0, c] * (Exclude(0, c).Determinant() * (c % 2 == 0 ? 1 : -1)));
                return result;
            }
        }

        private Matrix2D Exclude(int line, int column)
        {
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1);
            int lin = 0;
            for (int l = 0; l < Matrix.GetLength(0); l++)
            {
                if (l == line) continue;
                int col = 0;
                for (int c = 0; c < Matrix.GetLength(1); c++)
                {
                    if (c == column) continue;
                    result.Matrix[lin, col] = Matrix[l, c];
                    col++;
                }
                lin++;
            }
            return result;
        }

        public Matrix2D Comatrix()
        {
            // À compléter (1 pt)
            // Doit retourner une matrice qui est la comatrice de celle de l'objet
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(0), Matrix.GetLength(1));
            if (Matrix.GetLength(0) < 3)
            {
                result.Matrix[0, 0] = Matrix[1, 1];
                result.Matrix[1, 0] = -Matrix[0, 1];
                result.Matrix[0, 1] = -Matrix[1, 0];
                result.Matrix[1, 1] = Matrix[0, 0];
            } else
            {
                for (int l = 0; l < Matrix.GetLength(0); l++)
                    for (int c = 0; c < Matrix.GetLength(1); c++)
                        result.Matrix[l, c] = (Exclude(l, c).Determinant() * ((l + c) % 2 == 0 ? 1 : -1));
            }

            return result;
        }

        public Matrix2D Inverse()
        {
            // À compléter (0.25 pt)
            // Doit retourner une matrice qui est l'inverse de celle de l'objet;
            // Si le déterminant est nul ou la matrice non carrée, on retourne null.
            double det = Determinant();
            if (det == 0 || !IsSquare()) return null;
            return Comatrix().Transpose().Multiply(1 / det);
        }

        public Matrix2D Multiply(double d)
        {
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(0), Matrix.GetLength(1));
            for (int l = 0; l < Matrix.GetLength(0); l++)
                for (int c = 0; c < Matrix.GetLength(1); c++)
                    result.Matrix[l, c] = Matrix[l, c] * d;
            return result;
        }

        public Matrix2D Clone()
        {
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(0), Matrix.GetLength(1));
            for (int l = 0; l < Matrix.GetLength(0); l++)
                for (int c = 0; c < Matrix.GetLength(1); c++)
                    result.Matrix[l, c] = Matrix[l, c];
            return result;
        }

        public Matrix2D WithName(String name)
        {
            Matrix2D result = Clone();
            result.Name = name;
            return result;
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
            string result = this.Name + ":\n";
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                result += "| ";
                for (int j = 0; j < Matrix.GetLength(1); j++)
                    result += Matrix[i, j] + " ";
                result += "|\n";
            }
            return result;
        }
    }
}
