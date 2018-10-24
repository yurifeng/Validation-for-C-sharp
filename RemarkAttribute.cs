using System;
using System.Reflection;

namespace MyAttribute.Extend
{
    /// <summary>
    /// 是给枚举用  提供一个额外信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }

        public string Remark { get; private set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static class RemarkExtend
    {

        public static string GetRemark(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            FieldInfo field = type.GetField(enumValue.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute remarkAttribute =
                    (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
                return remarkAttribute.Remark;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }

    [Remark("User Status")]
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("Normal")]
        Normal = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        [Remark("Frozen")]
        Frozen = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Remark("Deleted")]
        Deleted = 2
    }
}
