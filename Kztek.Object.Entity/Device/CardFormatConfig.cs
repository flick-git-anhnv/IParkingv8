
namespace Kztek.Object
{
    public class CardFormatConfig
    {
        public int ReaderIndex { get; set; }
        public CardFormat.EmCardFormat OutputFormat { get; set; } = CardFormat.EmCardFormat.HEXA;
        public CardFormat.EmCardFormatOption OutputOption { get; set; } = CardFormat.EmCardFormatOption.Toi_Gian;
        public string CardGroupId { get; set; } = string.Empty;
        public string DailyCardGroupIdManual { get; set; } = string.Empty;
        public string CloseBarrieIndex { get; set; } = string.Empty;
    }
}
