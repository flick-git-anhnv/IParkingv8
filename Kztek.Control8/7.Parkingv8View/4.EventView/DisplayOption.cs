using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.ucDataGridViewInfo
{
    public class DisplayOptions
    {
        public enum Em_DisplayTemplate
        {
            TemplatelaneIn,
            TemplateLaneOut,
        }

        public static List<KeyValuePair<string, string>> GetTemplateFields(Em_DisplayTemplate Template, DataInfoModel data)
        {
            switch (Template)
            {
                case Em_DisplayTemplate.TemplatelaneIn: //Vào
                    {
                        List<KeyValuePair<string, string>> list = [];
                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.AccesskeyName, !string.IsNullOrEmpty(data.Identity) ? data.Identity : "-"));
                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleType, !string.IsNullOrEmpty(data.VehicleType) ? data.VehicleType : "-"));

                        if (data.DateTimeIn != null)
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeIn, data.DateTimeIn.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeIn, "-"));
                        }

                        if (!string.IsNullOrEmpty(data.PlateRegister))
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleCodeAcronym, data.PlateRegister));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleCodeAcronym, "-"));
                        }

                        if (data.DateExpired != null)
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleExpiredDate, data.DateExpired.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleExpiredDate, "-"));
                        }

                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.CustomerName, !string.IsNullOrEmpty(data.CustomerInfo) ? data.CustomerInfo : "-"));

                        return list;
                    }
                case Em_DisplayTemplate.TemplateLaneOut: // Ra
                    {
                        List<KeyValuePair<string, string>> list = [];
                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.AccesskeyName, !string.IsNullOrEmpty(data.Identity) ? data.Identity : "-"));
                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleType, !string.IsNullOrEmpty(data.VehicleType) ? data.VehicleType : "-"));

                        if (data.DateTimeIn != null)
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeIn, data.DateTimeIn.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeIn, "-"));
                        }

                        if (data.DateTimeOut != null)
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeOut, data.DateTimeOut.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.TimeOut, "-"));
                        }

                        if (!string.IsNullOrEmpty(data.PlateRegister))
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleCodeAcronym, data.PlateRegister));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleCodeAcronym, "-"));
                        }

                        if (data.DateExpired != null)
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleExpiredDate, data.DateExpired.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                        }
                        else
                        {
                            list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.VehicleExpiredDate, "-"));
                        }
                        list.Add(new KeyValuePair<string, string>(KZUIStyles.CurrentResources.CustomerName, !string.IsNullOrEmpty(data.CustomerInfo) ? data.CustomerInfo : "-"));
                        return list;
                    }
                default:
                    return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
