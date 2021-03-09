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
    public class Painting : IReadOnlyList<LineAttribute>, IComparable<Painting>, IEquatable<Painting>
    {
        private readonly LineAttribute[] checkedLines;
        public Painting(params LineAttribute[] lines)
            : this((IEnumerable<LineAttribute>)lines) { }

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
        public LineAttribute this[int index]
            => this.checkedLines[index];
        public int Count
            => this.checkedLines.Length;
        public IEnumerator<LineAttribute> GetEnumerator()
            => ((IEnumerable<LineAttribute>)this.checkedLines).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => this.checkedLines.GetEnumerator();
        #endregion

        #region Comparing
        public int CompareTo(Painting? other)
        {
            if (other is null)
                return 1;

            var thisLength = this.checkedLines.Length;
            {
                var otherLength = this.checkedLines.Length;
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
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 1;
                foreach (var line in this.checkedLines)
                {
                    result <<= 1;
                    result += (int)LineAttribute.Yang;
                }
                return result;
            }
        }
        public override bool Equals(object? other)
        {
            if (other is Painting painting)
                return this.checkedLines.SequenceEqual(painting.checkedLines);
            return false;
        }
        public bool Equals(Painting? other)
        {
            if (other is null)
                return false;
            return this.checkedLines.SequenceEqual(other.checkedLines);
        }

        public static bool operator ==(Painting? left, Painting? right)
        {
            if (left is null)
                return right is null;
            if (right is null)
                return false;
            return left.checkedLines.SequenceEqual(right.checkedLines);
        }

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
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(this.checkedLines.Length);
            foreach (var line in this.checkedLines)
                stringBuilder.Append((int)line);
            return stringBuilder.ToString();
        }

        public static bool TryParse(
            string s, 
            [NotNullWhen(true)] out Painting? result,
            bool useStrict = true)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            var max = useStrict ? '1' : '9';
            List<LineAttribute> r = new List<LineAttribute>(s.Length);
            foreach (var c in s)
            {
                if (c < '0' || c > max)
                {
                    result = null;
                    return false;
                }
                r.Add((LineAttribute)(c % 2));
            }
            result = new Painting(r);
            return true;
        }

        public byte[] ToBytes()
        {
            var thisLength = this.checkedLines.Length;
            BitArray bitArray = new BitArray(thisLength + 1);
            for (int i = 0; i < thisLength; i++)
                bitArray.Set(i, Convert.ToBoolean((int)this.checkedLines[i]));
            bitArray.Set(thisLength, true);
            byte[] bytes = new byte[1];
            bitArray.CopyTo(bytes, bitArray.Length + 7 / 8);
            return bytes;
        }

        public static Painting FromBytes(params byte[] bytes)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));
            BitArray bitArray = new BitArray(bytes);
            List<LineAttribute> r = new List<LineAttribute>(bytes.Length);
            bool notStarted = true;
            for (int i = bitArray.Length - 1; i >= 0; i--)
            {
                var bit = bitArray[i];
                if (notStarted)
                {
                    if (bit)
                        notStarted = false;
                    continue;
                }
                r.Add(bit ? LineAttribute.Yang : LineAttribute.Yin);
            }
            return new Painting(r);
        }
        #endregion
    }
}
