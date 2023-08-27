using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Test
{
    /// <summary>
    /// Base class meant for classes that can build an instance of a class with private constructors and / or private setters.
    /// </summary>
    /// <typeparam name="T">Type of the class of which an instance will be built.</typeparam>
    public abstract class BuilderBase<T> where T : class
    {
        protected static Random Random = new Random();

        private Type _itemType;

        protected T Item;

        /// <summary>
        /// Use this method when the constructor of the item to build is private or internal
        /// </summary>
        /// <param name="constructorParameters">The parameter values to pass to the constructor</param>
        protected void ConstructItem(params object[] constructorParameters)
        {
            _itemType = typeof(T);
            Item = Activator.CreateInstance(_itemType,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
                constructorParameters,
                null) as T;
        }

        /// <summary>
        /// Use this method when the setter of a property of the item is not public
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to set</typeparam>
        /// <param name="propertyFunc">Expression that identifies the property to set</param>
        /// <param name="value">The value for the property</param>
        protected void SetProperty<TProperty>(Expression<Func<T, TProperty>> propertyFunc, TProperty value)
        {
            var member = propertyFunc.Body as MemberExpression;
            var propertyInfo = member?.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Expression does not match a property: {propertyFunc}");
            }

            object target = Item;
            if (!(member.Expression is ParameterExpression))
            {
                var parentFunc = Expression.Lambda(member.Expression, propertyFunc.Parameters);
                target = parentFunc.Compile().DynamicInvoke(target);
            }

            propertyInfo.SetValue(target, value);
        }

        public virtual T Build()
        {
            return Item;
        }
    }
}