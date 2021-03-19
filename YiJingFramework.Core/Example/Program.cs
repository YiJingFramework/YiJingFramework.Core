using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YiJingFramework.Core;

namespace Example
{
    class Program
    {
        static void Main()
        {
            #region to create paintings
            var dui = new Painting(LineAttribute.Yang, LineAttribute.Yang, LineAttribute.Yin); // 兑

            var lineArray = new LineAttribute[] { LineAttribute.Yin, LineAttribute.Yang };
            var shaoYang = new Painting(lineArray); // 少阳

            IEnumerable<LineAttribute> GetRandomLines()
            {
                Random random = new Random();
                for (; ; )
                    yield return (LineAttribute)random.Next(0, 2);
            }
            var random = new Painting(GetRandomLines().Take(5)); // A painting with five lines.
            #endregion

            #region to use as lists of lines
            Console.WriteLine(dui.Count);
            Console.WriteLine();
            // Output: 3

            for (int i = 0; i < shaoYang.Count; i++)
                Console.Write(shaoYang[i]);
            Console.WriteLine();
            Console.WriteLine();
            // Output: YinYang

            foreach (var line in random)
                Console.Write(line);
            Console.WriteLine();
            Console.WriteLine();
            // The output will be the 5 random lines.
            #endregion

            #region convert to string and try parse
            Console.WriteLine(dui.ToString()); // yang: 1, yin: 0, yang-yang-yin: 110
            Console.WriteLine();
            // Output: 110

            Console.WriteLine(shaoYang.ToString());
            Console.WriteLine();
            // Output: 01

            var r = Painting.TryParse("111011111", out var myPainting);
            Debug.Assert(r is true);
            Debug.Assert(myPainting is not null);
            // 111011111 -> yang-yang-yang-yin-yang-yang-yang-yang-yang
            Console.WriteLine(myPainting[3]);
            Console.WriteLine();
            // Output: Yin
            #endregion

            #region convert to bytes and back
            byte[] bytes = dui.ToBytes();
            /* 
             * yang -> 1, yin -> 0
             * yang,yang,yin -> 0,1,1 (the order is not the same as the string representation)
             * 
             * Add a '1' to represent that it has ended: 0,1,1 -> 1,0,1,1
             * 
             * Expand to a whole byte: 1,0,1,1 -> 0,0,0,0,  1,0,1,1
             */
            Debug.Assert(bytes.Length == 1);
            Console.WriteLine(Convert.ToString(bytes[0], 2));
            Console.WriteLine();
            // Output: 1011

            bytes = myPainting.ToBytes(); 
            /*
             * 0,0,0,0,  0,0,1,1,  1,1,1,1,  0,1,1,1
             */
            Debug.Assert(bytes.Length == 2);
            Console.Write(Convert.ToString(bytes[0], 2));
            Console.Write(" ");
            Console.WriteLine(Convert.ToString(bytes[1], 2));
            Console.WriteLine();
            // Output: 11110111 11

            var myPainting2 = Painting.FromBytes(bytes);
            Console.WriteLine(myPainting2.Equals(myPainting));
            Console.WriteLine();
            // Output: True
            #endregion

            #region compare two paintings
            Console.WriteLine($"{myPainting2.Equals(myPainting)} " +
                $"{myPainting2 == myPainting} " +
                $"{myPainting2.CompareTo(myPainting)} " +
                $"{myPainting2 != myPainting}");
            // Output: True True 0 False
            #endregion
        }
    }
}
