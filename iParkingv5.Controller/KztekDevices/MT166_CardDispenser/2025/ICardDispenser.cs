namespace iParkingv5.Controller.KztekDevices.MT166_CardDispenser
{
    public interface ICardDispenser : IController
    {
        /// <summary>
        /// Ra lệnh nuốt thẻ
        /// </summary>
        /// <returns></returns>
        Task<bool> CollectCard();
        /// <summary>
        /// Ra lệnh nhả thẻ
        /// </summary>
        /// <returns></returns>
        Task<bool> DispenseCard();
        Task<bool> RejectCard();
        Task<bool> SetAudio(int relayIndex);
    }
}
