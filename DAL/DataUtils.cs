using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DataUtils
    {
        /// <summary>
        /// Copies the data of one object to another. The target object 'pulls' properties of the first. 
        /// This any matching properties are written to the target.
        /// 
        /// The object copy is a shallow copy only. Any nested types will be copied as 
        /// whole values rather than individual property assignments (ie. via assignment)
        /// </summary>
        /// <param name="source">The source object to copy from</param>
        /// <param name="target">The object to copy to</param>
        /// <param name="excludedProperties">A comma delimited list of properties that should not be copied</param>
        /// <param name="memberAccess">Reflection binding access</param>
        public static void CopyObjectData(object source, object target, string excludedProperties, BindingFlags memberAccess)
        {
            string[] excluded = null;
            if (!string.IsNullOrEmpty(excludedProperties))
                excluded = excludedProperties.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            MemberInfo[] miT = target.GetType().GetMembers(memberAccess);
            foreach (MemberInfo Field in miT)
            {
                string name = Field.Name;

                // Skip over any property exceptions
                if (!string.IsNullOrEmpty(excludedProperties) &&
                    excluded.Contains(name))
                    continue;

                if (Field.MemberType == MemberTypes.Field)
                {
                    FieldInfo SourceField = source.GetType().GetField(name);
                    if (SourceField == null)
                        continue;

                    object SourceValue = SourceField.GetValue(source);
                    ((FieldInfo)Field).SetValue(target, SourceValue);
                }
                else if (Field.MemberType == MemberTypes.Property)
                {
                    PropertyInfo piTarget = Field as PropertyInfo;
                    PropertyInfo SourceField = source.GetType().GetProperty(name, memberAccess);
                    if (SourceField == null)
                        continue;

                    if (piTarget.CanWrite && SourceField.CanRead)
                    {
                        object SourceValue = SourceField.GetValue(source, null);
                        piTarget.SetValue(target, SourceValue, null);
                    }
                }
            }
        }


        public static TDATA Map<TDATA>(object oldObject) where TDATA : new()
        {
            // Create a new object of type TDATA
            TDATA newObject = new TDATA();

            try
            {

                // If the old object is null, just return the new object
                if (oldObject == null)
                    return newObject;

                // Get the type of the new object and the type of the old object passed in
                Type newObjType = typeof(TDATA);
                Type oldObjType = oldObject.GetType();

                // Get a list of all the properties in the new object
                var propertyList = newObjType.GetProperties();

                // If the new object has properties
                if (propertyList.Length > 0)
                {
                    // Loop through each property in the new object
                    foreach (var newObjProp in propertyList)
                    {
                        // Get the corresponding property in the old object
                        var oldProp = oldObjType.GetProperty(newObjProp.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.ExactBinding);

                        // If there is a corresponding property in the old object and it can be read and the new object's property can be written to 
                        if (oldProp != null && oldProp.CanRead && newObjProp.CanWrite)
                        {
                            // assign property type of both object to new variables 
                            var oldPropertyType = oldProp.PropertyType;
                            var newPropertyType = newObjProp.PropertyType;

                            //check if property is nullable or not. if property is nullable then get it's original data type from generic argument
                            if (oldPropertyType.IsGenericType && oldPropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                oldPropertyType = oldPropertyType.GetGenericArguments()[0];

                            if (newPropertyType.IsGenericType && newPropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                newPropertyType = newPropertyType.GetGenericArguments()[0];

                            //check type of both property if match then set value
                            if (newPropertyType == oldPropertyType)
                            {
                                // Get the value of the property in the old object
                                var value = oldProp.GetValue(oldObject);

                                // Set the value of the property in the new object
                                newObjProp.SetValue(newObject, value);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // If there is an exception, log it
            }

            // Return the new object
            return newObject;
        }
    }

    public static class AutoMapper
    {
        public static TU AutoMap<T, TU>(this T source, TU destination)
            where T : class, new()
            where TU : class, new()
        {
            if (source == null)
            {
                return null;
            }
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name && item.GetType() == sourceProperty.GetType());

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException) { /*skeep if invalid argument*/ }
                }
            }

            return destination;
        }
    }
}
