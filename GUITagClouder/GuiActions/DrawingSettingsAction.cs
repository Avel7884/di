using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsAction : IGuiAction
    {
        public DrawingSettingsAction(DrawingSettings settings, CloudProcessor imageProcessor)
        {
            this.settings = settings;
            this.imageProcessor = imageProcessor;
        }

        private readonly DrawingSettings settings;
        private readonly CloudProcessor imageProcessor;
        public string Category => "���������";
        public string Name => "���������";
        public string Description => "��������� ���������";

        public void Perform()
        {
            new DrawingSettingsForm(settings).ShowDialog();
            imageProcessor.RecreateImage(settings);
        }
    }
}