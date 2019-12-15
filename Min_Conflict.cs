using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Text;
using System.CodeDom;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    class Min_Conflict
    {
        public static int n = 0;
        //public static int[,] sudoku;
        
        public void Main()
        {
            #region stream reader:
            StreamReader reader = new StreamReader(@"E:\test.txt");
            n = int.Parse(reader.ReadLine());
            List<int[]> list = new List<int[]>();
            int[,] sudoku = new int[n * n, n * n];
            for (int i = 0; i < n * n; i++)
            {
                String temp = reader.ReadLine();
                String[] s2 = temp.Split(' ');
                for (int j = 0; j < n * n; j++)
                {
                    if (int.Parse(s2[j]) == 0)
                    {
                        int[] t= { i, j };
                        list.Add(t);
                    }
                    sudoku[i, j] = int.Parse(s2[j]);

                }
            }
            reader.Close();
            #endregion

            for(int i = 0; i < n * n; i++)
            {
                for(int j = 0; j < n * n; j++)
                {
                    Random rnd = new Random();
                    int random = rnd.Next(1, n * n + 1);
                    if (sudoku[i, j]==0)
                    {
                        sudoku[i, j] = random;
                    }
                }
            }
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Console.Write(sudoku[i, j] + "\t");

                    //conflictsColumn[array[j, i], j]++;
                }
                Console.Write("\n ");
            }
            Console.Write("\n "); Console.Write("\n "); Console.Write("\n ");

            MinConflict(sudoku);
            Sudoku_Solver(sudoku, list);

        }

        public static int MinConflict(int[,] array)
        {
            int sub = 0;
            int[,] conflictsRow = new int[n * n, n * n];
            int[,] conflictsColumn = new int[n * n, n * n];
            for (int i = 0; i < n * n; i++)
            {
                for(int j = 0; j < n * n; j++)
                {
                    conflictsRow[i, j] = 0;
                    conflictsColumn[i, j] = 0;
                }
            }
            
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    conflictsRow[i, array[i, j] - 1]++;
                    conflictsColumn[i, array[j, i] - 1]++;
                    //conflictsColumn[array[j,i],j]++;
                }
            }
            /*int section = 0;
            for (int p = 0; p < n; p++)
                for (int q = 0; q < n; q++)
                {
                    for (int i = p * n; i < (p + 1) * n; i++)
                    {
                        for (int j = p * n; j < (p + 1) * n; j++)
                        {
                            Console.Write(i + " " + j + "\n");
                        }
                     
                    }
                    Console.Write(++section+"\n");
                }*/
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    if (conflictsRow[i, j] != 0 && conflictsRow[i, j] != 1)
                        sub += conflictsRow[i, j] - 1;

                    if (conflictsColumn[i, j] != 0 && conflictsColumn[i, j] != 1)
                        sub += conflictsColumn[i, j] - 1;                    
                   
                }
            }

            //Console.WriteLine(sub);

            return sub;

        }
        public static void Sudoku_Solver(int[,] s, List<int[]> list )
        {
            if (Zero_Conflict(s))
            {
                Console.WriteLine("DOOOOOOOOOOOOONNNNNNNNNNNNNNE");

                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        Console.Write(s[i, j] + "\t");
                    }
                    Console.WriteLine("\n");
                }
                return;
            }
            else
            {

                int row = 0, column = 0, min = n * n + 1;

                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {

                        if (contain(i,j,list))
                        {
                            row = i;
                            column = j;

                            
                        }
                    }
                }
                // int MRv = MRV_Array[row, column];

                int[] pos = { row, column };
                int temp = s[row, column];
                for (int p = 1; p <= n * n; p++)
                {
                    if (p!=s[row,column])
                    {
                        s[row, column] = p;
                        Sudoku_Solver(s,list);
                    }
                }

                s[row, column] = temp;
                list.Add(pos);



            }
        }

             public static Boolean contain(int i, int j, List<int[]> list)
            {
                for(int p = 0; p < list.Count; p++)
                {
                    if (i == list[p][0] && j == list[p][1]) {
                    list.Remove(list[p]);
                        return true;
                    }
                }
                return false;
            }
        public static Boolean Zero_Conflict(int[,] s)
        {
            if (MinConflict(s) == 0)
                return true;

            return false;
            
        }
        }
}
