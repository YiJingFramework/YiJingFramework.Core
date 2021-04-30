using System;
using YiJingFramework.Core;

namespace YinYangUseCases
{
    class Program
    {
        static void Main()
        {
            #region to get or convert yin-yangs
            YinYang yin = YinYang.Yin;
            YinYang yang = new YinYang(isYang: true);
            Console.WriteLine($"{yin.ToString()}-{yang}!");
            Console.WriteLine();
            // Output: Yin-Yang!

            _ = YinYang.TryParse(" yang \t\n", out yang);
            // case-insensitive and allows white spaces preceding and trailing.

            yin = (YinYang)false;

            Console.WriteLine($"-1 0 1 2: {(YinYang)(-1)} {(YinYang)0} {(YinYang)1} {(YinYang)2}");
            Console.WriteLine();
            // Output: -1 0 1 2: Yang Yin Yang Yang

            Console.WriteLine($"2 -> {(int)((YinYang)2)}");
            Console.WriteLine();
            // Output: 2 -> 1
            #endregion

            #region to do calculation on yin-yangs
            // Calculations on yin-yangs works as booleans (yang: true, yin: false).
            Console.WriteLine($"yin&yang: {yin & yang} yin|yang: {yin | yang} yin^yang: {yin ^ yang} !yin: {!yin}");
            // Output: yin&yang: Yin yin|yang: Yang yin^yang: Yang !yin: Yang
            #endregion
        }
    }
}
