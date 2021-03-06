﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Microsoft.Ajax.Utilities;
using ShoppingMail.Models;



namespace ShoppingMail.Controllers
{
    public class HomeController : Controller
    {
        //說明:建立可存取此資料庫的類別物件db
        dbShoppingMailEntities db = new dbShoppingMailEntities();

        //說明:允許匿名存取
        [AllowAnonymous]
        public ActionResult Index()
        {
            //說明:取得所有產品並放入products
            var products = db.tProduct.ToList();

            //說明:會員未登入
            if (Session["Member"] == null)
            {
                //說明:指定Index.cshtml套用_Layout.cshtml，View使用products
                return View("Index", "_Layout", products);
            }
            //說明:會員登入
            //說明:指定Index.cshtml套用_LayoutMember.cshtml，View使用products
            return View("Index", "_LayoutMember", products);
        }


        //GET:/Home/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string fUserId, string fPwd)
        {
            //說明:依照帳密取得會員並指定給member
            var member = db.tMember
                .Where(m => m.fUserId == fUserId && m.fPwd == fPwd)
                .FirstOrDefault();
            //說明:若member為null表示未註冊
            if (member == null)
            {
                ViewBag.Message = "帳號或密碼錯誤";
                return View();
            }
            //說明:使用Session變數紀錄歡迎詞
            Session["Welcome"] = member.fName + "歡迎光臨";
            //說明:使用Session變數紀錄登入的會員物件
            Session["Member"] = member;
            return RedirectToAction("Index");
        }
        //GET:Home/Register
        public ActionResult Register()
        {
            return View();
        }

        //POST:Home/Register
        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            //說明:模型沒通過驗證
            if (ModelState.IsValid == false)
            {
                return View();
            }

            //說明:依帳號取得會員並指定給member
            var member = db.tMember
                .Where(m => m.fUserId == pMember.fUserId)
                .FirstOrDefault();
            //說明:若member未註冊
            if (member == null)
            {
                db.tMember.Add(pMember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號已有人使用";
            return View();
        }
        //GET:Index/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        //功能:建立訂單
        //GET:Home/OrderList
        public ActionResult OrderList()
        {
            //說明:取得會員帳號並指定給fUserId
            string fUserId = (Session["Member"] as tMember).fUserId;
            //說明:找出訂單主檔並依照fDate排序，並將結果指定給orders
            var orders = db.tOrder.Where(m => m.fUserId == fUserId).OrderByDescending(m => m.fDate).ToList();
            //說明:顯示訂單主檔
            return View("OrderList", "_LayoutMember", orders);
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //功能:按讚功能
        //筆記:jqXHR.responseText 找出錯誤原因細項
        //筆記:錯誤原因未加上JsonRequestBehavior.AllowGet
        public ActionResult like(int pid)
        {
            //說明:紀錄目前登入的會員是誰
            var mid = (Session["Member"] as tMember).fId;
            //說明:將會員及點選哪一個商品的資料新增至資料表(ThumbsUp)
            var data = db.ThumbsUp.FirstOrDefault(x => x.fMId == mid && x.fPId == pid);
            //說明:資料不存在，就新增一筆資料，並記錄到tProduct按讚數
            if (data == null)
            {
                db.ThumbsUp.Add(new ThumbsUp
                {
                    fMId = ((tMember)Session["Member"]).fId,
                    fPId = pid
                });
                //說明:增加商品的按讚數
                var like = db.tProduct.FirstOrDefault(x => x.fPId == pid).fP_islike;
                if (like == null)
                {
                    like = 0;
                }
                like += 1;

                db.tProduct.FirstOrDefault(x => x.fPId == pid).fP_islike = like;
            }
            //說明:資料存在，就移除
            else
            {
                db.ThumbsUp.Remove(data);
                //說明:減少商品的按讚數
                var dislike = db.tProduct.FirstOrDefault(x => x.fPId == pid).fP_islike;
                if (dislike == null)
                {
                    dislike = 0;
                }
                dislike -= 1;

                db.tProduct.FirstOrDefault(x => x.fPId == pid).fP_islike = dislike;
            }
            db.SaveChanges();
            var likenum = db.tProduct.FirstOrDefault(x => x.fPId == pid).fP_islike;
            return Json(likenum, JsonRequestBehavior.AllowGet);
        }

        //功能:無窮選單_定義新資料結構
        public class tTree
        {
            public string name { get; set; }
            public Nullable<int> Id { get; set; }
            public Nullable<int> Parent_Id { get; set; }
            public List<tTree> subdir { get; set; }
        }

        //功能:無窮選單_產生想要的資料結構
        //說明:遞迴方式_將子節點(name subdir)裝進fParent的subdir一層一層丟回來
        //筆記
        //(1)內容物型態轉換:Select(p=> new tTree { Id =  p.Id, name = p.fName })
        //(2)前後端資料串接:處理json function用public.處理function用private


        //功能:將tTree加入到找父層的subdir
        public List<tTree> findFather(List<tTree> result2, tProductCategory item, tTree smallTree_2)
        {
            
            foreach (var ba in result2)
            {
                //說明:找到爸爸
                if (ba.Id == item.fParent_Id)
                {
                    ba.subdir.Add(smallTree_2);
                }
                //說明:這層沒有找到，利用遞迴繼續往下層找
                else
                {
                    findFather(ba.subdir, item, smallTree_2);
                }
            }
            return result2;
        }

        //功能:長出樹狀結構
        public List<tTree> setTreemodel(List<tProductCategory> model)
        {
            var result2 = new List<tTree>();
            foreach (var item in model)
            {
                //說明:沒有父層
                if (item.fParent_Id == 0)
                {
                    var smallTree_1 = new tTree();
                    smallTree_1.name = item.fName;
                    smallTree_1.Id = item.Id;
                    smallTree_1.Parent_Id = item.fParent_Id;
                    smallTree_1.subdir = new List<tTree>();
                    result2.Add(smallTree_1);
                }
                //說明:有父層
                else
                {
                    var smallTree_2 = new tTree();
                    smallTree_2.name = item.fName;
                    smallTree_2.Id = item.Id;
                    smallTree_2.Parent_Id = item.fParent_Id;
                    smallTree_2.subdir = new List<tTree>();
                    result2 = findFather(result2, item, smallTree_2);
                }
            }
            return result2;
        }

        //功能:無窮選單_傳送分類資料至前端
        [HttpGet]
        public JsonResult GetCategory()
        {
            var allProductcategory = db.tProductCategory.ToList();
            var model = setTreemodel(allProductcategory);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //功能:Cards View的商品資訊內容_取得分類節點並傳送至前端
        [HttpGet]
        public JsonResult Getnodemodel(int nodeId)
        {
            //說明:找出同分類節點的集合
            var nodeModel = db.tProduct.Where(m => m.fCategory == nodeId).OrderByDescending(m => m.fId).ToList();
            return Json(nodeModel, JsonRequestBehavior.AllowGet);

        }

        

        public class ProductCard
        {
            public int fId { get; set; }
            public string fName { get; set; }
            public int? fPrice { get; set; }
            public int? fQty { get; set; }
            public string fImg { get; set; }
            public int? fP_islike { get; set; }

            public string fCategory { get; set; }
        }

        [HttpGet]
        public ActionResult GetProductcards_2(int NowPage)
        {
            //說明:分筆取出資料
            int PageSize = 4; //每頁顯示筆數
            int AllData = (NowPage + 1) * PageSize; //總顯示筆數

            var model = (from A in db.tProduct
                        join B in db.tProductStock on A.fPId equals B.fPId
                        orderby A.fPId
                        select new ProductCard { fId = A.fId, fName = A.fName, fPrice = A.fPrice, fP_islike = A.fP_islike, fQty = B.fQty, fImg = A.fImg }).Take(AllData);

            return PartialView("_ProductcardPartial",model.ToList());
        }

        

        //功能:建立產品新增頁面
        //GET:ComplexBind
        public ActionResult Create() 
        {
            return View();
        }

        //功能:建立產品新增頁面
        [HttpPost]
        public ActionResult Create(ProductModel_2 product,HttpPostedFileBase photo)
        {
            ProductModel temp = new ProductModel();   
            temp.CategoryId = product.CategoryId;
            temp.ParentCategoryId = product.ParentCategory_Id;
            temp.Name = product.Name;
            temp.SupplierName = product.SupplierName;
            temp.Description = product.Description;
            temp.Stock = product.Stock;
            temp.Price = product.Price;
            temp.Img = product.Name + "_0.jfif";
            db.ProductModel.Add(temp);           

            //說明:檔案上傳
            if (photo != null)  
            {
                string fileName = "";
                //說明:判斷檔案大小
                if (photo.ContentLength > 0)
                {
                    //說明:取得檔名
                    fileName = Path.GetFileName(photo.FileName); 
                    var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                }
            }
            db.SaveChanges();
            //說明:顯示目前新增哪些商品照片
            return RedirectToAction("ShowPhotos");       
        }

        //功能:展示目前已上傳那些商品圖片
        public string ShowPhotos() 
        {
            string show = "";
            //說明:建立可操作Photos資料夾的物件(dir)
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Photos"));
            //說明:取得dir物件下所有檔案，並放入fInfo檔案資訊陣列
            FileInfo[] fInfo = dir.GetFiles();
            int n = 0;
            foreach (FileInfo item in fInfo) 
            {
                n++;
                show += "<a href='../Photos/" + item.Name + "'targer='_blank'><img src='../Photos/" +
                        item.Name + "'width='350' height='400' border='0'></a>";
                if (n % 5 == 0) 
                {
                    show += "<p>";
                }
            }
            show += "<p><a href='Create'>返回商品新增頁面</a></p>";
            return show;
        }
        //功能:展示商品列表
        public ActionResult Product()
        {
            //說明:讀取資料庫所有商品至allproduct
            var allproduct= db.ProductModel.ToList();
            //說明://說明:Product.cshtml套用_LayoutMember.cshtml，view套用allproduct模型
            return View("Product", "_LayoutMember", allproduct);
            
        }

        //功能:顯示購物車
        public ActionResult ShoppingCar()
        {
            //說明:取得會員帳號並指定給fUserId
            string fUserId = (Session["Member"] as tMember).fUserId;
            //說明:找出未成為訂單明細的資料(購物車內容)
            var orderDetails = db.tOrderDetail.Where(m => m.fUserId == fUserId && m.fIsApproved == "否").ToList();
            //說明:使用LINQ方式join多張資料表
            var temp = from A in db.tProduct
                       join B in db.tProductStock on A.fPId equals B.fPId
                       join C in db.tOrderDetail on A.fPId equals C.fPId
                       join D in db.tMember on C.fUserId equals D.fUserId
                       join E in db.tAttributes on B.fPId equals E.Id
                       orderby C.fId
                       //說明:動態型別                     
                       select new Shoppingcarmodel
                       { fOrderId = C.fId, fUserId = C.fUserId, fPId = C.fPId, fPName = C.fName, fPrice = A.fPrice,
                           fMaxQty = B.fQty, fOrderQty = C.fQty, fImg = A.fImg, fChangeQTY = A.fChangeQTY,
                           fSupplyLimit = B.fSupplyLimit, fAId_1 = B.fAId_1, fAId_2 = B.fAId_2, fAName = E.fAName }
                       ;          
            var shoppingcarModel = temp.ToList();
            
            ShowOrderdetail(shoppingcarModel);
            //說明:ShoppinCar.cshtml套用_LayoutMember.cshtml，view套用shoppingcarModel模型
            return View("ShoppingCar", "_LayoutMember", shoppingcarModel);
        }

        [HttpGet]
        public ActionResult ShowOrderdetail()
        {
            var allOrderdetail = db.tShoppingcarmodel.ToList();
            return View("ShowOrderdetail", "_LayoutMember", allOrderdetail);
          
        }


        [HttpPost]
        public ActionResult ShowOrderdetail(List<Shoppingcarmodel> model)
        {
            var temp = new tShoppingcarmodel();
            foreach (var item in model) 
            {
                temp.fOrderId = item.fOrderId;
                temp.fUserId = item.fUserId;
                temp.fPId = item.fPId;
                temp.fPName = item.fPName;
                temp.fPrice = item.fPrice;
                temp.fMaxQty = item.fMaxQty;
                temp.fOrderQty = item.fOrderQty;
                temp.fImg = item.fImg;
                temp.fChangeQty = item.fChangeQTY;
                temp.fSupplyLimit = item.fSupplyLimit;
                temp.fAId_1 = item.fAId_1;
                temp.fAId_2 = item.fAId_2;
                db.tShoppingcarmodel.Add(temp);
            }
            //db.SaveChanges();
            //var allOrderdetail = db.tShoppingcarmodel.ToList();
            //return View("ShowOrderdetail", "_LayoutMember", allOrderdetail);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        //說明:處理訂單
        //Post:Index/ShoppingCar
        [HttpPost]
        public ActionResult ShoppingCar(string fReceiver, string fEmail)
        {
            //說明:取得會員帳號並指定給fUserId
            string fUserId = (Session["Member"] as tMember).fUserId;
            //說明:
            //建立唯一識別值並指定給guid變數(用來當作訂單編號)
            //tOrder的fOrderId欄位會關連到tOrderDetail的fOrderId
            //一筆訂單資料會對應到多筆訂單明細
            string gid = Guid.NewGuid().ToString();
            int guid = Convert.ToInt32(gid);
            //說明:建立訂單主檔資料
            tOrder order = new tOrder();
            order.fOrderId = guid;
            order.fUserId = fUserId;
            order.fReceiver = fReceiver;
            order.fEmail = fEmail;
            order.fAddress = "1" /*fAddress*/;
            order.fDate = DateTime.Now;
            db.tOrder.Add(order);
            //說明:找出是購物車狀態的產品
            var carList = db.tOrderDetail
                .Where(m => m.fIsApproved == "否" && m.fUserId == fUserId)
                .ToList();
            //說明:找出確認訂購地產品
            foreach (var item in carList)
            {
                item.fOrderId = guid;
                item.fIsApproved = "是";
            }
            //說明:完成tOrder及更新tOrderDetail
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }

        //說明:加入購物車
        public ActionResult AddCar(int fId)
        {
            //說明:取得會員帳號並指定給fUserId
            string fUserId = (Session["Member"] as tMember).fUserId;
            //說明:找出會員放入購物車的產品
            var currentCar = db.tOrderDetail
                .Where(m => m.fId == fId && m.fIsApproved == "否" && m.fUserId == fUserId)
                .FirstOrDefault();
            //說明:會員選購的產品非購物車狀態
            if (currentCar == null)
            {
                //說明:將選購的產品指定給product
                var product = db.tProduct
                    .Where(m => m.fId == fId)
                    .FirstOrDefault();
                //說明:將產品放入訂單明細
                tOrderDetail orderDetail = new tOrderDetail();
                orderDetail.fUserId = fUserId;
                orderDetail.fPId = fId;
                orderDetail.fName = product.fName;
                orderDetail.fPrice = product.fPrice;
                orderDetail.fQty = 1;
                orderDetail.fIsApproved = "否";
                db.tOrderDetail.Add(orderDetail);
            }
            else
            {
                currentCar.fQty += 1;
            }
            db.SaveChanges();
            var test = db.tOrderDetail.ToList();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //說明:編輯(刪除)購物車
        //Get:Index/DeleteCar
        public ActionResult DeleteCar(int? fPId)
        {
            //說明:使用者沒有選資料
            if (fPId == null)
                return RedirectToAction("ShoppingCar");
            //說明:依照fId找要刪除的產品
            var orderDetail = db.tOrderDetail.Where(m => m.fPId == fPId).FirstOrDefault();
            db.tOrderDetail.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }

        //功能:選取(刪除)購物車
        [HttpPost]
        public ActionResult DeleteSelected(List<int> fPId)
        {
            //說明:使用者沒有選資料
            if (fPId == null)
                return RedirectToAction("ShoppingCar");
            //說明:利用變數抓取每筆資料編號
            int delfPId;
            //說明:逐筆抓刪除的資料並刪除
            for (int i = 0; i < fPId.Count; i++)
            {
                delfPId = fPId[i];
                var orderDetail = db.tOrderDetail.Where(m => m.fPId == delfPId).FirstOrDefault();
                db.tOrderDetail.Remove(orderDetail);
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }

        [HttpPost]
        public ActionResult SaveForm(List<Shoppingcarmodel> model)
        {
            //foreach (var item in model)
            //{


            //}
            var temp = model;
           
            
            return RedirectToAction("ShowOrderdetail");
        }


        //功能:搜尋(關鍵字比對)
        [HttpGet]
        public ActionResult SearchAccountContact()
        {	//參數key為使用者輸入在input的資訊
            var data = (from A in db.tProduct
                        join B in db.tProductCategory on A.fCategory equals B.Id
                        orderby A.fPId
                        select new ProductCard { fId = A.fId, fName = A.fName, fPrice = A.fPrice, fP_islike = A.fP_islike, fImg = A.fImg, fCategory = B.fName })
                .Select(m => new
                {
                    value = m.fName,
                    label = m.fCategory,
                }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //功能:搜尋(Sidebar)
        [HttpGet]
        public ActionResult Search(string search)
        {
            var all = db.tProductCategory.Where(m => m.fName == search).Count();

            var model = (from A in db.tProduct
                         join B in db.tProductCategory.Where(m => m.fName == search) on A.fCategory equals B.Id
                         orderby A.fPId
                         select new ProductCard { fId = A.fId, fName = A.fName, fPrice = A.fPrice, fP_islike = A.fP_islike, fImg = A.fImg, fCategory = B.fName });

            return PartialView("_ProductcardPartial", model.ToList());
        }

        public ActionResult Test()
        {
            return View();
        }









    }

}

 