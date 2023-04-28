using System;
using Foundation;

namespace ListMvvmIOS.Util
{
	public static class ConstantVariables
	{
		public static string TitleLocalizable = "Title";
        public static string DescriptionLocalizable = "Description";
        public static string CategoryLocalizable = "Category";
        public static string AddLocalizable = "Add";
        public static string CleanLocalizable = "Clean";
        public static string DoneLocalizable = "Done";
        public static string PendingLocalizable = "Pending";
        public static string DeleteTaskLocalizable = "DeleteTask";
        public static string DeleteTaskMsgLocalizable = "DeleteTaskMsg";
        public static string YesLocalizable = "Yes";
        public static string NoLocalizable = "No";

        public static string GetLocalizable(string key)
        {
            var localizedString = NSBundle.MainBundle.GetLocalizedString(key, null);
            return localizedString;
        }
    }
}

