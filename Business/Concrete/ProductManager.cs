using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal product)
        {
            _productDal = product;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var result= BusinessRules.Run(ProductLimitByCategory(product.CategoryId));
            if (result != null) return result;


            _productDal.Add(product);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<Product>> GetAll()
        {
            var result = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(result, Messages.Listed);
        }

        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            var result = _productDal.GetAll(p => p.CategoryId == id);
            return new SuccessDataResult<List<Product>>(result, Messages.Listed);
        }

        public IDataResult<Product> GetById(int id)
        {
            var result = _productDal.Get(p => p.Id == id);
            return new SuccessDataResult<Product>(result, Messages.Geted);
        }

        public IResult Update(Product product)
        {
            var result = BusinessRules.Run(ProductLimitByCategory(product.CategoryId));
            if (result != null) return result;

            _productDal.Update(product);
            return new SuccessResult(Messages.Updated);
        }

        private IResult ProductLimitByCategory(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId);
            if (result.Count > 10)
            {
                return new ErrorResult(Messages.ProductLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
