﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YiJingFramework.Core.Tests
{
    [TestClass()]
    public class PaintingTests
    {
        private Painting GetEmptyPainting()
        {
            return new Painting();
        }
        private YinYang[] GetLinesOfPainting1()
        {
            return new YinYang[] { YinYang.Yang, YinYang.Yang, YinYang.Yin };
        }
        private Painting GetPainting1()
        {
            var p1 = new Painting(YinYang.Yang, YinYang.Yang, YinYang.Yin);
            var pp1 = new Painting(GetLinesOfPainting1());
            Assert.IsTrue(p1.SequenceEqual(pp1));
            return pp1;
        }

        private IEnumerable<YinYang> GetLinesOfPainting2()
        {
            yield return YinYang.Yang;
            yield return YinYang.Yang;
            yield return YinYang.Yin;
            yield return YinYang.Yang;
        }
        private Painting GetPainting2()
        {
            return new Painting(GetLinesOfPainting2());
        }
        [TestMethod()]
        public void PaintingTest()
        {
            _ = GetEmptyPainting();
            _ = GetPainting1();
            _ = GetPainting2();
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            var p0 = GetEmptyPainting();
            Assert.AreEqual(0, p0.Count());
            var p1 = GetPainting1();
            Assert.IsTrue(p1.SequenceEqual(GetLinesOfPainting1()));
            var p2 = GetPainting2();
            Assert.IsTrue(p2.SequenceEqual(GetLinesOfPainting2()));
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var p0 = GetEmptyPainting();
            var p1 = GetPainting1();
            var p2 = GetPainting2();
            Assert.AreEqual(-1, p0.CompareTo(p1));
            Assert.AreEqual(1, p1.CompareTo(p0));
            Assert.AreEqual(-1, p1.CompareTo(p2));
            Assert.AreEqual(1, p2.CompareTo(p1));
            Assert.AreEqual(1, p2.CompareTo(null));
            Assert.AreEqual(0, p2.CompareTo(p2));

            var p3 = GetPainting2();
            Assert.AreEqual(0, p2.CompareTo(p3));

            var p4 = new Painting(YinYang.Yin, YinYang.Yin, YinYang.Yang);
            var p5 = new Painting(YinYang.Yang, YinYang.Yang, YinYang.Yang);
            Assert.AreEqual(-1, p4.CompareTo(p5));
            Assert.AreEqual(1, p5.CompareTo(p4));

            var p6 = new Painting(YinYang.Yang, YinYang.Yang, YinYang.Yin);
            var p7 = new Painting(YinYang.Yang, YinYang.Yang, YinYang.Yang);
            Assert.AreEqual(-1, p6.CompareTo(p7));
            Assert.AreEqual(1, p7.CompareTo(p6));

            p6 = new Painting(YinYang.Yang, YinYang.Yin, YinYang.Yang, YinYang.Yang);
            p7 = new Painting(YinYang.Yin, YinYang.Yang, YinYang.Yang, YinYang.Yang);
            Assert.AreEqual(-1, p6.CompareTo(p7));
            Assert.AreEqual(1, p7.CompareTo(p6));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var p0 = GetEmptyPainting();
            Assert.AreEqual(0b1, p0.GetHashCode());
            var p1 = GetPainting1();
            Assert.AreEqual(0b1110, p1.GetHashCode());
            var p2 = GetPainting2();
            Assert.AreEqual(0b11101, p2.GetHashCode());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsFalse(new Painting().Equals(null));
            Random random = new Random();
            for (int i = 0; i < 20;)
            {
                var c = random.Next(5, 10);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                {
                    lines1.Add((YinYang)random.Next(0, 2));
                }
                List<YinYang> lines2 = new();
                for (int j = 0; j < c; j++)
                {
                    lines2.Add((YinYang)random.Next(0, 2));
                }
                if (lines1.SequenceEqual(lines2))
                {
                    Assert.IsTrue(new Painting(lines1).Equals(new Painting(lines2)));
                    i++;
                }
                else
                {
                    Assert.IsFalse(new Painting(lines1).Equals((object)new Painting(lines2)));
                }
            }
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            Assert.IsFalse(new Painting().Equals((object)null));
            Random random = new Random();
            for (int i = 0; i < 20;)
            {
                var c = random.Next(5, 10);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                {
                    lines1.Add((YinYang)random.Next(0, 2));
                }
                List<YinYang> lines2 = new();
                for (int j = 0; j < c; j++)
                {
                    lines2.Add((YinYang)random.Next(0, 2));
                }
                if (lines1.SequenceEqual(lines2))
                {
                    Assert.IsTrue(new Painting(lines1).Equals((object)new Painting(lines2)));
                    i++;
                }
                else
                {
                    Assert.IsFalse(new Painting(lines1).Equals((object)new Painting(lines2)));
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var p0 = GetEmptyPainting();
            Assert.AreEqual("", p0.ToString());
            var p1 = GetPainting1();
            Assert.AreEqual("110", p1.ToString());
            var p2 = GetPainting2();
            Assert.AreEqual("1101", p2.ToString());
        }

        [TestMethod()]
        public void ParseTest()
        {
            T Parse<T>(string s) where T : IParsable<T>
            {
                return T.Parse(s, null);
            }

            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var c = random.Next(0, 100);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                    lines1.Add((YinYang)random.Next(0, 2));
                var painting = new Painting(lines1);
                Assert.IsTrue(Painting.Parse(painting.ToString()).SequenceEqual(painting));

                Assert.IsTrue(Parse<Painting>(painting.ToString()).SequenceEqual(painting));
            }
        }

        [TestMethod()]
        public void TryParseTest()
        {
            bool TryParse<T>(string s, out T result) where T : IParsable<T>
            {
                return T.TryParse(s, null, out result);
            }

            Assert.IsFalse(Painting.TryParse("1112", out var r));
            Assert.IsNull(r);
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var c = random.Next(0, 100);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                    lines1.Add((YinYang)random.Next(0, 2));
                var painting = new Painting(lines1);
                Assert.IsTrue(Painting.TryParse(painting.ToString(), out var rr));
                Assert.IsTrue(rr.SequenceEqual(painting));

                Assert.IsTrue(TryParse<Painting>(painting.ToString(), out _));
            }
        }

        [TestMethod()]
        public void ToBytesTest()
        {
            var p0 = GetEmptyPainting();
            BitArray bitArray0 = new BitArray(new bool[] { true });
            byte[] bytes0 = new byte[(bitArray0.Length + 7) / 8];
            bitArray0.CopyTo(bytes0, 0);
            Assert.IsTrue(p0.ToBytes().SequenceEqual(bytes0));
            var p1 = GetPainting1();
            BitArray bitArray1 = new BitArray(new bool[] { true, true, false, true });
            byte[] bytes1 = new byte[(bitArray1.Length + 7) / 8];
            bitArray1.CopyTo(bytes1, 0);
            Assert.IsTrue(p1.ToBytes().SequenceEqual(bytes1));
            var p2 = GetPainting2();
            BitArray bitArray2 = new BitArray(new bool[] { true, true, false, true, true });
            byte[] bytes2 = new byte[(bitArray2.Length + 7) / 8];
            bitArray2.CopyTo(bytes2, 0);
            Assert.IsTrue(p2.ToBytes().SequenceEqual(bytes2));
        }

        [TestMethod()]
        public void FromBytesTest()
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var c = random.Next(0, 100);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                    lines1.Add((YinYang)random.Next(0, 2));
                var painting = new Painting(lines1);
                Assert.IsTrue(Painting.FromBytes(painting.ToBytes())
                    .SequenceEqual(painting));
            }
        }

        [TestMethod()]
        public void PropertiesTest()
        {
            var p0 = GetEmptyPainting();
            var p1 = GetPainting1();

            Assert.AreEqual(0, p0.Count);
            Assert.AreEqual(3, p1.Count);
            Assert.AreEqual(YinYang.Yang, p1[0]);
            Assert.AreEqual(YinYang.Yang, p1[1]);
            Assert.AreEqual(YinYang.Yin, p1[2]);
        }
        [TestMethod()]
        public void OperatorsTest()
        {
            Assert.IsFalse(new Painting() == null);
            Assert.IsFalse(null == new Painting());
            Assert.IsTrue(new Painting() != null);
            Assert.IsTrue(null != new Painting());
            Random random = new Random();
            for (int i = 0; i < 20;)
            {
                var c = random.Next(5, 10);
                List<YinYang> lines1 = new();
                for (int j = 0; j < c; j++)
                {
                    lines1.Add((YinYang)random.Next(0, 2));
                }
                List<YinYang> lines2 = new();
                for (int j = 0; j < c; j++)
                {
                    lines2.Add((YinYang)random.Next(0, 2));
                }
                if (lines1.SequenceEqual(lines2))
                {
                    Assert.IsTrue(new Painting(lines1) == new Painting(lines2));
                    Assert.IsFalse(new Painting(lines1) != new Painting(lines2));
                    Assert.IsTrue(new Painting(lines2) == new Painting(lines1));
                    Assert.IsFalse(new Painting(lines2) != new Painting(lines1));
                    i++;
                }
                else
                {
                    Assert.IsFalse(new Painting(lines1) == new Painting(lines2));
                    Assert.IsTrue(new Painting(lines1) != new Painting(lines2));
                    Assert.IsFalse(new Painting(lines2) == new Painting(lines1));
                    Assert.IsTrue(new Painting(lines2) != new Painting(lines1));
                }
            }
        }
    }
}