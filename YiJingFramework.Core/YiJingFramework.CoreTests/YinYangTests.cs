using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System;

namespace YiJingFramework.Core.Tests
{
    [TestClass()]
    public class YinYangTests
    {
        [TestMethod()]
        public void CalculatingTest()
        {
            Assert.AreEqual(YinYang.Yang, YinYang.Yang & YinYang.Yang);
            Assert.AreEqual(YinYang.Yin, YinYang.Yang & YinYang.Yin);
            Assert.AreEqual(YinYang.Yin, YinYang.Yin & YinYang.Yang);
            Assert.AreEqual(YinYang.Yin, YinYang.Yin & YinYang.Yin);

            Assert.AreEqual(YinYang.Yang, YinYang.Yang | YinYang.Yang);
            Assert.AreEqual(YinYang.Yang, YinYang.Yang | YinYang.Yin);
            Assert.AreEqual(YinYang.Yang, YinYang.Yin | YinYang.Yang);
            Assert.AreEqual(YinYang.Yin, YinYang.Yin | YinYang.Yin);

            Assert.AreEqual(YinYang.Yin, YinYang.Yang ^ YinYang.Yang);
            Assert.AreEqual(YinYang.Yang, YinYang.Yang ^ YinYang.Yin);
            Assert.AreEqual(YinYang.Yang, YinYang.Yin ^ YinYang.Yang);
            Assert.AreEqual(YinYang.Yin, YinYang.Yin ^ YinYang.Yin);

            Assert.AreEqual(YinYang.Yin, !YinYang.Yang);
            Assert.AreEqual(YinYang.Yang, !YinYang.Yin);

            T Tide<T>(T t1) where T : IBitwiseOperators<T, T, T>
            {
                return ~t1;
            }
            Assert.AreEqual(YinYang.Yin, Tide(YinYang.Yang));
            Assert.AreEqual(YinYang.Yang, Tide(YinYang.Yin));
        }

        [TestMethod()]
        public void ConvertingTest()
        {
            Assert.AreEqual(false, YinYang.Yin.IsYang);
            Assert.AreEqual(true, YinYang.Yang.IsYang);
            Assert.AreEqual(false, new YinYang(false).IsYang);
            Assert.AreEqual(true, new YinYang(true).IsYang);

            Assert.AreEqual("Yin", YinYang.Yin.ToString());
            Assert.AreEqual("Yang", YinYang.Yang.ToString());
            Assert.AreEqual("Yin", new YinYang(false).ToString());
            Assert.AreEqual("Yang", new YinYang(true).ToString());
            Assert.AreEqual("阴", YinYang.Yin.ToString("C"));
            Assert.AreEqual("阳", YinYang.Yang.ToString("C"));
            Assert.AreEqual("Yin", YinYang.Yin.ToString("G"));
            Assert.AreEqual("Yang", YinYang.Yang.ToString(null));

            Assert.AreEqual(YinYang.Yin, YinYang.Parse("Yin"));
            Assert.AreEqual(YinYang.Yin, YinYang.Parse("\r\nYIN "));
            Assert.AreEqual(YinYang.Yin, YinYang.Parse("阴"));
            Assert.AreEqual(YinYang.Yang, YinYang.Parse("yANg"));
            Assert.AreEqual(YinYang.Yang, YinYang.Parse("\r\nYANG "));
            Assert.AreEqual(YinYang.Yang, YinYang.Parse("\r\n阳 "));

            Assert.IsTrue(YinYang.TryParse("Yin", out YinYang r));
            Assert.AreEqual(YinYang.Yin, r);
            Assert.IsTrue(YinYang.TryParse("\r\nYIN ", out r));
            Assert.AreEqual(YinYang.Yin, r);
            Assert.IsTrue(YinYang.TryParse("阴", out r));
            Assert.AreEqual(YinYang.Yin, r);
            Assert.IsTrue(YinYang.TryParse("yANg", out r));
            Assert.AreEqual(YinYang.Yang, r);
            Assert.IsTrue(YinYang.TryParse("\r\nYANG ", out r));
            Assert.AreEqual(YinYang.Yang, r);
            Assert.IsTrue(YinYang.TryParse("\r\n阳 ", out r));
            Assert.AreEqual(YinYang.Yang, r);
            Assert.IsFalse(YinYang.TryParse("yinyang", out _));
            Assert.IsFalse(YinYang.TryParse("false", out _));
            Assert.IsFalse(YinYang.TryParse(null, out _));

            T Parse<T>(string s) where T : IParsable<T>
            {
                return T.Parse(s, null);
            }
            bool TryParse<T>(string s, out T result) where T : IParsable<T>
            {
                return T.TryParse(s, null, out result);
            }
            Assert.AreEqual(YinYang.Yang, Parse<YinYang>("yang"));
            Assert.AreEqual(true, TryParse<YinYang>("yang", out _));

            Assert.AreEqual(false, (bool)YinYang.Yin);
            Assert.AreEqual(true, (bool)YinYang.Yang);
            Assert.AreEqual(false, (bool)new YinYang(false));
            Assert.AreEqual(true, (bool)new YinYang(true));

            Assert.AreEqual(0, (int)YinYang.Yin);
            Assert.AreEqual(1, (int)YinYang.Yang);
            Assert.AreEqual(0, (int)new YinYang(false));
            Assert.AreEqual(1, (int)new YinYang(true));

            Assert.AreEqual(YinYang.Yang, (YinYang)2);
            Assert.AreEqual(YinYang.Yang, (YinYang)1);
            Assert.AreEqual(YinYang.Yin, (YinYang)0);
            Assert.AreEqual(YinYang.Yang, (YinYang)(-1));

            Assert.AreEqual(YinYang.Yang, (YinYang)true);
            Assert.AreEqual(YinYang.Yin, (YinYang)false);
        }

        [TestMethod()]
        public void ComparingTest()
        {
            Assert.AreEqual(0, YinYang.Yang.CompareTo(YinYang.Yang));
            Assert.AreEqual(1, YinYang.Yang.CompareTo(YinYang.Yin));
            Assert.AreEqual(-1, YinYang.Yin.CompareTo(YinYang.Yang));
            Assert.AreEqual(0, YinYang.Yin.CompareTo(YinYang.Yin));

            Assert.AreEqual(true, YinYang.Yang.Equals(YinYang.Yang));
            Assert.AreEqual(false, YinYang.Yang.Equals(YinYang.Yin));
            Assert.AreEqual(false, YinYang.Yin.Equals(YinYang.Yang));
            Assert.AreEqual(true, YinYang.Yin.Equals(YinYang.Yin));

            Assert.AreEqual(true, YinYang.Yang.Equals((object)YinYang.Yang));
            Assert.AreEqual(false, YinYang.Yang.Equals((object)YinYang.Yin));
            Assert.AreEqual(false, YinYang.Yin.Equals((object)YinYang.Yang));
            Assert.AreEqual(true, YinYang.Yin.Equals((object)YinYang.Yin));
            Assert.AreEqual(false, YinYang.Yang.Equals(null));
            Assert.AreEqual(false, YinYang.Yin.Equals(true));

            Assert.AreEqual(YinYang.Yang.GetHashCode(), YinYang.Yang.GetHashCode());
            Assert.AreEqual(YinYang.Yin.GetHashCode(), YinYang.Yin.GetHashCode());
            Assert.AreNotEqual(YinYang.Yang.GetHashCode(), YinYang.Yin.GetHashCode());

            Assert.AreEqual(true, YinYang.Yang == YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yang == YinYang.Yin);
            Assert.AreEqual(false, YinYang.Yin == YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yin == YinYang.Yin);

            Assert.AreEqual(false, YinYang.Yang != YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yang != YinYang.Yin);
            Assert.AreEqual(true, YinYang.Yin != YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yin != YinYang.Yin);

            /*
#pragma warning disable CS0618 // 类型或成员已过时
            Assert.AreEqual(false, YinYang.Yang < YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yang < YinYang.Yin);
            Assert.AreEqual(true, YinYang.Yin < YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yin < YinYang.Yin);

            Assert.AreEqual(true, YinYang.Yang <= YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yang <= YinYang.Yin);
            Assert.AreEqual(true, YinYang.Yin <= YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yin <= YinYang.Yin);

            Assert.AreEqual(false, YinYang.Yang > YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yang > YinYang.Yin);
            Assert.AreEqual(false, YinYang.Yin > YinYang.Yang);
            Assert.AreEqual(false, YinYang.Yin > YinYang.Yin);

            Assert.AreEqual(true, YinYang.Yang >= YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yang >= YinYang.Yin);
            Assert.AreEqual(false, YinYang.Yin >= YinYang.Yang);
            Assert.AreEqual(true, YinYang.Yin >= YinYang.Yin);
#pragma warning restore CS0618 // 类型或成员已过时
            */
        }
    }
}