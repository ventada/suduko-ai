using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
       
        
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw;
            sw = Stopwatch.StartNew();
             FC_MRV fC_MRV = new FC_MRV();
             fC_MRV.Main();
            sw.Stop();
            Console.WriteLine("\n");
           // Min_Conflict mc = new Min_Conflict();
            //mc.Main();


            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            
            Console.Read();
        }

          }
    
}
