using iParkingv8.Object.Objects.Parkings;

namespace Kztek.Control8.UserControls
{
    public partial class UcAccessKeyCollection : UserControl
    {
        #region Properties
        public delegate void OnSelectedCollectionEventHandler(Collection collection);
        public event OnSelectedCollectionEventHandler? OnSelectedCollectionEvent;
        private readonly Collection collection;
        #endregion

        #region Forms
        public UcAccessKeyCollection(Collection collection)
        {
            InitializeComponent();
            this.collection = collection;
            btnName.Text = collection.Name;
            btnName.Click += OnClickEventHandler;
        }
        #endregion

        #region Controls In Form
        private void OnClickEventHandler(object? sender, EventArgs e)
        {
            OnSelectedCollectionEvent?.Invoke(this.collection);
        }
        #endregion
    }
}
