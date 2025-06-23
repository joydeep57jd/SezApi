using System.Xml.Linq;

namespace SezApi.Model.Request
{
    public class XmlConvertercs
    {
        public static string ConvertToXmlGatePassDtl(List<GatePassDtl> details)

        {
            var xDoc = new XElement("Details",
                details.Select(d => new XElement("Detail",
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
    }
}

