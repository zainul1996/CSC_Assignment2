using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using CSC_Assignment2.Models;
using System.Web.Http;
using System.Net.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity;

namespace CSC_Assignment2.Controllers
{
    public class BlobsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // References: http://stackoverflow.com/questions/21387749/using-join-group-by-and-sum-in-entity-framework
        //             http://stackoverflow.com/questions/21051612/entity-framework-join-3-tables
        [HttpGet]
        [Route("api/v1/Albums")]
        public IHttpActionResult GetAllImages()
        {
            try
            {
                var query = (
                    from blobs in db.Blobs
                    join users in db.Users on blobs.UserId equals users.Id
                    group new { blobs, users } by new { blobs.UserId, users.UserName } into g

                    select new
                    {
                        userId = (from row2 in g select row2.blobs.UserId).FirstOrDefault(),
                        userName = (from row2 in g select row2.users.UserName).FirstOrDefault(),
                        totalImage = (from row2 in g select row2.blobs.Url).Count(),
                        lastestImage = (from row2 in g orderby row2.blobs.BlobId descending select row2.blobs.Url).FirstOrDefault()
                    }
                ).ToList();

                return Ok(query);
            } catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/v1/Albums/{id}")]
        public IHttpActionResult GetImagesByUserId(string id)
        {
            try
            {
                var query = (
                    from blobs in db.Blobs
                    join users in db.Users on blobs.UserId equals users.Id
                    where blobs.UserId == id

                    select new
                    {
                        userName = users.UserName,
                        imageId = blobs.BlobId,
                        url = blobs.Url
                    }
                ); 

                return Ok(query);
            } catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/v1/Photo/{id}")]
        public IHttpActionResult GetPhotoById(int id)
        {
            try
            {
                var query = (
                    from blobs in db.Blobs
                    where blobs.BlobId == id

                    select new
                    {
                        imageId = blobs.BlobId,
                        url = blobs.Url
                    }
                );

                return Ok(query);
            } catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/v1/UploadFile")]
        public IHttpActionResult UploadFile()
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                // Retrieve image from the client
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];

                // Check if there is file
                if (file.FileName == String.Empty)
                {
                    return BadRequest("No image specified");
                }

                // Prepare Azure Blob storage
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                container.CreateIfNotExists();

                // Prepare the new file name
                string fileName = DateTime.Now.ToUniversalTime().Subtract(
                                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                  ).TotalMilliseconds + "";

                // Upload the image to azure storage
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                blob.Properties.ContentType = file.ContentType;
                blob.UploadFromStream(file.InputStream);

                // Save to DB
                Blob blobModel = new Blob();
                blobModel.UserId = User.Identity.GetUserId();
                blobModel.FileName = fileName;
                blobModel.Url = "https://cscassignment.blob.core.windows.net/images/" + fileName;
                db.Blobs.Add(blobModel);
                db.SaveChanges();

                return Ok("Uploaded: " + blobModel.Url);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: Blobs/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blob = db.Blobs.Find(id);
            if (blob == null)
            {
                return HttpNotFound();
            }
            return View(blob);
        }

        // GET: Blobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlobId,UserId,Url,FileName")] Blob blob)
        {
            if (ModelState.IsValid)
            {
                db.Blobs.Add(blob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blob);
        }

        // GET: Blobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blob = db.Blobs.Find(id);
            if (blob == null)
            {
                return HttpNotFound();
            }
            return View(blob);
        }

        // POST: Blobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlobId,UserId,Url,FileName")] Blob blob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blob);
        }

        // GET: Blobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blob blob = db.Blobs.Find(id);
            if (blob == null)
            {
                return HttpNotFound();
            }
            return View(blob);
        }

        // POST: Blobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blob blob = db.Blobs.Find(id);
            db.Blobs.Remove(blob);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } */
    }
}
