using ArvitumNew.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ArvitumNew.Util
{
    public sealed class ImageInfo
    {
        const double WImg = 240;
        const double HImg = 640;
        public static bool CalcHeightWidthImage(byte[] ImageArray, string FileNane, out int ImageHeight, out int ImageWidth)
        {

            try
            { 
                string fileNane = FileNane + ".jpg";

                using (System.IO.FileStream fs = new System.IO.FileStream(fileNane, FileMode.OpenOrCreate))
                {
                    fs.Write(ImageArray, 0, ImageArray.Length);
                    Image image = Image.FromStream(fs);
                    ImageHeight = image.Height;
                    ImageWidth = image.Width;
                }
                if (File.Exists(fileNane))
                {
                    File.Delete(fileNane);
                }
                return true;
            }
            catch
            {
                ImageHeight = 0;
                ImageWidth = 0;
                return false;
            }
        }

        public static bool CalcDistance(ApplicationDbContext db, ExaminationBottomPhoto buffer, out decimal LeghtL, out decimal WidthL, out decimal LeghtR, out decimal WidthR)
        {
            double XСorrection;
            double YСorrection;

            //var buffer = db.ExaminationBottomPhotos.Where(e => e.ExaminationId == id).FirstOrDefault();
            if(buffer!=null)
            {
                int hl, wl;
                CalcHeightWidthImage(buffer.ImageDataLeft, buffer.ExaminationId.ToString(), out hl, out wl);
                int CalcDPIL = db.ScannerResolutions.Where(s => s.Height == hl).FirstOrDefault().CalcDPI;
                CalcDPIL = CalcDPIL == 0 ? 1 : CalcDPIL;
                XСorrection = hl / HImg / CalcDPIL * 2.54;
                YСorrection = wl / WImg / CalcDPIL * 2.54;

                LeghtL = (decimal)Math.Round(Math.Sqrt(Math.Pow(buffer.LDX - buffer.LTX, 2) + Math.Pow(buffer.LDY - buffer.LTY, 2)) * XСorrection, 2);
                WidthL = (decimal)Math.Round(Math.Sqrt(Math.Pow(buffer.LRX - buffer.LLX, 2) + Math.Pow(buffer.LRY - buffer.LLY, 2)) * XСorrection, 2);

                int hr, wr;
                CalcHeightWidthImage(buffer.ImageDataRight, buffer.ExaminationId.ToString(), out hr, out wr);
                int CalcDPIR = db.ScannerResolutions.Where(s => s.Height == hr).FirstOrDefault().CalcDPI;
                CalcDPIR = CalcDPIR == 0 ? 1 : CalcDPIR;
                XСorrection = hr / HImg / CalcDPIR * 2.54;
                YСorrection = wr / WImg / CalcDPIR * 2.54;

                LeghtR = (decimal)Math.Round(Math.Sqrt(Math.Pow(buffer.RDX - buffer.RTX, 2) + Math.Pow(buffer.RDY - buffer.RTY, 2)) * XСorrection, 2);
                WidthR = (decimal)Math.Round(Math.Sqrt(Math.Pow(buffer.RRX - buffer.RLX, 2) + Math.Pow(buffer.RRY - buffer.RLY, 2)) * XСorrection, 2);

                return true;
            }
            else
            {
                LeghtL = 0;
                WidthL = 0;
                LeghtR = 0;
                WidthR = 0;
                return false;
            }
        }
    }
}
