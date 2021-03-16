using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Core.Exceptions;

namespace YiJingFramework.Core
{
    /// <summary>
    /// 卦画。
    /// 爻位置越低，序号越小。
    /// A painting made up by the yin and yang lines.
    /// The lower a line, the smaller its index.
    /// </summary>
    public class Painting : IReadOnlyList<LineAttribute>, IComparable<Painting>, IEquatable<Painting>
    {
        private readonly LineAttribute[] checkedLines;
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
        /// <exception cref="UnexpectedLineAttributeException">
        /// <paramref name="lines"/> 中存在不应该出现的值。
        /// <paramref name="lines"/> contains a invalid value.
        /// </exception>
        public Painting(params LineAttribute[] lines)
            : this((IEnumerable<LineAttribute>)lines) { }

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
        /// <exception cref="UnexpectedLineAttributeException">
        /// <paramref name="lines"/> 中存在不应该出现的值。
        /// <paramref name="lines"/> contains a invalid value.
        /// </exception>
        public Painting(IEnumerable<LineAttribute> lines)
        {
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            var list = new List<LineAttribute>();
            foreach (var line in lines)
            {
                if (line is not LineAttribute.Yang and not LineAttribute.Yin)
                    throw new UnexpectedLineAttributeException(line);
                list.Add(line);
            }
            this.checkedLines = list.ToArray();
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
        public LineAttribute this[int index]
            => this.checkedLines[index];

        /// <summary>
        /// 获取爻的个数。
        /// Get the count of the lines.
        /// </summary>
        public int Count
            => this.checkedLines.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LineAttribute> GetEnumerator()
        {
            return ((IEnumerable<LineAttribute>)this.checkedLines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.checkedLines.GetEnumerator();
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

            var thisLength = this.checkedLines.Length;
            {
                var otherLength = other.checkedLines.Length;
                if (thisLength > otherLength)
                    return 1;
                else if (thisLength < otherLength)
                    return -1;
            }

            for (int i = thisLength - 1; i >= 0; i--)
            {
                var cur = this.checkedLines[i];
                var com = other.checkedLines[i];
                if (cur < com)
                    return -1;
                if (cur > com)
                    return 1;
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
                foreach (var line in this.checkedLines)
                {
                    result <<= 1;
                    result += (int)line;
                }
                return result;
            }
        }
        /// <summary>
        /// 判断是否与另一个对象相同。
        /// Determine whether this instance is same as a specified object.
        /// </summary>
        /// <param name="other">
        /// 要比较的对象。
        /// The object to compare with.
        /// </param>
        /// <returns>
        /// 判断结果。
        /// The result.
        /// </returns>
        public override bool Equals(object? other)
        {
            if (other is Painting painting)
                return this.checkedLines.SequenceEqual(painting.checkedLines);
            return false;
        }
        /// <summary>
        /// 判断是否与另一个卦画相同。
        /// Determine whether this instance is same as a specified painting.
        /// </summary>
        /// <param name="other">
        /// 要比较的卦画。
        /// The painting to compare with.
        /// </param>
        /// <returns>
        /// 判断结果。
        /// The result.
        /// </returns>
        public bool Equals(Painting? other)
        {
            if (other is null)
                return false;
            return this.checkedLines.SequenceEqual(other.checkedLines);
        }

        /// <summary>
        /// 判断两个卦画是否相同。
        /// Determine whether two paintings are the same.
        /// </summary>
        /// <param name="left">
        /// 左值。
        /// The left value.
        /// </param>
        /// <param name="right">
        /// 右值。
        /// The right value.
        /// </param>
        /// <returns>
        /// 判断结果。
        /// </returns>
        public static bool operator ==(Painting? left, Painting? right)
        {
            if (left is null)
                return right is null;
            if (right is null)
                return false;
            return left.checkedLines.SequenceEqual(right.checkedLines);
        }

        /// <summary>
        /// 判断两个卦画是否不相同。
        /// Determine whether two paintings are not the same.
        /// </summary>
        /// <param name="left">
        /// 左值。
        /// The left value.
        /// </param>
        /// <param name="right">
        /// 右值。
        /// The right value.
        /// </param>
        /// <returns>
        /// 判断结果。
        /// </returns>
        public static bool operator !=(Painting? left, Painting? right)
        {
            if (left is null)
                return right is not null;
            if (right is null)
                return true;
            return !left.checkedLines.SequenceEqual(right.checkedLines);
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
            StringBuilder stringBuilder = new(this.checkedLines.Length);
            foreach(var line in checkedLines)
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
            List<LineAttribute> r = new(s.Length);
            foreach(var c in s)
            {
                switch (c)
                {
                    case '0':
                        r.Add(LineAttribute.Yin);
                        break;
                    case '1':
                        r.Add(LineAttribute.Yang);
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
            var thisLength = this.checkedLines.Length;
            BitArray bitArray = new(thisLength + 1);
            for (int i = 0; i < thisLength; i++)
                bitArray.Set(i, Convert.ToBoolean((int)this.checkedLines[i]));
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
            List<LineAttribute> r = new(bitArray.Length);
            int zeroCount = 0;
            for (int i = 0; i < bitArray.Length; i++)
            {
                var bit = bitArray[i];
                if (bit)
                {
                    for (int j = 0; j < zeroCount; j++)
                        r.Add(LineAttribute.Yin);
                    r.Add(LineAttribute.Yang);
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
