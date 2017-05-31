using Billing.Api.Helpers;
using Billing.Api.Helpers.PDF;
using Billing.Api.Models;
using Billing.API.Models;
using Billing.Database;
using Billing.Repository;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : BaseController
    {
        [Route("")]
        public IHttpActionResult GetAll(int page = 0)
        {
            int PageSize = 12;
            var query = UnitOfWork.Invoices.Get().OrderBy(x => x.Id).ToList();
            int TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

            var returnObject = new
            {
                pageSize = PageSize,
                currentPage = page,
                totalPages = TotalPages,
                invoicesList = query.Skip(PageSize * page).Take(PageSize).Select(x => Factory.Create(x)).ToList()
            };
            return Ok(returnObject);
        }
        //[Route("")]
        ////[TokenAuthorization("user,admin")]
        //public IHttpActionResult Get()
        //{
        //    //try
        //    //{

        //        return Ok(UnitOfWork.Invoices.Get().ToList().Select(x => Factory.Create(x)).ToList());
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Helper.Log(ex.Message, "ERROR");
        //    //    return BadRequest(ex.Message);
        //    //}
        //}

        [Route("customer/{id}")]
        public IHttpActionResult GetByCustomer(int id)
        {
            return Ok(UnitOfWork.Invoices.Get().Where(x => x.Customer.Id == id).ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("agent/{id}")]
        public IHttpActionResult GetByAgent(int id)
        {
            return Ok(UnitOfWork.Invoices.Get().Where(x => x.Agent.Id == id).ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{id:int}")]
        //[TokenAuthorization("user,admin")]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Invoice invoice = UnitOfWork.Invoices.Get(id);
                if (invoice == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(invoice));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        public IHttpActionResult Post(InvoiceModel model)
        {
            try
            {
                Invoice invoice = Factory.Create(model);
                UnitOfWork.Invoices.Insert(invoice);
                UnitOfWork.Commit();
                return Ok(Factory.Create(invoice));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, InvoiceModel model)
        {
            try
            {
                Invoice invoice = Factory.Create(model);
                UnitOfWork.Invoices.Update(invoice, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(invoice));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Invoice entity = UnitOfWork.Invoices.Get(id);
                if (entity == null) return NotFound();
                UnitOfWork.Invoices.Delete(id);
                UnitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}/next/{cancel}")]
        public IHttpActionResult GetNext(int id, bool cancel = false)
        {
            try
            {
                InvoiceHelper helper = new InvoiceHelper();
                Invoice entity = helper.NextStep(UnitOfWork, id, cancel);
                return Ok(Factory.Create(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TokenAuthorization("user")]
        [HttpPost]
        [Route("mail")]
        public IHttpActionResult SendMail([FromBody]MailRequest model)
        {
            Invoice invoice = UnitOfWork.Invoices.Get(model.InvoiceId);

            string mailFrom = invoice.Agent.Username;
            Helper.SendEmail(invoice, mailFrom, model.MailTo);
            return Ok("Email sent");
        }

        [TokenAuthorization("user")]
        [Route("download/{id}")]
        public IHttpActionResult GetDownload(int id)
        {
            /* Get Invoice PDF */
            Invoice invoice = UnitOfWork.Invoices.Get(id);
            InvoicePdf pdf = new InvoicePdf(invoice);
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false);
            pdfRenderer.Document = pdf.CreateDocument();
            pdfRenderer.RenderDocument();
            MemoryStream stream = new MemoryStream();
            pdfRenderer.Save(stream, false);

            /* Send PDF file */
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            var response = ResponseMessage(result);

            return response;
        }
    }
}
