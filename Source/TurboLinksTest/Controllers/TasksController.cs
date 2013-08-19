namespace TurboLinksTest.Controllers
{
    using System.Data;
    using System.Web.Mvc;

    using Models;

    public class TasksController : Controller
    {
        private readonly DataContext dataContext = new DataContext();

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection fields)
        {
            var model = dataContext.Tasks.Create();

            TryUpdateModel(
                model,
                new[] { "Name", "ProjectId" },
                fields.ToValueProvider());

            if (ModelState.IsValid)
            {
                dataContext.Tasks.Add(model);
                dataContext.SaveChanges();
            }

            return RedirectToProjectDetailsOrReturnHttpStatusCode(
                201 /* http status code created*/,
                model.ProjectId);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection fields)
        {
            var model = dataContext.Tasks.Find(id);

            TryUpdateModel(
                model,
                new[] { "Completed" },
                fields.ToValueProvider());

            if (ModelState.IsValid)
            {
                dataContext.Entry(model).State = EntityState.Modified;
                dataContext.SaveChanges();
            }

            return RedirectToProjectDetailsOrReturnHttpStatusCode(
                204 /* http status no content */,
                model.ProjectId);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = dataContext.Tasks.Find(id);

            dataContext.Tasks.Remove(model);
            dataContext.SaveChanges();

            return  RedirectToProjectDetailsOrReturnHttpStatusCode(
                204 /* http status no content */,
                model.ProjectId);
        }

        private ActionResult RedirectToProjectDetailsOrReturnHttpStatusCode(
            int httpStatusCode,
            int projectId)
        {
            return Request.IsAjaxRequest() ?
                new HttpStatusCodeResult(httpStatusCode) : 
                RedirectToAction("Details", "Projects", new { id = projectId })
                as ActionResult;
        }
    }
}