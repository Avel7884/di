using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsAction : IGuiAction
    {
        public DrawingSettingsAction(DrawingSettings settings, CloudHolder imageHolder)
        {
            this.settings = settings;
            this.imageHolder = imageHolder;
        }

        private readonly DrawingSettings settings;
        private readonly CloudHolder imageHolder;
        public string Category => "���������";
        public string Name => "���������";
        public string Description => "��������� ���������";

        public void Perform()
        {
            new DrawingSettingsForm(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}