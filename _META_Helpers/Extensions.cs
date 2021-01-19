using System;

namespace _META_Helpers
{
    public static class Extensions
    {
        public static string GetFriendlyName(this Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int backtickIndex = friendlyName.IndexOf('`');
                if (backtickIndex > 0)
                {
                    friendlyName = friendlyName.Remove(backtickIndex);
                }
                friendlyName += "<";
                var typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = GetFriendlyName(typeParameters[i]);
                    friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
                }
                friendlyName += ">";
            }

            return friendlyName;
        }
    }
}