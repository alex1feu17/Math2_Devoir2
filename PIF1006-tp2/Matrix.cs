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
        public double[,] Matrix { get; set; }
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
            if(Matrix.GetLength(1) == 2)
            {
                return (Matrix[0, 0] * Matrix[1, 1]) - (Matrix[0, 1] * Matrix[1, 0]);
            } else
            {
                double result = 0;
                for(int c = 0; c < Matrix.GetLength(1); c++)
                    result += (Matrix[0, c] * (Exclude(c).Determinant() * (c % 2 == 0 ? 1 : -1)));
                return result;
            }

        }

        private Matrix2D Exclude(int column)
        {
            Matrix2D m = new Matrix2D("D", Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1);
            for(int l = 1; l < Matrix.GetLength(0); l++)
            {
                int col = 0;
                for (int c = 0; c < Matrix.GetLength(1); c++)
                {
                    if (c != column)
                    {
                        m.Matrix[l - 1, col] = Matrix[l, c];
                        col++;
                    }
                }
            }
            return m;
        }

        public Matrix2D Comatrix()
        {
            // À compléter (1 pt)
            // Doit retourner une matrice qui est la comatrice de celle de l'objet
            Matrix2D comatrice = new Matrix2D("C", Matrix.GetLength(0), Matrix.GetLength(1));
            Matrix2D result;

            for (int i = 0; i < comatrice.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < comatrice.Matrix.GetLength(1); j++)
                {
                    result = this.SousMatrice(i, j);
                    if ((i + j) % 2 == 0) { comatrice.Matrix[i, j] = result.Determinant(); }
                    else { comatrice.Matrix[i, j] = -1 * result.Determinant(); }
                }
            }

            return comatrice;
        }

        public Matrix2D Inverse()
        {
            // À compléter (0.25 pt)
            // Doit retourner une matrice qui est l'inverse de celle de l'objet;
            // Si le déterminant est nul ou la matrice non carrée, on retourne null.
            double det = Determinant();
            Matrix2D t_Comatrice = Comatrix();
            t_Comatrice = t_Comatrice.Transpose();

            Matrix2D Inverse = new Matrix2D("I", Matrix.GetLength(0), Matrix.GetLength(1));
            //Inverse = t_Comatrice.Matrix * (1 / det);
            return Inverse;
        }

        public Matrix2D SousMatrice(int ib, int jb)
        {
            Matrix2D B = new Matrix2D("S",Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1);
            int ir = 0, jr = 0;
            for (int i = 0; i < B.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < B.Matrix.GetLength(1); j++)
                {
                    if (i != (ib) && j != (jb))
                    {
                        B.Matrix[ir, jr] = Matrix[i, j];
                        if (jr < B.Matrix.GetLength(0) - 1) jr++;
                        else { jr = 0; ir++; }
                    }
                }
            }
            return B;
        }

        public Matrix2D Clone()
        {
            Matrix2D result = new Matrix2D(Name, Matrix.GetLength(0), Matrix.GetLength(1));
            for (int l = 0; l < Matrix.GetLength(0); l++)
                for (int c = 0; c < Matrix.GetLength(1); c++)
                    result.Matrix[l, c] = Matrix[l, c];
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
