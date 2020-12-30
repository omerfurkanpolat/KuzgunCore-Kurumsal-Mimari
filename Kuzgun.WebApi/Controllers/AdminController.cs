using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Concrete.Managers;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;
using Kuzgun.Entities.ComplexTypes.RolesDTO;
using Kuzgun.Entities.ComplexTypes.SubCategoriesDTO;
using Kuzgun.Entities.ComplexTypes.UsersDTO;
using Kuzgun.Entities.Concrete;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ISubCategoryService _subCategoryService;
        private IMapper _mapper;
        private IAuthService _authService;


        public AdminController(ICategoryService categoryService, ISubCategoryService subCategoryService,  IMapper mapper, IAuthService authService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _mapper = mapper;
            _authService = authService;
        }
        
        [HttpGet]
        [Route("getcategories")]

        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAll();
            
            if (categories.Success)
            {
                var result = _mapper.Map<IEnumerable<CategoryForReturnDTO>>(categories.Data);
                return Ok(result);
            }

            return BadRequest(categories.Message);

        }
        
        [HttpPost]
        [Route("createcategory")]
        public IActionResult CreateCategory(CategoryForCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            var category = new Category
            {
                CategoryName = model.CategoryName,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };

            var result= _categoryService.Create(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }

        [HttpGet]
        [Route("getCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            
            var category = _categoryService.GetById(id);
            if (!category.Success)
            {
                return BadRequest(category.Message);
            }
            var result = _mapper.Map<CategoryForReturnDTO>(category.Data);
            return Ok(result);

        }

        [HttpPut]
        [Route("updateCategory/{id}")]
        public IActionResult UpdateCategory(CategoryForUpdateDTO model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            var category = _categoryService.GetById(id);
            if (!category.Success)
            {
                return BadRequest(category.Message);
            }
            category.Data.CategoryName = model.CategoryName;
            _categoryService.Update(category.Data);
            return Ok();

        }

        [HttpDelete]
        [Route("deleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {

            var category = _categoryService.GetById(id);
            if (!category.Success)
            {
                return BadRequest(category.Message);
            }
            category.Data.IsDeleted = true;
            return Ok(category.Message);

            

        }
        [HttpPut]
        [Route("reviveCategory/{id}")]
        public IActionResult ReviveCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (!category.Success)
            {
                return BadRequest(category.Message);
            }
            category.Data.IsDeleted = false;
            _categoryService.Update(category.Data);
            return Ok(category.Message);

        }

        [HttpGet]
        [Route("getCategories/{id}/GetSubCategories")]
        public IActionResult GetSubCategories(int id)
        {
            var subCategories = _subCategoryService.GetSubCategoriesByCategoryId(id);
            if (subCategories.Success)
            {
                var result = _mapper.Map<IEnumerable<SubCategoryForReturnDTO>>(subCategories.Data);
                return Ok(result);
            }
           
            return BadRequest(subCategories.Message);

        }

        [HttpPost]
        [Route("getCategories/{id}/CreateSubCategory")]
        public IActionResult CreateSubCategory(SubCategoryForCreationDTO model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            var subCategory = new SubCategory
            {
                SubCategoryName = model.SubCategoryName,
                CategoryId = id

            };

            _subCategoryService.Create(subCategory);
            return Ok();

        }


        [HttpGet]
        [Route("getSubCategory/{id}")]
        public IActionResult GetSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            if (!subCategory.Success)
            {
                return BadRequest(subCategory.Message);
            }
            var result = _mapper.Map<SubCategoryForReturnDTO>(subCategory);
            return Ok(result);

        }

        [HttpPut]
        [Route("updateSubCategory/{id}")]
        public IActionResult UpdateSubCategory(SubCategoryForUpdateDTO model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            var subCategory = _subCategoryService.GetById(id);
            if (subCategory.Success)
            {
                subCategory.Data.SubCategoryName = model.SubCategoryName;

                _subCategoryService.Update(subCategory.Data);
                return Ok(subCategory.Message);
            }
            
            return BadRequest(subCategory.Message);



        }

        [HttpDelete]
        [Route("deleteSubCategory/{id}")]
        public IActionResult DeleteSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            if (subCategory.Success)
            {
                subCategory.Data.IsDeleted = true;
                _subCategoryService.Update(subCategory.Data);
                return Ok(subCategory.Message);
                
            }
            return BadRequest(subCategory.Message);


        }

        [HttpPut]
        [Route("reviveSubCategory/{id}")]
        public IActionResult ReviveSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            if (subCategory.Success)
            {
                subCategory.Data.IsDeleted = false;
                return Ok(subCategory.Message);
            }

            return BadRequest(subCategory.Message);

        }


        [HttpGet]
        [Route("getRoles")]
        public IActionResult GetRoles()
        {
            var roles =_authService.GetRoles();
            
            if (roles.Success)
            {
                return Ok(roles.Data);
            }
            return BadRequest(roles.Message);
            
        }

        [HttpPost]
        [Route("addRole")]
        public async Task<IActionResult> AddRole(RoleForCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            

            var addRole =await _authService.CreateRoleAsync(model.Name.ToLower());
            if (addRole.Success)
            {
                Ok(addRole.Message);
            }

            return BadRequest(addRole.Message);


        }

        [HttpPut]
        [Route("updateRole/{id}")]

        public async Task<IActionResult> UpdateRole(int id, RoleForUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }

            var role =await _authService.FindRoleByIdAsync(id);
            if (!role.Success)
            {
                return BadRequest(role.Message);
            }
            role.Data.Name= model.Name;
            var result = await _authService.UpdateRoleAsync(role.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpDelete]
        [Route("deleteRole/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var role = await _authService.FindRoleByIdAsync(id);
                if (!role.Success)
                {
                    return BadRequest(role.Message);
                }
                var result = await _authService.DeleteRoleAsync(role.Data);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);

            }
            return BadRequest(Messages.Error);


        }

        [HttpPut]
        [Route("changeUserRole/{userId}")]
        public async Task<IActionResult> ChangeUserRole(int userId, UserForChangeRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Messages.ModelNullOrEmpty);

            var user = await _authService.FindUserByUserIdAsync(userId);
            if (!user.Success)
            {
                return BadRequest(user.Message);
            }

            var userRole = model.Name;
            var result =await _authService.ChangeUserRoleAsync(user.Data,userRole);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUserList()
        {
            var result = await _authService.GetUsersAsync();
            if (result.Success)
            {
                var users= _mapper.Map<IEnumerable<UserForDetailDTO>>(result.Data);
                return Ok(users);
            }

            return BadRequest(result.Message);

        }

        [HttpGet]
        [Route("getUserDetail/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var result = await _authService.FindUserByUserIdAsync(id);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                var role = await _authService.GetUserRoleAsync(result.Data);
                if (!role.Success)
                {
                    return BadRequest(role.Message);
                }
                var user = _mapper.Map<UserForDetailDTO>(result.Data);
                user.UserRole = role.Data;

                return Ok(user);
            }

            return BadRequest(Messages.Error);
        }



        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id > 0)
            {
                var result = await _authService.DeleteUserAsync(id);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }

            return BadRequest(Messages.Error);

        }

        [HttpPut]
        [Route("reviveUser/{id}")]
        public async Task<IActionResult> ReviveUser(int id)
        {
            if (id > 0)
            {
                var result = await _authService.ReviveUserAsync(id);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }

            return BadRequest();

        }


    }
}
