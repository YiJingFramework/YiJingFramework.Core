using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Core
{
    /// <summary>
    /// 卦画。
    /// 爻位置越低，序号越小。
    /// A painting made up by the yin and yang lines.
    /// The lower a line, the smaller its index.
    /// </summary>
    public class Painting : IReadOnlyList<YinYang>, IComparable<Painting>, IEquatable<Painting>
    {
        private readonly YinYang[] lines;
        /// <summary>
        /// 创建新实例。
        /// Initializes a new instance.
        /// </summary>
        /// <param name="lines">
        /// 各爻的性质。
        /// The lines' attributes.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> 是 <c>null</c> 。
        /// <paramref name="lines"/> is <c>null</c>.
        /// </exception>
        public Painting(params YinYang[] lines)
            : this((IEnumerable<YinYang>)lines) { }

        /// <summary>
        /// 创建新实例。
        /// Initializes a new instance.
        /// </summary>
        /// <param name="lines">
        /// 各爻的性质。
        /// The lines' attributes.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> 是 <c>null</c> 。
        /// <paramref name="lines"/> is <c>null</c>.
        /// </exception>
        public Painting(IEnumerable<YinYang> lines)
        {
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            this.lines = lines.ToArray();
        }

        #region Collecting
        /// <summary>
        /// 获取某一根爻的性质。
        /// Get the attribute of a line.
        /// </summary>
        /// <param name="index">
        /// 爻的序号。
        /// The index of the line.
        /// </param>
        /// <returns>
        /// 爻的性质。
        /// The line.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="index"/> 超出范围。
        /// <paramref name="index"/> is out of range.
        /// </exception>
        public YinYang this[int index]
            => this.lines[index];

        /// <summary>
        /// 获取爻的个数。
        /// Get the count of the lines.
        /// </summary>
        public int Count
            => this.lines.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<YinYang> GetEnumerator()
        {
            return ((IEnumerable<YinYang>)this.lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lines.GetEnumerator();
        }
        #endregion

        #region Comparing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Painting? other)
        {
            if (other is null)
                return 1;

            var thisLength = this.lines.Length;
            {
                var otherLength = other.lines.Length;
                if (thisLength > otherLength)
                    return 1;
                else if (thisLength < otherLength)
                    return -1;
            }

            for (int i = thisLength - 1; i >= 0; i--)
            {
                var cur = this.lines[i];
                var com = other.lines[i];

                var cr = cur.CompareTo(com);
                if (cr != 0)
                    return cr;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 1;
                foreach (var line in this.lines)
                {
                    result <<= 1;
                    result += (int)line;
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object? other)
        {
            if (other is Painting painting)
                return this.lines.SequenceEqual(painting.lines);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Painting? other)
        {
            if (other is null)
                return false;
            return this.lines.SequenceEqual(other.lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Painting? left, Painting? right)
        {
            if (left is null)
                return right is null;
            if (right is null)
                return false;
            return left.lines.SequenceEqual(right.lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Painting? left, Painting? right)
        {
            if (left is null)
                return right is not null;
            if (right is null)
                return true;
            return !left.lines.SequenceEqual(right.lines);
        }
        #endregion

        #region Converting

        /// <summary>
        /// 返回一个可以完全表示此卦画的字符串。
        /// 可以使用 <seealso cref="TryParse(string?, out Painting)"/> 转换回。
        /// Returns a string that can completely represents the painting.
        /// You can use <seealso cref="TryParse(string?, out Painting)"/> to convert it back.
        /// </summary>
        /// <returns>
        /// 结果字符串。
        /// The string.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new(this.lines.Length);
            foreach(var line in lines)
                _ = stringBuilder.Append((int)line);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从字符串转回。
        /// Convert from a string.
        /// </summary>
        /// <param name="s">
        /// 可以表示此卦画的字符串。
        /// The string that represents the painting.
        /// </param>
        /// <param name="result">
        /// 卦画。
        /// The painting.
        /// </param>
        /// <returns>
        /// 一个指示转换成功与否的值。
        /// A value indicates whether it has been successfully converted or not.
        /// </returns>
        public static bool TryParse(
            [NotNullWhen(true)] string? s,
            [MaybeNullWhen(false)] out Painting result)
        {
            if (s is null)
            {
                result = null;
                return false;
            }
            List<YinYang> r = new(s.Length);
            foreach(var c in s)
            {
                switch (c)
                {
                    case '0':
                        r.Add(YinYang.Yin);
                        break;
                    case '1':
                        r.Add(YinYang.Yang);
                        break;
                    default:
                        result = null;
                        return false;
                }
            }
            result = new Painting(r);
            return true;
        }

        /// <summary>
        /// 返回一个可以完全表示此卦画的字节数组。
        /// 可以使用 <seealso cref="FromBytes(byte[])"/> 转换回。
        /// Returns a byte array that can completely represents the painting.
        /// You can use <seealso cref="FromBytes(byte[])"/> to convert it back.
        /// </summary>
        /// <returns>
        /// 结果字节数组。
        /// The array.
        /// </returns>
        public byte[] ToBytes()
        {
            var thisLength = this.lines.Length;
            BitArray bitArray = new(thisLength + 1);
            for (int i = 0; i < thisLength; i++)
                bitArray.Set(i, (bool)this.lines[i]);
            bitArray.Set(thisLength, true);
            byte[] bytes = new byte[(bitArray.Length + 7) / 8];
            bitArray.CopyTo(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// 从字节数组转回。
        /// Convert from a byte array.
        /// </summary>
        /// <param name="bytes">
        /// 可以表示此卦画的字节数组。
        /// The byte array that represents the painting.
        /// </param>
        /// <returns>
        /// 卦画。
        /// The painting.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="bytes"/> 是 <c>null</c> 。
        /// <paramref name="bytes"/> is <c>null</c>.
        /// </exception>
        public static Painting FromBytes(params byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));
            BitArray bitArray = new(bytes);
            List<YinYang> r = new(bitArray.Length);
            int zeroCount = 0;
            for (int i = 0; i < bitArray.Length; i++)
            {
                var bit = bitArray[i];
                if (bit)
                {
                    for (int j = 0; j < zeroCount; j++)
                        r.Add(YinYang.Yin);
                    r.Add(YinYang.Yang);
                    zeroCount = 0;
                }
                else
                    zeroCount++;
            }
            r.RemoveAt(r.Count - 1);
            return new Painting(r);
        }
        #endregion
    }
}
