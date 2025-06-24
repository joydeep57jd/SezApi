using SezApi.Model.DBModels;
using System.Xml.Linq;

namespace SezApi.Model.Request
{
    public class XmlConvertercs
    {
        public static string ConvertToXmlGatePassDtl(List<GatePassDtl> details)

        {
            var xDoc = new XElement("Details",
                details.Select(d => new XElement("Detail",
				   new XElement("DtlId", d.GatepassDtlId),
					new XElement("ContainerNo", d.ContainerNo),
                    new XElement("IsReefer", d.IsReefer),
                    new XElement("Size", d.Size),
                    new XElement("CargoDescription", d.CargoDescription),
                    new XElement("CargeType", d.CargeType),
                    new XElement("NoOfUnits", d.NoOfUnits),
                    new XElement("VehicleNo", d.VehicleNo),
                    new XElement("Weight", d.Weight),
                    new XElement("Location", d.Location),
                    new XElement("PortOfDispatch", d.PortOfDispatch),
                    new XElement("ElwbTareWeight", d.ElwbTareWeight),
                    new XElement("ElwbCargoWeight", d.ElwbCargoWeight)
                ))
            );
            return xDoc.ToString();
        }


        public static string ConvertToXmlExitThroughGateDetails(List<ExitThroughGateDetails> details)
        {
            var xml = new XElement("GatePassDetails",
                details.Select(d =>
                    new XElement("GatePassDetail",
                        new XElement("ExitIdDtls", d.ExitIdDtls),
                        new XElement("ExitIdHeader", d.ExitIdHeader),
                        new XElement("ContainerNo", d.ContainerNo),
                        new XElement("Size", d.Size),
                        new XElement("Reefer", d.Reefer),
                        new XElement("ShippingLine", d.ShippingLine),
                        new XElement("CHAName", d.CHAName),
                        new XElement("CargoDescription", d.CargoDescription),
                        new XElement("CargoType", d.CargoType),
                        new XElement("VehicleNo", d.VehicleNo),
                        new XElement("NoOfPackages", d.NoOfPackages),
                        new XElement("GrossWeight", d.GrossWeight),
                        new XElement("DepositorName", d.DepositorName),
                        new XElement("Remarks", d.Remarks),
                        new XElement("CreatedBy", d.CreatedBy),
                        new XElement("CreatedOn", d.CreatedOn?.ToString("yyyy-MM-ddTHH:mm:ss")),
                        new XElement("UpdatedBy", d.UpdatedBy),
                        new XElement("UpdatedOn", d.UpdatedOn?.ToString("yyyy-MM-ddTHH:mm:ss")),
                        new XElement("ShippingLineID", d.ShippingLineID),
                        new XElement("CFSCode", d.CFSCode),
                        new XElement("ExpectedArrivalDateTime", d.ExpectedArrivalDateTime?.ToString("yyyy-MM-ddTHH:mm:ss"))
                    )
                )
            );

            return xml.ToString();
        }
    }
}

