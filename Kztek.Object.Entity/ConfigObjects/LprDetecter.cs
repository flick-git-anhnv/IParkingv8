namespace Kztek.Object
{
    public class LprDetecter
    {
        public enum EmLprDetecter
        {
            KztekLpr,
            AmericalLpr,
            KztekLPRAIServer,
        }

        public static string ToString(EmLprDetecter lprDetecter)
        {
            switch (lprDetecter)
            {
                case EmLprDetecter.KztekLpr:
                    return "kztekLpr";
                case EmLprDetecter.AmericalLpr:
                    return "Americal Lpr";
                case EmLprDetecter.KztekLPRAIServer:
                    return "Kztek Lpr AI Server";
                default:
                    return string.Empty;
            }
        }
    }
}
