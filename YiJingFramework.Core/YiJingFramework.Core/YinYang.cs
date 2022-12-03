using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json.Serialization;
using YiJingFramework.Serialization;

namespace YiJingFramework.Core
{
    /// <summary>
    /// 阴阳属性。
    /// The yin-yang attribute.
    /// </summary>
    [JsonConverter(typeof(JsonConverterOfStringConvertibleForJson<YinYang>))]
    public readonly struct YinYang :
        IComparable<YinYang>, IEquatable<YinYang>, IFormattable,
        IParsable<YinYang>, IEqualityOperators<YinYang, YinYang, bool>,
        IBitwiseOperators<YinYang, YinYang, YinYang>,
        IStringConvertibleForJson<YinYang>
    {
        #region creating
        /// <summary>
        /// 创建新实例。
        /// Initializes a new instance.
        /// </summary>
        /// <param name="isYang">
        /// 若值为 <c>true</c> ，则此实例将表示阳；否则表示阴。
        /// If the value is <c>true</c>, the instance will represents yang; otherwise, yin,
        /// </param>
        public YinYang(bool isYang)
        {
            IsYang = isYang;
        }

        /// <summary>
        /// 阳。
        /// Yang.
        /// </summary>
        public static YinYang Yang => new YinYang(true);

        /// <summary>
        /// 阴。
        /// Yin.
        /// </summary>
        public static YinYang Yin => default; // => new YinYang(false);
        #endregion

        #region calculating
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static YinYang operator &(YinYang left, YinYang right)
        {
            return new YinYang(left.IsYang & right.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static YinYang operator |(YinYang left, YinYang right)
        {
            return new YinYang(left.IsYang | right.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static YinYang operator ^(YinYang left, YinYang right)
        {
            return new YinYang(left.IsYang ^ right.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yinYang"></param>
        /// <returns></returns>
        public static YinYang operator !(YinYang yinYang)
        {
            return new YinYang(!yinYang.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yinYang"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static YinYang IBitwiseOperators<YinYang, YinYang, YinYang>.operator ~(YinYang yinYang)
        {
            return !yinYang;
        }
        #endregion

        #region converting
        /// <summary>
        /// 获取此实例是否表示阳。
        /// Get whether the instance represents yang.
        /// </summary>
        public bool IsYang { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return IsYang ? "Yang" : "Yin";
        }

        /// <summary>
        /// 按照指定格式转换为字符串。
        /// Convert to a string with the given format.
        /// </summary>
        /// <param name="format">
        /// 要使用的格式。
        /// <c>"G"</c> 表示英文； <c>"C"</c> 表示中文。
        /// The format to be used.
        /// <c>"G"</c> represents English; and <c>"C"</c> represents Chinese.
        /// </param>
        /// <param name="formatProvider">
        /// 不会使用此参数。
        /// This parameter won't be used.
        /// </param>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        /// <exception cref="FormatException">
        /// 给出的格式化字符串不受支持。
        /// The given format is not supported.
        /// </exception>
        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";
            return format.ToUpperInvariant() switch {
                "G" => ToString(),
                "C" => IsYang ? "阳" : "阴",
                _ => throw new FormatException($"The format string \"{format}\" is not supported.")
            };
        }

        /// <summary>
        /// 从字符串转换。
        /// Convert from a string.
        /// </summary>
        /// <param name="s">
        /// 字符串。
        /// The string.
        /// </param>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> 是 <c>null</c> 。
        /// <paramref name="s"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="FormatException">
        /// 传入字符串的格式不受支持。
        /// The input string was not in the supported format.
        /// </exception>
        public static YinYang Parse(string s)
        {
            ArgumentNullException.ThrowIfNull(s);

            return s.Trim().ToLowerInvariant() switch {
                "阳" or "yang" => Yang,
                "阴" or "yin" => Yin,
                _ => throw new FormatException(
                    $"Cannot parse \"{s}\" as {nameof(YinYang)}."),
            };
        }

        /// <summary>
        /// 从字符串转换。
        /// Convert from a string.
        /// </summary>
        /// <param name="s">
        /// 字符串。
        /// The string.
        /// </param>
        /// <param name="result">
        /// 结果。
        /// The result.
        /// </param>
        /// <returns>
        /// 一个指示转换成功与否的值。
        /// A value indicates whether it has been successfully converted or not.
        /// </returns>
        public static bool TryParse(
            [NotNullWhen(true)] string? s,
            [MaybeNullWhen(false)] out YinYang result)
        {
            switch (s?.Trim()?.ToLowerInvariant())
            {
                case "阳":
                case "yang":
                    result = Yang;
                    return true;
                case "阴":
                case "yin":
                    result = Yin;
                    return true;
                default:
                    result = default;
                    return false;
            }
        }

        static YinYang IParsable<YinYang>.Parse(string s, IFormatProvider? provider)
        {
            return Parse(s);
        }

        static bool IParsable<YinYang>.TryParse(
            [NotNullWhen(true)] string? s,
            IFormatProvider? provider,
            [MaybeNullWhen(false)] out YinYang result)
        {
            return TryParse(s, out result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yinYang"></param>
        public static explicit operator bool(YinYang yinYang)
        {
            return yinYang.IsYang;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator YinYang(bool value)
        {
            return new YinYang(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yinYang"></param>
        public static explicit operator int(YinYang yinYang)
        {
            return Convert.ToInt32(yinYang.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator YinYang(int value)
        {
            return new YinYang(Convert.ToBoolean(value));
        }
        #endregion

        #region comparing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(YinYang other)
        {
            return IsYang.CompareTo(other.IsYang);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(YinYang other)
        {
            return IsYang.Equals(other.IsYang);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is not YinYang other)
                return false;
            return IsYang.Equals(other.IsYang);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return IsYang.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(YinYang left, YinYang right)
        {
            return left.IsYang == right.IsYang;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(YinYang left, YinYang right)
        {
            return left.IsYang != right.IsYang;
        }
        #endregion

        #region serializing
        static bool IStringConvertibleForJson<YinYang>.FromStringForJson(string s, out YinYang result)
        {
            return TryParse(s, out result);
        }

        string IStringConvertibleForJson<YinYang>.ToStringForJson()
        {
            return this.ToString();
        }
        #endregion
    }
}