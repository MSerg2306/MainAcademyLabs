using ArvitumNew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ArvitumNew
{
    public static class StartUpData
    {
        public static void Initialize(ApplicationDbContext db)
        {
            bool status;
            status = InitializeExaminationStatus(db);

            if (!db.PayTypes.Any())
            {
                db.PayTypes.AddRange(
                    new PayType
                    {
                        PayTypeName = "Оплата на месте",
                        ExaminationStatusId = 3,
                    },
                    new PayType
                    {
                        PayTypeName = "Подтверждение оплаты в бухгалтерии",
                        ExaminationStatusId = 2,
                    }
                );
                db.SaveChanges();
            }

            if (!db.TypeWorkPlaces.Any())
            {
                db.TypeWorkPlaces.AddRange(
                    new TypeWorkPlace
                    {
                        NameTypeWorkPlace = "Global",
                        PayTypeId = 1,
                    },
                    new TypeWorkPlace
                    {
                        NameTypeWorkPlace = "Internal",
                        PayTypeId = 1,
                    },
                    new TypeWorkPlace
                    {
                        NameTypeWorkPlace = "External",
                        PayTypeId = 2,
                    }
                );
                db.SaveChanges();
            }

            if (!db.Citys.Any())
            {
                db.Citys.AddRange(
                    new City
                    {
                        NameCity = "Global",
                    }
                );
                db.SaveChanges();
            }

            if (!db.WorkPlaces.Any())
            {
                db.WorkPlaces.AddRange(
                    new WorkPlace
                    {
                        NameWorkPlace = "Main office",
                        TypeWorkPlaceId = db.TypeWorkPlaces.Where(b => b.NameTypeWorkPlace == "Global").FirstOrDefault().TypeWorkPlaceId,
                        CityId = db.Citys.Where(b => b.NameCity == "Global").FirstOrDefault().CityId,
                    }
                );
            db.SaveChanges();
            }

            if (!db.ScannerResolutions.Any())
            {
                db.ScannerResolutions.AddRange(
                    new ScannerResolution
                    {
                        Height = 1700,
                        CalcDPI = 100,
                    },
                    new ScannerResolution
                    {
                        Height = 2550,
                        CalcDPI = 150,
                    },
                    new ScannerResolution
                    {
                        Height = 3400,
                        CalcDPI = 200,
                    },
                    new ScannerResolution
                    {
                        Height = 5100,
                        CalcDPI = 300,
                    },
                    new ScannerResolution
                    {
                        Height = 6800,
                        CalcDPI = 400,
                    },
                    new ScannerResolution
                    {
                        Height = 10200,
                        CalcDPI = 600,
                    }
                );
                db.SaveChanges();
            }

            status = InitializeCoatingThickness(db);
            status = InitializeCoatingType(db);
            status = InitializeShoesType(db);
            status = InitializeShoesSize(db);
            status = InitializeInformationChannel(db);

        }

        private static bool InitializeExaminationStatus(ApplicationDbContext db)
        {
            try
            { 
                try
                {
                    if (!db.ExaminationStatuss.Any())
                    {
                        List<ExaminationStatusForXML> ExaminationStatusFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<ExaminationStatusForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/ExaminationStatus.xml", FileMode.OpenOrCreate))
                        {
                            ExaminationStatusFromXML = (List<ExaminationStatusForXML>)formatter.Deserialize(fs);
                            foreach (ExaminationStatusForXML p in ExaminationStatusFromXML)
                            {
                                db.ExaminationStatuss.AddRange(
                                    new ExaminationStatus
                                    {
                                        ExaminationStatusName = p.ExaminationStatusName,
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.ExaminationStatuss.Any())
                    {
                        db.ExaminationStatuss.AddRange(
                            new ExaminationStatus
                            {
                                ExaminationStatusName = "-Hе оплачен-",
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool InitializeCoatingThickness(ApplicationDbContext db)
        {
            try
            {
                try
                {
                    if (!db.CoatingThicknesss.Any())
                    {
                        List<CoatingThicknessForXML> CoatingThicknessFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<CoatingThicknessForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/CoatingThickness.xml", FileMode.OpenOrCreate))
                        {
                            CoatingThicknessFromXML = (List<CoatingThicknessForXML>)formatter.Deserialize(fs);
                            foreach (CoatingThicknessForXML p in CoatingThicknessFromXML)
                            {
                                db.CoatingThicknesss.AddRange(
                                    new CoatingThickness
                                    {
                                        CoatingThicknessName = p.CoatingThicknessName,
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.CoatingThicknesss.Any())
                    {
                        db.CoatingThicknesss.AddRange(
                            new CoatingThickness
                            {
                                CoatingThicknessName = "-Толщина покрытия не определена-",
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static bool InitializeCoatingType(ApplicationDbContext db)
        {
            try
            {
                try
                {
                    if (!db.CoatingTypes.Any())
                    {
                        List<CoatingTypeForXML> CoatingTypeFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<CoatingTypeForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/CoatingType.xml", FileMode.OpenOrCreate))
                        {
                            CoatingTypeFromXML = (List<CoatingTypeForXML>)formatter.Deserialize(fs);
                            foreach (CoatingTypeForXML p in CoatingTypeFromXML)
                            {
                                db.CoatingTypes.AddRange(
                                    new CoatingType
                                    {
                                        CoatingTypeName = p.CoatingTypeName,
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.CoatingTypes.Any())
                    {
                        db.CoatingTypes.AddRange(
                            new CoatingType
                            {
                                CoatingTypeName = "-Тип покрытия не определен-",
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool InitializeShoesType(ApplicationDbContext db)
        {
            try
            {
                try
                {
                    if (!db.ShoesTypes.Any())
                    {
                        List<ShoesTypeForXML> ShoesTypeFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<ShoesTypeForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/ShoesType.xml", FileMode.OpenOrCreate))
                        {
                            ShoesTypeFromXML = (List<ShoesTypeForXML>)formatter.Deserialize(fs);
                            foreach (ShoesTypeForXML p in ShoesTypeFromXML)
                            {
                                db.ShoesTypes.AddRange(
                                    new ShoesType
                                    {
                                        ShoesTypeName = p.ShoesTypeName,
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.ShoesTypes.Any())
                    {
                        db.ShoesTypes.AddRange(
                            new ShoesType
                            {
                                ShoesTypeName = "-Тип изделия не определен-",
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static bool InitializeShoesSize(ApplicationDbContext db)
        {
            try
            {
                try
                {
                    if (!db.ShoesSizes.Any())
                    {
                        List<ShoesSizeForXML> ShoesSizeFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<ShoesSizeForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/ShoesSize.xml", FileMode.OpenOrCreate))
                        {
                            ShoesSizeFromXML = (List<ShoesSizeForXML>)formatter.Deserialize(fs);
                            foreach (ShoesSizeForXML p in ShoesSizeFromXML)
                            {
                                db.ShoesSizes.AddRange(
                                    new ShoesSize
                                    {
                                        FootLength = p.FootLength,
                                        Size = p.Size,
                                        Activ = p.Activ
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.ShoesSizes.Any())
                    {
                        db.ShoesSizes.AddRange(
                            new ShoesSize
                            {
                                FootLength = 0,
                                Size = 0
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool InitializeInformationChannel(ApplicationDbContext db)
        {
            try
            {
                try
                {
                    if (!db.InformationChannels.Any())
                    {
                        List<InformationChannelForXML> InformationChannelFromXML;
                        XmlSerializer formatter = new XmlSerializer(typeof(List<InformationChannelForXML>));
                        using (FileStream fs = new FileStream("wwwroot/Files/InformationChannel.xml", FileMode.OpenOrCreate))
                        {
                            InformationChannelFromXML = (List<InformationChannelForXML>)formatter.Deserialize(fs);
                            foreach (InformationChannelForXML p in InformationChannelFromXML)
                            {
                                db.InformationChannels.AddRange(
                                    new InformationChannel
                                    {
                                        InformationChannelName = p.InformationChannelName,
                                    }
                                );
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!db.InformationChannels.Any())
                    {
                        db.InformationChannels.AddRange(
                            new InformationChannel
                            {
                                InformationChannelName = "-Источник привлечения клиентов не определен-",
                            }
                        );
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
