using System;

namespace MyAttribute.Extend
{
    //抽象父类
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }

    //新建的LongValidateAttribute类继承父类(重写抽象方法)
    public class LongValidateAttribute : AbstractValidateAttribute
    {
        private long _lMin = 0;
        private long _lMax = 0;

        public LongValidateAttribute(long lMin, long lMax)
        {
            this._lMin = lMin;
            this._lMax = lMax;
        }

        //范围的检查
        public override bool Validate(object oValue)
        {
            return this._lMin < (long)oValue && (long)oValue < this._lMax;
        }
    }

    //新建的RequirdValidateAttribute类继承抽象父类(重写抽象方法)
    public class RequirdValidateAttribute : AbstractValidateAttribute
    {
        //是否为空的检查
        public override bool Validate(object oValue)
        {
            return oValue != null;
        }
    }

    /// <summary>
    /// 数据检验(让特性完成特定功能的类)
    /// </summary>
    public class DataValidate
    {
        public static bool Validate<T>(T t)
        {
            Type type = t.GetType();
            bool result = true;
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object item = prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true)[0];
                    AbstractValidateAttribute attribute = item as AbstractValidateAttribute;
                    if (!attribute.Validate(prop.GetValue(t)))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;

        }
    }
}
