using InventoryManagement.Business;
using InventoryManagement.Entity.Common;
using System;
using System.IO;
using System.Web.Mvc;
using InventoryManagement.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace InventoryManagement.Controllers
{

    public class InvoiceController : Controller
    {
        TransactionManager objTransacManager = new TransactionManager();
        public ActionResult DownloadPdf(string Pm, string id)
        {

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                string LogoPath = Convert.ToString(Session["LogoPath"]);
                var base64DecodedBytes = System.Convert.FromBase64String(Pm);
                string BillNoValue = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);
                // string CurrentPartyCode = "";
                try
                {
                    //CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;

                    DistributorBillModel model = objTransacManager.getInvoice(BillNoValue, "", "F");
                    if (model == null)
                    {
                        model = new DistributorBillModel();
                    }
                    if (model.BillNo != null)
                    {
                        string result = string.Empty;
                        ConnModel objCon;
                        objCon = (System.Web.HttpContext.Current.Session["ConModel"] as ConnModel);
                        if (objCon.CompID == "1008")
                        {
                            result = RazorViewToString.RenderRazorViewToString(this, "../Transaction/InvoicePrint-Sunvis", model);
                        }
                        else
                        {
                            result = RazorViewToString.RenderRazorViewToString(this, "../Transaction/InvoicePrint", model);
                        }
                        //string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/images");
                        //imagePath = Path.Combine(imagePath, "logo.png");

                        result = result.Replace("/images/logo.png", LogoPath);

                        StringReader sr = new StringReader(result);
                        Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 0f, 0f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        return File(stream.ToArray(), "application/pdf", "Invoice_" + model.BillNo + ".pdf");
                    }
                    else
                    {
                        string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/images");
                        imagePath = Path.Combine(imagePath, "NoImage.jpg");

                        //result = result.Replace("/images/NoImage.jpg", imagePath);
                        return File(imagePath, "Invoice.pdf");
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }
    }
}