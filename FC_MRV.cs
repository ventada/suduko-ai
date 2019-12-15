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
    class FC_MRV
    {
        public static List<int>[,] sudoko;        
        public static int n;        
        public static int[,] MRV_Array;
        public void Main()
            
        {
            int[,] board;
            #region stream reader:
            StreamReader reader = new StreamReader(@"E:\test.txt");
            n = int.Parse(reader.ReadLine());
            board = new int[n * n, n * n];
            MRV_Array = new int[n * n, n * n];
            sudoko = new List<int>[n * n, n * n];
            for (int i = 0; i < n * n; i++)
            {
                String temp = reader.ReadLine();
                String[] s2 = temp.Split(' ');
                for (int j = 0; j < n * n; j++)
                {
                    sudoko[i, j] = new List<int>();
                    sudoko[i, j].Add(int.Parse(s2[j]));
                    board[i, j] = int.Parse(s2[j]);
                    MRV_Array[i, j] = n * n + 1;


                }

            }
            reader.Close();
            #endregion
            


                Setting_Valid_value();
            int[] pos = Exist(0, 2, board);
            for (int m = 0; m < n * n + 1; m++)
                Console.Write(pos[m]);
            Sudoku_Solver(board);


        }


        public static void Sudoku_Solver(int[,] s)
        {



            if (Isfull(s))
            {
                Console.WriteLine("DOOOOOOOOOOOOONNNNNNNNNNNNNNE");

                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        Console.Write(s[i,j]+"\t");
                    }
                    Console.WriteLine("\n");
                }
                return ;
            }
            else
            {

                int row =0 , column = 0, min = n * n + 1;
                
                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                       
                        if (MRV_Array[i, j] < min && s[i,j]==0)
                        {
                            row = i;
                            column = j;
                            
                            min = MRV_Array[i, j]; 
                        }
                    }
                }
               // int MRv = MRV_Array[row, column];
               
                int[] pos = Exist(row, column, s);

                 for (int p = 1; p <= n * n; p++)
                {
                    if (pos[p]==0)
                    {
                        s[row, column] = p;
                        Sudoku_Solver(s);
                    }
                } 

               s[row, column] = 0;//BackTrack
               //MRV_Array[row, column] = MRv;


            }

        }
        public static Boolean Isfull(int[,] s)
        {
            //int[,] temp1 = new int[n * n, n * n];
           // int[,] temp2 = new int[n * n, n * n];

             int temp = n * n + 1;
        for (int i = 0; i < n * n; i++)
        {
            for (int j = 0; j < n * n; j++)
            {
                if(s[i,j]==0)
                    return false;
            }
        } 
            return true;
        }
        public static void Setting_Valid_value()
        {

            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    if (sudoko[i, j].Count == 1 && sudoko[i, j][0] == 0)//خالی نباشه و اندازش برابر 1 باشه و مقدارش برابر 0 باشه
                    {
                        String s = Exist(i, j);
                        Console.WriteLine(s);
                        MRv_count(i, j, s);

                    }

                }

            }

            /*for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(MRV_Array[i, j] + " ");
                }
                Console.WriteLine(" ");
            }*/
        }     

        public static int[] Exist(int i,int j,int[,] array)
        {
            int[] popossibilityArray = new int[n*n+1];
            for (int m = 0; m < n * n + 1; m++)
                popossibilityArray[m] = 0;
            int row = i / n;
            int column = j / n;
            int section = row * n + column + 1;
            for (int p = 0; p < n * n; p++)
            {

                if (array[i, p] != 0 )
                {
                    //Console.WriteLine(sudoko[i, p][0]+"  "+ sudoko[i, p].Count);
                   popossibilityArray[array[i, p]] =1;//اگه مقدار خونه مخالف صفر بود و اندازش برابر 1 بود یعنی مقدار از اول بوده و تئ سطر خونه مورد نظرمون ثرار داره و دارای کانفلیکت هستش
                }
            }//مربوط به سطر


            for (int q = 0; q < n * n; q++)
            {

                if (array[q, j]!=0    )
                {
                    //Console.WriteLine(sudoko[q, j][0] + "  " + sudoko[q, j].Count);
                    popossibilityArray[array[q, j]] = 1; //مث بالا فقط ستون حساب میشه
                }
            }//مربوط به ستون*/

            for (int p = row * n; p < row * n + 3; p++)
            {
                for (int q = column * n; q < column * n + 3; q++)
                {
                    if (array[p,q] != 0 )
                    {
                        //Console.WriteLine(sudoko[p, q][0] + "  " + sudoko[p, q].Count);
                        popossibilityArray[array[p,q]] = 1;
                    }
                }
            }//کانفلکتهای مربوط به سکشن مربوط به خونه


            return popossibilityArray;
        }
        public static string Exist(int i, int j)
        {

            string s = "";
            int row = i / n;
            int column = j / n;
            int section = row * n + column + 1;
            for (int p = 0; p < n * n; p++)
            {

                if (sudoko[i, p].Count == 1 && sudoko[i, p][0] != 0 && !(p == j))
                {
                    //Console.WriteLine(sudoko[i, p][0]+"  "+ sudoko[i, p].Count);
                    s += sudoko[i, p][0];//اگه مقدار خونه مخالف صفر بود و اندازش برابر 1 بود یعنی مقدار از اول بوده و تئ سطر خونه مورد نظرمون ثرار داره و دارای کانفلیکت هستش
                }
            }//مربوط به سطر


            for (int q = 0; q < n * n; q++)
            {

                if (sudoko[q, j].Count == 1 && sudoko[q, j][0] != 0 && !(q == j))
                {
                    //Console.WriteLine(sudoko[q, j][0] + "  " + sudoko[q, j].Count);
                    s += sudoko[q, j][0]; //مث بالا فقط ستون حساب میشه
                }
            }//مربوط به ستون*/

            for (int p = row * n; p < row * n + 3; p++)
            {
                for (int q = column * n; q < column * n + 3; q++)
                {
                    if (sudoko[p, q].Count == 1 && sudoko[p, q][0] != 0)
                    {
                        //Console.WriteLine(sudoko[p, q][0] + "  " + sudoko[p, q].Count);
                        s += sudoko[p, q][0];
                    }
                }
            }//کانفلکتهای مربوط به سکشن مربوط به خونه


            return s;
        }
        public static void MRv_count(int i, int j, string s)
        {
            //sudoko[i, j].Clear();
            for (int p = 1; p <= n * n; p++)
            {
                if (!s.Contains(p + "") && !sudoko[i, j].Contains(p))
                {
                    sudoko[i, j].Add(p);
                }
            }
            MRV_Array[i, j] = sudoko[i, j].Count;
            Console.WriteLine(i + " " + j);
            Console.WriteLine(sudoko[i, j].Count);
            for (int k = 0; k < sudoko[i, j].Count; k++)
                Console.WriteLine(sudoko[i, j][k]);
            Console.WriteLine("-------------------");
        }

        /*public static void Find_Min()
        {
            int row = -1, column = -1;
            int min = n * n + 1;
            while (Check_End())
            {
                for(int i = 0; i < n * n; i++)
                {
                    for(int j = 0; j < n * n; j++)
                    {
                        if (MRV_Array[i, j] < min)
                        {
                            min = MRV_Array[i, j];
                            row = i;
                            column = j;
                        }
                    }
                }
                if (min == n * n + 1) { Console.Write("ens"); break; }

                else
                {
                    if (check())
                    {
                       

                        

                            Random rnd = new Random(); int random = rnd.Next(1, sudoko[row, column].Count);
                            Console.Write("Random" + random+"\n");
                            stack.Push(sudoko); MRV_Stack.Push(MRV_Array);
                            int temp = sudoko[row, column][random - 1];
                            sudoko[row, column].Clear();
                            sudoko[row, column].Add(temp);
                            MRV_Array[row, column] = n * n + 1;
                            Delete_adjacent(row, column, temp);
                        
                    }
                    else
                    {
                        if(stack.Count!=0 && MRV_Stack.Count != 0) {
                            stack.Pop(); MRV_Stack.Pop();
                            sudoko = stack.Pop();
                        MRV_Array = MRV_Stack.Pop();}
                       
                    }


                }
            }
            Console.Write("n");
            Console.Write("After while");
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Console.Write(sudoko[i, j][0] + " ");
                }
                Console.WriteLine();
                Console.Write("---------------\n");
            }
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Console.Write(MRV_Array[i, j] + " ");
                }
                Console.WriteLine();
                Console.Write("---------------\n");
            }

        }*/
        public static Boolean check()
        {

            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    if (MRV_Array[i, j] == 1)
                        return false;
                }
                
            }
            return true;
        }
        public static Boolean Check_End()
        {
            Boolean b = false;
            int check = n * n + 1;
            for(int i = 0; i < n * n; i++)
            {
                for(int j = 0; j < n * n; j++)
                {
                    if (MRV_Array[i, j] != check)
                    { b = true; break; }
                }
                if (b) break;
            }
               



            return b;
        }

       /* public static void Find_Min(int temp)
        {
            int row = -1, column = -1;
            
            while (Exist(temp))
            {
                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        if (MRV_Array[i, j] == temp)
                        {
                            row = i; column = j;
                            int t = sudoko[row, column][1];
                            sudoko[row, column].Clear();
                            sudoko[row, column].Add(t);
                            MRV_Array[row, column] = n * n + 1;
                            Delete_adjacent(row, column, t);
                        }
                    }
                }
            }
        }*/
        public static Boolean Exist(int temp)
        {
            Boolean b = false;
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    if (MRV_Array[i, j] == temp)
                    { b = true; break; }
                }
                if (b)
                    break;

            }
            return b;
        }       
        public static void Delete_adjacent(int i, int j, int number)
        {
            int row = i / n;
            int column = j / n;
            int section = row * n + column + 1;

            for (int p = 0; p < n * n; p++)
            {

                if (sudoko[i, p].Count > 1 && !(p == j) && sudoko[i, p].Contains(number))
                {
                    sudoko[i, p].Remove(number);
                    --MRV_Array[i, p];
                }
            }//مربوط به سطر


            for (int q = 0; q < n * n; q++)
            {

                if (sudoko[q, j].Count > 1 && !(q == j) && sudoko[q, j].Contains(number))
                {
                    sudoko[q, j].Remove(number);
                    --MRV_Array[q, j];
                }
            }//مربوط به ستون*/

            for (int p = row * n; p < row * n + 3; p++)
            {
                for (int q = column * n; q < column * n + 3; q++)
                {
                    if (sudoko[p, q].Count > 1 && !(p == i) && !(q == j) && sudoko[p, q].Contains(number))
                    {
                        sudoko[p, q].Remove(number);
                        --MRV_Array[p, q];
                    }
                }
            }//کانفلکتهای مربوط به سکشن مربوط به خونه

        }

    }
}
