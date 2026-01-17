using System.Drawing;

namespace iParkingv8.Ultility
{
    public static class CheckBoxColorManagement
    {
        public static Color CHECKED_STATE_BORDER_COLOR = Color.FromArgb(41, 97, 27);
        public static Color CHECKED_STATE_FILL_COLOR = Color.FromArgb(41, 97, 27);

        public static Color UNCHECKED_STATE_BORDER_COLOR = Color.FromArgb(41, 97, 27);
        public static Color UNCHECKED_STATE_FILL_COLOR = Color.White;
    }

    public static class ButtonColorManagement
    {
        public static Color BUTTON_NORMAL_BORDER_COLOR = Color.FromArgb(41, 97, 27);
        public static Color BUTTON_NORMAL_FORE_COLOR = Color.FromArgb(41, 97, 27);
        public static Color BUTTON_NORMAL_BACK_COLOR = Color.White;
        public static Color BUTTON_NORMAL_FILL_COLOR = Color.White;

        public static Color BUTTON_FOCUS_BORDER_COLOR = Color.FromArgb(41, 97, 27);
        public static Color BUTTON_FOCUS_FORE_COLOR = Color.White;
        public static Color BUTTON_FOCUS_BACK_COLOR = Color.White;
        public static Color BUTTON_FOCUS_FILL_COLOR = Color.FromArgb(41, 97, 27);
    }
    public static class ColorManagement
    {
        public static Color ControlBackgroud = ColorTranslator.FromHtml("#FFFFFFFF");
        public static Color BackgroudSecondary = ColorTranslator.FromHtml("#DCDCDC");
        public static Color BorderColor = ColorTranslator.FromHtml("#063BA7");
        public static Color TitleBackColor = ColorTranslator.FromHtml("#063BA7");
        public static Color ErrorColor = ColorTranslator.FromHtml("#B71D18");
        public static Color SuccessColor = ColorTranslator.FromHtml("#118D57");
        public static Color AppBackgroudColor = ColorTranslator.FromHtml("#E2EEFF");
        public static Color ProcessColor = Color.FromArgb(6, 59, 167);
        public static Color LaneInColor = ColorTranslator.FromHtml("#118D57");
        public static Color LaneOutColor = ColorTranslator.FromHtml("#B71D18");
    }
}
