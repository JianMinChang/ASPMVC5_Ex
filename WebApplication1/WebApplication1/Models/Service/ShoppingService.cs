using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Interface;
using WebApplication1.Models.Repository;
using WebApplication1.Models.ViewModel;


namespace WebApplication1.Models.Service
{
    public class ShoppingService
    {
        private IListRepository _ilistRepository;
        private IListDetailRepository _ilistDetailRepository;
        private IUserRepository _iUserRepository;
        private IProductRepository _iProductRepository;

        public ShoppingService()
        {
            this._ilistRepository = new ListRepository();
            this._ilistDetailRepository = new ListDetailRepository();
            this._iUserRepository = new UserRepository();
            this._iProductRepository = new ProductRepository();
        }

        public bool HasSession() {

            return HttpContext.Current.Session["ShoppingList"] != null ? true : false;
        }

        public IList<ProductDetail> GetSession()
        {
            if (HasSession())
            {
                return (IList<ProductDetail>)HttpContext.Current.Session["ShoppingList"];
            }
            else
            {
                return null;
            }
        }


        public Users GetUser(string UserAccount)
        {
            return this._iUserRepository.Get(x => x.Email == UserAccount);
        }

        public string GetProductName(int ProductID)
        {
            return this._iProductRepository.Get(x => x.ProductID == ProductID).ProductName;
        }

        public int GetProductQuantity(int ProductID) 
        {
            return this._iProductRepository.Get(x => x.ProductID == ProductID).Quantity;
        }


        public void SetSession (IList<ProductDetail>  value)
        {
            HttpContext.Current.Session["ShoppingList"] = value;
        }

        /// <summary>
        /// 取得User的歷史消費紀錄
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public IList<ShoppingViewModel> GetUserShoppingHistory(string Account)
        {
            var user = GetUser(Account);
            var userlist = this._ilistRepository.GetAll().ToList().FindAll(x => x.UserID == user.UserID);

            var prdlist = this._iProductRepository.GetAll().ToList();

            IList<ShoppingViewModel> Data = new List<ShoppingViewModel>();

            foreach (var item in userlist)
            {
                ShoppingViewModel model = new ShoppingViewModel();

                model.ListID = item.ListID;
                model.Status = item.Status;
                model.Total = item.Total;
                model.UserID = item.UserID;

                var ProductDetail = this._ilistDetailRepository.GetAll().Where(x => x.ListID == item.ListID);

                IList<ProductDetail> Detail = new List<ProductDetail>();
                foreach (var prditem in ProductDetail)
                {
                    Detail.Add(new ProductDetail()
                    {
                        ProductID = prditem.ProductID,
                        Price = prditem.Price,
                        Quantity = prditem.Quantity,
                        ProductName = prdlist.Find(x => x.ProductID == prditem.ProductID).ProductName
                    });
                }


                model.List = Detail;

                Data.Add(model);
            }

            return Data;
        }

        /// <summary>
        /// 檢查庫存
        /// </summary>
        /// <returns></returns>
        public bool AllHasinventory()
        {
            var list = this.GetSession();

            bool IsPass = true;

            for (int i = 0; i < list.Count; i++)
            {
                if (IsPass)
                {
                    var  num = list[i].ProductID;
                    var ogj =this._iProductRepository.Get(x => x.ProductID == num );
                    if ((ogj.Quantity - list[i].Quantity) < 0)
                    {
                        IsPass = false;
                    }
                }
            }

            return IsPass;
        }

        /// <summary>
        /// 購物車結帳
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="list"></param>
        public void ShoppingCheckin(string Account,IList<ProductDetail> list)
        {
            var user =this._iUserRepository.Get(x => x.Email == Account);

            WebApplication1.Models.List ListObj = new List();
            
            ListObj.Total = list.Sum(x => x.Price * x.Quantity);
            ListObj.Status = true;
            ListObj.UserID = user.UserID;

            this._ilistRepository.Create(ListObj);

            var List = this._ilistRepository.GetAll().ToList().Last(x => x.UserID == user.UserID);

            if (List != null && List.ListID > 0)
            {
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var poduct = this._iProductRepository.Get(x => x.ProductID == item.ProductID);
                        poduct.Quantity = poduct.Quantity - item.Quantity;
                        this._iProductRepository.Update(poduct);
                        this._ilistDetailRepository.Create(new ListDetail() { ListID = List.ListID, ProductID = item.ProductID, Quantity = item.Quantity, Price = item.Price });
                    }
                }
            }
        }
    }
}