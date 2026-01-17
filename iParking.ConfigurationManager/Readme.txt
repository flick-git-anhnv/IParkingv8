Khi cần thêm 1 loại config mới
1. Thêm view config
1.1 thêm Interface IConfigNewView tương tự các interface đã có trong mục Views/Interfaces
1.2 Thêm ucConfigNew : IconfigNewView là giao diện tương tác người dùng

2.Thêm hàm xử lý vào trong Views/Interfaces/IConnectionConfigView
3.Thêm logic xử lý trong Presenters/ConnectionConfigPresenter
4.Thêm logic xử lý trong Views/Implementations/Forms/FrmConnectionConfig