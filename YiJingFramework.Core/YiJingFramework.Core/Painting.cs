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
    /// Represents a painting made up by the yin and yang lines.
    /// When you use this as an <see cref="IEnumerable"/> or an <see cref="IEnumerable{T}"/> , you will get the lower lines first.
    /// </summary>
    public class Painting : IReadOnlyList<LineAttribute>, IComparable<Painting>, IEquatable<Painting>
    {
        private readonly LineAttribute[] checkedLines;
        /// <summary>
        /// Initializes a new instance of <seealso cref="Painting"/>.
        /// </summary>
        /// <param name="lines">The lines, with the lower ones going first.</param>
        public Painting(params LineAttribute[] lines)
            : this((IEnumerable<LineAttribute>)lines) { }

        /// <summary>
        /// Initializes a new instance of <seealso cref="Painting"/>.
        /// </summary>
        /// <param name="lines">The lines, with the lower ones going first.</param>
        public Painting(IEnumerable<LineAttribute> lines)
        {
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            var list = new List<LineAttribute>();
            foreach (var line in lines)
            {
                if (line != LineAttribute.Yang && line != LineAttribute.Yin)
                    throw new UnexpectedLineAttributeException(line);
                list.Add(line);
            }
            this.checkedLines = list.ToArray();
        }

        #region Collecting
        /// <summary>
        /// Get a line in the painting.
        /// </summary>
        /// <param name="index">
        /// The index of the line you want to get.
        /// The higher the value is, the upper the line it represents.
        /// The minimum value is 0.
        /// </param>
        /// <returns>The line.</returns>
        /// <exception cref="IndexOutOfRangeException"> <paramref name="index"/> is out of range.</exception>
        public LineAttribute this[int index]
            => this.checkedLines[index];

        /// <summary>
        /// Get the count of the lines in this painting.
        /// </summary>
        public int Count
            => this.checkedLines.Length;
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// When you use this enumerator, you will get the lower lines first.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<LineAttribute> GetEnumerator()
            => ((IEnumerable<LineAttribute>)this.checkedLines).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.checkedLines.GetEnumerator();
        #endregion

        #region Comparing

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
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
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
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
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">An object to compare with this instance, or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? other)
        {
            if (other is Painting painting)
                return this.checkedLines.SequenceEqual(painting.checkedLines);
            return false;
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="Painting"/> object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance, or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Painting? other)
        {
            if (other is null)
                return false;
            return this.checkedLines.SequenceEqual(other.checkedLines);
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="Painting"/> objects represent the same value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Painting? left, Painting? right)
        {
            if (left is null)
                return right is null;
            if (right is null)
                return false;
            return left.checkedLines.SequenceEqual(right.checkedLines);
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="Painting"/> objects represent two different values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns><c>true</c> if <paramref name="left"/> isn't equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
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
        /// Returns a string that represents the painting.
        /// You can use <seealso cref="TryParse(string?, out Painting)"/> to convert it back.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new(this.checkedLines.Length);
            for (int i = this.checkedLines.Length - 1; i >= 0; i--)
                stringBuilder.Append((int)this.checkedLines[i]);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Convert from a <see cref="string"/>.
        /// </summary>
        /// <param name="s">
        /// The string represents the painting.
        /// </param>
        /// <param name="result">The painting.</param>
        /// <returns>A value indicates whether it has been successfully converted or not.</returns>
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
            for (int i = s.Length - 1; i >= 0; i--)
            {
                var c = s[i];
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
        /// Convert to a <see cref="byte"/> array.
        /// You can convert it back by using <seealso cref="FromBytes(byte[])"/> .
        /// </summary>
        /// <returns>The byte that can represent this painting.</returns>
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
        /// Convert from a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">The bytes that represents this painting.</param>
        /// <returns>The painting.</returns>
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
                    {
                        r.Add(LineAttribute.Yin);
                    }
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
