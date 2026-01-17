using iParkingv8.Object.Objects.Events;
using IParkingv8.API.Interfaces;
using Kztek.Object;

namespace Kztek.Control8.UserControls
{
    public interface IKZUI_UcPlate
    {
        void Init(string title, Image? defaultImage);
        void DisplayLprResult(string plate, Image? lprImage);
        void DisplayNewPlate(string newPlate);
        Task DisplayLprResultData(string plate, EventImageDto? lprImageData);
        void ClearView();
        Task<bool?> UpdatePlate(string eventId, IAPIServer apiServer, bool isEntry, string currentPlate);
    }
}
