using iParkingv5.Objects.Events;

namespace iParkingv5.Controller.KztekDevices.MT166_CardDispenser
{
    /// <summary>
    /// Bản sử dụng máy nuốt thẻ MT166
    /// Ở chế độ chờ máy tính xác minh
    /// </summary>
    public interface ICardDispenserv2 : IController
    {
        event CardOnRFEventHandler? OnCardInRFEvent;
        event OnCardNotSupportEventHandler? OnCardNotSupportEvent;

        #region Máy nuốt thẻ
        /// <summary>
        /// Ra lệnh nuốt thẻ
        /// </summary>
        /// <returns></returns>
        Task<bool> CollectCard();
        Task<bool> RejectCard();
        #endregion

        #region Máy nhả thẻ
        /// <summary>
        /// Ra lệnh nhả thẻ cho người dùng lấy
        /// </summary>
        /// <returns></returns>
        Task<bool> DispenseCard();

        /// <summary>
        /// Ra lệnh đưa thẻ vào khay thẻ lỗi
        /// </summary>
        /// <returns></returns>
        Task<bool> DispenseCardToRecycle();
        #endregion

        Task<bool> SetAudio(int relayIndex);
    }
}
