using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PIF1006_tp2
{

    // - Répartition des points -:
    // Program.cs: 2 pts
    // Matrix.cs: 4 pts
    // System.cs: 3 pts
    // Rapport + guide: 1 pt)

    class Program
    {
        static void Main(string[] args)
        {
            // À compléter: 2 pts (0.5 menu / 1.5 chargement)
            /* Vous devez avoir un menu utilisateur avec l'arboresence de menus suivants:
             * 1) Charger un fichier de système -> doit être un fichier structuré ou semi structurée qui décrit
             *    2 matrices; vous pouvez choisir de demander de charger 2 fichiers de matrices séparées (A et B)
             *    si vous préférez;
             *    
             *    ex. de format en "plain text" potentiel:
             *    
             *    3 1 5
             *    4 2 -1
             *    0 -6 4
             *    0
             *    4
             *    6
             *    
             *    Il faut ensuite "parser" ligne par ligne et déduire la taille de la matrice carrée (plusieurs
             *    façons de vérifier cela).  Créez le chargement dans une classe à part ou dans une méthode privée ici.
             *    Si le format est invalide, il faut retourner null ou l'équivalent et cela doit être
             *    indiqué à l'utilisateur; on affiche le système chargé (en appelant implicitement le TOString() du système);
             *    on retourne au menu dans tous les cas;
             *    
             *    Conseil: utilisez du JSON pour vous pratiquer
             *    
             *    Vous pouvez avoir un fichier chargé par défaut; je vous conseille d'avoir plusieurs fichiers de sy`stèmes sous la main prêt
             *    à être chargés.
             *    
             * 2) Afficher le système (note: et le ToString() du système en "mode équation", et les matrices en vue matrices qui composent les équiations
             * 3) Résoudre avec Cramer
             * 4) Résoudre avec la méthode de la matrice inverse : si dét. nul, on recoit nul et on doit afficher un message à l'utilisateur
             * 5) Résoudre avec Gauss
             * 6) Résoudre
             * 
             * Après chaque option on revient au menu utilisateur, sauf pour quitter bien évidemment.
             * 
             */

            System system = new System(new Matrix2D("A", 3, 3), new Matrix2D("A", 3, 1));
            try
            {
                Tuple<Matrix2D, Matrix2D> matrixes = LoadMatrixesFromFile("default.txt");
                system = new System(matrixes.Item1, matrixes.Item2);
            }
            catch (Exception)
            {
            }

            string option;

            do
            {
                Console.Clear();
                Console.WriteLine("1) Charger un fichier de système");
                Console.WriteLine("2) Afficher le système");
                Console.WriteLine("3) Résoudre avec Cramer");
                Console.WriteLine("4) Résoudre avec la méthode de la matrice inverse");
                Console.WriteLine("5) Résoudre avec Gauss");
                Console.WriteLine("6) Exit");
                Console.Write("\nSelectionnez une option: ");

                switch (option = Console.ReadLine())
                {
                    case "1":
                        Console.Write("Entrez le nom du fichier: ");
                        try
                        {
                            Tuple<Matrix2D, Matrix2D>  matrixes = LoadMatrixesFromFile(Console.ReadLine());
                            system = new System(matrixes.Item1, matrixes.Item2);
                            Console.WriteLine("Système chargé:");
                            Console.Write(system);
                        } catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Système:");
                        Console.Write(system);
                        break;
                    case "3":
                        if (system.IsValid()) Console.Write(system.SolveUsingCramer());
                        else Console.WriteLine("Système invalide.");
                        break;
                    case "4":
                        if (system.IsValid()) Console.Write(system.SolveUsingInverseMatrix());
                        else Console.WriteLine("Système invalide.");
                        break;
                    case "5":
                        if (system.IsValid()) Console.Write(system.SolveUsingGauss());
                        else Console.WriteLine("Système invalide.");
                        break;
                    default:
                        break;
                }

                if (!option.Equals("6"))
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadLine();
                }
            } while (option != "6");
        }

        static Tuple<Matrix2D, Matrix2D> LoadMatrixesFromFile(string filePath)
        {
            if (!File.Exists(filePath)) throw new Exception("Fichier introuvable.");

            string[] lines = File.ReadAllLines(filePath);

            if(lines.Length < 2)
                throw new Exception("Fichier invalide.");

            Matrix2D a = LoadMatrix("A", lines.Take(lines.Length / 2).ToArray());
            if(!a.IsSquare()) new Exception("Fichier invalide.");
            Matrix2D b = LoadMatrix("B", lines.Skip(lines.Length / 2).Take(lines.Length / 2).ToArray());
            if(b.Matrix.GetLength(1) != 1) new Exception("Fichier invalide.");
            return new Tuple<Matrix2D, Matrix2D>(a, b);
        }

        static Matrix2D LoadMatrix(string name, string[] lines)
        {
            if(lines.Length == 0) throw new Exception("Fichier invalide.");

            int columns = lines[0].Split(' ').Length;

            Matrix2D matrix = new Matrix2D(name, lines.Length, columns);
            for (int l = 0; l < lines.Length; l++)
            {
                String[] column = lines[l].Split(' ');
                if (column.Length != columns) throw new Exception("Fichier invalide.");
                try
                {
                    for (int col = 0; col < column.Length; col++)
                        matrix.Set(l, col, int.Parse(column[col]));
                } catch (Exception e)
                {
                    throw new Exception("Fichier invalide.");
                }
            }

            return matrix;
        }
    }
}
