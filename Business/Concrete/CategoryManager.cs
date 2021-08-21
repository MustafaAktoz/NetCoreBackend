using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(result, Messages.Listed);
        }

        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryDal.Get(c=>c.Id==id);
            return new SuccessDataResult<Category>(result, Messages.Geted);
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.Updated);
        }
    }
}
