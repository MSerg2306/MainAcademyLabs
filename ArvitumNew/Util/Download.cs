using ArvitumNew.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Util
{
    public sealed class Download
    {
        public static bool DownloadExamination(ApplicationDbContext db, int id)
        {
            IQueryable<Examination> examinations;
            examinations = db.Examinations.Where(e => e.ExaminationId == id);
            examinations = examinations.Include(p => p.Сustomer);
            examinations = examinations.Include(p => p.Сustomer.WorkPlace);
            examinations = examinations.Include(p => p.CoatingThickness);
            examinations = examinations.Include(p => p.CoatingType);
            examinations = examinations.Include(p => p.ShoesType);
            examinations = examinations.Include(p => p.ShoesSize);
            examinations = examinations.Include(e => e.ExaminationBottomPhoto);
            var items = examinations.FirstOrDefault();

            return WriteOrder(items, db);
        }

        private static bool WriteOrder(Examination examinations, ApplicationDbContext db)
        {
            try
            {
                string fileNaneImage;
                string ss, ssl="L.jpg", ssr="R.jpg";
                List<string> writeOrder = ReadOriginalOrder();

                string writePath = @"C:\Temp\";
                if (!Directory.Exists(writePath + examinations.Сustomer.WorkPlace.NameWorkPlace))
                {
                    Directory.CreateDirectory(writePath + examinations.Сustomer.WorkPlace.NameWorkPlace);
                }
                    writePath = writePath + examinations.Сustomer.WorkPlace.NameWorkPlace + @"\";
                if (!Directory.Exists(writePath + examinations.DateExamination.Value.ToString("d")))
                {
                    Directory.CreateDirectory(writePath + examinations.DateExamination.Value.ToString("d"));
                }
                    writePath = writePath + examinations.DateExamination.Value.ToString("d") + @"\";

                using (StreamWriter sw = new StreamWriter(writePath + "orderlst.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("");
                    foreach (var s in writeOrder)
                    {
                        switch (s)
                        {
                            //%% Prihod`ko, Viktoriya Vyacheslavovna; *29.01.1979:  #28389  (26.12.2018.15.11.03) S
                            case "%%":
                                {
                                    ss = s;
                                    ss += examinations.Сustomer.FirstName + ", ";
                                    ss += examinations.Сustomer.LastName + "; ";
                                    ss += "*" + examinations.Сustomer.Birthday.Value.ToString("d") + ":  ";
                                    ss += "#" + examinations.ExaminationId + "  ";
                                    ss += "(" + examinations.DateExamination + ") S";
                                    break;
                                }
                            //ordr - vrsnr = 1.3
                            //syst - vrson = 1.99.2
                            //ordr - msyst = ScanPed
                            //ordr - remit = Easyped 2014
                            //wibu - boxnr = 12 - 12228276
                            //ordr - place = Odessa 2
                            case "  ordr-place=":
                                {
                                    ss = s;
                                    ss += examinations.Сustomer.WorkPlace.NameWorkPlace;
                                    break;
                                }
                            //ordr - xkdnr = -
                            //ordr - xprfx =
                            //ordr - mdate = 26.12.2018
                            case "  ordr-mdate=":
                                {
                                    ss = s;
                                    ss += examinations.DateExamination.Value.ToString("d");
                                    break;
                                }
                            //ordr - xdate = 26.12.2018
                            case "  ordr-xdate=":
                                {
                                    ss = s;
                                    ss += examinations.DateExamination.Value.ToString("d");
                                    break;
                                }
                            //ordr - numbe = 28389
                            case "  ordr-numbe=":
                                {
                                    ss = s;
                                    ss += examinations.ExaminationId;
                                    break;
                                }
                            //ordr - xpres = Klinika
                            //flag - xpres = Normal
                            //cust - sname = Prihod`ko
                            case "  cust-sname=":
                                {
                                    ss = s;
                                    ss += examinations.Сustomer.FirstName;
                                    break;
                                }
                            //cust - fname = Viktoriya Vyacheslavovna
                            case "  cust-fname=":
                                {
                                    ss = s;
                                    ss += examinations.Сustomer.LastName;
                                    break;
                                }
                            //cust - birth = 29.01.1979
                            case "  cust-birth=":
                                {
                                    ss = s;
                                    ss += examinations.Сustomer.Birthday.Value.ToString("d");
                                    break;
                                }
                            //cust - sexmf = F
                            //cust - strnr =
                            //cust - pcode =
                            //cust - ptown =
                            //cust - ctry =
                            //cust - phone =
                            //cust - email =
                            //cust - insur = Самооплата
                            //cust - weigh =
                            //cust - bodyh =
                            //cust - sizeL = 40
                            case "  cust-sizeL=":
                                {
                                    ss = s;
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeLId).Size);
                                    break;
                                }
                            //cust - sizeR = 39
                            case "  cust-sizeR=":
                                {
                                    ss = s;
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeRId).Size);
                                    break;
                                }
                            //inso - pairs = 1
                            //inso - matL1 =
                            //inso - matR1 =
                            //inso - covrL = Kira
                            case "  inso-covrL=":
                                {
                                    ss = s;
                                    ss += examinations.CoatingType.CoatingTypeName;
                                    break;
                                }
                            //inso - covrR = Kira
                            case "  inso-covrR=":
                                {
                                    ss = s;
                                    ss += examinations.CoatingType.CoatingTypeName;
                                    break;
                                }
                            //inso - TypeL = Comfine full EVA
                            case "  inso-TypeL=":
                                {
                                    ss = s;
                                    ss += examinations.ShoesType.ShoesTypeName;
                                    break;
                                }
                            //inso - TypeR = Comfine full EVA
                            case "  inso-TypeR=":
                                {
                                    ss = s;
                                    ss += examinations.ShoesType.ShoesTypeName;
                                    break;
                                }
                            //inso - TypPL =
                            //inso - TypPR =
                            //INSO - DOWNS = 1
                            //inso - cshiL =
                            //inso - cshiR =
                            //inso - intaL =
                            //inso - intaR =
                            //shoe - brand =
                            //shoe - type = Comfine full EVA
                            case "  shoe-type=":
                                {
                                    ss = s;
                                    ss += examinations.ShoesType.ShoesTypeName;
                                    break;
                                }
                            //shoe - size = 40
                            case "  shoe-size=":
                                {
                                    ss = s;
                                    ss += Math.Truncate(examinations.ShoesSize.Size);
                                    break;
                                }
                            //shoe - width =
                            //Pelt - Heigh =
                            //Pelt - Posit =
                            //inso - pitch =
                            //diag - mainL = pola ? stopa
                            //diag - mainR = pola ? stopa
                            //diag - detnl =
                            //diag - detnr =
                            //Cust - Pain - L =
                            //Cust - Pain - R =
                            //name - ins - L = BUKHO40L.VEC
                            case "  name-ins-L=":
                                {
                                    ss = s+ "BUKHO";
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeLId).Size);
                                    ss += "L.VEC";
                                    break;
                                }
                            //name - ins - R = BUKHO39R.VEC
                            case "  name-ins-R=":
                                {
                                    ss = s + "BUKHO";
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeRId).Size);
                                    ss += "R.VEC";
                                    break;
                                }
                            //file - pdm - L = Prihod`k_----8220_-- - 28389_040L.JPG
                            //file - jpg - L = Prihod`k_----8220_-- - 28389_040L.JPG
                            //file - bmp - L = Prihod`k_----8220_-- - 28389_040L.JPG
                            case "  file-pdm-L=":
                            case "  file-jpg-L=":
                            case "  file-bmp-L=":
                                {
                                    ss = s;

                                    var firstName = examinations.Сustomer.FirstName;
                                    if(firstName.Length <= 8)
                                    {
                                        for (int i = firstName.Length; i < 9; i++)
                                        {
                                            firstName += "-";
                                        }
                                    }
                                    else
                                    {
                                        firstName = firstName.Substring(0, 8);
                                    }

                                    ssl = firstName + "_----8219_---";
                                    ssl += examinations.ExaminationId + "_0";
                                    ssl += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeLId).Size);
                                    ssl += "L.JPG";
                                    ss+=ssl;
                                    break;
                                }
                            //file - pdm - R = Prihod`k_----8220_-- - 28389_039R.JPG
                            //file - jpg - R = Prihod`k_----8220_-- - 28389_039R.JPG
                            //file - bmp - R = Prihod`k_----8220_-- - 28389_039R.JPG
                            case "  file-pdm-R=":
                            case "  file-jpg-R=":
                            case "  file-bmp-R=":
                                {
                                    ss = s;

                                    var firstName = examinations.Сustomer.FirstName;
                                    if (firstName.Length <= 8)
                                    {
                                        for (int i = firstName.Length; i < 9; i++)
                                        {
                                            firstName += "-";
                                        }
                                    }
                                    else
                                    {
                                        firstName = firstName.Substring(0, 8);
                                    }

                                    ssr = firstName + "_----8219_---";
                                    ssr += examinations.ExaminationId + "_0";
                                    ssr += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeRId).Size);
                                    ssr += "R.JPG";
                                    ss += ssr;
                                    break;
                                }
                            //alig - bmp - l = -0.82,0.00,0.00; 0.00,0.00,0.00; 0.00; 40L
                            case "  alig-bmp-l=":
                                {
                                    ss = s;
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeLId).Size) + "L";
                                    break;
                                }
                            //alig - bmp - r = -0.59,0.00,0.00; 0.00,0.00,0.00; 0.00; 40R
                            case "  alig-bmp-r=":
                                {
                                    ss = s;
                                    ss += Math.Truncate(db.ShoesSizes.FirstOrDefault(sh => sh.ShoesSizeId == examinations.ExaminationBottomPhoto.ShoesSizeRId).Size) + "R";
                                    break;
                                }
                            //alig - pdm - l =
                            //alig - pdm - r =
                            //alin - defxl = 0; 266#0;31;56;64;71;83;100/16
                            //alin - defxr = 0; 259#0;31;56;64;71;83;100/16
                            //alin - selxl = 0; 264.4#0;30.8;55.7;63.6;70.6;82.5;99.4/16
                            //alin - selxr = 0; 260.8#0;31.2;56.4;64.5;71.5;83.6;100.7/16
                            //info - L - 001 = infoEnd
                            //info - R - 001 = infoEnd
                            //info - B - 001 = infoEnd
                            default:
                                {
                                    ss = s;
                                    break;
                                }
                        }
                        sw.WriteLine(Translit.DoTranslit(ss));
                    }
                }

                fileNaneImage = writePath + Translit.DoTranslit(ssl);
                using (System.IO.FileStream fs = new System.IO.FileStream(fileNaneImage, FileMode.OpenOrCreate))
                {
                    fs.Write(examinations.ExaminationBottomPhoto.ImageDataLeft, 0, examinations.ExaminationBottomPhoto.ImageDataLeft.Length);
                }
                fileNaneImage = writePath + Translit.DoTranslit(ssr);
                using (System.IO.FileStream fs = new System.IO.FileStream(fileNaneImage, FileMode.OpenOrCreate))
                {
                    fs.Write(examinations.ExaminationBottomPhoto.ImageDataRight, 0, examinations.ExaminationBottomPhoto.ImageDataRight.Length);
                }

                return true;
            }
            catch { return false; }
        }
        private static List<string> ReadOriginalOrder()
        {
            string PathOriginalOrder = "./wwwroot/Files/orderlst.txt";
            List<string> readOriginalOrder = new List<string>();
            string s;
            try
            {
                using (StreamReader sr = new StreamReader(PathOriginalOrder, System.Text.Encoding.Default))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        readOriginalOrder.Add(Translit.DoTranslit(s));
                    }
                }
            }
            catch(Exception ex)
            {
                var e = ex.Message;
            }
            return readOriginalOrder;
        }
    }
}
