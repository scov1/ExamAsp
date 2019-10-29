using AutoMapper;
using BusinessLayer.BModel;
using ExamAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamAsp.Controllers
{
    public class CommentController : Controller
    {
        protected IMapper mapper;

        public CommentController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var commentBO = DependencyResolver.Current.GetService<CommentBO>().GetListComment();
            var usersBO = DependencyResolver.Current.GetService<UserBO>().GetUsersList();
            var booksBO = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

            ViewBag.Comments = commentBO.Select(m => mapper.Map<CommentModel>(m)).ToList();
            ViewBag.Books = booksBO.Select(m => mapper.Map<BookModel>(m)).ToList();
            ViewBag.Users = usersBO.Select(a => mapper.Map<UserModel>(a)).ToList();

            return View();
        }asd

        [HttpPost]
        public ActionResult CreateComment(CommentModel model)
        {

            var commentBO = mapper.Map<CommentBO>(model);
            commentBO.UserId = model.UserId;
            commentBO.BookId = model.BookId;
            if (ModelState.IsValid)
            {
                commentBO.Save();
                if (Request.IsAjaxRequest())
                {
                    string comment = "Комментарий добавлен!!!";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult CreateComment(int bookId)
        {
            var usersBO = DependencyResolver.Current.GetService<UserBO>();
            var commentBO = DependencyResolver.Current.GetService<CommentBO>();

            var commentsUsersModel = mapper.Map<CommentModel>(commentBO);

            var userId = usersBO.GetUserByLogin(User.Identity.Name).Id;

            commentsUsersModel.BookId = bookId;
            commentsUsersModel.UserId = userId;

            return View("Create", commentsUsersModel);
        }
    }
}


//@model IEnumerable<ExamAsp.Models.CommentModel>

//@{
//    ViewBag.Title = "Index";
//}

//<h2>Index</h2>

//@section head {
//    <script type = "text/javascript"
//            src="@Url.Content("~/scripts/Ajax.js")">
//    </script>
//}


//    @foreach(var comment in Model)
//{
//        < li > @comment </ li >
//    }

//<form method = "post" id="commentForm" action="@Url.Action("CreateComment")">
//    @Html.TextArea("Comment", new { rows = 5, cols = 50 })
//    <br />
//    <input type = "submit" value="Add Comment" />
//</form>