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


        public static string ConvertToXmlImpDestuffingEntryDtls(List<ImpDestuffingEntryDtl> details)
        {
            var xml = new XElement("DestuffingDetails",
                details.Select(d =>
                    new XElement("DestuffingDetail",
                        new XElement("DestuffingEntryDtlId", d.DestuffingEntryDtlId),
                        new XElement("DestuffingEntryId", d.DestuffingEntryId),
                        new XElement("TallySheetDtlId", d.TallySheetDtlId),
                        new XElement("OblHblNo", d.OblHblNo),
                        new XElement("OblHblDate", d.OblHblDate?.ToString("yyyy-MM-dd")),
                        new XElement("CommodityId", d.CommodityId),
                        new XElement("BOENo", d.BOENo),
                        new XElement("BOEDate", d.BOEDate?.ToString("yyyy-MM-dd")),
                        new XElement("LineNo", d.LineNo),
                        new XElement("CargoDescription", d.CargoDescription),
                        new XElement("NoOfPackages", d.NoOfPackages),
                        new XElement("ReceivedPackages", d.ReceivedPackages),
                        new XElement("UOM", d.UOM),
                        new XElement("GrossWeight", d.GrossWeight),
                        new XElement("DestuffWeight", d.DestuffWeight),
                        new XElement("CIFValue", d.CIFValue),
                        new XElement("GrossDuty", d.GrossDuty),
                        new XElement("Area", d.Area),
                        new XElement("GodownWiseLocationIds", d.GodownWiseLocationIds),
                        new XElement("GodownWiseLctnNames", d.GodownWiseLctnNames),
                        new XElement("Remarks", d.Remarks),
                        new XElement("OblWiseDestuffingDate", d.OblWiseDestuffingDate?.ToString("yyyy-MM-dd")),
                        new XElement("CargoType", d.CargoType),
                        new XElement("LocationId", d.LocationId),
                        new XElement("Location", d.Location)
                    )
                )
            );

            return xml.ToString();
        }



        public static string ConvertToXmlLoadContainerRequestDetails(List<LoadContainerRequestDetails> details)
        {
            var xml = new XElement("LoadContainerRequestDetailsList",
                details.Select(d =>
                {
                    var element = new XElement("LoadContainerRequestDetail");

                    void AddIfNotNull(string name, object? value)
                    {
                        if (value != null)
                            element.Add(new XElement(name, value));
                    }

                    AddIfNotNull("LoadContReqDetlId", d.LoadContReqDetlId);
                    AddIfNotNull("LoadContReqId", d.LoadContReqId);
                    AddIfNotNull("ExporterId", d.ExporterId);
                    AddIfNotNull("ShippingLineId", d.ShippingLineId);
                    AddIfNotNull("ContainerNo", d.ContainerNo);
                    AddIfNotNull("Size", d.Size);
                    AddIfNotNull("Reefer", d.Reefer);
                    AddIfNotNull("IsInsured", d.IsInsured);
                    AddIfNotNull("ShippingBillNo", d.ShippingBillNo);
                    AddIfNotNull("ShippingBillDate", d.ShippingBillDate?.ToString("yyyy-MM-dd"));
                    AddIfNotNull("CommodityId", d.CommodityId);
                    AddIfNotNull("CargoType", d.CargoType);
                    AddIfNotNull("CargoDescription", d.CargoDescription);
                    AddIfNotNull("GrossWt", d.GrossWt);
                    AddIfNotNull("NoOfUnits", d.NoOfUnits);
                    AddIfNotNull("FobValue", d.FobValue);
                    AddIfNotNull("PackUQCCode", d.PackUQCCode);
                    AddIfNotNull("PackUQCDesc", d.PackUQCDesc);
                    AddIfNotNull("SEZ", d.SEZ);
                    AddIfNotNull("SFSend", d.SFSend);
                    AddIfNotNull("EquipmentSealType", d.EquipmentSealType);
                    AddIfNotNull("EquipmentStatus", d.EquipmentStatus);
                    AddIfNotNull("EquipmentQUC", d.EquipmentQUC);
                    AddIfNotNull("PackageType", d.PackageType);
                    AddIfNotNull("ContLoadType", d.ContLoadType);
                    AddIfNotNull("CustomSeal", d.CustomSeal);
                    AddIfNotNull("PacketsFrom", d.packetsFrom);
                    AddIfNotNull("PacketsTo", d.packetsTo);

                    return element;
                })
            );

            return xml.ToString();
        }



        public static string ConvertToXmlImpDeliveryApplicationDtls(List<ImpDeliveryApplicationDtl> details)
        {
            var xml = new XElement("DeliveryDetails",
                details.Select(d =>
                    new XElement("Detail",
                        new XElement("DeliveryDtlId", d.DeliveryDtlId),
                        new XElement("DeliveryId", d.DeliveryId),
                        new XElement("DestuffingEntryDtlId", d.DestuffingEntryDtlId),
                        new XElement("LineNo", d.LineNo),
                        new XElement("OBL", d.OBL),
                        new XElement("CargoDescription", d.CargoDescription),
                        new XElement("CommodityId", d.CommodityId),
                        new XElement("NoOfPackages", d.NoOfPackages),
                        new XElement("GrossWt", d.GrossWt),
                        new XElement("SQM", d.SQM),
                        new XElement("CUM", d.CUM),
                        new XElement("CIF", d.CIF),
                        new XElement("Duty", d.Duty),
                        new XElement("DelNoOfPackages", d.DelNoOfPackages),
                        new XElement("DelGrossWt", d.DelGrossWt),
                        new XElement("DelSQM", d.DelSQM),
                        new XElement("DelCUM", d.DelCUM),
                        new XElement("DelCIF", d.DelCIF),
                        new XElement("DelDuty", d.DelDuty),
                        new XElement("BOE_NO", d.BOE_NO),
                        new XElement("BOE_DATE", d.BOE_DATE),
                        new XElement("ImporterId", d.ImporterId),
                        new XElement("InvCancel", d.InvCancel ?? 0)
                    )
                )
            );

            return xml.ToString();
        }
		public static string ConvertToXmlContainerStuffingDetails(List<ContainerStuffingDetails> details)
		{
			var xml = new XElement("StuffingDetails",
				details.Select(d =>
					new XElement("Detail",
						new XElement("StuffingDtlId", d.StuffingDtlId),
						new XElement("StuffingReqId", d.StuffingReqId),
						new XElement("ShippingBillNo", d.ShippingBillNo ?? string.Empty),
						new XElement("ShippingDate", d.ShippingDate?.ToString("yyyy-MM-dd") ?? string.Empty),
						new XElement("CHAId", d.CHAId ?? 0),
						new XElement("CHA", d.CHA ?? string.Empty),
						new XElement("ContainerNo", d.ContainerNo ?? string.Empty),
						new XElement("Exporter", d.Exporter ?? string.Empty),
						new XElement("Consignee", d.Consignee ?? string.Empty),
						new XElement("CargoDescription", d.CargoDescription ?? string.Empty),
						new XElement("MarksNo", d.MarksNo ?? string.Empty),
						new XElement("Fob", d.Fob ?? 0),
						new XElement("StuffQuantity", d.StuffQuantity ?? 0),
						new XElement("StuffWeight", d.StuffWeight),
						new XElement("Insured", d.Insured.HasValue && d.Insured.Value ? 1 : 0),
						new XElement("MCINPCIN", d.MCINPCIN ?? string.Empty),
						new XElement("CFSCode", d.CFSCode ?? string.Empty),
						new XElement("Size", d.Size ?? 0),
						new XElement("StuffingType", d.StuffingType ?? string.Empty)
					)
				)
			);

			return xml.ToString();
		}
	}

}


