namespace TurboLinksTest.Controllers
{
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using Models;

    public class ProjectsController : Controller
    {
        private readonly DataContext dataContext = new DataContext();

        public ActionResult Index()
        {
            var model = dataContext.Projects
                .Include(p => p.Tasks)
                .ToList();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var project = dataContext.Projects
                .Include(p => p.Tasks)
                .First(p => p.Id == id);

            var model = new ProjectViewModel(project);

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Project());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection fields)
        {
            var model = dataContext.Projects.Create();

            MergeModel(fields, model);

            if (ModelState.IsValid)
            {
                dataContext.Projects.Add(model);
                dataContext.SaveChanges();

                return RedirectToIndexOrHttpStatusCode(201 /* http status created */);
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = dataContext.Projects.Find(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection fields)
        {
            var model = dataContext.Projects.Find(id);

            MergeModel(fields, model);

            if (ModelState.IsValid)
            {
                dataContext.Entry(model).State = EntityState.Modified;
                dataContext.SaveChanges();

                return RedirectToIndexOrHttpStatusCode(204 /* http status no content */);
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = dataContext.Projects.Find(id);

            dataContext.Projects.Remove(model);
            dataContext.SaveChanges();

            return RedirectToIndexOrHttpStatusCode(204 /* http status no content */);
        }

        protected override void Dispose(bool disposing)
        {
            dataContext.Dispose();
            base.Dispose(disposing);
        }

        private void MergeModel(FormCollection fields, Project model)
        {
            TryUpdateModel(model, new[] { "Name" }, fields.ToValueProvider());
        }

        private ActionResult RedirectToIndexOrHttpStatusCode(
            int httpStatusCode)
        {
            return Request.IsAjaxRequest() ?
                new HttpStatusCodeResult(httpStatusCode) :
                RedirectToAction("Index") as ActionResult;
        }
    }
}