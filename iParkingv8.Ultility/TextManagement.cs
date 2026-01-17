namespace iParkingv8.Ultility
{
    public static class TextManagement
    {
        public static int H1_SIZE = 64;
        public static int H2_SIZE = 64;
        public static int H3_SIZE = 64;
        public static int ROOT_SIZE = 12;
        public static EmLanguage ROOT_LANGUAGE = EmLanguage.Vietnamese;
        public enum EmTextDisplay
        {
            NOTIFY_INFORMATION,
            NOTIFY_ERROR,
            NOTIFY_WARNING,
            NOTIFY_QUESTION,
            NOTIFY_WAITING,

            RESULT_CONFIRM,
            RESULT_CANCEL,

            FORM_ACTION_SEARCH,
            FORM_ACTION_PRINT,
            FORM_ACTION_EXPORT,
        }
        public enum EmLanguage
        {
            Vietnamese,
            English
        }
        public static Dictionary<EmTextDisplay, string> vietnameseDict = new Dictionary<EmTextDisplay, string>()
        {
            { EmTextDisplay.NOTIFY_INFORMATION, "Thông báo!" },
            { EmTextDisplay.NOTIFY_ERROR, "Lỗi!" },
            { EmTextDisplay.NOTIFY_WARNING, "Cảnh báo!" },
            { EmTextDisplay.NOTIFY_QUESTION, "Câu hỏi!" },
            { EmTextDisplay.NOTIFY_WAITING, "Vui lòng chờ!" },

            { EmTextDisplay.RESULT_CONFIRM, "Xác nhận" },
            { EmTextDisplay.RESULT_CANCEL, "Đóng" },

            { EmTextDisplay.FORM_ACTION_SEARCH, "Tìm kiếm" },
            { EmTextDisplay.FORM_ACTION_PRINT, "In" },
            { EmTextDisplay.FORM_ACTION_EXPORT, "Xuất Excel" },
        };
        public static Dictionary<EmTextDisplay, string> englistDict = new Dictionary<EmTextDisplay, string>()
        {
            { EmTextDisplay.NOTIFY_INFORMATION, "Information!" },
            { EmTextDisplay.NOTIFY_ERROR, "Error!" },
            { EmTextDisplay.NOTIFY_WARNING, "Warning!" },
            { EmTextDisplay.NOTIFY_QUESTION, "Question!" },
            { EmTextDisplay.NOTIFY_WAITING, "Please waiting!" },

            { EmTextDisplay.RESULT_CONFIRM, "Confirm" },
            { EmTextDisplay.RESULT_CANCEL, "Close" },

            { EmTextDisplay.FORM_ACTION_SEARCH, "Search" },
            { EmTextDisplay.FORM_ACTION_PRINT, "Print" },
            { EmTextDisplay.FORM_ACTION_EXPORT, "Export Excel" },
        };
        public static string GetDisplayStr(EmTextDisplay emTextDisplay, EmLanguage language)
        {
            switch (language)
            {
                case EmLanguage.Vietnamese:
                    if (vietnameseDict.ContainsKey(emTextDisplay))
                    {
                        return vietnameseDict[emTextDisplay];
                    }
                    break;
                case EmLanguage.English:
                    if (englistDict.ContainsKey(emTextDisplay))
                    {
                        return englistDict[emTextDisplay];
                    }
                    break;
                default:
                    return string.Empty;
            }
            return string.Empty;
        }
    }
}
