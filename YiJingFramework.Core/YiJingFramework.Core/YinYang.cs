using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Core
{
    /// <summary>
    /// 阴阳属性。
    /// The yin-yang attribute.
    /// </summary>
    public struct YinYang : IComparable<YinYang>, IEquatable<YinYang>
    {
        #region creating
        /// <summary>
        /// 创建新实例。
        /// Initializes a new instance.
        /// </summary>
        /// <param name="IsYang">
        /// 若值为 <c>true</c> ，则此实例将表示阳；否则表示阴。
        /// If the value is <c>true</c>, the instance will represents yang; otherwise, yin,
        /// </param>
        public YinYang(bool IsYang)
        {
            this.IsYang = IsYang;
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
        public static YinYang Yin => new YinYang(false);
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
            return left.IsYang.Equals(right.IsYang);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(YinYang left, YinYang right)
        {
            return !(left.IsYang.Equals(right.IsYang));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(YinYang left, YinYang right)
        {
            return left.IsYang.CompareTo(right.IsYang) < 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(YinYang left, YinYang right)
        {
            return left.IsYang.CompareTo(right.IsYang) <= 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(YinYang left, YinYang right)
        {
            return left.IsYang.CompareTo(right.IsYang) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(YinYang left, YinYang right)
        {
            return left.IsYang.CompareTo(right.IsYang) >= 0;
        }
        #endregion
    }
}