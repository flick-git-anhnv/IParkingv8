namespace iParkingv5.Controller.KztekDevices.MT166_CardDispenser._2025
{
    public enum MT166V8EventType
    {
        /// <summary>
        /// Sự kiện thẻ được tự động nuốt vào khu vực RF và chờ tín hiệu điều khiển từ máy tính hoặc nút bấm
        /// </summary>
        WaitingCard = 0,

        /// <summary>
        /// ( Nút nhấn BTN1) thẻ được nhả ra sau khi nhấn nút trên BTN1 và có sự kiện thẻ
        /// </summary>
        Button1 = 1,

        /// <summary>
        /// (Nút nhấn BTN2) thẻ được nhả ra sau khi nhấn nút trên BTN2 và có sự kiện thẻ
        /// </summary>
        Button2 = 2,

        /// <summary>
        /// Quẹt thẻ trên reader 1
        /// </summary>
        Reader1 = 3,

        /// <summary>
        /// Quẹt thẻ trên reader 2
        /// </summary>
        Reader2 = 4,

        /// <summary>
        /// Loop1
        /// </summary>
        Loop1 = 9,

        /// <summary>
        /// Loop2
        /// </summary>
        Loop2 = 10,

        /// <summary>
        /// Loop3
        /// </summary>
        Loop3 = 11,

        /// <summary>
        /// Loop4
        /// </summary>
        Loop4 = 12,

        /// <summary>
        /// Spare
        /// </summary>
        Spare = 13,

        /// <summary>
        /// Sự kiện có thẻ được rút ra khỏi miệng nhả thẻ ( Bezel)
        /// </summary>
        CardbeTaken = 14,

        /// <summary>
        /// sự kiện nhấn nút trên BTN1, nhưng vòng loop 1 không được kích hoạt hoặc có một lý do khác mà nhấn nút nhưng thẻ không nhả ra
        /// </summary>
        BTN1_ABNORMAL = 15,

        /// <summary>
        /// sự kiện nhấn nút trên BTN2, nhưng vòng loop 1 không được kích hoạt hoặc có một lý do khác mà nhấn nút nhưng thẻ không nhả ra
        /// </summary>
        BTN2_ABNORMAL = 16,

        /// <summary>
        /// Sự kiện nhấn nút BTN1 nhưng trạng thái máy nhả thẻ đang ở trạng thái STOP và không nhả thẻ.
        /// </summary>
        BTN1_STOP = 17,
        /// <summary>
        /// Sự kiện nhấn nút BTN2 nhưng trạng thái máy nhả thẻ đang ở trạng thái STOP và không nhả thẻ.
        /// </summary>
        BTN2_STOP = 18,

        /// <summary>
        /// Sự kiện nhấn nút BTN1 nhưng trạng thái máy nhả thẻ đang ở trạng thái PAUSE.
        /// </summary>
        BTN1_PAUSE = 19,
        /// <summary>
        /// Sự kiện nhấn nút BTN2 nhưng trạng thái máy nhả thẻ đang ở trạng thái PAUSE.
        /// </summary>
        BTN2_PAUSE = 20,

        /// <summary>
        /// thẻ được nuốt vào khay nhả thẻ sau khi được nhấn trên BTN1, nhưng người dùng đã không rút thẻ sau một thời gian quy định.
        /// </summary>
        CardRevertedInTray1 = 21,

        /// <summary>
        /// thẻ được nuốt vào khay nhả thẻ sau khi được nhấn trên BTN2, nhưng người dùng đã không rút thẻ sau một thời gian quy định.
        /// </summary>
        CardRevertedInTray2 = 22,

        /// <summary>
        /// Thẻ được nhả ra sau khi nhận lệnh điều khiển từ máy tính
        /// </summary>
        CardOut = 23,

        /// <summary>
        /// Thẻ được bị nuốt vào khay thẻ sau khi máy tính ra lệnh nhả thẻ, nhưng người dùng đã không rút thẻ sau một thời gian quy định
        /// </summary>
        CardRevertedInTray = 24,
        BTN1_LOOP1 = 25,
        BTN2_LOOP1 = 26,

        /// <summary>
        /// Tín hiệu ra lệnh mở Barrie
        /// </summary>
        Open = 30,

        /// <summary>
        /// Tín hiệu bắt đầu dừng Barrie
        /// </summary>
        Stop_Start = 31,

        /// <summary>
        /// Tín hiệu hết lệnh dừng Barrie
        /// </summary>
        Stop_End = 32,

        /// <summary>
        /// Tín hiệu ra lệnh đóng Barrie
        /// </summary>
        Close = 33,
        KET_THE = 34,
        CardOnRFPosition = 42,
    }
}
