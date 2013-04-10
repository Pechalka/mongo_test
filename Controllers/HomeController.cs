using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            var connectionString = "mongodb://localhost:27020";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("ToDo");
            var collectin = db.GetCollection<Todo>("Todos");

            List<Todo> list = collectin.FindAllAs<Todo>().ToList();
            
            var vm = new TodoListViewModel
                         {
                             Todos = list,
                             AddForm = new Todo{ Title = "new title", Text = "new text"}
                         };

            return View(vm);
        }

        public ActionResult Save(string title, string text)
        {
            var connectionString = "mongodb://localhost:27020";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("ToDo");
            var collectin = db.GetCollection<Todo>("Todos");
            var newitem = new Todo { Title = title, Text = text, Id = Guid.NewGuid().ToString() };
            collectin.Save(newitem);

            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            var connectionString = "mongodb://localhost:27020";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("ToDo");
            var collectin = db.GetCollection<Todo>("Todos");

            collectin.Remove(Query.EQ("_id", id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var connectionString = "mongodb://localhost:27020";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("ToDo");
            var collectin = db.GetCollection<Todo>("Todos");
            var item = collectin.FindOneById(id);
            return View(item);
        }


        [HttpPost]
        public ActionResult Edit(string title, string text, string id)
        {
            var connectionString = "mongodb://localhost:27020";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("ToDo");
            var collectin = db.GetCollection<Todo>("Todos");
            var item = collectin.FindOneById(id);
            item.Text = text;
            item.Title = title;
            collectin.Save(item);
            return RedirectToAction("Index");
        }
    }
}