using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class frmRegions : Form
    {
        public frmRegions()
        {
            InitializeComponent();
            defineRegionsControl.OnNewRectangle += new NewRectangleHandler(defineRegionsControl_NewRectangleHandler);
        }

        // Video frame sample to show
        public Bitmap VideoFrame
        {
            set
            {
                defineRegionsControl.BackgroundImage = value;
            }
        }

        // Motion rectangles
        public Rectangle[] MotionRectangles
        {
            get
            {
                if (this.defineRegionsControl.BackgroundImage != null)
                {
                    float arg_31_0 = (float)this.defineRegionsControl.BackgroundImage.Width;
                    int height = this.defineRegionsControl.BackgroundImage.Height;
                    float num = arg_31_0 / (float)this.defineRegionsControl.Width;
                    float num2 = (float)height / (float)this.defineRegionsControl.Height;
                    Rectangle[] rectangles = this.defineRegionsControl.Rectangles;
                    if (rectangles != null)
                    {
                        List<Rectangle> list = new List<Rectangle>();
                        Rectangle[] array = rectangles;
                        for (int i = 0; i < array.Length; i++)
                        {
                            Rectangle rectangle = array[i];
                            list.Add(new Rectangle
                            {
                                X = (int)((float)rectangle.X * num),
                                Width = (int)((float)rectangle.Width * num),
                                Y = (int)((float)rectangle.Y * num2),
                                Height = (int)((float)rectangle.Height * num2)
                            });
                        }
                        Rectangle[] array2 = new Rectangle[list.Count];
                        list.CopyTo(array2);
                        return array2;
                    }
                }
                return null;
            }
            set
            {
                if (this.defineRegionsControl.BackgroundImage != null)
                {
                    int width = this.defineRegionsControl.BackgroundImage.Width;
                    int height = this.defineRegionsControl.BackgroundImage.Height;
                    float num = (float)this.defineRegionsControl.Width / (float)width;
                    float num2 = (float)this.defineRegionsControl.Height / (float)height;
                    if (value != null)
                    {
                        List<Rectangle> list = new List<Rectangle>();
                        for (int i = 0; i < value.Length; i++)
                        {
                            Rectangle rectangle = value[i];
                            list.Add(new Rectangle
                            {
                                X = (int)((float)rectangle.X * num),
                                Width = (int)((float)rectangle.Width * num),
                                Y = (int)((float)rectangle.Y * num2),
                                Height = (int)((float)rectangle.Height * num2)
                            });
                        }
                        Rectangle[] array = new Rectangle[list.Count];
                        list.CopyTo(array);
                        this.defineRegionsControl.Rectangles = array;
                    }
                }
            }
        }



        // On rectangle button click
        private void rectangleButton_Click(object sender, EventArgs e)
        {
            DrawingMode currentMode = defineRegionsControl.DrawingMode;

            // change current mode
            currentMode = (currentMode == DrawingMode.Rectangular) ? DrawingMode.None : DrawingMode.Rectangular;
            // update current mode
            defineRegionsControl.DrawingMode = currentMode;
            // change button status
            rectangleButton.Checked = (currentMode == DrawingMode.Rectangular);
        }

        // New rectangle definition was finished
        private void defineRegionsControl_NewRectangleHandler(object sender, Rectangle rect)
        {
            rectangleButton.Checked = false;
        }

        // On clear button click
        private void clearButton_Click(object sender, EventArgs e)
        {
            defineRegionsControl.RemoveAllRegions();
        }

        // On first displaying of the form
        protected override void OnLoad(EventArgs e)
        {
            //// get video frame dimension
            //if (defineRegionsControl.BackgroundImage != null)
            //{
            //    int imageWidth = defineRegionsControl.BackgroundImage.Width;
            //    int imageHeight = defineRegionsControl.BackgroundImage.Height;

            //    // resize region definition control
            //    defineRegionsControl.Size = new Size(imageWidth + 2, imageHeight + 2);
            //    // resize window
            //    this.Size = new Size(imageWidth + 2 + 26, imageHeight + 2 + 118);
            //}

            //base.OnLoad(e);
        }
        private void frmRegions_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }


    }
}
