using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class ProductRepository:IProductRepository
    {
        ProductAPIController objProductAPI = new ProductAPIController();
        //public ResponseDetail AddCategoryDetails(CategoryDetails model)
        //{
        //    ResponseDetail objResponse = objProductAPI.AddCategoryDetails(model);
        //    return objResponse;
        //}
        //public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        //{
        //    ResponseDetail objResponse = objProductAPI.IsMasterExists(model);
        //    return objResponse;
        //}
        //public ResponseDetail AddSubCategoryDetails(SubCategoryDetails model)
        //{
        //    ResponseDetail objResponse = objProductAPI.AddSubCategoryDetails(model);
        //    return objResponse;
        //}
        //public List<CategoryDetails> GetCategoryList(string ActiveFlag)
        //{
        //    List<CategoryDetails> objCategoryList = new List<CategoryDetails>();
        //    objCategoryList = objProductAPI.GetCategoryList(ActiveFlag);
        //    return objCategoryList;
        //}
        //public List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus)
        //{
        //    List<SubCategoryDetails> objSubCategoryList = new List<SubCategoryDetails>();
        //    objSubCategoryList = objProductAPI.GetSubcategoryDetails(CategoryId, ActiveStatus);
        //    return objSubCategoryList;
        //}
        //public ResponseDetail SaveProductMaster(ProductDetails model)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    objResponse = objProductAPI.SaveProductMaster(model);
        //    return objResponse;
        //}
        //public int MaxBarCode()
        //{
        //    int MaxCode = objProductAPI.MaxBarCode();
        //    return MaxCode;
        //}
        //public int MaxProductCode()
        //{
        //    int MaxCode = objProductAPI.MaxProductCode();
        //    return MaxCode;
        //}
        public ResponseDetail AddCategoryDetails(CategoryDetails model)
        {
            ResponseDetail objResponse = objProductAPI.AddCategoryDetails(model);
            return objResponse;
        }
        public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        {
            ResponseDetail objResponse = objProductAPI.IsMasterExists(model);
            return objResponse;
        }
        public ResponseDetail AddSubCategoryDetails(SubCategoryDetails model)
        {
            ResponseDetail objResponse = objProductAPI.AddSubCategoryDetails(model);
            return objResponse;
        }
        public List<CategoryDetails> GetCategoryList(string ActiveFlag)
        {
            List<CategoryDetails> objCategoryList = new List<CategoryDetails>();
            objCategoryList = objProductAPI.GetCategoryList(ActiveFlag);
            return objCategoryList;
        }
        public List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus)
        {
            List<SubCategoryDetails> objSubCategoryList = new List<SubCategoryDetails>();
            objSubCategoryList = objProductAPI.GetSubcategoryDetails(CategoryId, ActiveStatus);
            return objSubCategoryList;
        }
        public ResponseDetail SaveProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.SaveProductMaster(model);
            return objResponse;
        }
        public int MaxBarCode()
        {
            int MaxCode = objProductAPI.MaxBarCode();
            return MaxCode;
        }
        public int MaxProductCode()
        {
            int MaxCode = objProductAPI.MaxProductCode();
            return MaxCode;
        }
        public List<ProductDetails> GetProductList(decimal LoginStateCode)
        {
            List<ProductDetails> objproductList = objProductAPI.GetProductList(LoginStateCode);
            return objproductList;
        }
        public ResponseDetail EditProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.EditProductMaster(model);
            return objResponse;
        }
        public ResponseDetail DeleteProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.DeleteProductMaster(model);
            return objResponse;
        }
        public ProductDetails GetProductDetail(decimal ProductId, decimal LoginStateCode)
        {
            ProductDetails objproduct = objProductAPI.GetProductDetail(ProductId,LoginStateCode);
            return objproduct;
        }

        //1030
        public List<ColorDetails> GetColorList(string ActiveFlag)
        {
            List<ColorDetails> objCategoryList = new List<ColorDetails>();
            objCategoryList = objProductAPI.GetColorList(ActiveFlag);
            return objCategoryList;
        }
        //1030
        public List<SizeDetails> GetSizeList(string ActiveFlag)
        {
            List<SizeDetails> objCategoryList = new List<SizeDetails>();
            objCategoryList = objProductAPI.GetSizeList(ActiveFlag);
            return objCategoryList;
        }
        //1030
        public ResponseDetail SaveProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.SaveProductMaster1030(model);
            return objResponse;
        }
        public ResponseDetail AddSizeDetails(SizeDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.AddSizeDetails(model);
            return objResponse;
        }
        public ResponseDetail AddColorDetails(ColorDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.AddColorDetails(model);
            return objResponse;
        }

        //1030
        public ResponseDetail EditProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.EditProductMaster1030(model);
            return objResponse;
        }

        //1030
        public ResponseDetail DeleteProductMaster1030(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductAPI.DeleteProductMaster1030(model);
            return objResponse;
        }
    }
}