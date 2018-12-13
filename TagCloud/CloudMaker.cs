using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;
using Autofac;

namespace TagCloud
{
    public class CloudMaker
    {
        public static CloudMaker Create(ClouderSettings settings)
        {
            var container = new ContainerBuilder();
            container.Register(c=>settings.DrawingSettings).AsSelf();
            container.RegisterType(settings.TCounter).As<IWordsCounter>();
            container.RegisterType(settings.TLayouter).As<ICloudLayouter>();            
            container.RegisterType<CloudDrawer>().AsSelf();
            container.RegisterType<CloudMaker>().AsSelf();
            return (CloudMaker) container.Build().Resolve(typeof(CloudMaker));
        }

        public static CloudMaker CreateDefault() =>
            CloudMaker.Create(ClouderSettings.Default());
        
        private readonly IWordsCounter counter;
        private readonly IWeightScaler scaler;
        private readonly CloudDrawer drawer;


        public CloudMaker(IWordsCounter counter, IWeightScaler scaler, CloudDrawer drawer)
        {
            this.counter = counter;
            this.scaler = scaler;
            this.drawer = drawer;
        }
        
        public void UpdateWith(string text)=>
            counter.UpdateWith(text);

        public void UpdateFrom(Stream stream)
        {   //TODO make bufferization 
            using (var reader = new StreamReader(stream,Encoding.UTF8))
                UpdateWith(reader.ReadToEnd());
        }

        public Bitmap DrawCloud()
        {
            var pairs = counter.CountedWords.Select(x => (x.Key, x.Value));
            pairs = counter.CountedWords.Select(x => x.Key).Zip(scaler.Scale(pairs), ValueTuple.Create);
            return drawer.Draw(pairs);
        }
    }
}