using System.IO;
using System.Windows.Forms;

namespace GUITagClouder
{
    public class SaveImageAction : IGuiAction
    {
        
        public SaveImageAction(CloudHolder imageHolder, IPathProvider provider)
        {
            this.imageHolder = imageHolder;
            this.provider = provider;
        }
        private readonly CloudHolder imageHolder;
        private readonly IPathProvider provider;

        public string Category => "����";
        public string Name => "���������...";
        public string Description => "��������� ����������� � ����";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                DefaultExt = ".png"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK) return;
            provider.TargetPath = dialog.FileName;
            imageHolder.SaveImage();
        }
    }
}