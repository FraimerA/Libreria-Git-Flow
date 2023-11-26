using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibreriaFlow.Models;

namespace LibreriaFlow.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoEntities db = new ProductoEntities();

        // GET: Producto
        public ActionResult Index()
        {
            var bibliotecas = db.Bibliotecas.Include(b => b.Usuario);
            return View(bibliotecas.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Bibliotecas.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.usuario_id = new SelectList(db.Usuarios, "id", "nombre");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,URL,Fecha,usuario_id")] Biblioteca biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Bibliotecas.Add(biblioteca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuario_id = new SelectList(db.Usuarios, "id", "nombre", biblioteca.usuario_id);
            return View(biblioteca);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Bibliotecas.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.Usuarios, "id", "nombre", biblioteca.usuario_id);
            return View(biblioteca);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,URL,Fecha,usuario_id")] Biblioteca biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biblioteca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuario_id = new SelectList(db.Usuarios, "id", "nombre", biblioteca.usuario_id);
            return View(biblioteca);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Bibliotecas.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biblioteca biblioteca = db.Bibliotecas.Find(id);
            db.Bibliotecas.Remove(biblioteca);
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
        }
    }
}
