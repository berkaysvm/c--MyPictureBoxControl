using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace custom_picture_box
{
    /// <summary>
    /// Control  sınıfından kalıtım yaptık 
    /// </summary>
    public partial class MyPictureBox : Control

    {
        // Image sınıfından "Picture" adlı değişken oluşturdum.
        public Image Picture { get; set; }

        // control sınıfının bir özelliği olan "PictureBoxSizeMod" özelliğinden bir değişken oluşturdum.
        // Bu özellik ile özellik kısmından seçilen moda göre resmimimzi ekranda farklı modlarda boyutlandıracağız.
        public PictureBoxSizeMode Mode { get; set; }
        public MyPictureBox()
        {
            InitializeComponent();

            // Picture değişkenimizi 100px genişliğinde ve yüksekliğinde bir Bitmap olarak atadık başlangıç olarak bu boyutları alacak alanımız.
            Picture = new Bitmap(100, 100);


        }

        
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Solidbrush ile fırça rengimizi seçiyoruz.
            SolidBrush b = new SolidBrush(Color.Black);

            // if , else if ve else ile seçilen modlara göre yapılacak işlemleri yazdık
            if (Mode == PictureBoxSizeMode.Zoom)
            {
                // farklı mod metodlarımıza picture ve boyutlarımızı gönderdik döndürülen değeri Image sınıfından "toDraw" adlı değişkene atadık.    
                Image toDraw = zoom1(Picture, this.Size);

                // "Graphics" kütüphanesinin "DrawImage" metodunu kullanarak bir çizdirme yaptırdık.
                pe.Graphics.DrawImage(toDraw, new Point(0, 50));
            }
            else if (Mode == PictureBoxSizeMode.StretchImage)
            {

                Image toDraw = strımage(Picture, this.Size);
                pe.Graphics.DrawImage(toDraw, new Point(0, 0));
            }
            else if (Mode == PictureBoxSizeMode.CenterImage)
            {
                Image toDraw = centerımage(Picture, this.Size);
                pe.Graphics.DrawImage(toDraw, new Point(0, 0));


            }
            else if (Mode == PictureBoxSizeMode.AutoSize)
            {

                Image toDraw = autoımage(Picture, this.Size);
                pe.Graphics.DrawImage(toDraw, new Point(0, 0));

            }
            else if (Mode == PictureBoxSizeMode.Normal)
            {
                Image toDraw = normalımage(Picture, this.Size);
                pe.Graphics.DrawImage(toDraw, new Point(0, 0));

            }
            else
            {
                pe.Graphics.DrawImage(Picture, new Point(0, 0));
            }
        }
            
        private static System.Drawing.Image normalımage(System.Drawing.Image imgToResize,Size size)
        {
            
            int sourceWidth = imgToResize.Width; 
            int sourceHeight = imgToResize.Height;
            Bitmap b = new Bitmap(sourceWidth, sourceHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgToResize, 0, 0, sourceWidth, sourceHeight);
            g.Dispose();

            return (System.Drawing.Image)b;
        }
        private static System.Drawing.Image zoom1(System.Drawing.Image imgToResize, Size size)
        {
            // fotoğraf genişlik değerini işlem yapmak için integer tipinde "sourceWidht" değişkenine atadık.
            int sourceWidth = imgToResize.Width;

            // fotoğraf yükseklik değerini işlem yapmak için integer tipinde "sourceHeight" değişkenine atadık.
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;  
             
            // resmimizi boyutlandırma yaptık.
            nPercentW = ((float)size.Width / (float)sourceWidth); 
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            int destWidth = (int)(sourceWidth*1.5 * nPercentW);
            int destHeight = (int)(sourceHeight *1.5 * nPercent);

            // Boyutlandırma sonucu oluşan yeni resim boyutundan bir bitmap oluşturduk
            Bitmap b = new Bitmap(destWidth, destHeight);

            // oluşturduğumuz bitmapi Graphics kütüphanseinden g adlı değişkenine aktardık.
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            
            //ınterpolation ile bir görüntü kalitesi ayarı yaptık.
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            // DrawImage metodu ile gelen resmi verdiğimiz genişlik yükseklik değerleri ile  çizdirdik.
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private static System.Drawing.Image strımage(System.Drawing.Image imgToResize, Size size)
        {

           
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            nPercentW = ((float)size.Width / (float)sourceWidth);  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            int destWidth = (int)(sourceWidth * nPercentW);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private static System.Drawing.Image centerımage(System.Drawing.Image imgToResize, Size size)
        {
  
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0; 
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            
            int destWidth = (int)(sourceWidth * nPercent);
          
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
         
            g.DrawImage(imgToResize,0,0,destWidth,destHeight);
            g.Dispose();
           

            return (System.Drawing.Image)b;
        }
        private static System.Drawing.Image autoımage(System.Drawing.Image imgToResize, Size size)
        {

          
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            Bitmap b = new Bitmap(sourceWidth, sourceHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgToResize, 0, 0, sourceWidth, sourceHeight);
            g.Dispose();
            
            return (System.Drawing.Image)b;

        }
    }
    
}
